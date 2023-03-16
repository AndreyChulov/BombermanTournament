using BombermanGame.Shared.Enums;
using BombermanGame.Shared.Interfaces;

namespace TestAi
{
  public class stas : IPlayer
  {
    public string Nickname { get; } = "Stas";
    public string StrategyDescription { get; } = "Rand";
    public bool IsDebugMode { get; } = false;

    public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
    {
      for (int i = 0; i<gameInfo.FieldHeight; i++)
      {
        for (int j = 0; j<gameInfo.FieldWidth; j++)
        {
          if (gameInfo.Field[i][j]==FieldItemEnum.DestructibleField)
          {
            return PlayerTurnEnum.PutBomb;
          }
        }
      }

      if (currentPlayerInfo.X>0)
      {
        return PlayerTurnEnum.MoveLeft;
      }
      else if (currentPlayerInfo.X<gameInfo.FieldWidth-1)
      {
        return PlayerTurnEnum.MoveRight;
      }
      else if (currentPlayerInfo.Y>0)
      {
        return PlayerTurnEnum.MoveUp;
      }
      else if (currentPlayerInfo.Y<gameInfo.FieldHeight-1)
      {
        return PlayerTurnEnum.MoveDown;
      }

      return PlayerTurnEnum.None;
    }
    public void OnTurnTimeExceeded()
    {

    }
  }
}