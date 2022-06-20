using Sentience.Models.Research;

namespace Sentience.Models.Jobs
{
    public class CookieClicker : Job
    {
        public CookieClicker(GameEngine engine)
        {
            Name = "Cookie Clicker";
            Active = false;
            Unlocked = false;
            BaseIncome = 0.04f;
            BaseXP = 120;
            NextLevel = 120;
            JobType = JobTypes.Amatuer;
        }

        public CookieClicker()
        {
            Name = "Cookie Clicker";
            Active = false;
            Unlocked = false;
            BaseIncome = 0.04f;
            BaseXP = 120;
            NextLevel = 120;
            JobType = JobTypes.Amatuer;
        }

        public bool CanUnlock(Job job, ResearchProject research)
        {
            if(job.Level > 9 && research.Level > 20)
            {
                return true;
            }

            return false;
        }
    }
}

