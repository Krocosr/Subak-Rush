using System;

namespace TimerSystem
{
    public class Cooldown
    {
        public float RemainingSeconds { get; private set;}

        public Cooldown(float duration)
        {
            RemainingSeconds = duration;
        }

        public event Action onCooldownEnd;

        public void Tick(float deltaTime)
        {
             if (RemainingSeconds == 0) { return; }

            RemainingSeconds -= deltaTime;

            CheckForTimerEnd();
        }

        private void CheckForTimerEnd()
        {
            if (RemainingSeconds > 0f) { return; }

            RemainingSeconds = 0f;

            onCooldownEnd?.Invoke();
        }
    }
}