using Blazored.Toast.Services;

namespace Sentience.Models.Research
{
    public class NoviceResearchTwo: ResearchProject
    {
        public NoviceResearchTwo(GameEngine engine)
        {
            Name = "Functions";
            Active = true;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            ResearchType = ResearchTypes.Novice;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.NoviceResearchOne.Level > 24 && engine.GameData.JobFive.Level > 49)
            {
                if (!Unlocked)
                {
                    Unlocked = true;
                    engine.ShowToast(this.Name + " has been unlocked!", "Research Unlocked", ToastLevel.Warning);
                }
                return true;
            }
            return false;
        }

        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.GameData.NoviceResearchOne.Name + ": " + engine.GameData.NoviceResearchOne.Level + "/100   " + engine.GameData.JobFive.Name + ": " + engine.GameData.JobFive.Level + "/50";
        }
    }
}
