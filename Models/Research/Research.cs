namespace Sentience.Models.Research
{
    public class Research
    {
        public bool Active;
        public bool Unlocked;
        public int BaseXP { get; set; } = 240;
        public int NextLevel { get; set; } = 240;
        public float CurrentXP { get; set; } = 0;
        public int Level { get; set; } = 0;
        public float Multiplier { get; set; } = 1f;
        public int BaseCost { get; set; }
        public string Name { get; set; }
        public Modifiers Modifier { get; set; }
        public float ModifierValue { get; set; } = 0f;
        public float ModifierIncrementValue { get; set; } = .01f;

        public Research Create()
        {
            return new Research();
        }

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
            engine.ApplyResearchModifiers();
        }

        public float UpdateXP(GameEngine engine)
        {
            return engine.GetResearchXPGain();
        }

        public int GetNextUpdateAmount(int lastValue, GameEngine engine)
        {
            return (int)(Math.Floor(BaseXP * Math.Pow(engine.GetUpgradeMultiplier(), Level)));
        }
    }
    
}
