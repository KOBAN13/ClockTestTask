using System;
using Client;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    public class ClockView : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI Time { get; private set; }
        [field: SerializeField] public TMP_InputField TimeDigital { get; private set; }
        [field: SerializeField] public Button ButtonChangeTime { get; private set; }
        [field: SerializeField] public Button ButtonOk { get; private set; }
        [field: SerializeField] public Canvas ChangeTimeCanvas { get; private set; }
        [field: SerializeField] public RectTransform Hours { get; private set; }
        [field: SerializeField] public RectTransform Minutes { get; private set; }
        [field: SerializeField] public RectTransform Seconds { get; private set; }
        [field: SerializeField] public ClockHandController MinutesChanged { get; private set; }
        [field: SerializeField] public ClockHandController HoursChanged { get; private set; }

        public ReactiveProperty<float> NewTimeDigitalInMilliseconds { get; private set; } = new();
        public ClockController ClockController { get; private set; }

        private CompositeDisposable _compositeDisposable = new();

        [Inject]
        private void Construct(ClockController clockController) => ClockController = clockController;

        private void OnEnable()
        {
            SubscribeButtons();
            SetActiveScripts(false);
        }

        private void OnDestroy()
        {
            UnsubscribeButtons();
        }

        private void SubscribeButtons()
        {
            ButtonChangeTime.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    ClockController.IsStopTimer = true;
                    SetActiveCanvases(true);
                    SetActiveScripts(true);
                })
                .AddTo(_compositeDisposable);

            ButtonOk.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    ClockController.IsStopTimer = false;
                    SetActiveCanvases(false);
                    SetActiveScripts(false);
                    SetNewDigitalTime();
                })
                .AddTo(_compositeDisposable);
        }

        private void UnsubscribeButtons()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
        
        private void SetActiveCanvases(bool isActive)
        {
            ChangeTimeCanvas.enabled = isActive;
        }

        private void SetActiveScripts(bool isActive)
        {
            MinutesChanged.enabled = isActive;
            HoursChanged.enabled = isActive;
        }

        private void SetNewDigitalTime()
        {
            var inputTime = TimeDigital.text;

            if (TryParseTime(inputTime, out int hours, out int minutes, out int seconds))
            {
                NewTimeDigitalInMilliseconds.Value = hours * 3600 + minutes * 60 + seconds;
            }
        }

        private bool TryParseTime(string time, out int hours, out int minutes, out int seconds)
        {
            hours = minutes = seconds = 0;
            var timeSplit = time.Split(':');

            if (timeSplit.Length != 3) return false;

            if (int.TryParse(timeSplit[0], out hours) && int.TryParse(timeSplit[1], out minutes) &&
                int.TryParse(timeSplit[2], out minutes))
            {
                if (hours is >= 0 and < 24 && minutes is >= 0 and < 60 && seconds is >= 0 and < 60)
                {
                    return true;
                }
            }

            return false;
        }
    }
}