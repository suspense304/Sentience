using Blazored.Toast.Services;

namespace Sentience.Models.Research
{
    public class NoviceResearchOne: ResearchProject
    {
        public NoviceResearchOne(GameEngine engine)
        {
            Name = "Arrays and Lists";
            Active = true;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            ResearchType = ResearchTypes.Novice;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.ResearchTwo.Level > 49 && engine.GameData.JobFour.Level > 24)
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
            return engine.GameData.ResearchTwo.Name + ": " + engine.GameData.ResearchTwo.Level + "/50   " + engine.GameData.JobFour.Name + ": " + engine.GameData.JobFour.Level + "/25";
        }
    }
}
