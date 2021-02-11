namespace ThreeTermController
{
    public record Pid
    {
        public double PreviousProcessVariable { get; init; }
        public double IntegratorState { get; init; }
    }
}