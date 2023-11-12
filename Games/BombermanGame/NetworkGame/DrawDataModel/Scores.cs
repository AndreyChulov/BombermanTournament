using Core.Engine.Shared.Interfaces;
using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Core.Engine.Shared.Objects.GraphicEngine.Draw;
using Games.BombermanGame.NetworkGame.DrawDataModel.Score;
using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.GameDataModel.Player;
using Games.BombermanGame.Shared.Interfaces.DrawableObject;
using Games.BombermanGame.Shared.RamResources.Multi;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Games.BombermanGame.NetworkGame.DrawDataModel;

public class Scores : BaseDraw
{
    private const int GRID_ROW_COUNT = 2;
    private const int GRID_COLUMN_COUNT = 2;
    private const float FONT_SIZE_MULTIPLICATOR = 0.5f;
    
    protected override string LinkedResourceName => "BombermanNetworkGame.Score";
    protected override int LinkedResourceGroupId => ScoresResource.ResourceGroupId;

    private readonly PlayerInfoCollection _playerInfoCollection;
    private readonly FieldCellsGrid _fieldCellsGrid;
    private ScoresResource _scoresResource;
    private List<IDrawableScoreObject> _drawableObjects = new();

    public static Scores Create(IEngine engine, BombermanNetworkGame game)
    {
        var canvasSize = engine.GetCanvasSize();
        var canvasWidth = canvasSize.Width;
        var canvasHeight = canvasSize.Height;

        return new Scores(
            new RectangleF(
                0.13f * canvasWidth,
                0.015f * canvasHeight,
                0.74f * canvasWidth,
                0.1f * canvasHeight
            ),
            game.PlayerCollectionMediator.PlayersInfo
        );
    }
    
    private Scores(
        RectangleF targetRectangle, 
        PlayerInfoCollection playerInfoCollection)
    {
        _playerInfoCollection = playerInfoCollection;

        _fieldCellsGrid = new FieldCellsGrid(
            targetRectangle, 
            GRID_COLUMN_COUNT, 
            GRID_ROW_COUNT);
        
        PopulateDrawableObjects();
    }

    private void PopulateDrawableObjects()
    {
        _fieldCellsGrid.EnumerateCells((rowIndex, columnIndex, cell) =>
        {
            var playerIndex = rowIndex * GRID_ROW_COUNT + columnIndex;
            var scoreHeight = cell.Height;
            var fontSize = scoreHeight * FONT_SIZE_MULTIPLICATOR;
            
            if (playerIndex < 4)
            {
                _drawableObjects.Add(playerIndex switch
                {
                    0 => new Player1Score(cell, fontSize, _playerInfoCollection),
                    1 => new Player2Score(cell, fontSize, _playerInfoCollection),
                    2 => new Player3Score(cell, fontSize, _playerInfoCollection),
                    3 => new Player4Score(cell, fontSize, _playerInfoCollection),
                    _ => throw new InvalidOperationException(
                        $"Unexpected [{nameof(playerIndex)}] value [{playerIndex}]")
                });
            }

            return (int?)null;
        });
    }

    protected override void SetRamResource(IRamResource resource)
    {
        _scoresResource = (ScoresResource)resource;

        foreach (var drawableScoreObject in _drawableObjects)
        {
            drawableScoreObject.SetFieldResource(_scoresResource);
        }
    }

    protected override IRamResource CreateIRamResource(ID2D1HwndRenderTarget renderTarget, IDWriteFactory directWriteFactory)
    {
        var scoreHeight = _fieldCellsGrid.GetCellRectangle(0, 0).Height;
        var fontSize = scoreHeight * FONT_SIZE_MULTIPLICATOR;
        
        ScoresResource fieldResource = new(
            LinkedResourceName,
            Shared.DrawDataModel.Scores.Score.CreateTextFormatResource(LinkedResourceName, directWriteFactory, fontSize),
            Shared.DrawDataModel.Scores.Score.CreateForegroundBrushResource(LinkedResourceName, renderTarget),
            Shared.DrawDataModel.Scores.Score.CreateShadowBrushResource(LinkedResourceName, renderTarget),
            Shared.DrawDataModel.Scores.Score.CreatePlayer1BackgroundBrushResource(LinkedResourceName, renderTarget),
            Shared.DrawDataModel.Scores.Score.CreatePlayer2BackgroundBrushResource(LinkedResourceName, renderTarget),
            Shared.DrawDataModel.Scores.Score.CreatePlayer3BackgroundBrushResource(LinkedResourceName, renderTarget),
            Shared.DrawDataModel.Scores.Score.CreatePlayer4BackgroundBrushResource(LinkedResourceName, renderTarget)
        );

        return fieldResource;
    }

    public override void Draw(ID2D1HwndRenderTarget renderTarget)
    {
        foreach (var drawableScoreObject in _drawableObjects)
        {
            drawableScoreObject.Draw(renderTarget);
        }
    }
}