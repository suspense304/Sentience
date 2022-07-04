using Blazored.Toast.Services;

namespace Sentience.Models.Research
{
    public class NoviceResearchThree : ResearchProject
    {
        public NoviceResearchThree(GameEngine engine)
        {
            Name = "Classes and Interfaces";
            Active = true;
            Unlocked = false;
            Modifier = Modifiers.Income;
            ResearchType = ResearchTypes.Novice;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.NoviceResearchTwo.Level > 249 && engine.GameData.JobSix.Level > 49)
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
            return engine.GameData.NoviceResearchTwo.Name + ": " + engine.GameData.NoviceResearchTwo.Level + "/250   " + engine.GameData.JobSix.Name + ": " + engine.GameData.JobSix.Level + "/50";
        }
    }
}
