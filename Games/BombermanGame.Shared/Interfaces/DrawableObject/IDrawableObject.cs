using Vortice.Direct2D1;

namespace Games.BombermanGame.Shared.Interfaces.DrawableObject;

public interface IDrawableObject
{
    void Draw(ID2D1HwndRenderTarget renderTarget);
}