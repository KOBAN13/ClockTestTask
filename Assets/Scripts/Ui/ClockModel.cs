using Client;
using Extensions;
using UniRx;

namespace Ui
{
    public class ClockModel
    {
        public ReactiveProperty<string> TimeToUi { get; private set; } = new();
        public ReactiveProperty<float> Hours { get; private set; } = new();
        public ReactiveProperty<float> Minutes { get; private set; } = new();
        public ReactiveProperty<float> Seconds { get; private set; } = new();
        public ClockController ClockController { get; private set; }
        
        private CompositeDisposable _compositeDisposable = new();
        public ClockModel(ClockController clockController) => ClockController = clockController;
        
        public void SetTimeString(string time)
        {
            Preconditions.CheckNotNull(time);
            TimeToUi.Value = time;
        }
        
        public void SetHoursAngle(float hours)
        {
            Preconditions.CheckValidateData(hours);
            Hours.Value = hours;
        }
        
        public void SetMinutesAngle(float minutes)
        {
            Preconditions.CheckValidateData(minutes);
            Minutes.Value = minutes;
        }
        
        public void SetSecondsAngle(float seconds)
        {
            Preconditions.CheckValidateData(seconds);
            Seconds.Value = seconds;
        }

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}