using BombermanGame.Shared.Interfaces;
using Engine.Shared.GraphicEngine.Draw;
using Engine.Shared.GraphicEngine.RamResources.Multi;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Engine.SharedInterfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

namespace BombermanGame.DrawDataModel.Draw.Score
{
    public abstract class BasePlayerScore : TextWithShadow
    {
        protected override string LinkedResourceName => "BombermanGame.Score.BasePlayerScore";
        protected override int LinkedResourceGroupId => SystemTextWithShadow.ResourceGroupId;

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

            TextFormat textFormat = new(
                "BombermanGame.Draw.Score.BasePlayerScoreTextFormat", idWriteTextFormat);
            Engine.Shared.GraphicEngine.RamResources.Single.Brush textBrush = new(
                "BombermanGame.Draw.Score.BasePlayerScoreTextBrush", id2D1TextBrush);
            Engine.Shared.GraphicEngine.RamResources.Single.Brush shadowBrush = new(
                "BombermanGame.Draw.Score.BasePlayerScoreTextShadowBrush", id2D1ShadowBrush);
            return new SystemTextWithShadow(LinkedResourceName, textFormat, shadowBrush, textBrush);
        }

        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            TextToDraw = ConstructTextToDraw();
            base.Draw(renderTarget);
        }
    }
}