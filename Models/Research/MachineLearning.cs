using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Research
{
    public class MachineLearning : Research
    {
        public MachineLearning(GameEngine engine)
        {
            Name = "Machine Learning";
            Active = false;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
        }

        public MachineLearning()
        {
            Name = "Machine Learning";
            Active = false;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
        }

        public bool CanUnlock(Research research)
        {
            return (research.Level > 9) ? true : false;
        }
    }
}
