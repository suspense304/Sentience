using Microsoft.AspNetCore.Components;
using Sentience.Models.Research;
using Sentience.Models.Upgrades;
using System.Timers;
using Blazored.Toast.Services;
using DecimalMath;

namespace Sentience.Models.Jobs
{
    public class Job
    {
        public bool Active;
        public bool Unlocked;
        public decimal BaseIncome { get; set; }
        public decimal BaseXP { get; set; }
        public bool GenerateIncome { get; set; } = false;
        public decimal NextLevel { get; set; }
        public decimal CurrentXP { get; set; } = 0;
        public decimal Income { get; set; } = 0;
        public int Level { get; set; } = 0;
        public int BaseCost { get; set; }
        public string Name { get; set; }
        public JobTypes JobType { get; set; }
        public decimal XPRemaining(decimal current)
        {
            decimal value = NextLevel - current;
            return (value <= 0) ? 0 : value;
        }
        public void LevelUp(GameEngine engine)
        {
            Level++;
            Income = UpdateIncome(engine);
            NextLevel = GetNextUpdateAmount(NextLevel, engine);
            engine.GetIncomeMultiplier();
            engine.SetDailyIncome(Income);
            CurrentXP = 0;
            engine.UnlockJobs();
        }
        public decimal GetNextUpdateAmount(decimal lastValue, GameEngine engine)
        {
            return (decimal)(Math.Floor(BaseXP * DecimalEx.Pow(engine.GetUpgradeMultiplier(), Level)));
        }
        public decimal UpdateIncome(GameEngine engine)
        {
            decimal newIncome = Income * (decimal )engine.GetIncomeMultiplier();
            newIncome = (decimal)(Math.Ceiling(newIncome * 100) / 100);
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
