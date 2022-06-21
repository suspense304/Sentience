namespace Sentience.Models.Upgrades
{
    public abstract class Upgrade
    {
        public bool Active;
        public bool Unlocked;
        public float Multiplier { get; set; }
        public float Expense { get; set; }
        public Modifiers Modifier { get; set; }
        public string Name { get; set; } = "";
        public UpgradeTypes UpgradeType { get; set; }
        public virtual string UpgradeMessage(GameEngine engine)
        {
            return "";
        }
    }
    
}
