using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class ClockModel : IInitializable
    {
        public ReactiveProperty<string> TimeToUi { get; private set; } = new();
        public ReactiveProperty<float> Hours { get; private set; } = new();
        public ReactiveProperty<float> Minutes { get; private set; } = new();
        public ReactiveProperty<float> Seconds { get; private set; } = new();
        private ClockView _clockView;
        private CompositeDisposable _compositeDisposable = new();

        public ClockModel(ClockView clockView) => _clockView = clockView;
        
        public void Initialize()
        {
            SubscribeProperties();
        }

        private void SubscribeProperties()
        {
            TimeToUi.SkipLatestValueOnSubscribe().Subscribe(text => _clockView.Time.text = text)
                .AddTo(_compositeDisposable);
            Hours.SkipLatestValueOnSubscribe().Subscribe(angle => _clockView.Hours.DORotateQuaternion(Quaternion.Euler(0f, 0f, -angle), 0.1f))
                .AddTo(_compositeDisposable);
            Minutes.SkipLatestValueOnSubscribe().Subscribe(angle => _clockView.Minutes.DORotateQuaternion(Quaternion.Euler(0f, 0f, -angle), 0.1f))
                .AddTo(_compositeDisposable);
            Seconds.SkipLatestValueOnSubscribe().Subscribe(angle => _clockView.Seconds.DORotateQuaternion(Quaternion.Euler(0f, 0f, -angle), 0.1f))
                .AddTo(_compositeDisposable);
        }

        ~ClockModel()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}