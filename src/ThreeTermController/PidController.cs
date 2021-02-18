using static ThreeTermController.PidControllerExtensions;

namespace ThreeTermController
{
    public record PidController
    {
        public Gains Gains { get; init; }
        public IntegratorLimits Limits { get; init; }

        public double PreviousProcessVariable { get; init; }
        public double IntegratorState { get; init; }

        public PidController(Gains gains, IntegratorLimits limits)
        {
            Gains = gains;
            Limits = limits;
        }

        public PidController(double proportionalGain, double integralGain, double derivativeGain, double integratorMinimumLimit, double integratorMaximumLimit)
        : this(
            Gains(proportionalGain, integralGain, derivativeGain), 
            IntegratorLimits(integratorMinimumLimit, integratorMaximumLimit))
        {
        }

        public static (PidController controller, double output) Update(PidController controller, double error, double processVariable)
        {
            var proportionalTerm = ProportionalTerm(controller.Gains.Proportional, error);
            
            var newIntegratorState = controller.IntegratorState
                .UpdateIntegratorState(error)
                .Clamp(controller.Limits);

            var integralTerm = IntegralTerm(controller.Gains.Integral, newIntegratorState);

            var derivativeTerm = DerivativeTerm(controller.Gains.Derivative, controller.PreviousProcessVariable, processVariable);

            var newController = UpdateController(controller, newIntegratorState, processVariable);
            var output = proportionalTerm + integralTerm + derivativeTerm;

            return (newController, output);
        }
    }
}