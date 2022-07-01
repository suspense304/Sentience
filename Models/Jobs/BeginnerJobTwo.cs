using Blazored.Toast.Services;

namespace Sentience.Models.Jobs
{
    public class BeginnerJobTwo: Job
    {
        public BeginnerJobTwo(GameEngine engine)
        {
            Name = "Chess";
            Active = false;
            Unlocked = false;
            BaseIncome = 1326M;
            BaseXP = 30686890;
            NextLevel = 30686890;
            JobType = JobTypes.Beginner;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.BeginnerJobOne.Level > 24)
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
            return engine.GameData.BeginnerJobOne.Name + ": " + engine.GameData.BeginnerJobOne.Level + "/25";
        }
    }
}
