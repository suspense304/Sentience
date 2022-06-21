namespace Sentience.Models.Upgrades
{
    public class UpgradeTwo : Upgrade
    {
        public UpgradeTwo(GameEngine engine)
        {
            Name = "Ancient Processor";
            Active = false;
            Expense = 0.35f;
            Unlocked = false;
            Modifier = Modifiers.ResearchSpeed;
            Multiplier = 4f;
            UpgradeType = UpgradeTypes.Scrapyard;
        }

        public bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GetMoney() > 100)
                {
                    Unlocked = true;
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
            return "Money: " + engine.FormatNumber(engine.GetMoney()) + "/100";
        }
    }
}

