namespace Sentience.Models.Research
{
    public class ResearchThree: ResearchProject
    {
        public ResearchThree(GameEngine engine)
        {
            Name = "For Loops";
            Active = true;
            Unlocked = false;
            Modifier = Modifiers.ResearchSpeed;
            ResearchType = ResearchTypes.Fundamentals;
        }
        public bool CanUnlock(GameEngine engine)
        {
            return (engine.GameData.ResearchTwo.Level > 19) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.GameData.ResearchTwo.Name + ": " + engine.GameData.ResearchTwo.Level + "/20";
        }
    }
}
