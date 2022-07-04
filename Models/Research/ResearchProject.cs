

using DecimalMath;

namespace Sentience.Models.Research
{
    public class ResearchProject
    {
        public bool Active;
        public bool Unlocked;
        public decimal BaseXP { get; set; } = 80;
        public decimal NextLevel { get; set; } = 80;
        public decimal CurrentXP { get; set; } = 0;
        public bool GenerateXP { get; set; } = true;
        public int Level { get; set; } = 0;
        public decimal Multiplier { get; set; } = 1M;
        public string Name { get; set; }
        public Modifiers Modifier { get; set; }
        public decimal ModifierValue { get; set; } = 0M;
        public decimal ModifierIncrementValue { get; set; } = .01M;
        public ResearchTypes ResearchType { get; set; }
        public string GetModifierAmount(GameEngine engine)
        {
            return "x" + engine.FormatNumber(ModifierValue);
        }
        public decimal XPRemaining(decimal current)
        {
            decimal value = (decimal)(NextLevel - current);
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

            if (engine.AutoLevelResearch)
            {
                engine.SetActiveResearch(engine.GetNextResearchToLevel());
            }
        }
        public void UpdateModifier(GameEngine engine)
        {
            ModifierValue += ModifierIncrementValue;
            ModifierValue = (decimal)Math.Ceiling(ModifierValue * 100) / 100;
            engine.ApplyModifiers();
        }
        public decimal UpdateXP(GameEngine engine)
        {
            return engine.GetResearchXPGain();
        }
        public decimal GetNextUpdateAmount(decimal lastValue, GameEngine engine)
        {
            return (decimal)(Math.Floor(BaseXP * DecimalEx.Pow(engine.GetUpgradeMultiplier(), Level)));
        }
        public virtual string UpgradeMessage(GameEngine engine)
        {
            return "";
        }
    }
    
}
