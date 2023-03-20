using System.Reflection;
using BombermanGame.DrawDataModel.Draw.Field.Cell;
using BombermanGame.DrawDataModel.RamResources.Multi;
using BombermanGame.Game.DataModel;
using BombermanGame.Shared.Enums;
using Engine.Shared.GraphicEngine;
using Engine.Shared.GraphicEngine.Draw;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Engine.SharedInterfaces;
using Engine.SharedInterfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;
using Bitmap = Engine.Shared.GraphicEngine.RamResources.Single.Bitmap;
using Brush = Engine.Shared.GraphicEngine.RamResources.Single.Brush;
using FontStyle = Vortice.DirectWrite.FontStyle;

namespace BombermanGame.DrawDataModel.Draw.Field
{
    public class Field :BaseDraw
    {
        protected override string LinkedResourceName => "BombermanGame.Field";
        protected override int LinkedResourceGroupId => FieldItem.ResourceGroupId;

        private ID2D1Bitmap? _fieldBackgroundBitmap;
        private ID2D1Bitmap? _indestructibleFieldBitmap;
        private ID2D1Bitmap? _destructibleFieldBitmap;
        private ID2D1Bitmap? _player1StartPointBitmap;
        private ID2D1Bitmap? _player2StartPointBitmap;
        private ID2D1Bitmap? _player3StartPointBitmap;
        private ID2D1Bitmap? _player4StartPointBitmap;
        private ID2D1Bitmap? _bombBitmap;
        private ID2D1Bitmap? _player1Bitmap;
        private ID2D1Bitmap? _player2Bitmap;
        private ID2D1Bitmap? _player3Bitmap;
        private ID2D1Bitmap? _player4Bitmap;
        private IDWriteTextFormat? _nicknameTextFormat;
        private ID2D1Brush? _nicknameForegroundBrush;
        private ID2D1Brush? _nicknameShadowBrush;

        private readonly FieldItemEnum[][] _fieldItems;
        private readonly RectangleF _targetRectangle;
        private readonly PlayerInfoCollection _playerInfoCollection;
        private readonly int _fieldItemsWidth;
        private readonly int _fieldItemsHeight;
        private readonly float _nicknameFontSize;

        private FieldCellsGrid _fieldCellsGrid;

        public static Field Create(IEngine engine, Game.BombermanGame game)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new Field(
                game.GetFieldEnum(),
                new RectangleF(
                    0.13f * canvasWidth,
                    0.13f * canvasHeight,
                    0.74f * canvasWidth,
                    0.74f * canvasHeight
                ),
                game.PlayerInfoCollection
            );
        }

        private Field(
            FieldItemEnum[][] fieldItems, 
            RectangleF targetRectangle, 
            PlayerInfoCollection playerInfoCollection)
        {
            _fieldItems = fieldItems;
            _targetRectangle = targetRectangle;
            _playerInfoCollection = playerInfoCollection;
            _fieldItemsHeight = _fieldItems.Length;
            _fieldItemsWidth = _fieldItems[0].Length;
            _nicknameFontSize = targetRectangle.Height * 0.02f;

            _fieldCellsGrid = new FieldCellsGrid(_targetRectangle, _fieldItemsWidth, _fieldItemsHeight);
        }

        protected override void SetRamResource(IRamResource resource)
        {
            FieldItem fieldItem = (FieldItem)resource;
            
            _fieldBackgroundBitmap = (ID2D1Bitmap) (fieldItem.FieldBackgroundBitmap.Resource);
            _indestructibleFieldBitmap = (ID2D1Bitmap) (fieldItem.IndestructibleFieldBitmap.Resource);
            _destructibleFieldBitmap = (ID2D1Bitmap) (fieldItem.DestructibleFieldBitmap.Resource);
            _player1StartPointBitmap = (ID2D1Bitmap) (fieldItem.Player1StartPoint.Resource);
            _player2StartPointBitmap = (ID2D1Bitmap) (fieldItem.Player2StartPoint.Resource);
            _player3StartPointBitmap = (ID2D1Bitmap) (fieldItem.Player3StartPoint.Resource);
            _player4StartPointBitmap = (ID2D1Bitmap) (fieldItem.Player4StartPoint.Resource);
            _bombBitmap = (ID2D1Bitmap) (fieldItem.Bomb.Resource);
            _player1Bitmap = (ID2D1Bitmap) (fieldItem.Player1.Resource);
            _player2Bitmap = (ID2D1Bitmap) (fieldItem.Player2.Resource);
            _player3Bitmap = (ID2D1Bitmap) (fieldItem.Player3.Resource);
            _player4Bitmap = (ID2D1Bitmap) (fieldItem.Player4.Resource);
            _nicknameForegroundBrush = (ID2D1Brush) (fieldItem.NicknameForegroundBrush.Resource);
            _nicknameTextFormat = (IDWriteTextFormat) (fieldItem.NicknameTextFormat.Resource);
            _nicknameShadowBrush = (ID2D1Brush) (fieldItem.NicknameShadowBrush.Resource);
        }
        
        protected override IRamResource CreateIRamResource(
            ID2D1HwndRenderTarget renderTarget, IDWriteFactory directWriteFactory)
        {
            FieldItem fieldItem = new(
                LinkedResourceName,
                FieldBackground.CreateBitmapResource(LinkedResourceName, renderTarget),
                IndestructibleCell.CreateBitmapResource(LinkedResourceName, renderTarget),
                DestructibleCell.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player1StartPointCell.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player2StartPointCell.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player3StartPointCell.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player4StartPointCell.CreateBitmapResource(LinkedResourceName, renderTarget),
                BombCell.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player1Cell.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player2Cell.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player3Cell.CreateBitmapResource(LinkedResourceName, renderTarget),
                Player4Cell.CreateBitmapResource(LinkedResourceName, renderTarget),
                NicknameCell.CreateTextFormatResource(LinkedResourceName, directWriteFactory, _nicknameFontSize),
                NicknameCell.CreateForegroundBrushResource(LinkedResourceName, renderTarget),
                NicknameCell.CreateShadowBrushResource(LinkedResourceName, renderTarget)
            );

            return fieldItem;
        }
        
        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            BitmapCell.Draw(renderTarget, _fieldBackgroundBitmap, _targetRectangle);
            
            for (int rowCounter = 0; rowCounter < _fieldItemsHeight; rowCounter++)
            {
                for (int columnCounter = 0; columnCounter < _fieldItemsWidth; columnCounter++)
                {
                    var targetRectangle = _fieldCellsGrid.GetCellRectangle(columnCounter, rowCounter);
                    
                    switch (_fieldItems[rowCounter][columnCounter])
                    {
                        case FieldItemEnum.EmptyField:
                            break;
                        case FieldItemEnum.IndestructibleField:
                            BitmapCell.Draw(renderTarget, _indestructibleFieldBitmap, targetRectangle);
                            break;
                        case FieldItemEnum.DestructibleField:
                            BitmapCell.Draw(renderTarget, _destructibleFieldBitmap, targetRectangle);
                            break;
                        case FieldItemEnum.Player1StartPoint:
                            BitmapCell.Draw(renderTarget, _player1StartPointBitmap, targetRectangle);
                            break;
                        case FieldItemEnum.Player2StartPoint:
                            BitmapCell.Draw(renderTarget, _player2StartPointBitmap, targetRectangle);
                            break;
                        case FieldItemEnum.Player3StartPoint:
                            BitmapCell.Draw(renderTarget, _player3StartPointBitmap, targetRectangle);
                            break;
                        case FieldItemEnum.Player4StartPoint:
                            BitmapCell.Draw(renderTarget, _player4StartPointBitmap, targetRectangle);
                            break;
                        case FieldItemEnum.Bomb:
                            BitmapCell.Draw(renderTarget, _bombBitmap, targetRectangle);
                            break;
                        case FieldItemEnum.Player1:
                            PlayerCell.Draw(renderTarget, targetRectangle, _player1Bitmap, 
                                _playerInfoCollection.GetPlayerInfo(0).Nickname, _nicknameFontSize, 
                                _nicknameForegroundBrush, _nicknameShadowBrush, _nicknameTextFormat);
                            break;
                        case FieldItemEnum.Player2:
                            PlayerCell.Draw(renderTarget, targetRectangle, _player2Bitmap, 
                                _playerInfoCollection.GetPlayerInfo(1).Nickname, _nicknameFontSize, 
                                _nicknameForegroundBrush, _nicknameShadowBrush, _nicknameTextFormat);
                            break;
                        case FieldItemEnum.Player3:
                            PlayerCell.Draw(renderTarget, targetRectangle, _player3Bitmap, 
                                _playerInfoCollection.GetPlayerInfo(2).Nickname, _nicknameFontSize, 
                                _nicknameForegroundBrush, _nicknameShadowBrush, _nicknameTextFormat);
                            break;
                        case FieldItemEnum.Player4:
                            PlayerCell.Draw(renderTarget, targetRectangle, _player4Bitmap, 
                                _playerInfoCollection.GetPlayerInfo(3).Nickname, _nicknameFontSize, 
                                _nicknameForegroundBrush, _nicknameShadowBrush, _nicknameTextFormat);
                            break;
                        case FieldItemEnum.Player1WithBomb:
                            PlayerWithBombCell.Draw(renderTarget, targetRectangle, _player1Bitmap, _bombBitmap, 
                                _playerInfoCollection.GetPlayerInfo(0).Nickname, _nicknameFontSize, 
                                _nicknameForegroundBrush, _nicknameShadowBrush, _nicknameTextFormat);
                            break;
                        case FieldItemEnum.Player2WithBomb:
                            PlayerWithBombCell.Draw(renderTarget, targetRectangle, _player2Bitmap, _bombBitmap, 
                                _playerInfoCollection.GetPlayerInfo(1).Nickname, _nicknameFontSize, 
                                _nicknameForegroundBrush, _nicknameShadowBrush, _nicknameTextFormat);
                            break;
                        case FieldItemEnum.Player3WithBomb:
                            PlayerWithBombCell.Draw(renderTarget, targetRectangle, _player3Bitmap, _bombBitmap, 
                                _playerInfoCollection.GetPlayerInfo(2).Nickname, _nicknameFontSize, 
                                _nicknameForegroundBrush, _nicknameShadowBrush, _nicknameTextFormat);
                            break;
                        case FieldItemEnum.Player4WithBomb:
                            PlayerWithBombCell.Draw(renderTarget, targetRectangle, _player4Bitmap, _bombBitmap, 
                                _playerInfoCollection.GetPlayerInfo(3).Nickname, _nicknameFontSize, 
                                _nicknameForegroundBrush, _nicknameShadowBrush, _nicknameTextFormat);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}