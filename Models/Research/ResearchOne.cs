using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Research
{
    public class ResearchOne : ResearchProject
    {
        public ResearchOne(GameEngine engine)
        {
            Name = "Input Response";
            Active = true;
            Unlocked = true;
            Modifier = Modifiers.JobXP;
            ResearchType = ResearchTypes.Fundamentals;
        }
    }
}
