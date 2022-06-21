using Sentience.Models.Research;

namespace Sentience.Models.Jobs
{
    public class JobFour : Job
    {
        public JobFour(GameEngine engine)
        {
            Name = "Random Number Generator";
            Active = false;
            Unlocked = false;
            BaseIncome = 24.39f;
            BaseXP = 183450;
            NextLevel = 183450;
            JobType = JobTypes.Basics;
        }
        public bool CanUnlock(GameEngine engine)
        {
            return (engine.JobThree.Level > 19 && engine.ResearchOne.Level > 49) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.JobThree.Name + ": " + engine.JobThree.Level + "/20   " + engine.ResearchOne.Name + ": " + engine.ResearchOne.Level + "/50";
        }
    }
}
