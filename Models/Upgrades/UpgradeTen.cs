using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeTen: Upgrade
    {
        public UpgradeTen(GameEngine engine)
        {
            Name = "Duron processor";
            Active = false;
            Expense = 50000M;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            MoneyNeeded = 5000000000;
            Multiplier = 16M;
            UpgradeType = UpgradeTypes.Refurbished;
        }

        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GetMoney() >= MoneyNeeded)
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
    }
}
