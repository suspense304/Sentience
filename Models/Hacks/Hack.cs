namespace Sentience.Models.Hacks
{
    public class Hack
    {
        public bool Active;
        public bool Unlocked;
        public float Multiplier { get; set; }
        public Modifiers Modifier { get; set; }
        public int CurrentXp { get; set; }
        public int XpNeeded { get; set; }
        public string Name { get; set; } = "";
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
