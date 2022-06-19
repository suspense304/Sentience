using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Research
{
    public class ProcessingPower : Research
    {
        public ProcessingPower(GameEngine engine)
        {
            Name = "Processing Power";
            Active = false;
            Unlocked = true;
            Modifier = Modifiers.GlobalXP;
        }

        public ProcessingPower()
        {
            Name = "Processing Power";
            Active = false;
            Unlocked = true;
            Modifier = Modifiers.GlobalXP;
        }
    }
}
