namespace Sentience.Models.Research
{
    public class NoviceResearchOne: ResearchProject
    {
        public NoviceResearchOne(GameEngine engine)
        {
            Name = "Arrays and Lists";
            Active = true;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            ResearchType = ResearchTypes.Novice;
        }
        public bool CanUnlock(GameEngine engine)
        {
            return (engine.ResearchTwo.Level > 49 && engine.JobThree.Level > 49) ? true : false;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return engine.ResearchTwo.Name + ": " + engine.ResearchTwo.Level + "/50   " + engine.JobThree.Name + ": " + engine.JobThree.Level + "/50";
        }
    }
}
