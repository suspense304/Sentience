using Microsoft.AspNetCore.Components;
using Sentience.Models.Research;
using Sentience.Models.Upgrades;
using System.Timers;

namespace Sentience.Models.Jobs
{
    public abstract class Job
    {
        public bool Active;
        public bool Unlocked;
        public float BaseIncome { get; set; }
        public int BaseXP { get; set; }
        public bool GenerateIncome { get; set; } = false;
        public int NextLevel { get; set; }
        public float CurrentXP { get; set; } = 0;
        public float Income { get; set; } = 0;
        public int Level { get; set; } = 0;
        public int BaseCost { get; set; }
        public string Name { get; set; }
        public JobTypes JobType { get; set; }

        //public Job Create() 
        //{
        //    return new Job();
        //}
        public float XPRemaining(float current)
        {
            int value = (int)(NextLevel - current);
            return (value <= 0) ? 0 : value;
        }
        public void LevelUp(GameEngine engine)
        {
            Level++;
            NextLevel = GetNextUpdateAmount(NextLevel, engine);
            engine.SetIncomeMultiplier(this);
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
        public virtual string UpgradeMessage(GameEngine engine)
        {
            return "";
        }
    }
}
