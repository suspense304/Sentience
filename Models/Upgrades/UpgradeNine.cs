using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeNine : Upgrade
    {
        public UpgradeNine(GameEngine engine)
        {
            Name = "K7T Pro2";
            Active = false;
            Expense = 10000f;
            Unlocked = false;
            Modifier = Modifiers.ResearchSpeed;
            Multiplier = 16f;
            UpgradeType = UpgradeTypes.Refurbished;
        }

        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GetMoney() >= 500000000)
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
            return (!Unlocked) ? $"Money: " + engine.FormatNumber(engine.GetMoney()) + "/500m" : "";
        }
    }
}
