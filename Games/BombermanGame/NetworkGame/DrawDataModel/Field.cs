using Core.Engine.Shared.Interfaces;
using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Core.Engine.Shared.Objects.GraphicEngine.Draw;
using Games.BombermanGame.NetworkGame.DrawDataModel.DrawableFieldObjects;
using Games.BombermanGame.Shared.DrawDataModel.Field;
using Games.BombermanGame.NetworkGame.DrawDataModel.DrawableFieldObjects.Players;
using Games.BombermanGame.Shared.DrawDataModel.Field.Cell;
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Extensions;
using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.GameDataModel.Player;
using Games.BombermanGame.Shared.Interfaces.DrawableObject;
using Games.BombermanGame.Shared.RamResources.Multi;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using BombCell = Games.BombermanGame.NetworkGame.DrawDataModel.DrawableFieldObjects.BombCell;
using DestructibleCell = Games.BombermanGame.NetworkGame.DrawDataModel.DrawableFieldObjects.DestructibleCell;
using IndestructibleCell = Games.BombermanGame.NetworkGame.DrawDataModel.DrawableFieldObjects.IndestructibleCell;

namespace Games.BombermanGame.NetworkGame.DrawDataModel;

    public class Field :BaseDraw
    {
        protected override string LinkedResourceName => "BombermanNetworkGame.Field";
        protected override int LinkedResourceGroupId => FieldResource.ResourceGroupId;
        
        private readonly BombermanNetworkGame _game;
        private readonly RectangleF _targetRectangle;
        private readonly PlayerCollection _playerCollection;
        private readonly FieldCellsGrid _fieldCellsGrid;
        
        private List<IDrawableFieldObject> _drawableFieldObjects = new();
        private FieldResource? _fieldResource;

        public static Field Create(IEngine engine, BombermanNetworkGame game)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new Field(
                game,
                new RectangleF(
                    0.13f * canvasWidth,
                    0.13f * canvasHeight,
                    0.74f * canvasWidth,
                    0.74f * canvasHeight
                ),
                game.Players
            );
        }

        private Field(
            BombermanNetworkGame game,
            RectangleF targetRectangle, 
            PlayerCollection playerCollection)
        {
            _game = game;
            _game.Field.OnFieldUpdated  = Field_OnFieldUpdated;
            _targetRectangle = targetRectangle;
            _playerCollection = playerCollection;

            _fieldCellsGrid = new FieldCellsGrid(
                _targetRectangle, 
                _game.Field.FieldWidth, 
                _game.Field.FieldHeight);
        }

        private void Field_OnFieldUpdated()
        {
            var drawableFieldObjects = new List<IDrawableFieldObject>();
            AddFieldBackgroundToDrawableFieldObjects(drawableFieldObjects);
            AddIndestructibleCellsToDrawableFieldObjects(drawableFieldObjects);
            AddDestructibleCellsToDrawableFieldObjects(drawableFieldObjects);
            AddPlayersToDrawableFieldObjects(drawableFieldObjects);
            AddBombsToDrawableFieldObjects(drawableFieldObjects);

            _drawableFieldObjects = drawableFieldObjects;
        }

        private void AddDestructibleCellsToDrawableFieldObjects(List<IDrawableFieldObject> drawableFieldObjects)
        {
            _game.Field.EnumerateField((row, column, cell) =>
            {
                if (cell != FieldItemEnum.DestructibleField)
                {
                    return null;
                }

                var indestructibleCell = new DestructibleCell(_fieldCellsGrid.GetCellRectangle(column, row));
                indestructibleCell.SetFieldResource(_fieldResource);
                drawableFieldObjects.Add(indestructibleCell);

                return (int?)null;
            });
        }

        private void AddIndestructibleCellsToDrawableFieldObjects(List<IDrawableFieldObject> drawableFieldObjects)
        {
            _game.Field.EnumerateField((row, column, cell) =>
            {
                if (cell != FieldItemEnum.IndestructibleField)
                {
                    return null;
                }

                var indestructibleCell = new IndestructibleCell(_fieldCellsGrid.GetCellRectangle(column, row));
                indestructibleCell.SetFieldResource(_fieldResource);
                drawableFieldObjects.Add(indestructibleCell);

                return (int?)null;
            });
        }

        private void AddBombsToDrawableFieldObjects(List<IDrawableFieldObject> drawableFieldObjects)
        {
            _game.Field.EnumerateField((row, column, cell) =>
            {
                if (!cell.IsBombOnField())
                {
                    return null;
                }

                var bomb = new BombCell(_fieldCellsGrid.GetCellRectangle(column, row));
                bomb.SetFieldResource(_fieldResource);
                drawableFieldObjects.Add(bomb);

                return (int?)null;
            });
        }

        private void AddFieldBackgroundToDrawableFieldObjects(List<IDrawableFieldObject> drawableFieldObjects)
        {
            var fieldBackground = new Background(_targetRectangle);
            fieldBackground.SetFieldResource(_fieldResource);
            drawableFieldObjects.Add(fieldBackground);
        }

        private void AddPlayersToDrawableFieldObjects(List<IDrawableFieldObject> drawableFieldObjects)
        {
            _game.Field.EnumerateField((row, column, cell) =>
            {
                if (!cell.IsPlayerOnField())
                {
                    return null;
                }

                IDrawableFieldObject player = cell switch
                {
                    FieldItemEnum.Player1 => 
                        new Player1(_fieldCellsGrid.GetCellRectangle(column, row), _playerCollection.Player1),
                    FieldItemEnum.Player2 => 
                        new Player2(_fieldCellsGrid.GetCellRectangle(column, row), _playerCollection.Player2),
                    FieldItemEnum.Player3 => 
                        new Player3(_fieldCellsGrid.GetCellRectangle(column, row), _playerCollection.Player3),
                    FieldItemEnum.Player4 => 
                        new Player4(_fieldCellsGrid.GetCellRectangle(column, row), _playerCollection.Player4),
                    _ => throw new InvalidOperationException(
                        $"Unexpected [{nameof(cell)}] value [{cell}]")
                };

                player.SetFieldResource(_fieldResource);
                drawableFieldObjects.Add(player);

                return (int?)null;
            });
        }

        protected override void SetRamResource(IRamResource resource)
        {
            _fieldResource = (FieldResource)resource;
            Field_OnFieldUpdated();
        }
        
        protected override IRamResource CreateIRamResource(
            ID2D1HwndRenderTarget renderTarget, IDWriteFactory directWriteFactory)
        {
            BasePlayer player = new Player1(
                _fieldCellsGrid.GetCellRectangle(0,0), 
                _playerCollection.Player1);
            FieldResource fieldResource = new(
                LinkedResourceName,
                Background.CreateBitmapResource(LinkedResourceName, renderTarget),
                IndestructibleCell.CreateBitmapResource(LinkedResourceName, renderTarget),
                DestructibleCell.CreateBitmapResource(LinkedResourceName, renderTarget),
                Background.CreateBitmapResource(LinkedResourceName, renderTarget),
                Background.CreateBitmapResource(LinkedResourceName, renderTarget),
                Background.CreateBitmapResource(LinkedResourceName, renderTarget),
                Background.CreateBitmapResource(LinkedResourceName, renderTarget),
                BombCell.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player1.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player2.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player3.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player4.CreateBitmapResource(LinkedResourceName, renderTarget),
                NicknameCell.CreateTextFormatResource(LinkedResourceName, directWriteFactory, player.NicknameFontSize),
                NicknameCell.CreateForegroundBrushResource(LinkedResourceName, renderTarget),
                NicknameCell.CreateShadowBrushResource(LinkedResourceName, renderTarget)
            );

            return fieldResource;
        }
        
        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            foreach (var drawableFieldObject in _drawableFieldObjects)
            {
                drawableFieldObject.Draw(renderTarget);
            }
        }
    }