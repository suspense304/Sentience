using Blazored.Toast.Services;

namespace Sentience.Models.Research
{
    public class ResearchThree: ResearchProject
    {
        public ResearchThree(GameEngine engine)
        {
            Name = "For Loops";
            Active = true;
            Unlocked = false;
            Modifier = Modifiers.ResearchSpeed;
            ResearchType = ResearchTypes.Fundamentals;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (engine.GameData.ResearchTwo.Level > 49)
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
            return engine.GameData.ResearchTwo.Name + ": " + engine.GameData.ResearchTwo.Level + "/50";
        }
    }
}
