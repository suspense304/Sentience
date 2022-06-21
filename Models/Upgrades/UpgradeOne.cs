namespace Sentience.Models.Upgrades
{
    public class UpgradeOne : Upgrade
    {
        public UpgradeOne(GameEngine engine)
        {
            Name = "Cheap VM";
            Active = false;
            Expense = 0.25f;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            Multiplier = 2f;
            UpgradeType = UpgradeTypes.Scrapyard;
        }
        public bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if(engine.GetMoney() > 25)
                {
                    Unlocked = true;
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
            return "Money: " + engine.FormatNumber(engine.GetMoney()) + "/25";
        }
    }
}
