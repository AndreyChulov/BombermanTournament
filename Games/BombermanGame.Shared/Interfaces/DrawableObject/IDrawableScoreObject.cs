using Games.BombermanGame.Shared.RamResources.Multi;

namespace Games.BombermanGame.Shared.Interfaces.DrawableObject;

public interface IDrawableScoreObject : IDrawableObject
{
    void SetFieldResource(ScoresResource resource);
}