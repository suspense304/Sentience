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
            Active = true;
            Unlocked = true;
            BaseIncome = 0.04f;
            BaseXP = 120;
            NextLevel = 120;
            JobType = JobTypes.Basics;
        }

        public ChatBot()
        {
            Name = "Chat Bot";
            Active = true;
            Unlocked = true;
            BaseIncome = 0.04f;
            BaseXP = 120;
            NextLevel = 120;
            JobType = JobTypes.Basics;
        }

        private string GetCustomUpgradeMessage(GameEngine engine)
        {
            return "Chat Bot Level: " + Level + "/10";
        }
        public override string GetUpgradeMessage(GameEngine engine)
        {
            return GetCustomUpgradeMessage(engine);
        }
    }
}
