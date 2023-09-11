using BombermanGame.Shared.Enums;
using BombermanGame.Shared.Interfaces;

namespace BombermanBots._2023_03_26.Utils
{
    // структура для упрощения работы с координатами
    struct Coords
    {

        public int x;
        public int y;


        public Coords()
        {
            x = -1; y = -1; 
        }

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Coords(Coords other)
        {
            this.x = other.x;
            this.y = other.y;
        }

        public static bool isNeirbours(Coords c1, Coords c2)
        {
            Coords diff = c1 - c2;
            diff = new Coords(Math.Abs(diff.x), Math.Abs(diff.y));
            if (diff == new Coords(0, 1) || diff == new Coords(1, 0))
                return true;
            else return false;
            
        }

        public static bool operator ==(Coords c1, Coords c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coords c1, Coords c2)
        {
            return !(c1 == c2);
        }

        public static Coords operator -(Coords c1, Coords c2)
        {
            return new Coords(c1.x - c2.x, c1.y - c2.y);
        }

        public int GetDistanceBetweenCoords(Coords otherCoords)
        {
            return (int)Math.Sqrt(
                Math.Pow(this.x - otherCoords.x, 2) + 
                Math.Pow(this.y - otherCoords.y, 2)
                ); 
        }


    }


    class Node
    {
        public Coords NodeCoords; // координаты текущей ноды
        public Node PreviousNode; //ссылка на предыдущую ноду
        public int F;             // F = G+H
        public int G;             // расстояние от старта до ноды
        public int H;             // расстояние от ноды до цели


        public Node(Coords NodeCoords, Coords EndCoords, int g,  Node previousNode)
        {
            this.NodeCoords = NodeCoords;
            G = g;
            H = Heuristic(NodeCoords,EndCoords);
            F = G + H;
            PreviousNode = previousNode;
        }


        // Эвристическая оценка расстояния от текущей вершины до конечной
        private int Heuristic(Coords NodeCoords, Coords EndCoords)
        {
            return Math.Abs(NodeCoords.x - EndCoords.x) + Math.Abs(NodeCoords.y - EndCoords.y); 
        }


    }
    class AStarAlgorithm
    {
        public List<Coords> pathToEnd; //конечный результат - путь от начала до конца
        private IGameInfo _field; // поле
        private List<Node> _waitingNodes; //на проверку
        private List<Node> _checkedNodes; //проверенные ноды
       // Coords _end;
        Coords _start;
        private int width;
        private int height;

        public AStarAlgorithm(IGameInfo gameInfo, int startX, int startY)
        {
            pathToEnd = new List<Coords>();
            _waitingNodes = new List<Node>();
            _checkedNodes = new List<Node>();
            _field = gameInfo;
            //_end = new Coords(endX,endY);
            _start = new Coords(startX, startY);

            width = gameInfo.FieldWidth;
            height = gameInfo.FieldHeight;

        }

        public List<Coords> GetPath(Coords end)
        {

            if(_start == end) return pathToEnd;

            Node startNode = new Node(_start, end, 0, null);
            _checkedNodes.Add(startNode);
            _waitingNodes.AddRange(GetNeighbourNodes(startNode, end));

            while(_waitingNodes.Count > 0)
            {
                Node? nodeToCheck = _waitingNodes.FirstOrDefault(x => x.F == _waitingNodes.Min(x => x.F));
                if(nodeToCheck.NodeCoords== end)
                {
                    return CalcPath(nodeToCheck); 
                }

                var isValidNode= IsValid(nodeToCheck.NodeCoords);
                _waitingNodes.Remove(nodeToCheck);
                if (!isValidNode)_checkedNodes.Add(nodeToCheck);
                else if(isValidNode)
                {
                    if (!_checkedNodes.Where(x => x.NodeCoords == nodeToCheck.NodeCoords).Any())
                    {
                        _checkedNodes.Add(nodeToCheck);
                        _waitingNodes.AddRange(GetNeighbourNodes(nodeToCheck, end));
                    }
                    else
                    {
                        var sameNode = _checkedNodes.Where(x => x.NodeCoords == nodeToCheck.NodeCoords).ToList();
                        for (int i = 0; i < sameNode.Count; i++)
                        {
                            if (sameNode[i].F > nodeToCheck.F)
                                sameNode[i].F = nodeToCheck.F;
                        }
                    }
                }
            }
            return pathToEnd;
        }

        // Проверка, что ячейка находится в пределах поля и не является стеной
        private bool IsValid(Coords coords)
        {
            if (coords.x >= 0 && coords.x < width &&
                   coords.y >= 0 && coords.y < height)
                if (_field.Field[coords.y][coords.x] != FieldItemEnum.IndestructibleField &&
                    _field.Field[coords.y][coords.x] != FieldItemEnum.DestructibleField &&
                    _field.Field[coords.y][coords.x] != FieldItemEnum.Bomb &&
                    _field.Field[coords.y][coords.x] != FieldItemEnum.Player1 &&
                    _field.Field[coords.y][coords.x] != FieldItemEnum.Player2 &&
                    _field.Field[coords.y][coords.x] != FieldItemEnum.Player3 &&
                    _field.Field[coords.y][coords.x] != FieldItemEnum.Player4) return true;
                else return false;

            else return false;

            //return coords.x >= 0 && coords.x < width+1 &&
            //       coords.y >= 0 && coords.y < height+1 &&
            //       _field.Field[coords.x][coords.y] != FieldItemEnum.IndestructibleField &&
            //       _field.Field[coords.x][coords.y] != FieldItemEnum.DestructibleField;
        }


        private List<Node> GetNeighbourNodes(Node node, Coords end)
        {
            List<Node> NeighbourNodes = new List<Node>
            {
                new Node(new Coords(node.NodeCoords.x + 1, node.NodeCoords.y),
                                end,
                                node.G + 1,
                                node),
                new Node(new Coords(node.NodeCoords.x - 1, node.NodeCoords.y),
                                end,
                                node.G + 1,
                                node),
                new Node(new Coords(node.NodeCoords.x, node.NodeCoords.y + 1),
                                end,
                                node.G + 1,
                                node),
                new Node(new Coords(node.NodeCoords.x, node.NodeCoords.y - 1),
                                end,
                                node.G + 1,
                                node)
            };

            return NeighbourNodes;
            
        }

        public List<Coords> CalcPath(Node? node)
        {
            List<Coords> path = new List<Coords>();
            Node currentNode = node;

            while (currentNode.PreviousNode != null)
            {
                path.Add(new Coords(currentNode.NodeCoords));
                currentNode = currentNode.PreviousNode;
            }
            return path;
        }

    }
}
