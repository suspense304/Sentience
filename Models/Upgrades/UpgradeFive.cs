using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeFive: Upgrade
    {
        public UpgradeFive(GameEngine engine)
        {
            Name = "Pentium Processor";
            Active = false;
            Expense = 500M;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            MoneyNeeded = 249999;
            Multiplier = 8M;
            UpgradeType = UpgradeTypes.Refurbished;
        }

        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GetMoney() > MoneyNeeded)
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
