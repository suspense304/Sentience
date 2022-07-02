using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeSix: Upgrade
    {
        public UpgradeSix(GameEngine engine)
        {
            Name = "3dfx Voodoo2";
            Active = false;
            Expense = 2000M;
            Unlocked = false;
            Modifier = Modifiers.ResearchSpeed;
            MoneyNeeded = 2000000;
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
