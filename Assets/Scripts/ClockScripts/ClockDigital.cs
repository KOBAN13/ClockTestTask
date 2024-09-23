using System;

namespace Client
{
    [Serializable]
    public class ClockDigital
    {
        public float Time { get; private set; }

        public ClockDigital(float time)
        {
            Time = time;
        }

        public void Update(float deltaTime)
        {
            Time += deltaTime;
        }
    }
}