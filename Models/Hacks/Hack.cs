namespace Sentience.Models.Hacks
{
    public class Hack
    {
        public bool Active = false;
        public bool Unlocked = false;
        public decimal  Multiplier { get; set; }
        public Modifiers Modifier { get; set; }
        public decimal CurrentXp { get; set; }
        public decimal XpNeeded { get; set; }
        public string Name { get; set; } = "";

        public decimal XPRemaining(decimal current)
        {
            decimal value = XpNeeded - current;
            return (value <= 0) ? 0 : value;
        }
        public string GetModifierAmount(GameEngine engine)
        {
            return "+ x" + engine.FormatNumber(Multiplier);
        }
        public virtual string UpgradeMessage(GameEngine engine)
        {
            return "";
        }
        public void LevelUp(GameEngine engine)
        {
            Active = true;
        }
        public virtual bool CanUnlock(GameEngine engine)
        {
            return Unlocked;
        }
    }
}
