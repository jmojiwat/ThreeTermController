namespace ThreeTermController
{
    public record Gains
    {
        public double Proportional { get; init; }
        public double Integral { get; init; }
        public double Derivative { get; init; }
    }
}