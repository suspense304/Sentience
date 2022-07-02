using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeThree: Upgrade
    {
        public UpgradeThree(GameEngine engine)
        {
            Name = "90s GPU";
            Active = false;
            Expense = 7.5M;
            Unlocked = false;
            Modifier = Modifiers.GameSpeed;
            MoneyNeeded = 500;
            Multiplier = 1M;
            UpgradeType = UpgradeTypes.Scrapyard;
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
