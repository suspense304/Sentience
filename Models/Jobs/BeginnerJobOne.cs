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
            BaseIncome = 68.01M;
            BaseXP = 793007;
            NextLevel = 793007;
            JobType = JobTypes.Beginner;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.ResearchOne.Level > 149 && engine.GameData.ResearchTwo.Level > 149 && engine.GameData.ResearchThree.Level > 149)
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
            return engine.GameData.ResearchOne.Name + ": " + engine.GameData.ResearchThree.Level + "/150  " + engine.GameData.ResearchTwo.Name + ": " + engine.GameData.ResearchTwo.Level + "/150  " + engine.GameData.ResearchThree.Name + ": " + engine.GameData.ResearchThree.Level + "/150";
        }
    }
}
