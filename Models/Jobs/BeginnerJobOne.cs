namespace Sentience.Models.Jobs
{
    public class BeginnerJobOne : Job
    {
        public BeginnerJobOne(GameEngine engine)
        {
            Name = "Cookie Clicker";
            Active = false;
            Unlocked = false;
            BaseIncome = 1.73f;
            BaseXP = 7649;
            NextLevel = 7649;
            JobType = JobTypes.Beginner;
        }
        public bool CanUnlock(GameEngine engine)
        {
            return (engine.ResearchOne.Level > 49 && engine.ResearchTwo.Level > 49 && engine.ResearchThree.Level > 49) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.ResearchOne.Name + ": " + engine.ResearchOne.Level + "/50  " + engine.ResearchTwo.Name + ": " + engine.ResearchTwo.Level + "/50  " + engine.ResearchThree.Name + ": " + engine.ResearchThree.Level + "/50";
        }
    }
}
