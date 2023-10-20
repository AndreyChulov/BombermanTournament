namespace Games.BombermanGame.Shared.Interfaces
{
    public interface IPlayerInfo : IPositionItem
    {
        int Score { get; }
        string Nickname { get; }

        void BlowUpPlayer();
        void PlayerBlowUpDestroyableCell();
    }
}