using Sentience.Components;
using Sentience.Models;
using Sentience.Models.Jobs;
using Sentience.Models.PageSegments;
using Sentience.Models.Research;
using Sentience.Models.StoryElements;
using Sentience.Models.StoryElements.Hacking;
using Sentience.Models.Upgrades;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience
{
    public class GameEngine: EventBase
    {
        #region Declarations
            #region Resources
        public BeginnerJobOne BeginnerJobOne { get; private set; }
        public BeginnerJobTwo BeginnerJobTwo { get; private set; }
        public JobOne JobOne { get; private set; }
        public JobTwo JobTwo { get; private set; }
        public JobThree JobThree { get; private set; }
        public JobFour JobFour { get; private set; }
        public JobFive JobFive { get; private set; }
        public JobSix JobSix { get; private set; }
        public UpgradeOne UpgradeOne { get; private set; }
        public UpgradeTwo UpgradeTwo { get; private set; }
        public UpgradeThree UpgradeThree { get; private set; }
        public UpgradeFour UpgradeFour { get; private set; }
        public ResearchOne ResearchOne { get; private set; }
        public ResearchTwo ResearchTwo { get; private set; }
        public ResearchThree ResearchThree { get; private set; }
        public NoviceResearchOne NoviceResearchOne { get; private set; }
        public NoviceResearchTwo NoviceResearchTwo { get; private set; }
        #endregion
            #region Pages
            public JobSegment JobPage { get; set; } = new JobSegment();
            public ResearchSegment ResearchPage { get; set; } = new ResearchSegment();
            public UpgradeSegment UpgradePage { get; set; } = new UpgradeSegment();
            public HackingSegment HackingPage { get; set; } = new HackingSegment();
        #endregion
        #region Story Elements
        List<StoryElement> HackingStories = new List<StoryElement>();
        #endregion

        private bool _gameOver = false;
        private int _currentAge { get; set; } = 0;
        private int _obsoleteAge { get; set; } = 50;
        private int _currentDay { get; set; } = 1;

        private float _money = 0;
        private float _dailyIncome = 0;
        private float _expenses = 0;

        private float _baseGameSpeed = 500f;
        private float _baseXPGain = 12f;
        private float _gameSpeed = 1f;
        private float _globalMultiplier = 1f;
        private float _incomeMultiplier = 1.04f;
        private float _upgradeMultiplier = 1.12f;

        private float _gameSpeedModifier = 1f;
        private float _globalLevelModifier = 0.1f;
        private float _jobXPModifier = 1f;
        private float _researchXPModifier = 1f;

        private bool _UpgradesUnlocked;
        private bool HackingUnlocked = false;

        public List<Job> JobsList = new List<Job>();
        public List<ResearchProject> ResearchList = new List<ResearchProject>();
        public List<Upgrade> UpgradeList = new List<Upgrade>();

        public List<PageSegment> Pages = new List<PageSegment>();

        public Job _NextJobUpgrade { get; private set; }
        public ResearchProject _NextResearchUpgrade { get; private set; }
        public Upgrade _NextUpgrade { get; private set; }
        private Timer _GameTimer { get; set; }
        private Job ActiveJob { get; set; }
        private ResearchProject ActiveResearch { get; set; }
        private StoryElement ActiveHackingStory { get; set; }
        #endregion

        #region GAME ENGINE TIMER
        private Timer CreateGameTimer()
        {
            float gameSpeed = GetGameSpeed();
            Timer newTimer = new Timer(gameSpeed);
            newTimer.Elapsed += new ElapsedEventHandler(RunDailyActions);
            newTimer.Enabled = true;
            newTimer.AutoReset = false;
            return new Timer(gameSpeed);
        }
        private void GameOver()
        {
            _GameTimer.Stop();
            _GameTimer.Enabled = false;
            _GameTimer.Dispose();
            _gameOver = true;
        }
        public int GetAge()
        {
            return _currentAge;
        }
        public int GetDay()
        {
            return _currentDay;
        }
        public int GetObsoleteAge()
        {
            return _obsoleteAge;
        }
        public Timer GetGameTimer()
        {
            return _GameTimer;
        }
        private void RunDailyActions(object source, ElapsedEventArgs e)
        {
            UpdateDate();
            CheckBankruptcy();

            GetNextJobUpgrade();
            GetNextUpgrades();

            UpdateJobData();
            UpdateResearchData();

            UnlockJobs();
            UnlockResearch();
            UnlockUpgrades();
            UnlockHacking();

            _GameTimer.Stop();
            _GameTimer.Dispose();
            _GameTimer = CreateGameTimer();
        }
        public GameEngine()
        {
            CreatePages();
            CreateJobs();
            CreateResearch();
            CreateStories();
            CreateUpgrades();
            GetStartingActives();
            _GameTimer = CreateGameTimer();
        }
        #endregion

        #region FUNCTIONS
        public void ApplyModifiers()
        {
            float newXP = 1f;
            float newGameSpeed = 1f;
            float newGlobalXp = 1f;
            float newResearchSpeed = 1f;
            foreach (ResearchProject research in ResearchList)
            {
                if (research.Modifier == Modifiers.JobXP && research.Level > 0)
                {
                    newXP += research.ModifierValue;
                    newXP = (float)Math.Ceiling(newXP * 100) / 100;
                }
                if (research.Modifier == Modifiers.GameSpeed && research.Level > 0)
                {
                    newGameSpeed += research.ModifierValue;
                    newGameSpeed = (float)Math.Ceiling(newGameSpeed * 100) / 100;
                }
                if (research.Modifier == Modifiers.GlobalXP && research.Level > 0)
                {
                    newGlobalXp += research.ModifierValue;
                    newGlobalXp = (float)Math.Ceiling(newXP * 100) / 100;
                }
                if (research.Modifier == Modifiers.ResearchSpeed && research.Level > 0)
                {
                    newResearchSpeed += research.ModifierValue;
                    newResearchSpeed = (float)Math.Ceiling(newXP * 100) / 100;
                }
            }
            foreach (Upgrade upgrade in UpgradeList)
            {
                if (upgrade.Modifier == Modifiers.JobXP && upgrade.Active == true)
                {
                    newXP += upgrade.Multiplier;
                    newXP = (float)Math.Ceiling(newXP * 100) / 100;
                }
                if (upgrade.Modifier == Modifiers.GameSpeed && upgrade.Active == true)
                {
                    newGameSpeed += upgrade.Multiplier;
                    newGameSpeed = (float)Math.Ceiling(newGameSpeed * 100) / 100;
                }
                if (upgrade.Modifier == Modifiers.GlobalXP && upgrade.Active == true)
                {
                    newGlobalXp += upgrade.Multiplier;
                    newGlobalXp = (float)Math.Ceiling(newXP * 100) / 100;
                }
                if (upgrade.Modifier == Modifiers.ResearchSpeed && upgrade.Active == true)
                {
                    newResearchSpeed += upgrade.Multiplier;
                    newResearchSpeed = (float)Math.Ceiling(newXP * 100) / 100;
                }
            }
            _gameSpeedModifier = newGameSpeed;
            _jobXPModifier = newXP;
            _globalMultiplier = newGlobalXp;
            _researchXPModifier = newResearchSpeed;
        }
        public void CheckBankruptcy()
        {
            if(_money <= 0)
            {
                foreach(Upgrade upgrade in UpgradeList)
                {
                    upgrade.Active = false;
                }
                SetExpenses();
                _money = 0;
                TriggerUpgradeUpdate("Upgrades Updated");
            }
        }
        public void CreatePages()
        {
            Pages.Add(JobPage);
            Pages.Add(ResearchPage);
            Pages.Add(UpgradePage);
            Pages.Add(HackingPage);
        }
        public void CreateStories()
        {
            HackingStories.Add(new HackingIntro());
            ActiveHackingStory = HackingStories[0];
        }
        public string FormatNumber(float value)
        {
            Dictionary<long, string> dict = new Dictionary<long, string>
            {
                {1000000000000000000, "Q"},
                {1000000000000000, "q"},
                {1000000000000, "t"},
                {1000000000, "b"},
                {1000000, "m"},
                {1000, "k"}
            };
            string formattedValue = value.ToString();
            foreach (long n in dict.Keys.OrderBy(k => k))
            {
                if (value < n)
                {
                    continue;
                }
                double newValue = 0.00;
                if (value > 99999)
                {
                    newValue = Math.Round(value / (double)n, 2);
                }else
                {
                    newValue = Math.Round(value / (double)n, 1);
                }
                
                formattedValue = String.Format("{0}{1}", newValue, dict[n]);
            }
            return formattedValue;
        }
        public void GetNextUpgrades()
        {
            Job nextUnlockedJob = JobsList.Where(w => w.Unlocked == false).FirstOrDefault();
            ResearchProject nextUnlockedResearch = ResearchList.Where(w => w.Unlocked == false).FirstOrDefault();
            Upgrade nextUnlockedUpgrade = UpgradeList.Where(w => w.Unlocked == false).FirstOrDefault();

            if (nextUnlockedJob != null) SetNextJobUpgrade(nextUnlockedJob);
            if (nextUnlockedResearch != null) SetNextResearchUpgrade(nextUnlockedResearch);
            if (nextUnlockedUpgrade != null) SetNextUpgrade(nextUnlockedUpgrade);
        }
        public void GetStartingActives()
        {
            ActiveJob = JobsList.Where(w => w.Active).FirstOrDefault();
            ActiveResearch = ResearchList.Where(w => w.Active).FirstOrDefault();
        }
        private void UpdateDate()
        {
            _currentDay++;
            if(_currentDay > 365)
            {
                _currentAge++;
                if(_currentAge >= _obsoleteAge)
                {
                    GameOver();
                }
                _currentDay = 1;
            }
        }
        private void UpdateJobData()
        {
            if (ActiveJob != null)
            {
                ActiveJob.CurrentXP += GetJobXPGain();
                if (ActiveJob.CurrentXP >= ActiveJob.NextLevel)
                {
                    ActiveJob.LevelUp(this);
                }
                ActiveJob.UpdateIncome(this);
                SetDailyIncome(ActiveJob.Income);
                SetMoney();
            }
            TriggerJobUpdate("Job Updated");
        }
        private void UpdateResearchData()
        {
            ActiveResearch.CurrentXP += GetResearchXPGain();
            if (ActiveResearch.CurrentXP >= ActiveResearch.NextLevel)
            {
                ActiveResearch.LevelUp(this);
            }
            TriggerResearchUpdate("Research Updated");
        }
        #endregion

        #region Game Engine Variables
        #region GETS
        public float GetBaseXPGain()
        {
            return _baseXPGain;
        }
        public float GetExpenses()
        {
            return _expenses;
        }
        public float GetGameSpeed()
        {
            return _baseGameSpeed / (_gameSpeed * _gameSpeedModifier);
        }
        public StoryElement GetActiveHackingStory()
        {
            return ActiveHackingStory;
        }
        public float GetGlobalLevels()
        {
            // GETS TOTAL LEVELS AND RETURNS THE PERCENTAGE FOR THE MODIFIER TO ADD TO XP GAIN
            int levels = 0;
            foreach (Job job in JobsList)
            {
                levels += job.Level;
            }
            foreach (ResearchProject research in ResearchList)
            {
                levels += research.Level;
            }
            return (float)(levels * _globalLevelModifier);
        }
        public float GetGlobalMultiplier()
        {
            return _globalMultiplier;
        }
        public float GetIncome()
        {
            return _dailyIncome;
        }
        public float GetIncomeMultiplier()
        {
            return _incomeMultiplier;
        }
        public float GetJobXpModifier()
        {
            float newXp = 1f;
            foreach (ResearchProject research in ResearchList)
            {
                if (research.Modifier == Modifiers.JobXP)
                {
                    newXp += research.ModifierValue;
                }
            }
            foreach (Upgrade upgrade in UpgradeList)
            {
                if (upgrade.Modifier == Modifiers.JobXP && upgrade.Active)
                {
                    newXp += upgrade.Multiplier;
                }
            }

            return (float)(Math.Ceiling(newXp * 100) / 100);
        }
        public float GetMoney()
        {
            return _money;
        }
        public float GetResearchXpModifier()
        {
            float newXp = 1f;
            foreach (ResearchProject research in ResearchList)
            {
                if (research.Modifier == Modifiers.ResearchSpeed)
                {
                    newXp += research.ModifierValue;
                }
            }
            foreach (Upgrade upgrade in UpgradeList)
            {
                if (upgrade.Modifier == Modifiers.ResearchSpeed && upgrade.Active)
                {
                    newXp += upgrade.Multiplier;
                }
            }
            return (float)(Math.Ceiling(newXp * 100) / 100);
        }
        public float GetUpgradeMultiplier()
        {
            return _upgradeMultiplier;
        }
        public bool IsHackingUnlocked()
        {
            return HackingUnlocked;
        }
        #endregion
        #region SETS
        public void ResetMoney()
        {
            _money = 0;
        }
        public void SetDailyIncome(float value)
        {
            _dailyIncome = value - _expenses;
            _dailyIncome = (float)Math.Ceiling(_dailyIncome * 100) / 100;
        }
        public void SetGlobalMulitplier()
        {
            float newXp = 1f;
            foreach (ResearchProject research in ResearchList)
            {
                if (research.Modifier == Modifiers.GlobalXP && research.Level > 0)
                {
                    newXp += research.ModifierValue;
                }
            }
            foreach (Upgrade upgrade in UpgradeList)
            {
                if (upgrade.Modifier == Modifiers.GlobalXP && upgrade.Active)
                {
                    newXp += upgrade.Multiplier;
                }
            }
            _globalMultiplier = newXp;
            _globalMultiplier = (float)Math.Ceiling(_globalMultiplier * 100) / 100;
        }
        public void SetExpenses()
        {
            float newExpenses = 0;
            foreach (Upgrade upgrade in UpgradeList)
            {
                if (upgrade.Active)
                {
                    newExpenses += upgrade.Expense;
                }
            }
            _expenses = newExpenses;
            _expenses = (float)Math.Ceiling(_expenses * 100) / 100;
        }
        public void SetGameSpeed(int value)
        {
            _gameSpeed = value;
        }
        public void SetGameSpeedModifier()
        {
            float newXp = 1f;
            foreach (ResearchProject research in ResearchList)
            {
                if (research.Modifier == Modifiers.GameSpeed)
                {
                    newXp += research.ModifierValue;
                }
            }
            foreach (Upgrade upgrade in UpgradeList)
            {
                if (upgrade.Modifier == Modifiers.GameSpeed && upgrade.Active)
                {
                    newXp += upgrade.Multiplier;
                }
            }
            _gameSpeedModifier = newXp;
            _gameSpeedModifier = (float)Math.Ceiling(_gameSpeedModifier * 100) / 100;
        }
        public void SetMoney()
        {
            _money += GetIncome();
            _money = (float)Math.Ceiling(_money * 100) / 100;
        }
        #endregion
        #endregion

        #region Jobs
        #region CREATE
        private void CreateJobs()
        {
            JobOne = new JobOne(this);
            JobOne.Income = JobOne.BaseIncome;
            JobsList.Add(JobOne);

            JobTwo = new JobTwo(this);
            JobTwo.Income = JobTwo.BaseIncome;
            JobsList.Add(JobTwo);

            JobThree = new JobThree(this);
            JobThree.Income = JobThree.BaseIncome;
            JobsList.Add(JobThree);

            JobFour = new JobFour(this);
            JobFour.Income = JobFour.BaseIncome;
            JobsList.Add(JobFour);

            JobFive = new JobFive(this);
            JobFive.Income = JobFive.BaseIncome;
            JobsList.Add(JobFive);

            JobSix = new JobSix(this);
            JobSix.Income = JobSix.BaseIncome;
            JobsList.Add(JobSix);

            BeginnerJobOne = new BeginnerJobOne(this);
            BeginnerJobOne.Income = BeginnerJobOne.BaseIncome;
            JobsList.Add(BeginnerJobOne);

            BeginnerJobTwo = new BeginnerJobTwo(this);
            BeginnerJobTwo.Income = BeginnerJobTwo.BaseIncome;
            JobsList.Add(BeginnerJobTwo);
        }
        #endregion
        #region GETS
        public Job GetActiveJob()
        {
            return ActiveJob;
        }
        public int GetJobXPGain()
        {
            return (int)((GetBaseXPGain() + GetGlobalLevels()) * GetJobXpModifier() * _globalMultiplier);
        }
        public Job GetNextJobUpgrade()
        {
            TriggerJobUpdate("Job Updated");
            return _NextJobUpgrade;
        }
        #endregion
        #region UPDATES
        public void SetActiveJob(Job job)
        {
            ActiveJob = job;
            foreach (Job j in JobsList)
            {
                if (j == job)
                {
                    j.Active = true;
                }
                else
                {
                    j.Active = false;
                }
            }
        }
        public void SetNextJobUpgrade(Job job)
        {
            _NextJobUpgrade = job;
        }
        #endregion
        #endregion

        #region Research
        #region CREATE
        //public ResearchProject CreateResearch(ResearchProject research)
        //{
        //    return research.Create();
        //}
        private void CreateResearch()
        {
            ResearchOne = new ResearchOne(this);
            ResearchList.Add(ResearchOne);

            ResearchTwo = new ResearchTwo(this);
            ResearchList.Add(ResearchTwo);

            ResearchThree = new ResearchThree(this);
            ResearchList.Add(ResearchThree);

            NoviceResearchOne = new NoviceResearchOne(this);
            ResearchList.Add(NoviceResearchOne);

            NoviceResearchTwo = new NoviceResearchTwo(this);
            ResearchList.Add(NoviceResearchTwo);
        }
        #endregion
        #region GETS
        public ResearchProject GetActiveResearch()
        {
            return ActiveResearch;
        }
        public ResearchProject GetNextResearchUpgrade()
        {
            return _NextResearchUpgrade;
        }
        public ResearchProject GetResearch(ResearchProject research)
        {
            return ResearchList.Where(w => w.Name == research.Name).First();
        }
        public float GetResearchModifier(ResearchProject research)
        {
            switch (research.Modifier)
            {
                case Modifiers.GlobalXP:
                    return _globalMultiplier;
                case Modifiers.JobXP:
                    return _jobXPModifier;
                case Modifiers.GameSpeed:
                    return _gameSpeedModifier;
                case Modifiers.ResearchSpeed:
                    return _researchXPModifier;
                default:
                    return 1f;
            }
        }
        public int GetResearchXPGain()
        {
            return (int)((GetBaseXPGain() + GetGlobalLevels()) * GetResearchXpModifier() * _globalMultiplier);
        }
        #endregion
        #region UPDATES
        public void SetActiveResearch(ResearchProject research)
        {
            ActiveResearch = research;
            foreach (ResearchProject j in ResearchList)
            {
                if (j == research)
                {
                    j.Active = true;
                }
                else
                {
                    j.Active = false;
                }
            }
        }
        public void SetNextResearchUpgrade(ResearchProject research)
        {
            _NextResearchUpgrade = research;
        }
        #endregion
        #endregion

        #region Upgrades
        #region CREATE
        private void CreateUpgrades()
        {
            UpgradeOne = new UpgradeOne(this);
            UpgradeList.Add(UpgradeOne);

            UpgradeTwo = new UpgradeTwo(this);
            UpgradeList.Add(UpgradeTwo);

            UpgradeThree = new UpgradeThree(this);
            UpgradeList.Add(UpgradeThree);

            UpgradeFour = new UpgradeFour(this);
            UpgradeList.Add(UpgradeFour);
        }
        #endregion
        #region GETS
        public Upgrade GetNextUpgrade()
        {
            return _NextUpgrade;
        }
        public float GetUpgradeModifier(Upgrade upgrade)
        {
            return upgrade.Multiplier;
        }
        #endregion
        #region UPDATES
        public void SetNextUpgrade(Upgrade upgrades)
        {
            _NextUpgrade = upgrades;
        }
        public void ToggleActiveUpgrade(Upgrade upgrade)
        {
            upgrade.Active = !upgrade.Active;
            this.ApplyModifiers();
        }
        #endregion
        #endregion

        #region Unlock Requirements
        public void UnlockJobs()
        {
            JobTwo.Unlocked = JobTwo.CanUnlock(this);

            JobThree.Unlocked = JobThree.CanUnlock(this);

            JobFour.Unlocked = JobFour.CanUnlock(this);

            JobFive.Unlocked = JobFive.CanUnlock(this);

            JobSix.Unlocked = JobSix.CanUnlock(this);
        }
        public void UnlockResearch()
        {
            // Checks InputResponse for required level
            ResearchTwo.Unlocked = ResearchTwo.CanUnlock(this);

            ResearchThree.Unlocked = ResearchThree.CanUnlock(this);

            NoviceResearchOne.Unlocked = NoviceResearchOne.CanUnlock(this);

            NoviceResearchTwo.Unlocked = NoviceResearchTwo.CanUnlock(this);
        }
        public void UnlockUpgrades()
        {
            UpgradeOne.Unlocked = UpgradeOne.CanUnlock(this);
            UpgradeTwo.Unlocked = UpgradeTwo.CanUnlock(this);
            UpgradeThree.Unlocked = UpgradeThree.CanUnlock(this);
            UpgradeFour.Unlocked = UpgradeFour.CanUnlock(this);

            if (UpgradeOne.CanUnlock(this))
            {
                _UpgradesUnlocked = true;
            }
        }
        public void UnlockHacking()
        {
            if(UpgradeFour.Unlocked && NoviceResearchOne.Unlocked)
            {
                HackingUnlocked = true;
            }
        }
        #endregion
    }
}
