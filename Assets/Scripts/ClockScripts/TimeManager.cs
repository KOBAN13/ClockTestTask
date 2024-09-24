﻿using Ui;
using UnityEngine;
using Zenject;

namespace Client
{
    public class TimeManager : ITickable
    {
        private float _currentTime;
        private ClockModel _clockModel;
        private int _hours;
        private int _minutes;
        private int _seconds;
        private ClockController _clockController;

        public TimeManager(ClockController clockController, ClockModel clockModel)
        {
            _clockController = clockController;
            _clockModel = clockModel;
        }
        
        public void Tick()
        {
            if(_clockController.ClockDigital == null || _clockController.ClockAnalog == null || _clockController.IsStopTimer) return;
            
            _clockController.ClockDigital.Update(Time.deltaTime);
            _clockController.ClockAnalog.UpdateTime(Time.deltaTime);
            _clockModel.SetTimeDigitalString(FormatTime(_clockController.ClockDigital.Time));
            _clockController.ClockAnalog.UpdateAngle();
            _clockModel.SetHoursAngle(_clockController.ClockAnalog.HourAngle);
            _clockModel.SetMinutesAngle(_clockController.ClockAnalog.MinuteAngle);
            _clockModel.SetSecondsAngle(_clockController.ClockAnalog.SecondAngle);
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