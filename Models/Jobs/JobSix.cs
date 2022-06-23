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
            return (engine.GameData.JobFive.Level > 99 && engine.GameData.ResearchTwo.Level > 199) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.GameData.JobFive.Name + ": " + engine.GameData.JobFive.Level + "/100   " + engine.GameData.ResearchTwo.Name + ": " + engine.GameData.ResearchTwo.Level + "/200";
        }
    }
}
