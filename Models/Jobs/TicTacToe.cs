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
        }

        public TicTacToe()
        {
            Name = "Tic-Tac-Toe";
            Active = false;
            Unlocked = false;
            BaseIncome = .51f;
            BaseXP = 1382;
            NextLevel = 1382;
        }

        public bool CanUnlock(Job job)
        {
            return (job.Level > 9) ? true : false;
        }
    }
}
