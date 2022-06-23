using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeThree: Upgrade
    {
        public UpgradeThree(GameEngine engine)
        {
            Name = "90s GPU";
            Active = false;
            Expense = 7.5f;
            Unlocked = false;
            Modifier = Modifiers.GameSpeed;
            Multiplier = 0.25f;
            UpgradeType = UpgradeTypes.Scrapyard;
        }

        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GetMoney() >= 500)
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
            return (!Unlocked) ? $"Money: " + engine.FormatNumber(engine.GetMoney()) + "/500" : "";
        }
    }
}
