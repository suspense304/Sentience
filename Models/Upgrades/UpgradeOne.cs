using Blazored.Toast.Services;

namespace Sentience.Models.Upgrades
{
    public class UpgradeOne : Upgrade
    {
        public UpgradeOne(GameEngine engine)
        {
            Name = "Cheap VM";
            Active = false;
            Expense = 0.37M;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            Multiplier = 4M;
            UpgradeType = UpgradeTypes.Scrapyard;
        }
        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if(engine.GetMoney() >= 10)
                {
                    Unlocked = true;
                    engine.ShowToast(this.Name + " has been unlocked!", "Upgrade Unlocked", ToastLevel.Info);
                    engine.GetNextUpgrades();
                    return true;
                } else
                {
                    return false;
                }
            }
            return true;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return (!Unlocked) ? $"Money: " + engine.FormatNumber(engine.GetMoney()) + "/10" : "";
        }
    }
}
