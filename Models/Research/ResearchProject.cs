namespace Sentience.Models.Research
{
    public abstract class ResearchProject
    {
        public bool Active;
        public bool Unlocked;
        public int BaseXP { get; set; } = 80;
        public int NextLevel { get; set; } = 80;
        public float CurrentXP { get; set; } = 0;
        public bool GenerateXP { get; set; } = true;
        public int Level { get; set; } = 0;
        public float Multiplier { get; set; } = 1f;
        public string Name { get; set; }
        public Modifiers Modifier { get; set; }
        public float ModifierValue { get; set; } = 0f;
        public float ModifierIncrementValue { get; set; } = .01f;
        public ResearchTypes ResearchType { get; set; }
        public float XPRemaining(float current)
        {
            int value = (int)(NextLevel - current);
            return (value <= 0) ? 0 : value;
        }
        public void LevelUp(GameEngine engine)
        {
            Level++;
            NextLevel = GetNextUpdateAmount(NextLevel, engine);
            CurrentXP = 0;
            UpdateModifier(engine);
            engine.UnlockResearch();
            engine.SetGlobalMulitplier();
        }

        public void UpdateModifier(GameEngine engine)
        {
            ModifierValue += ModifierIncrementValue;
            ModifierValue = (float)Math.Ceiling(ModifierValue * 100) / 100;
            engine.ApplyModifiers();
        }

        public float UpdateXP(GameEngine engine)
        {
            return engine.GetResearchXPGain();
        }

        public int GetNextUpdateAmount(int lastValue, GameEngine engine)
        {
            return (int)(Math.Floor(BaseXP * Math.Pow(engine.GetUpgradeMultiplier(), Level)));
        }

        public virtual string UpgradeMessage(GameEngine engine)
        {
            return "";
        }
    }
    
}
