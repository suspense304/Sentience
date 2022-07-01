using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeEight: Upgrade
    {
        public UpgradeEight(GameEngine engine)
        {
            Name = "Zip Drive";
            Active = false;
            Expense = 2000M;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            Multiplier = 8M;
            UpgradeType = UpgradeTypes.Refurbished;
        }

        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GetMoney() >= 100000000)
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
            return (!Unlocked) ? $"Money: " + engine.FormatNumber(engine.GetMoney()) + "/100m" : "";
        }
    }
}
