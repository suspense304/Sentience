using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience.Models.Jobs
{
    public class NeuralNetwork : Job
    {
        public NeuralNetwork(GameEngine engine)
        {
            Name = "Neural Network";
            Active = false;
            Unlocked = false;
            BaseIncome = .45f;
        }

        public NeuralNetwork()
        {
            Name = "Neural Network";
            Active = false;
            Unlocked = false;
            BaseIncome = .45f;
        }

        public bool CanUnlock(Job job)
        {
            return (job.Level > 4) ? true : false;
        }
    }
}
