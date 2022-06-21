using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Research
{
    public class ResearchTwo : ResearchProject
    {
        public ResearchTwo(GameEngine engine)
        {
            Name = "If Statements";
            Active = false;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            ResearchType = ResearchTypes.Fundamentals;
        }

        public ResearchTwo()
        {
            Name = "If Statements";
            Active = false;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            ResearchType = ResearchTypes.Fundamentals;
        }

        public bool CanUnlock(GameEngine engine)
        {
            return (engine.ResearchOne.Level > 9) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.ResearchOne.Name + ": " + engine.ResearchOne.Level + "/10";
        }
    }
}
