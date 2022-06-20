using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Research
{
    public class InputResponse : ResearchProject
    {
        public InputResponse(GameEngine engine)
        {
            Name = "Input Response";
            Active = true;
            Unlocked = true;
            Modifier = Modifiers.JobXP;
        }

        public InputResponse()
        {
            Name = "Input Response";
            Active = true;
            Unlocked = true;
            Modifier = Modifiers.JobXP;
        }

        private string GetCustomUpgradeMessage(GameEngine engine)
        {
            return "Input Response Level: " + Level + "/10";
        }
        public override string GetUpgradeMessage(GameEngine engine)
        {
            return GetCustomUpgradeMessage(engine);
        }
    }
}
