using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeNine : Upgrade
    {
        public UpgradeNine(GameEngine engine)
        {
            Name = "K7T Pro2";
            Active = false;
            Expense = 10000M;
            Unlocked = false;
            Modifier = Modifiers.ResearchSpeed;
            MoneyNeeded = 1000000000;
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
