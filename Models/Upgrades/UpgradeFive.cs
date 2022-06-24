using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeFive: Upgrade
    {
        public UpgradeFive(GameEngine engine)
        {
            Name = "Pentium Processor";
            Active = false;
            Expense = 500f;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            Multiplier = 8f;
            UpgradeType = UpgradeTypes.Refurbished;
        }

        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GetMoney() > 249999)
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
            return (!Unlocked) ? $"Money: " + engine.FormatNumber(engine.GetMoney()) + "/250k" : "";
        }
    }
}
