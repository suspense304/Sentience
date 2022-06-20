using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Jobs
{
    public class ChatBot : Job
    {
        public ChatBot(GameEngine engine)
        {
            Name = "Chat Bot";
            Active = false;
            Unlocked = true;
            BaseIncome = 0.04f;
            BaseXP = 120;
            NextLevel = 120;
        }

        public ChatBot()
        {
            Name = "Chat Bot";
            Active = false;
            Unlocked = true;
            BaseIncome = 0.04f;
            BaseXP = 120;
            NextLevel = 120;
        }
    }
}
