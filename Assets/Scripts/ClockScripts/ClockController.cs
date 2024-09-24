using Extensions;
using UniRx;

namespace Client
{
    public class ClockController
    {
        public ClockDigital ClockDigital { get; private set; }
        public ClockAnalog ClockAnalog { get; private set; }

        public bool IsStopTimer { get; set; }

        public void SetDigitalClock(ClockDigital clockDigital)
        {
            Preconditions.CheckNotNull(clockDigital);
            ClockDigital = clockDigital;
        }

        public void SetAnalogClock(ClockAnalog clockAnalog)
        {
            Preconditions.CheckNotNull(clockAnalog);
            ClockAnalog = clockAnalog;
        }
    }
}