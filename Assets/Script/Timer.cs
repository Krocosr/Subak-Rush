using System;

namespace originalTimer
{
    public class Timer
    {
        public bool isCounting;
        public float RemainingSeconds { get;  set; }
        public Timer(float duration)
        {
            RemainingSeconds = duration;
        }

        public void DecrementTimer(float deltaTime)
        {
            if (Math.Round(RemainingSeconds) == 0f)
            {
                isCounting = false;
                return;
            }
            RemainingSeconds -= deltaTime;
        }

        public void IncrementTimer(float deltaTime, int maxNumber)
        {
            if (Math.Round(RemainingSeconds) == maxNumber)
            {
                isCounting = false;
                return;
            }
            RemainingSeconds += deltaTime;
        }
    }
}