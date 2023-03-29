namespace Decision.Minimax
{
    public class Roll : IMinimaxRoll
    {
        public string Mark { get; private set; }

        public Roll(string mark)
        {
            Mark = mark;
        }
    }

    public class GameState : IGameState
    {
        string datagram;
        public string Datagram
        {
            get
            {
                return datagram;
            }

            private set
            {
                datagram = value;
                CrossOpenings = Rules.Openings(datagram, "x");
                CircleOpenings = Rules.Openings(datagram, "o");
                Result = Rules.Result(datagram);
            }
        }

        public int CrossOpenings { get; private set; }

        public int CircleOpenings { get; private set; }

        public Rules.GameResult Result { get; private set; }

        public GameState(string gameState)
        {
            Datagram = gameState;
        }
    }

    public class Move : IMinimaxMove
    {
        public int Cell { get; private set; }

        public IGameState gameState { get; private set; }

        public Move(int cell, IGameState gameState)
        {
            Cell = cell;
            this.gameState = gameState;
        }
    }

    public class Response<T> : IMinimaxResponse<T>
    {
        public IMinimaxMove Move { get; private set; }

        public T Utility { get; private set; }

        public Response(IMinimaxMove move, T utility)
        {
            Move = move;
            Utility = utility;
        }
    }
}
