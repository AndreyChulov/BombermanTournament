using Games.BombermanGame.Shared.RamResources.Multi;

namespace Games.BombermanGame.Shared.Interfaces.DrawableObject;

public interface IDrawableFieldObject : IDrawableObject
{
    void SetFieldResource(FieldResource resource);
}