using System;
using Clear;
using Client;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class ClockPresenter : IInitializable, IDisposable
    {
        private ClockModel _clockModel;
        private readonly Dispose _disposable;
        private ClockView _clockView;
        
        private CompositeDisposable _compositeDisposable = new();

        public ClockPresenter(ClockView clockView, ClockModel clockModel, Dispose disposable)
        {
            _clockView = clockView;
            _clockModel = clockModel;
            _disposable = disposable;
        }

        public void Initialize()
        {
            _disposable.Add(this);
            SubscribeModelProperties();
            SubscribeViewProperties();
        }

        private void SubscribeViewProperties()
        {
            _clockView.NewTimeDigitalInMilliseconds.SkipLatestValueOnSubscribe()
                .Subscribe(time => _clockView.ClockController.ClockDigital.SetTime(time))
                .AddTo(_compositeDisposable);

            _clockView.MinutesChanged.IsEndDrag += SetAnalogMinutesClock;
            _clockView.HoursChanged.IsEndDrag += SetAnalogHoursClock;
        }

        private void SetAnalogMinutesClock(Transform transform)
        {
            var currentMinutes = GetCurrentMinutes(transform);
            _clockModel.ClockController.ClockAnalog
                .SetTime(_clockModel.ClockController.ClockAnalog.Seconds + currentMinutes * 60f + _clockModel.ClockController.ClockAnalog.Hours * 3600f);
        }

        private void SetAnalogHoursClock(Transform transform)
        {
            var currentHours = GetCurrentHours(transform);
            _clockModel.ClockController.ClockAnalog
                .SetTime(_clockModel.ClockController.ClockAnalog.Seconds + _clockModel.ClockController.ClockAnalog.Minutes * 60f + currentHours * 3600f);
        }


        private void SubscribeModelProperties()
        {
            _clockModel.TimeToUiDigital.SkipLatestValueOnSubscribe().Subscribe(text => _clockView.Time.text = text)
                .AddTo(_compositeDisposable);
            _clockModel.Hours.SkipLatestValueOnSubscribe().Subscribe(angle => _clockView.Hours.DORotateQuaternion(Quaternion.Euler(0f, 0f,-angle), 0.1f))
                .AddTo(_compositeDisposable);
            _clockModel.Minutes.SkipLatestValueOnSubscribe().Subscribe(angle => _clockView.Minutes.DORotateQuaternion(Quaternion.Euler(0f, 0f,-angle), 0.1f))
                .AddTo(_compositeDisposable);
            _clockModel.Seconds.SkipLatestValueOnSubscribe().Subscribe(angle => _clockView.Seconds.DORotateQuaternion(Quaternion.Euler(0f, 0f,-angle), 0.1f))
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Clear();
            _compositeDisposable?.Dispose();
            _clockModel.CompositeDisposable?.Clear();
            _clockModel.CompositeDisposable?.Dispose();
        }
        
        private int GetCurrentMinutes(Transform minutesTransform)
        {
            var localRotationZ = -minutesTransform.localEulerAngles.z;
            var normalizedAngle = (localRotationZ + 360) % 360;
            var minutes = Mathf.FloorToInt(normalizedAngle / 6);

            return minutes;
        }
        
        private int GetCurrentHours(Transform hoursTransform)
        {
            var localRotationZ = -hoursTransform.localEulerAngles.z;
            var normalizedAngle = (localRotationZ + 360) % 360;
            var hours = Mathf.FloorToInt(normalizedAngle / 30);
            return hours % 12;
        }
    }
}