using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

namespace BombermanGame.DrawDataModel.Draw
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

        private readonly FieldItemEnum[][] _fieldItems;
        private readonly RectangleF _targetRectangle;
        private readonly PlayerInfoCollection _playerInfoCollection;
        private readonly int _fieldItemsWidth;
        private readonly int _fieldItemsHeight;
        private readonly RectangleF[][] _fieldItemsTargetRects;
        private readonly float _nicknameFontSize;

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
            _fieldItemsTargetRects = CreateFieldItemsTargetRects();
            _nicknameFontSize = targetRectangle.Height * 0.02f;
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
        }

        private RectangleF[][] CreateFieldItemsTargetRects()
        {
            var startX = _targetRectangle.X;
            var startY = _targetRectangle.Y;
            var targetWidth = _targetRectangle.Width;
            var targetHeight = _targetRectangle.Height;
            var deltaWidth = targetWidth / _fieldItemsWidth;
            var deltaHeight = targetHeight / _fieldItemsHeight;

            RectangleF[][] fieldItemsTargetRects = new RectangleF[_fieldItemsHeight][];

            for (int rowCounter = 0; rowCounter < _fieldItemsHeight; rowCounter++)
            {
                fieldItemsTargetRects[rowCounter] = new RectangleF[_fieldItemsWidth];
                
                for (int columnCounter = 0; columnCounter < _fieldItemsWidth; columnCounter++)
                {
                    fieldItemsTargetRects[rowCounter][columnCounter] = 
                        new RectangleF(
                            startX + columnCounter * deltaWidth,
                            startY + rowCounter * deltaHeight,
                            deltaWidth, 
                            deltaHeight
                            );
                }
            }

            return fieldItemsTargetRects;
        }

        protected override IRamResource CreateIRamResource(
            ID2D1HwndRenderTarget renderTarget, IDWriteFactory directWriteFactory)
        {
            FieldItem fieldItem = new(
                LinkedResourceName,
                new Bitmap(
                    $"{LinkedResourceName}.FieldBackground",
                    renderTarget.LoadBitmapFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.fieldBackground.png")
                ),
                new Bitmap(
                    $"{LinkedResourceName}.IndestructibleField",
                    renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.indestructibleCell.png")
                ),
                new Bitmap(
                    $"{LinkedResourceName}.DestructibleField",
                    renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.destructibleCell.png"
                    )
                ),
                new Bitmap(
                    $"{LinkedResourceName}.Player1StartPoint",
                    renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.playerGreenStartPoint.png"
                    )
                ),
                new Bitmap(
                    $"{LinkedResourceName}.Player2StartPoint",
                    renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.playerBlueStartPoint.png"
                    )
                ),
                new Bitmap(
                    $"{LinkedResourceName}.Player3StartPoint",
                    renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.playerRedStartPoint.png"
                    )
                ),
                new Bitmap(
                    $"{LinkedResourceName}.Player4StartPoint",
                    renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.playerYellowStartPoint.png"
                    )
                ),
                new Bitmap(
                    $"{LinkedResourceName}.Bomb",
                    renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.bomb.png"
                    )
                ),
                new Bitmap(
                    $"{LinkedResourceName}.Player1",
                    renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.Robo.p1.png"
                    )
                ),
                new Bitmap(
                    $"{LinkedResourceName}.Player2",
                    renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.Robo.p2.png"
                    )
                ),
                new Bitmap(
                    $"{LinkedResourceName}.Player3",
                    renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.Robo.p3.png"
                    )
                ),
                new Bitmap(
                    $"{LinkedResourceName}.Player4",
                    renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                        Assembly.GetExecutingAssembly(),
                        "BombermanGame.EmbeddedResources.Robo.p4.png"
                    )
                ),
                new TextFormat(
                    $"{LinkedResourceName}.NicknameTextFormat",
                    directWriteFactory.CreateTextFormat(
                        "Times new roman", FontWeight.Bold, FontStyle.Italic,
                        FontStretch.Normal, _nicknameFontSize
                    )
                ),
                new Brush(
                    $"{LinkedResourceName}.NicknameBrush",
                    renderTarget.CreateSolidColorBrush(new Color4(Color3.Red, 1f)
                    )
                )
            );

            return fieldItem;
        }

        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            renderTarget.DrawBitmap(
                _fieldBackgroundBitmap, 
                _targetRectangle,
                1f,
                BitmapInterpolationMode.Linear,
                null
            );
            
            for (int rowCounter = 0; rowCounter < _fieldItemsHeight; rowCounter++)
            {
                for (int columnCounter = 0; columnCounter < _fieldItemsWidth; columnCounter++)
                {
                    var targetRect = _fieldItemsTargetRects[rowCounter][columnCounter];
                    
                    switch (_fieldItems[rowCounter][columnCounter])
                    {
                        case FieldItemEnum.EmptyField:
                            break;
                        case FieldItemEnum.IndestructibleField:
                            renderTarget.DrawBitmap(
                                _indestructibleFieldBitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            break;
                        case FieldItemEnum.DestructibleField:
                            renderTarget.DrawBitmap(
                                _destructibleFieldBitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            break;
                        case FieldItemEnum.Player1StartPoint:
                            renderTarget.DrawBitmap(
                                _player1StartPointBitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            break;
                        case FieldItemEnum.Player2StartPoint:
                            renderTarget.DrawBitmap(
                                _player2StartPointBitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            break;
                        case FieldItemEnum.Player3StartPoint:
                            renderTarget.DrawBitmap(
                                _player3StartPointBitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            break;
                        case FieldItemEnum.Player4StartPoint:
                            renderTarget.DrawBitmap(
                                _player4StartPointBitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            break;
                        case FieldItemEnum.Bomb:
                            renderTarget.DrawBitmap(
                                _bombBitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            break;
                        case FieldItemEnum.Player1:
                            renderTarget.DrawBitmap(
                                _player1Bitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawText(
                                _playerInfoCollection.GetPlayerInfo(0).Nickname,
                                _nicknameTextFormat,
                                new RectangleF(
                                    targetRect.X, targetRect.Y - _nicknameFontSize, 
                                    targetRect.Width, _nicknameFontSize + 10),
                                _nicknameForegroundBrush
                            );
                            break;
                        case FieldItemEnum.Player2:
                            renderTarget.DrawBitmap(
                                _player2Bitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawText(
                                _playerInfoCollection.GetPlayerInfo(1).Nickname,
                                _nicknameTextFormat,
                                new RectangleF(
                                    targetRect.X, targetRect.Y - _nicknameFontSize, 
                                    targetRect.Width, _nicknameFontSize + 10),
                                _nicknameForegroundBrush
                            );
                            break;
                        case FieldItemEnum.Player3:
                            renderTarget.DrawBitmap(
                                _player3Bitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawText(
                                _playerInfoCollection.GetPlayerInfo(2).Nickname,
                                _nicknameTextFormat,
                                new RectangleF(
                                    targetRect.X, targetRect.Y - _nicknameFontSize, 
                                    targetRect.Width, _nicknameFontSize + 10),
                                _nicknameForegroundBrush
                            );
                            break;
                        case FieldItemEnum.Player4:
                            renderTarget.DrawBitmap(
                                _player4Bitmap, 
                                _fieldItemsTargetRects[rowCounter][columnCounter],
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawText(
                                _playerInfoCollection.GetPlayerInfo(3).Nickname,
                                _nicknameTextFormat,
                                new RectangleF(
                                    targetRect.X, targetRect.Y - _nicknameFontSize, 
                                    targetRect.Width, _nicknameFontSize + 10),
                                _nicknameForegroundBrush
                            );
                            break;
                        case FieldItemEnum.Player1WithBomb:
                            renderTarget.DrawBitmap(
                                _bombBitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawBitmap(
                                _player1Bitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawText(
                                _playerInfoCollection.GetPlayerInfo(0).Nickname,
                                _nicknameTextFormat,
                                new RectangleF(
                                    targetRect.X, targetRect.Y - _nicknameFontSize, 
                                    targetRect.Width, _nicknameFontSize + 10),
                                _nicknameForegroundBrush
                            );
                            break;
                        case FieldItemEnum.Player2WithBomb:
                            renderTarget.DrawBitmap(
                                _bombBitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawBitmap(
                                _player2Bitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawText(
                                _playerInfoCollection.GetPlayerInfo(1).Nickname,
                                _nicknameTextFormat,
                                new RectangleF(
                                    targetRect.X, targetRect.Y - _nicknameFontSize, 
                                    targetRect.Width, _nicknameFontSize + 10),
                                _nicknameForegroundBrush
                            );
                            break;
                        case FieldItemEnum.Player3WithBomb:
                            renderTarget.DrawBitmap(
                                _bombBitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawBitmap(
                                _player3Bitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawText(
                                _playerInfoCollection.GetPlayerInfo(2).Nickname,
                                _nicknameTextFormat,
                                new RectangleF(
                                    targetRect.X, targetRect.Y - _nicknameFontSize, 
                                    targetRect.Width, _nicknameFontSize + 10),
                                _nicknameForegroundBrush
                            );
                            break;
                        case FieldItemEnum.Player4WithBomb:
                            renderTarget.DrawBitmap(
                                _bombBitmap, 
                                targetRect,
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawBitmap(
                                _player4Bitmap, 
                                _fieldItemsTargetRects[rowCounter][columnCounter],
                                1f,
                                BitmapInterpolationMode.Linear,
                                null
                            );
                            renderTarget.DrawText(
                                _playerInfoCollection.GetPlayerInfo(3).Nickname,
                                _nicknameTextFormat,
                                new RectangleF(
                                    targetRect.X, targetRect.Y - _nicknameFontSize, 
                                    targetRect.Width, _nicknameFontSize + 10),
                                _nicknameForegroundBrush
                            );
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}