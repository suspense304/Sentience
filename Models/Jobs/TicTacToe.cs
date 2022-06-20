using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Jobs
{
    public class TicTacToe : Job
    {
        public TicTacToe(GameEngine engine)
        {
            Name = "Tic-Tac-Toe";
            Active = false;
            Unlocked = false;
            BaseIncome = .51f;
            BaseXP = 1382;
            NextLevel = 1382;
            JobType = JobTypes.Basics;
        }

        public TicTacToe()
        {
            Name = "Tic-Tac-Toe";
            Active = false;
            Unlocked = false;
            BaseIncome = .51f;
            BaseXP = 1382;
            NextLevel = 1382;
            JobType = JobTypes.Basics;
        }

        public bool CanUnlock(Job job)
        {
            return (job.Level > 9) ? true : false;
        }

        private string GetCustomUpgradeMessage(GameEngine engine)
        {
            return "Tic-Tac-Toe Level: " + Level + "/10";
        }
        public override string GetUpgradeMessage(GameEngine engine)
        {
            return GetCustomUpgradeMessage(engine);
        }
    }
}
