using static ThreeTermController.PidControllerExtensions;

namespace ThreeTermController
{
    public class PidController
    {
        private readonly Gains gains;
        private readonly IntegratorLimits limits;
        private Pid pid;

        public PidController(double proportionalGain, double integralGain, double derivativeGain, double integratorMinimumLimit, double integratorMaximumLimit)
            : this(
                Gains(proportionalGain, integralGain, derivativeGain),
                IntegratorLimits(integratorMinimumLimit, integratorMaximumLimit)
            )
        {
        }

        public PidController(Gains gains, IntegratorLimits limits)
        {
            this.gains = gains;
            this.limits = limits;
            pid = Pid();
        }

        public double Update(double error, double processVariable)
        {
            var proportionalTerm = ProportionalTerm(gains.Proportional, error);
            
            var newPid = pid.IntegratorState
                .UpdateIntegratorState(error)
                .Clamp(limits)
                .PidWithIntegratorState(pid);

            var integralTerm = IntegralTerm(gains.Integral, newPid.IntegratorState);

            var derivativeTerm = DerivativeTerm(gains.Derivative, newPid.PreviousProcessVariable, processVariable);

            pid = newPid;

            return proportionalTerm + integralTerm + derivativeTerm;
        }
    }
}