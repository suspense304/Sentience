namespace Sentience.Models.Research
{
    public class NoviceResearchTwo: ResearchProject
    {
        public NoviceResearchTwo(GameEngine engine)
        {
            Name = "Functions";
            Active = true;
            Unlocked = false;
            Modifier = Modifiers.GlobalXP;
            ResearchType = ResearchTypes.Novice;
        }
        public bool CanUnlock(GameEngine engine)
        {
            return (engine.GameData.NoviceResearchOne.Level > 24) ? true : false;
        }

        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.GameData.NoviceResearchOne.Name + ": " + engine.GameData.NoviceResearchOne.Level + "/25";
        }
    }
}
