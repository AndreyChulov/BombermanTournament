using System;
using System.Collections.Generic;
using Engine.Shared.GraphicEngine.RamResources.Multi;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Bitmap = Engine.Shared.GraphicEngine.RamResources.Single.Bitmap;
using Brush = Engine.Shared.GraphicEngine.RamResources.Single.Brush;

namespace BombermanGame.DrawDataModel.RamResources.Multi
{
    public class FieldItem :BaseRamMultiResource
    {
        public static int ResourceGroupId = 1001;

        public override string ResourceName { get; }
        public Bitmap FieldBackgroundBitmap { get; }
        public Bitmap IndestructibleFieldBitmap { get; }
        public Bitmap DestructibleFieldBitmap { get; }
        public Bitmap Player1StartPoint { get; }
        public Bitmap Player2StartPoint { get; }
        public Bitmap Player3StartPoint { get; }
        public Bitmap Player4StartPoint { get; }
        public Bitmap Bomb { get; }
        public Bitmap Player1 { get; }
        public Bitmap Player2 { get; }
        public Bitmap Player3 { get; }
        public Bitmap Player4 { get; }
        public TextFormat NicknameTextFormat { get; }
        public Brush NicknameForegroundBrush { get; }
        
        public override List<IDisposable> AssociatedResources { get; }
        
        public override int GetResourceGroupId() => ResourceGroupId;

        public FieldItem(
            string resourceName, 
            Bitmap fieldBackgroundBitmap,
            Bitmap indestructibleFieldBitmap, 
            Bitmap destructibleFieldBitmap, 
            Bitmap player1StartPoint, 
            Bitmap player2StartPoint, 
            Bitmap player3StartPoint, 
            Bitmap player4StartPoint, 
            Bitmap bomb, 
            Bitmap player1, 
            Bitmap player2, 
            Bitmap player3, 
            Bitmap player4, 
            TextFormat nicknameTextFormat, 
            Brush nicknameForegroundBrush)
        {
            ResourceName = resourceName;
            FieldBackgroundBitmap = fieldBackgroundBitmap;
            IndestructibleFieldBitmap = indestructibleFieldBitmap;
            DestructibleFieldBitmap = destructibleFieldBitmap;
            Player1StartPoint = player1StartPoint;
            Player2StartPoint = player2StartPoint;
            Player3StartPoint = player3StartPoint;
            Player4StartPoint = player4StartPoint;
            Bomb = bomb;
            Player1 = player1;
            Player2 = player2;
            Player3 = player3;
            Player4 = player4;
            NicknameTextFormat = nicknameTextFormat;
            NicknameForegroundBrush = nicknameForegroundBrush;

            AssociatedResources = new List<IDisposable>
            {
                fieldBackgroundBitmap,
                indestructibleFieldBitmap,
                destructibleFieldBitmap,
                player1StartPoint,
                player2StartPoint,
                player3StartPoint,
                player4StartPoint,
                bomb,
                player1,
                player2,
                player3,
                player4,
                nicknameTextFormat,
                nicknameForegroundBrush
            };
        }
    }
}