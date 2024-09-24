using System;
using Extensions;
using UnityEngine;

namespace Client
{
    public class ClockAnalog
    {
        public float Time { get; private set; }
        public float HourAngle { get; private set; }
        public float MinuteAngle { get; private set; }
        public float SecondAngle { get; private set; }
        
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public ClockAnalog(float time)
        {
            Time = time;
        }
        
        public void UpdateAngle()
        {
            FormatTime(Time);
            HourAngle = (Hours % 12 + Minutes / 60f) * 360 / 12;
            MinuteAngle = (Minutes + Seconds / 60f) * 360 / 60;
            SecondAngle = Seconds * 360 / 60;
        }
        
        public void SetTime(float time)
        {
            Preconditions.CheckValidateData(time);
            Time = time;
        }

        public void UpdateTime(float timeDelta)
        {
            Time += timeDelta;
        }
        
        public void FormatTime(float time)
        {
            Hours = Mathf.FloorToInt(time / 3600);
            Minutes = Mathf.FloorToInt((time % 3600) / 60);
            Seconds = Mathf.FloorToInt(time % 60);
        }
    }
}