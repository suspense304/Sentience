namespace Sentience.Models.Jobs
{
    public class RedditScraper : Job
    {
        public RedditScraper(GameEngine engine)
        {
            Name = "Reddit Scraper";
            Active = false;
            Unlocked = false;
            BaseIncome = 1.73f;
            BaseXP = 1080;
            NextLevel = 1080;
        }

        public RedditScraper()
        {
            Name = "Reddit Scraper";
            Active = false;
            Unlocked = false;
            BaseIncome = 1.73f;
            BaseXP = 1080;
            NextLevel = 1080;
        }
        public bool CanUnlock(Job job)
        {
            return (job.Level > 9) ? true : false;
        }
    }
}
