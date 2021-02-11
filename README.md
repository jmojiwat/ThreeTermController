# PID Controller

A simple PID Controller based on Tim Wescott's easy to understand and insightful article [PID Without a PhD: A Simple Software Controller Design Method](https://www.wescottdesign.com/articles/pid/pidWithoutAPhd.pdf).

```c#
var proportionalGain = 1d;
var integralGain = 1d;
var derivativeGain = 1d;

var integratorMinimumLimit;
var integratorMaximumLimit;


var controller = new PidController(
	proportionalGain,
	integralGain,
	derivativeGain,
	integratorMinimumLimit,
	integratorMaximumLimit);

var setPoint = 1d;
var processVariable = ReadPlantADC();

var output = controller.Update(setPoint - processVariable /* error */, processVariable); // error is usually setPoint - processVariable, but you may have your own tweaks for it.

DrivePlantDAC(output);
```
