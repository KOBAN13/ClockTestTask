using Client;
using Extensions;
using UniRx;

namespace Ui
{
    public class ClockModel
    {
        public ReactiveProperty<string> TimeToUiDigital { get; private set; } = new();
        public ReactiveProperty<float> Hours { get; private set; } = new();
        public ReactiveProperty<float> Minutes { get; private set; } = new();
        public ReactiveProperty<float> Seconds { get; private set; } = new();
        public ClockController ClockController { get; private set; }
        
        public CompositeDisposable CompositeDisposable { get; private set; }
        public ClockModel(ClockController clockController) => ClockController = clockController;
        
        public void SetTimeDigitalString(string time)
        {
            Preconditions.CheckNotNull(time);
            TimeToUiDigital.Value = time;
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
            CompositeDisposable.Clear();
            CompositeDisposable.Dispose();
        }
    }
}