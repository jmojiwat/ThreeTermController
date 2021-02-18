namespace ThreeTermController
{
    public class PidControllerOld
    {
        private readonly Gains gains;
        private readonly IntegratorLimits limits;
        private Pid pid;

        public PidControllerOld(double proportionalGain, double integralGain, double derivativeGain, double integratorMinimumLimit, double integratorMaximumLimit)
            : this(
                PidControllerExtensions.Gains(proportionalGain, integralGain, derivativeGain),
                PidControllerExtensions.IntegratorLimits(integratorMinimumLimit, integratorMaximumLimit)
            )
        {
        }

        public PidControllerOld(Gains gains, IntegratorLimits limits)
        {
            this.gains = gains;
            this.limits = limits;
            pid = PidControllerExtensions.Pid();
        }

        public double Update(double error, double processVariable)
        {
            var proportionalTerm = PidControllerExtensions.ProportionalTerm(gains.Proportional, error);
            
            var newPid = pid.IntegratorState
                .UpdateIntegratorState(error)
                .Clamp(limits)
                .PidWithIntegratorState(pid);

            var integralTerm = PidControllerExtensions.IntegralTerm(gains.Integral, newPid.IntegratorState);

            var derivativeTerm = PidControllerExtensions.DerivativeTerm(gains.Derivative, newPid.PreviousProcessVariable, processVariable);

            pid = newPid;

            return proportionalTerm + integralTerm + derivativeTerm;
        }
    }
}