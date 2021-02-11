using System;

namespace ThreeTermController
{
    public static class PidControllerExtensions
    {
        public static double Error(double setPoint, double processVariable) =>
            setPoint - processVariable;

        public static Gains Gains(double proportionalGain, double integralGain, double derivativeGain) =>
            new()
            {
                Proportional = proportionalGain,
                Integral = integralGain,
                Derivative = derivativeGain
            };

        public static IntegratorLimits IntegratorLimits(double minimum, double maximum) =>
            new()
            {
                Minimum = minimum,
                Maximum = maximum
            };

        public static Pid Pid() =>
            new()
            {
                PreviousProcessVariable = 0,
                IntegratorState = 0
            };

        internal static double Clamp(this double @this, IntegratorLimits limits) =>
            Math.Clamp(@this, limits.Minimum, limits.Maximum);

        internal static double DerivativeTerm(double derivativeGain, double previousProcessVariable,
            double processVariable) =>
            derivativeGain * (previousProcessVariable - processVariable);

        internal static double IntegralTerm(double integralGain, double integratorState) =>
            integralGain * integratorState;

        internal static double ProportionalTerm(double proportionalGain, double error) =>
            proportionalGain * error;

        internal static double UpdateIntegratorState(this double @this, double error) =>
            @this + error;

        internal static Pid PidWithIntegratorState(this double @this, Pid pid) =>
            pid with {IntegratorState = @this};
    }
}