using System.Drawing;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.GameDataModel;

public class BasePlayerInfo : IPlayerInfo
{
    public int Score { get; set; }
    public string Nickname { get; set; }

    public int X { get; set; }
    public int Y { get; set; }
        
    private BasePlayerInfo(int score, string nickname, int x, int y)
    {
        Score = score;
        Nickname = nickname;
        X = x;
        Y = y;
    }
        
    public BasePlayerInfo(string nickname, Point playerPosition) 
        : this(0, nickname, playerPosition.X, playerPosition.Y)
    {}

    public virtual void BlowUpPlayer()
    {
        throw new NotSupportedException(
            $"{nameof(BlowUpPlayer)} calls is not supported from {nameof(BasePlayerInfo)}");
    }

    public virtual void PlayerBlowUpDestroyableCell()
    {
        throw new NotSupportedException(
            $"{nameof(PlayerBlowUpDestroyableCell)} calls is not supported from {nameof(BasePlayerInfo)}");
    }
}