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
            return (engine.GameData.ResearchOne.Level > 49 && engine.GameData.ResearchTwo.Level > 49 && engine.GameData.ResearchThree.Level > 49) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.GameData.ResearchOne.Name + ": " + engine.GameData.ResearchOne.Level + "/50  " + engine.GameData.ResearchTwo.Name + ": " + engine.GameData.ResearchTwo.Level + "/50  " + engine.GameData.ResearchThree.Name + ": " + engine.GameData.ResearchThree.Level + "/50";
        }
    }
}
