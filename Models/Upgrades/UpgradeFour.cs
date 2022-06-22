namespace Sentience.Models.Upgrades
{
    public class UpgradeFour: Upgrade
    {
        public UpgradeFour(GameEngine engine)
        {
            Name = "Dusty Motherboard";
            Active = false;
            Expense = 45f;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            Multiplier = 4f;
            UpgradeType = UpgradeTypes.Scrapyard;
        }

        public bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GetMoney() >= 5000)
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
            return $"Money: " + engine.FormatNumber(engine.GetMoney()) + "/5000";
        }
    }
}
