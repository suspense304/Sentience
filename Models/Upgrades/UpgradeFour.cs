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
            MoneyNeeded = 2499;
            Multiplier = 4M;
            UpgradeType = UpgradeTypes.Scrapyard;
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
