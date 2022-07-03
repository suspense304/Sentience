using Blazored.Toast.Services;
using Sentience.Models.StoryElements;

namespace Sentience.Models.Hacks
{
    public class HackFour: Hack
    {
        public HackFour(GameEngine engine)
        {
            Name = "Hack Bitcoin";
            Active = false;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            Multiplier = 24M;
            CurrentXp = 0;
            XpNeeded = 500000000000;
            Message = "Look, we need money. These upgrades aren't going to pay for themselves. Let's just do a little tampering with the world's most popular crypto currency. Daddy needs a new pair of CPUs... or a few dozen.";
        }
        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GameData.HackingXp >= XpNeeded)
                {
                    Unlocked = true;
                    engine.ShowToast(this.Name + " has been unlocked!", "Hack Unlocked", ToastLevel.Success);
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
            return (!Unlocked) ? "Hacking Xp: " + engine.FormatNumber(engine.GameData.HackingXp) + "/" + engine.FormatNumber(XpNeeded) : "";
        }
    }
}
