using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Core.Engine.Shared.Objects.GraphicEngine.Draw;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Multi;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Games.BombermanGame.Shared.Interfaces;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

namespace Games.BombermanGame.DrawDataModel.Draw.Score
{
    public abstract class BasePlayerScore : TextWithShadow
    {
        protected override string LinkedResourceName => "BombermanGame.Score.BasePlayerScore";
        protected override int LinkedResourceGroupId => SystemTextWithShadowResource.ResourceGroupId;

        protected new float FontSize { get; }
        protected IPlayerInfo PlayerInfo { get; }
        protected abstract int PlayerNubmer { get; }

        private string ConstructTextToDraw()
        {
            return $"AI {PlayerNubmer} >> Nickname: {PlayerInfo.Nickname} >> Score: {PlayerInfo.Score}";
        }
        
        protected BasePlayerScore(IPlayerInfo playerInfo, Rectangle drawRectangle, Size canvasSize) 
            : base("", drawRectangle, canvasSize.Height * 0.03f)
        {
            PlayerInfo = playerInfo;
            TextToDraw = ConstructTextToDraw();
            FontSize = canvasSize.Height * 0.025f;
        }
        
        protected override IRamResource CreateIRamResource(
            ID2D1HwndRenderTarget renderTarget,
            IDWriteFactory directWriteFactory)
        {
            IDWriteTextFormat idWriteTextFormat = directWriteFactory.CreateTextFormat(
                "Times new roman", FontWeight.Normal, Vortice.DirectWrite.FontStyle.Italic, 
                FontStretch.Normal, FontSize);
            ID2D1Brush id2D1TextBrush= renderTarget.CreateSolidColorBrush(new Color4(Color3.Lime, 1f));
            ID2D1Brush id2D1ShadowBrush= renderTarget.CreateSolidColorBrush(new Color4(Color3.Black, 1f));

            TextFormatResource textFormatResource = new(
                "BombermanGame.Draw.Score.BasePlayerScoreTextFormat", idWriteTextFormat);
            BrushResource textBrushResource = new(
                "BombermanGame.Draw.Score.BasePlayerScoreTextBrush", id2D1TextBrush);
            BrushResource shadowBrushResource = new(
                "BombermanGame.Draw.Score.BasePlayerScoreTextShadowBrush", id2D1ShadowBrush);
            return new SystemTextWithShadowResource(LinkedResourceName, textFormatResource, shadowBrushResource, textBrushResource);
        }

        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            TextToDraw = ConstructTextToDraw();
            base.Draw(renderTarget);
        }
    }
}