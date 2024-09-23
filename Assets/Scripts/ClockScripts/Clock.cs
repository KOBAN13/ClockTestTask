using System;
using UnityEngine;

namespace Client
{
    [Serializable]
    public class Clock
    {
        private float _time;
        public string TimeFormated => FormatTime(_time);
        public Clock(float time)
        {
            _time = time;
        }

        public void Update(float deltaTime)
        {
            _time += deltaTime;
        }
        
        private string FormatTime(float time)
        {
            var hours = Mathf.FloorToInt(time / 3600);
            var minutes = Mathf.FloorToInt((time % 3600) / 60);
            var seconds = Mathf.FloorToInt(time % 60);

            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }
    }
}