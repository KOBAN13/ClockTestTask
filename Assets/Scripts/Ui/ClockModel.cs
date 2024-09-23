using UniRx;
using Zenject;

namespace Ui
{
    public class ClockModel : IInitializable
    {
        public ReactiveProperty<string> TimeToUi { get; private set; } = new();
        private ClockView _clockView;

        public ClockModel(ClockView clockView) => _clockView = clockView;
        
        
        public void Initialize()
        {
            SubscribeProperties();
        }

        private void SubscribeProperties()
        {
            TimeToUi.Subscribe(_ => _clockView.Time.text = TimeToUi.Value);
        }

        ~ClockModel()
        {
            TimeToUi.Dispose();
        }
    }
}