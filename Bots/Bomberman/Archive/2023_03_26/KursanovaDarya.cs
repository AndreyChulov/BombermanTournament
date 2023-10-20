using Bots.Bomberman.Archive._2023_03_26.Utils;
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Bots.Bomberman.Archive._2023_03_26
{


    struct DetectionArea
    {
        public Coords leftField;
        public Coords rightField;
        public Coords topField;
        public Coords bottomField;

    }

    public class TestAi1 : IPlayer
    {

        public string Nickname { get; } = "Darya";
        public string StrategyDescription { get; } = "move with an A*-algorithm pathfinder";
        public bool IsDebugMode { get; } = false;

        private bool isNotPathDefined; //проверка на наличие какого либо пути
        private bool isFirstTurn; //проверка на первый ход в игре

        //private List<Coords> BombsCoordsList;
        private List<Coords> PlayersCoordsList;
        private List<Coords> DestructibleFieldsCoordsList; //найденые разрушаемые стены
        private List<Coords> detectionBombsArea; //область вокруг бота с потенциальными бомбами
        private List<Coords> path;  //текущий путь по координатам, которому следует бот
        private Coords targetNearbyPosition; // координаты рядом с целью
        private Coords targetPosition;  // текущая цель
        private Coords currPos;

        /*индекс следующий ноды, к которой пойдет бот
        Это значение меняется каждый ход бота и запоминается для след. хода*/
        private int NextStepPathPosition;


        //инициализация и первичные параметры бота
        public TestAi1()
        {

            targetPosition = new Coords(17, 17);
            targetNearbyPosition = new Coords(17, 17);

            isFirstTurn = true;
            isNotPathDefined = true;

            NextStepPathPosition = -1;
            detectionBombsArea = new List<Coords>();
            path = new List<Coords>();

            DestructibleFieldsCoordsList = new List<Coords>();
        }


        public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {

            //инициализация в первом ходе
            if (isFirstTurn) { InitialScaningFields(gameInfo); isFirstTurn = false; }

            //получаем текущую позицию бота
            currPos = new Coords(currentPlayerInfo.X, currentPlayerInfo.Y);
            PlayerTurnEnum MoveTurn;

            //сканируем актуальные разрушаемые стены
            if (DestructibleFieldsCoordsList.Count != 0)
                CheckDestructibleFieldList(gameInfo, DestructibleFieldsCoordsList);
            else ScaningPlayersPosition(gameInfo, PlayersCoordsList);


            //вычисляем область вокруг бота
            detectionBombsArea.Clear();
            detectionBombsArea = ScaningNearbyArea(gameInfo, currPos);
            //сканируем ближайшие бомбы в области вокруг бота
            foreach (var detectionBombsCell in detectionBombsArea)
            {
                if (gameInfo.Field[detectionBombsCell.y][detectionBombsCell.x] == FieldItemEnum.Bomb)
                {
                    var tempAbs = new Coords(detectionBombsCell.x - currPos.x,
                                          detectionBombsCell.y - currPos.y);

                    if (NextStepPathPosition > 0 && NextStepPathPosition < path.Count - 1)
                    {
                        if (tempAbs == new Coords(1, 1)) return PlayerTurnEnum.None;
                        if (tempAbs == new Coords(0, 0))
                        {
                            if (Coords.isNeirbours(targetNearbyPosition, detectionBombsCell))
                            {
                               MoveTurn = GetDirectionForMove(currPos, path[NextStepPathPosition-1]);
                            }
                        }
                        else CreatePath(gameInfo, currPos, targetPosition);
                    }
                    break;
                }
            }


            //проверяем достигли ли мы цели
            if (currPos == targetNearbyPosition)
            {
                isNotPathDefined = true;
                if(DestructibleFieldsCoordsList.Count != 0)
                DestructibleFieldsCoordsList.Remove(targetPosition);
                else PlayersCoordsList.Remove(targetPosition);

                targetPosition = new Coords(17, 17);
                targetNearbyPosition = new Coords(17, 17);

                return PlayerTurnEnum.PutBomb;
            }


            //проверяем актуальность цели
            if (DestructibleFieldsCoordsList.Contains(targetPosition)) isNotPathDefined = false;
            else isNotPathDefined = true; 


            // если у бота не назначен путь куда-то
            if (isNotPathDefined)
            {
                if (DestructibleFieldsCoordsList.Count == 0) { CreatePath(gameInfo, currPos, CheckNearbyTarget(gameInfo, currPos, PlayersCoordsList)); }
                else { CreatePath(gameInfo, currPos, CheckNearbyTarget(gameInfo, currPos, DestructibleFieldsCoordsList)); }
                isNotPathDefined = false;
            }

            //проверяем возможность движения
            if (path.Count != 0)
            {
                MoveTurn = GetDirectionForMove(currPos, path[NextStepPathPosition]);
                if (NextStepPathPosition > 0) NextStepPathPosition -= 1;
            }
            else MoveTurn = PlayerTurnEnum.None;
            return MoveTurn;
        }

        private List<Coords> ScaningNearbyArea(IGameInfo gameInfo, Coords currPos)
        {
            List <Coords> NearbyArea = new List<Coords>();
            for (int i = currPos.x - 1; i <= currPos.x + 1; i++)
            {
                for (int j = currPos.y - 1; j <= currPos.y + 1; j++)
                {
                    Coords detectAreaCell = new Coords(i, j);
                    if (detectAreaCell.x >= 0 && detectAreaCell.x < gameInfo.FieldWidth &&
                        detectAreaCell.y >= 0 && detectAreaCell.y < gameInfo.FieldHeight)
                    {
                        if (gameInfo.Field[j][i] != FieldItemEnum.DestructibleField &&
                            gameInfo.Field[j][i] != FieldItemEnum.IndestructibleField)
                        {
                            NearbyArea.Add(detectAreaCell);
                        }
                    }
                }
            }
            return NearbyArea;
        }

        //Первичное сканирование поля на наличие разрушаемых стен
        private void InitialScaningFields(IGameInfo gameInfo)
        {
            for (int i = 0; i < gameInfo.FieldHeight; i++)
            {
                for (int j = 0; j < gameInfo.FieldHeight; j++)
                {
                    if (gameInfo.Field[i][j] == FieldItemEnum.DestructibleField)
                        DestructibleFieldsCoordsList.Add(new Coords(j, i));
                }
            }
        }

        //Проверка актуальности текущих разрушаемых стен на карте
        private void CheckDestructibleFieldList(IGameInfo gameInfo, List<Coords> DestructibleFieldList)
        {
            foreach (var field in DestructibleFieldList.ToList())
            {
                if (gameInfo.Field[field.y][field.x] != FieldItemEnum.DestructibleField){ DestructibleFieldList.Remove(field); return; }

                List<Coords> detectBombDestructionField = new List<Coords>
                {
                    new Coords(field.x + 1, field.y),
                    new Coords(field.x - 1, field.y),
                    new Coords(field.x, field.y + 1),
                    new Coords(field.x, field.y - 1)
                };
                foreach (var currCell in detectBombDestructionField)
                {
                    if (currCell.x >= 0 && currCell.x < gameInfo.FieldWidth &&
                        currCell.y >= 0 && currCell.y < gameInfo.FieldHeight && 
                        gameInfo.Field[currCell.y][currCell.x] == FieldItemEnum.Bomb)
                            DestructibleFieldList.Remove(field); return;
                }
            }
        }
        private void ScaningPlayersPosition(IGameInfo gameInfo, List<Coords> PlayersCoordsList)
        {
            for (int i = 0; i < gameInfo.FieldHeight; i++)
            {
                for (int j = 0; j < gameInfo.FieldHeight; j++)
                {
                    if ((gameInfo.Field[i][j] == FieldItemEnum.Player1 || gameInfo.Field[i][j] == FieldItemEnum.Player2 || 
                        gameInfo.Field[i][j] == FieldItemEnum.Player3 || gameInfo.Field[i][j] == FieldItemEnum.Player4) && gameInfo.Field[i][j] != gameInfo.Field[currPos.x][currPos.y])
                        PlayersCoordsList.Add(new Coords(i,j));
                }
            }
        }

        //ищет ближайшую цель
        private Coords CheckNearbyTarget(IGameInfo gameInfo, Coords currPos, List<Coords> TargetsCoordsList)
        {
            if(TargetsCoordsList.Count == 0) return new Coords(0,0);
            targetPosition = TargetsCoordsList[0];
            int minDist = currPos.GetDistanceBetweenCoords(targetPosition);
            foreach (Coords coord in TargetsCoordsList)
            {
                int distance = currPos.GetDistanceBetweenCoords(coord);
                if (distance < minDist)
                {
                    Coords temp = new Coords(Math.Abs(targetPosition.x - currPos.x), Math.Abs(targetPosition.y - currPos.y));
                    if (temp == new Coords(0, 2) || temp == new Coords(2, 0))
                    {
                        List<Coords> temp2 = ScaningNearbyArea(gameInfo, new Coords(targetPosition.x - currPos.x, targetPosition.y - currPos.y));
                        if (temp2.Count == 1 && temp2[0] == currPos) continue;
                        else if (TargetsCoordsList.Count == 1)
                        {
                            CheckNearbyTarget(gameInfo, currPos, PlayersCoordsList);
                        }
                        else { targetPosition = coord; minDist = distance; }
                    }
                    else { targetPosition = coord; minDist = distance; }
                }
            }
            if (Coords.isNeirbours(currPos, targetPosition))
            {
                DestructibleFieldsCoordsList.Remove(targetPosition);
                CheckNearbyTarget(gameInfo, currPos, TargetsCoordsList);
            }
            return targetPosition;
        }

        //создает путь до цели
        private void CreatePath(IGameInfo gameInfo, Coords currPos, Coords targetPos)
        {
            AStarAlgorithm pathfinder = new AStarAlgorithm(gameInfo, currPos.x, currPos.y);

            path = pathfinder.GetPath(targetPos);
            if (path.Count > 1) targetNearbyPosition = path[1];
            else if (path.Count == 1) targetNearbyPosition = path[0];
            NextStepPathPosition = path.Count - 1;

        }

        //вычисляет сторону для движения
        private PlayerTurnEnum GetDirectionForMove(Coords c1, Coords c2)
        {
            Coords diff = c1 - c2;

            if (diff.x == 1 && diff.y == 0) return PlayerTurnEnum.MoveLeft;
            if (diff.x == 0 && diff.y == 1) return PlayerTurnEnum.MoveUp;
            if (diff.x == -1 && diff.y == 0) return PlayerTurnEnum.MoveRight;
            if (diff.x == 0 && diff.y == -1) return PlayerTurnEnum.MoveDown;
            return PlayerTurnEnum.None;
        }


        public void OnTurnTimeExceeded()
        {

        }

    }
}