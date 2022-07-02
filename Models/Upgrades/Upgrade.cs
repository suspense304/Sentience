namespace Sentience.Models.Upgrades
{
    public class Upgrade
    {
        public bool Active;
        public bool Unlocked;
        public decimal Multiplier { get; set; }
        public decimal Expense { get; set; }
        public Modifiers Modifier { get; set; }
        public string Name { get; set; } = "";
        public UpgradeTypes UpgradeType { get; set; }
        public decimal MoneyNeeded { get; set; }
        public string GetModifierAmount(GameEngine engine)
        {
            return "+ x" + engine.FormatNumber(Multiplier);
        }
        public virtual string UpgradeMessage(GameEngine engine)
        {
            return (!Unlocked) ? $"Money: $" + engine.FormatNumber(engine.GetMoney()) + "/" + engine.FormatNumber(MoneyNeeded) : "";
        }
        public virtual bool CanUnlock(GameEngine engine)
        {
            return Unlocked;
        }

    }

}
