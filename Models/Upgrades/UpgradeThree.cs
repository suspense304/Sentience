namespace Sentience.Models.Upgrades
{
    public class UpgradeThree: Upgrade
    {
        public UpgradeThree(GameEngine engine)
        {
            Name = "90s GPU";
            Active = false;
            Expense = 28f;
            Unlocked = false;
            Modifier = Modifiers.GameSpeed;
            Multiplier = 0.25f;
            UpgradeType = UpgradeTypes.Scrapyard;
        }

        public bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GetMoney() >= 500)
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
            return $"Money: " + engine.FormatNumber(engine.GetMoney()) + "/500";
        }
    }
}
