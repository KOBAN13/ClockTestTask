using System;
using Ui;
using UnityEngine;
using Zenject;

namespace Client
{
    public class TimeManager : ITickable
    {
        private SyncClockTime _time;
        private float _currentTime;
        private ClockModel _clockModel;
        private int _hours;
        private int _minutes;
        private int _seconds;

        public TimeManager(SyncClockTime time, ClockModel clockModel)
        {
            _time = time;
            _clockModel = clockModel;
        }
        
        public void Tick()
        {
            if(_time.ClockDigital == null) return;
            
            _time.ClockDigital.Update(Time.deltaTime);
            _clockModel.TimeToUi.Value = FormatTime(_time.ClockDigital.Time);
            _time.ClockAnalog.Update(_hours, _minutes, _seconds);
            _clockModel.Hours.Value = _time.ClockAnalog.HourAngle;
            _clockModel.Minutes.Value = _time.ClockAnalog.MinuteAngle;
            _clockModel.Seconds.Value = _time.ClockAnalog.SecondAngle;
        }
        
        private string FormatTime(float time)
        {
            _hours = Mathf.FloorToInt(time / 3600);
            _minutes = Mathf.FloorToInt((time % 3600) / 60);
            _seconds = Mathf.FloorToInt(time % 60);

            return $"{_hours:00}:{_minutes:00}:{_seconds:00}";
        }
    }
}