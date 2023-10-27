using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Multi;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;

namespace Games.BombermanGame.ObsoleteGame.DrawDataModel.RamResources.Multi
{
    public class FieldResource :BaseRamMultiResource
    {
        public static int ResourceGroupId = 2000;

        public override string ResourceName { get; }
        public BitmapResource FieldBackgroundBitmapResource { get; }
        public BitmapResource IndestructibleFieldBitmapResource { get; }
        public BitmapResource DestructibleFieldBitmapResource { get; }
        public BitmapResource Player1StartPoint { get; }
        public BitmapResource Player2StartPoint { get; }
        public BitmapResource Player3StartPoint { get; }
        public BitmapResource Player4StartPoint { get; }
        public BitmapResource Bomb { get; }
        public BitmapResource Player1 { get; }
        public BitmapResource Player2 { get; }
        public BitmapResource Player3 { get; }
        public BitmapResource Player4 { get; }
        public TextFormatResource NicknameTextFormatResource { get; }
        public BrushResource NicknameForegroundBrushResource { get; }
        public BrushResource NicknameShadowBrushResource { get; }
        
        public override List<IDisposable> AssociatedResources { get; }
        
        public override int GetResourceGroupId() => ResourceGroupId;

        public FieldResource(
            string resourceName, 
            BitmapResource fieldBackgroundBitmapResource,
            BitmapResource indestructibleFieldBitmapResource, 
            BitmapResource destructibleFieldBitmapResource, 
            BitmapResource player1StartPoint, 
            BitmapResource player2StartPoint, 
            BitmapResource player3StartPoint, 
            BitmapResource player4StartPoint, 
            BitmapResource bomb, 
            BitmapResource player1, 
            BitmapResource player2, 
            BitmapResource player3, 
            BitmapResource player4, 
            TextFormatResource nicknameTextFormatResource, 
            BrushResource nicknameForegroundBrushResource, 
            BrushResource nicknameShadowBrushResource)
        {
            ResourceName = resourceName;
            FieldBackgroundBitmapResource = fieldBackgroundBitmapResource;
            IndestructibleFieldBitmapResource = indestructibleFieldBitmapResource;
            DestructibleFieldBitmapResource = destructibleFieldBitmapResource;
            Player1StartPoint = player1StartPoint;
            Player2StartPoint = player2StartPoint;
            Player3StartPoint = player3StartPoint;
            Player4StartPoint = player4StartPoint;
            Bomb = bomb;
            Player1 = player1;
            Player2 = player2;
            Player3 = player3;
            Player4 = player4;
            NicknameTextFormatResource = nicknameTextFormatResource;
            NicknameForegroundBrushResource = nicknameForegroundBrushResource;
            NicknameShadowBrushResource = nicknameShadowBrushResource;

            AssociatedResources = new List<IDisposable>
            {
                fieldBackgroundBitmapResource,
                indestructibleFieldBitmapResource,
                destructibleFieldBitmapResource,
                player1StartPoint,
                player2StartPoint,
                player3StartPoint,
                player4StartPoint,
                bomb,
                player1,
                player2,
                player3,
                player4,
                nicknameTextFormatResource,
                nicknameForegroundBrushResource,
                nicknameShadowBrushResource
            };
        }
    }
}