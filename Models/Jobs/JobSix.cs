using Sentience.Models.Research;

namespace Sentience.Models.Jobs
{
    public class JobSix: Job
    {
        public JobSix(GameEngine engine)
        {
            Name = "Pong";
            Active = false;
            Unlocked = false;
            BaseIncome = 94.13f;
            BaseXP = 382354;
            NextLevel = 382354;
            JobType = JobTypes.Basics;
        }
        public bool CanUnlock(GameEngine engine)
        {
            return (engine.JobFive.Level > 99 && engine.ResearchTwo.Level > 199) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.JobFive.Name + ": " + engine.JobFive.Level + "/100   " + engine.ResearchTwo.Name + ": " + engine.ResearchTwo.Level + "/200";
        }
    }
}
