namespace Decision.Minimax
{
    public interface IMinimaxRoll
    {
        string Mark { get; }
    }

    public interface IGameState
    {
        string Datagram { get; }

        int CrossOpenings { get; }
        int CircleOpenings { get; }
        Rules.GameResult Result { get; }
    }

    public interface IMinimaxMove : IDecisionMove
    {
        IGameState gameState { get; }
    }

    public interface IMinimaxUtilityParameters<T>
    {
        T MinValue { get; }
        T MaxValue { get; }
        T DefaultValue { get; }
    }

    public interface IMinimaxResponse<T>
    {
        IMinimaxMove Move { get; }
        T Utility { get; }
    }

    public interface IMinimaxExecutor<T> : IDecisionMoveProvider
    {
    }
}
