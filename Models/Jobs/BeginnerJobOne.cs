using Blazored.Toast.Services;

namespace Sentience.Models.Jobs
{
    public class BeginnerJobOne : Job
    {
        public BeginnerJobOne(GameEngine engine)
        {
            Name = "Cookie Clicker";
            Active = false;
            Unlocked = false;
            BaseIncome = 250M;
            BaseXP = 7930070;
            NextLevel = 7930070;
            JobType = JobTypes.Beginner;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.ResearchOne.Level > 199 && engine.GameData.ResearchTwo.Level > 199 && engine.GameData.ResearchThree.Level > 199)
            {
                if (!Unlocked)
                {
                    Unlocked = true;
                    engine.ShowToast(this.Name + " has been unlocked!", "Job Unlocked", ToastLevel.Success);
                }
                return true;
            }
            return false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.GameData.ResearchOne.Name + ": " + engine.GameData.ResearchThree.Level + "/200  " + engine.GameData.ResearchTwo.Name + ": " + engine.GameData.ResearchTwo.Level + "/200  " + engine.GameData.ResearchThree.Name + ": " + engine.GameData.ResearchThree.Level + "/200";
        }
    }
}
