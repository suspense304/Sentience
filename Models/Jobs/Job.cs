using Microsoft.AspNetCore.Components;
using System.Timers;

namespace Sentience.Models.Jobs
{
    public class Job
    {
        public bool Active;
        public bool Unlocked;
        public float BaseIncome { get; set; }
        public int BaseXP { get; set; } = 120;
        public int NextLevel { get; set; } = 120;
        public float CurrentXP { get; set; } = 0;
        public float Income { get; set; } = 0;
        public int Level { get; set; } = 0;
        public float Multiplier { get; set; } = 1f;
        public int BaseCost { get; set; }
        public string Name { get; set; }

        public Job Create() 
        {
            return new Job();
        }

        public float XPRemaining(float current)
        {
            int value = (int)(NextLevel - current);
            return (value <= 0) ? 0 : value;
        }

        public void LevelUp(GameEngine engine)
        {
            Level++;
            NextLevel = GetNextUpdateAmount(NextLevel, engine);
            Income = UpdateIncome(engine);
            engine.SetDailyIncome(Income);
            CurrentXP = 0;
            engine.UnlockJobs();
        }
        public int GetNextUpdateAmount(int lastValue, GameEngine engine)
        {
            return (int)(Math.Floor(BaseXP * Math.Pow(engine.GetUpgradeMultiplier(), Level)));
        }

        public float UpdateIncome(GameEngine engine)
        {
            float newIncome = Income * engine.GetIncomeMultiplier();
            newIncome = (float)(Math.Ceiling(newIncome * 100) / 100);
            if (newIncome >= 100)
            {
                return (int)newIncome;
            }
            else
            {
                return newIncome;
            }
        }
    }
}
