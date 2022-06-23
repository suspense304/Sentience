namespace Sentience.Models.Upgrades
{
    public class Upgrade
    {
        public bool Active;
        public bool Unlocked;
        public float Multiplier { get; set; }
        public float Expense { get; set; }
        public Modifiers Modifier { get; set; }
        public string Name { get; set; } = "";
        public UpgradeTypes UpgradeType { get; set; }
        public string GetModifierAmount(GameEngine engine)
        {
            return "x" + engine.FormatNumber(Multiplier);
        }
        public virtual string UpgradeMessage(GameEngine engine)
        {
            return "";
        }
        public virtual bool CanUnlock(GameEngine engine)
        {
            return Unlocked;
        }
    }
    
}
