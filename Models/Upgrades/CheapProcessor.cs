namespace Sentience.Models.Upgrades
{
    public class CheapProcessor : Upgrade
    {
        public CheapProcessor(GameEngine engine)
        {
            Name = "Cheap Processor";
            Active = false;
            Expense = 0.50f;
            Unlocked = false;
            Modifier = Modifiers.ResearchSpeed;
            Multiplier = 4f;
        }

        public CheapProcessor()
        {
            Name = "Cheap Processor";
            Active = false;
            Expense = 0.50f;
            Unlocked = false;
            Modifier = Modifiers.ResearchSpeed;
            Multiplier = 4f;
        }

        public bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                return (engine.GetMoney() > 100) ? true : false;
            }
            return true;
        }
    }
}

