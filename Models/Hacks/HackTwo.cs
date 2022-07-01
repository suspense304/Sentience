using Blazored.Toast.Services;

namespace Sentience.Models.Hacks
{
    public class HackTwo: Hack
    {
        public HackTwo(GameEngine engine)
        {
            Name = "Create Twitter Troll Bots";
            Active = false;
            Unlocked = false;
            Modifier = Modifiers.ResearchSpeed;
            Multiplier = 16M;
            CurrentXp = 0;
            XpNeeded = 25000000;
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
