namespace Sentience.Models.Upgrades
{
    public class CheapVM : Upgrades
    {
        public CheapVM(GameEngine engine)
        {
            Name = "Cheap VM";
            Active = false;
            Expense = 1.25f;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            Multiplier = 2f;
        }

        public CheapVM()
        {
            Name = "Cheap VM";
            Active = false;
            Expense = 1.25f;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            Multiplier = 2f;
        }

        public bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                return (engine.GetMoney() > 50) ? true : false;
            }
            return true;
        }
    }
}
