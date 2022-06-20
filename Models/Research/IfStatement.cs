using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Research
{
    public class IfStatement : ResearchProject
    {
        public IfStatement(GameEngine engine)
        {
            Name = "If Statements";
            Active = false;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
        }

        public IfStatement()
        {
            Name = "If Statements";
            Active = false;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
        }

        public bool CanUnlock(ResearchProject research)
        {
            return (research.Level > 9) ? true : false;
        }
    }
}
