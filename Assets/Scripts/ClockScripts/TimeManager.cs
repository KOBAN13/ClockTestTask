using System;
using Clear;
using Ui;
using UniRx;
using UnityEngine;
using Zenject;

namespace Client
{
    public class TimeManager : IInitializable, IDisposable, ITickable
    {
        private float _currentTime;
        private ClockModel _clockModel;
        private readonly Dispose _dispose;
        private int _hours;
        private int _minutes;
        private int _seconds;
        private ClockController _clockController;
        private IDisposable _disposable;

        public TimeManager(ClockController clockController, ClockModel clockModel, Dispose dispose)
        {
            _clockController = clockController;
            _clockModel = clockModel;
            _dispose = dispose;
            _dispose.Add(this);
        }
        
        public void Tick()
        {
            if(_clockController.ClockDigital == null || _clockController.ClockAnalog == null || _clockController.IsStopTimer) return;
            
            _clockController.ClockDigital.Update(Time.deltaTime);
            _clockController.ClockAnalog.UpdateTime(Time.deltaTime);
        }

        public void Initialize()
        {
            _disposable = Observable
                .Timer(TimeSpan.FromSeconds(0.9f), TimeSpan.FromSeconds(0.9f))
                .Subscribe(_ => UpdateAngle());
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        private void UpdateAngle()
        {
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