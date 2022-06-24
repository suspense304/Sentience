using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeTen: Upgrade
    {
        public UpgradeTen(GameEngine engine)
        {
            Name = "Duron processor";
            Active = false;
            Expense = 50000f;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            Multiplier = 16f;
            UpgradeType = UpgradeTypes.Refurbished;
        }

        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GetMoney() >= 1000000000)
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
            return (!Unlocked) ? $"Money: " + engine.FormatNumber(engine.GetMoney()) + "/1b" : "";
        }
    }
}
