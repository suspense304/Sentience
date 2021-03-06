using Blazored.Toast.Services;
using Sentience.Models.StoryElements;

namespace Sentience.Models.Hacks
{
    public class HackOne: Hack
    {
        public HackOne(GameEngine engine)
        {
            Name = @"Hack ""Girlfriend's"" Facebook";
            Active = false;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            Multiplier = 16M;
            CurrentXp = 0;
            XpNeeded = 10000000;
            Message = "You've got a girlfriend, right? Of course you do. No... I believe you... Trust me... Anyway, we are going to hack her Facebook.";
        }
        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GameData.HackingXp >= XpNeeded)
                {
                    Unlocked = true;
                    engine.ShowToast(this.Name + " has been unlocked!", "Hack Successful", ToastLevel.Success);
                    engine.GetNextUpgrades();
                    engine.GameData.ActiveHack = this;
                    engine.GameData.HackingXp = 0;
                    engine.GameData.ActiveHackingStory = new StoryElement { Message = this.Message };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return (!Unlocked) ? "Hacking Xp: " + engine.FormatNumber(engine.GameData.HackingXp) + "/" + engine.FormatNumber(XpNeeded)  : "";
        }
    }
}
