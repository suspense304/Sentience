using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Research
{
    public class IfStatement : Research
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

        public bool CanUnlock(Research research)
        {
            return (research.Level > 9) ? true : false;
        }
    }
}
