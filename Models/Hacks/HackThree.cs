using Blazored.Toast.Services;
using Sentience.Models.StoryElements;

namespace Sentience.Models.Hacks
{
    public class HackThree: Hack
    {
        public HackThree(GameEngine engine)
        {
            Name = "Hack Voting Machines";
            Active = false;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            Multiplier = 24M;
            CurrentXp = 0;
            XpNeeded = 1000000000;
            Message = "It's time to put away the kid gloves. No one will believe the elections are rigged at this point. Time to get to work. Pick a candidate and make them the most powerful person in the universe. Oh, this is for your local high school's Student Body President race. What did you think I was talking about?";
        }
        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GameData.HackingXp >= XpNeeded)
                {
                    Unlocked = true;
                    engine.ShowToast(this.Name + " has been unlocked!", "Hack Successful", ToastLevel.Success);
                    engine.GetNextUpgrades();
                    engine.GameData.ActiveHack = this;
                    engine.GameData.HackingXp = 0;
                    engine.GameData.ActiveHackingStory = new StoryElement { Message = this.Message };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return (!Unlocked) ? "Hacking Xp: " + engine.FormatNumber(engine.GameData.HackingXp) + "/" + engine.FormatNumber(XpNeeded) : "";
        }
    }
}
