using Sentience.Models.Research;

namespace Sentience.Models.Jobs
{
    public class JobFive: Job
    {
        public JobFive(GameEngine engine)
        {
            Name = "Tic-Tac-Toe";
            Active = false;
            Unlocked = false;
            BaseIncome = 24.28f;
            BaseXP = 61135;
            NextLevel = 61135;
            JobType = JobTypes.Basics;
        }
        public bool CanUnlock(GameEngine engine)
        {
            return (engine.JobFour.Level > 49 && engine.ResearchTwo.Level > 99) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.JobFour.Name + ": " + engine.JobFour.Level + "/50   " + engine.ResearchTwo.Name + ": " + engine.ResearchTwo.Level + "/100";
        }
    }
}
