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

        public TimeManager(SyncClockTime time, ClockModel clockModel)
        {
            _time = time;
            _clockModel = clockModel;
        }
        
        public void Tick()
        {
            if(_time.Clock == null) return;
            
            _time.Clock.Update(Time.deltaTime);
            _clockModel.TimeToUi.Value = _time.Clock.TimeFormated;
        }
    }
}