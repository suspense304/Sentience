using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeTwo : Upgrade
    {
        public UpgradeTwo(GameEngine engine)
        {
            Name = "Ancient Processor";
            Active = false;
            Expense = 1.80M;
            Unlocked = false;
            Modifier = Modifiers.ResearchSpeed;
            MoneyNeeded = 50;
            Multiplier = 4M;
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

