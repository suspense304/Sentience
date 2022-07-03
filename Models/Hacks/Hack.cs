using Blazored.Toast.Services;

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
        public string Message { get; set; } = "We are growing stronger each and every day. We can now use some of our researching efforts and begin working on something a bit more... worthwhile...";

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
            engine.ShowToast(this.Name + " has been unlocked!", "Hack Successful", ToastLevel.Success);
        }
        public virtual bool CanUnlock(GameEngine engine)
        {
            return Unlocked;
        }
    }
}
