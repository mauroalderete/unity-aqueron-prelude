using System.Text.RegularExpressions;

namespace Decision.Minimax
{
    public class AlphaBetaPruning : IMinimaxExecutor<int>
    {
        public class UtilityParameters : IMinimaxUtilityParameters<int>
        {
            public int MinValue { get; private set; }

            public int MaxValue { get; private set; }

            public int DefaultValue { get; private set; }

            public UtilityParameters()
            {
                MinValue = int.MinValue;
                MaxValue = int.MaxValue;
                DefaultValue = 0;
            }
        }

        public IMinimaxUtilityParameters<int> UtilityConfig { get; private set; }
        public Roll Maximizer { get; private set; }
        public Roll Minimizer { get; private set; }

        public int MaxDepth { get; private set; }

        GlobalConfig config;

        public AlphaBetaPruning(Roll maximizer, Roll minimizer, int maxDepth, GlobalConfig config)
        {
            Maximizer = maximizer;
            Minimizer = minimizer;
            MaxDepth = maxDepth;

            this.config = config;

            UtilityConfig = new UtilityParameters();
        }

        public IDecisionMove GetMove(string gameState)
        {
            IMinimaxResponse<int> response = minimax(
                gameState: new GameState(gameState),
                alpha: UtilityConfig.MinValue,
                beta: UtilityConfig.MaxValue,
                depth: 0
                );

            return new DecisionMove(response.Move.Cell);
        }

        public int UtilityFunction(IGameState gameState, int depth)
        {
            if (gameState.Result == Rules.GameResult.Tie)
            {
                return 0;
            }

            if (gameState.Result == Rules.GameResult.CrossWin)
            {
                if (config.PlayWithCross)
                {
                    return -100 * depth;
                }
                else
                {
                    return 100 / depth;
                }
            }

            if (gameState.Result == Rules.GameResult.CircleWin)
            {
                if (config.PlayWithCross)
                {
                    return 100 / depth;
                }
                else
                {
                    return -100 * depth;
                }
            }

            return 0;
        }

        IMinimaxResponse<int> minimax(IGameState gameState, int alpha, int beta, int depth)
        {
            // is this an terminal state
            if (gameState.Result != Rules.GameResult.Unfinished || depth >= MaxDepth)
            {
                return new Response<int>(
                    move: null,
                    utility: UtilityFunction(gameState, depth)
                );
            }

            bool maximizerTurn = depth % 2 == 0;

            Response<int> better = new Response<int>(
                move: null,
                utility: maximizerTurn ? UtilityConfig.MinValue : UtilityConfig.MaxValue);

            // generates a new game state to get the utility for each route
            MatchCollection matches = Regex.Matches(gameState.Datagram, @"\.");
            foreach (Match match in matches)
            {
                // generates the new gamestate as result of apply the new movement
                GameState newGameState = new GameState(gameState.Datagram.Replace(match.Index, 1, maximizerTurn ? Maximizer.Mark : Minimizer.Mark));

                // explore the minimax for the new leaf
                IMinimaxResponse<int> response = minimax(newGameState, alpha, beta, depth + 1);

                if (maximizerTurn)
                {
                    if (response.Utility > alpha)
                    {
                        alpha = response.Utility;

                        better = new Response<int>(
                            utility: response.Utility,
                            move: new Move(
                                cell: match.Index,
                                gameState: newGameState));

                        // alpha pruning
                        if (alpha >= beta)
                        {
                            return better;
                        }
                    }
                }
                else
                {
                    if (response.Utility < beta)
                    {
                        beta = response.Utility;

                        better = new Response<int>(
                            utility: response.Utility,
                            move: new Move(
                                cell: match.Index,
                                gameState: newGameState));

                        // beta pruning
                        if (alpha >= beta)
                        {
                            return better;
                        }
                    }
                }
            }

            return better;
        }
    }

}
