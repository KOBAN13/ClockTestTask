using System;
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
        private ClockView _clockView;
        
        public ReactiveProperty<string> TimeToUi { get; private set; } = new();
        public ReactiveProperty<float> Hours { get; private set; } = new();
        public ReactiveProperty<float> Minutes { get; private set; } = new();
        public ReactiveProperty<float> Seconds { get; private set; } = new();
        private CompositeDisposable _compositeDisposable = new();

        public ClockPresenter(ClockView clockView, ClockModel clockModel)
        {
            _clockView = clockView;
            _clockModel = clockModel;
        }

        public void Initialize()
        {
            SubscribePresenterProperties();
            SubscribeModelProperties();
            SubscribeViewProperties();
        }

        private void SubscribeViewProperties()
        {
            _clockView.NewTimeDigitalInMilliseconds
                .Subscribe(time => _clockModel.ClockController.SetDigitalClock(new ClockDigital(time)))
                .AddTo(_compositeDisposable);
        }

        private void SubscribePresenterProperties()
        {
            
        }

        private void SubscribeModelProperties()
        {
            _clockModel.TimeToUi.SkipLatestValueOnSubscribe().Subscribe(text => _clockView.Time.text = text)
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
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}