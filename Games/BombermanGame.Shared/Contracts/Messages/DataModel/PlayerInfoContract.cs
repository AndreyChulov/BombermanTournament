using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.Contracts.Messages.DataModel;

public class PlayerInfoContract : IPlayerInfo
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Score { get; set; }
    public string Nickname { get; set; }

    public Action? OnScoreUpdated
    {
        get => null;
        set { }
    }

    public void BlowUpPlayer()
    {
        throw new NotSupportedException();
    }

    public void PlayerBlowUpDestroyableCell()
    {
        throw new NotSupportedException();
    }

    public static PlayerInfoContract Initialize(IPlayerInfo playerInfo)
    {
        return new PlayerInfoContract
        {
            Nickname = playerInfo.Nickname,
            Score = playerInfo.Score,
            X = playerInfo.X,
            Y = playerInfo.Y
        };
    }
}