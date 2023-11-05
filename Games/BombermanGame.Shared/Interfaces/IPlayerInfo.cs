namespace Games.BombermanGame.Shared.Interfaces
{
    public interface IPlayerInfo : IPositionItem
    {
        int Score { get; }
        string Nickname { get; }
        Action? OnScoreUpdated { set; }

        void BlowUpPlayer();
        void PlayerBlowUpDestroyableCell();
    }
}