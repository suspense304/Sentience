using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Research
{
    public class InputResponse : Research
    {
        public InputResponse(GameEngine engine)
        {
            Name = "Input Response";
            Active = false;
            Unlocked = true;
            Modifier = Modifiers.JobXP;
        }

        public InputResponse()
        {
            Name = "Input Response";
            Active = false;
            Unlocked = true;
            Modifier = Modifiers.JobXP;
        }
    }
}
