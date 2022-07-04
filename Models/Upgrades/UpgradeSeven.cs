using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeSeven: Upgrade
    {
        public UpgradeSeven(GameEngine engine)
        {
            Name = "Zip Drive";
            Active = false;
            Expense = 10000M;
            Unlocked = false;
            Modifier = Modifiers.GameSpeed;
            MoneyNeeded = 10000000;
            Multiplier = 4M;
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
