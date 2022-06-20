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
            BaseXP = 380;
            NextLevel = 380;
        }

        public TicTacToe()
        {
            Name = "Tic-Tac-Toe";
            Active = false;
            Unlocked = false;
            BaseIncome = .51f;
            BaseXP = 380;
            NextLevel = 380;
        }

        public bool CanUnlock(Job job)
        {
            return (job.Level > 9) ? true : false;
        }
    }
}
