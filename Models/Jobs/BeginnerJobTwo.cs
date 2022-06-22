namespace Sentience.Models.Jobs
{
    public class BeginnerJobTwo: Job
    {
        public BeginnerJobTwo(GameEngine engine)
        {
            Name = "Chess";
            Active = false;
            Unlocked = false;
            BaseIncome = 1.73f;
            BaseXP = 7649;
            NextLevel = 7649;
            JobType = JobTypes.Beginner;
        }
        public bool CanUnlock(GameEngine engine)
        {
            return (engine.BeginnerJobOne.Level > 9) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.BeginnerJobOne.Name + ": " + engine.BeginnerJobOne.Level + "/10";
        }
    }
}
