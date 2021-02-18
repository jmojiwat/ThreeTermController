using udouble = ThreeTermController.UnsignedDouble;

namespace ThreeTermController
{
    public record Gains
    {
        public udouble Proportional { get; init; }
        public udouble Integral { get; init; }
        public udouble Derivative { get; init; }
    }
}