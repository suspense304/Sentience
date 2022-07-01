using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeFour: Upgrade
    {
        public UpgradeFour(GameEngine engine)
        {
            Name = "Dusty Motherboard";
            Active = false;
            Expense = 45M;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            Multiplier = 4M;
            UpgradeType = UpgradeTypes.Scrapyard;
        }

        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GetMoney() > 2499)
                {
                    Unlocked = true;
                    engine.ShowToast(this.Name + " has been unlocked!", "Upgrade Unlocked", ToastLevel.Info);
                    engine.GetNextUpgrades();
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
            return (!Unlocked) ? $"Money: " + engine.FormatNumber(engine.GetMoney()) + "/2500" : "";
        }
    }
}
