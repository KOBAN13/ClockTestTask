using System;

namespace Client
{
    public class ClockAnalog
    {
        private readonly DateTime _dateTime;
        public float HourAngle { get; private set; }
        public float MinuteAngle { get; private set; }
        public float SecondAngle { get; private set; }

        public void Update(float hour, float minute, float second)
        {
            HourAngle = (hour % 12 + minute / 60f) * 360 / 12;
            MinuteAngle = (minute + second / 60f) * 360 / 60;
            SecondAngle = second * 360 / 60;
        }
    }
}