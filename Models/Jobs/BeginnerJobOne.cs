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

        public BeginnerJobOne()
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
            return (engine.ResearchOne.Level > 99 && engine.ResearchTwo.Level > 99 && engine.ResearchThree.Level > 99) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.ResearchOne.Name + ": " + engine.ResearchOne.Level + "/100  " + engine.ResearchTwo.Name + ": " + engine.ResearchTwo.Level + "/100  " + engine.ResearchThree.Name + ": " + engine.ResearchThree.Level + "/100";
        }
    }
}
