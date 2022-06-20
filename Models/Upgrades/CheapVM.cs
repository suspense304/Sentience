namespace Sentience.Models.Upgrades
{
    public class CheapVM : Upgrade
    {
        public CheapVM(GameEngine engine)
        {
            Name = "Cheap VM";
            Active = false;
            Expense = 0.50f;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            Multiplier = 2f;
        }

        public CheapVM()
        {
            Name = "Cheap VM";
            Active = false;
            Expense = 0.50f;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            Multiplier = 2f;
        }

        public bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                return (engine.GetMoney() > 25) ? true : false;
            }
            return true;
        }
    }
}
