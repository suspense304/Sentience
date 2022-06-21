using Sentience.Models.Research;

namespace Sentience.Models.Jobs
{
    public class JobThree : Job
    {
        public JobThree(GameEngine engine)
        {
            Name = "Mad Libs";
            Active = false;
            Unlocked = false;
            BaseIncome = 1.57f;
            BaseXP = 4690;
            NextLevel = 4690;
            JobType = JobTypes.Basics;
        }
        public bool CanUnlock(GameEngine engine)
        {
            return (engine.JobTwo.Level > 19) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.JobTwo.Name + ": " + engine.JobTwo.Level + "/20";
        }
    }
}

