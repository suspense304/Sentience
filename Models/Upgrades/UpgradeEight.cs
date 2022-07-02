using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeEight: Upgrade
    {
        public UpgradeEight(GameEngine engine)
        {
            Name = "Zip Drive";
            Active = false;
            Expense = 250000M;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            MoneyNeeded = 500000000;
            Multiplier = 8M;
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
