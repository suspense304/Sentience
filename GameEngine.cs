using Sentience.Models;
using Sentience.Models.Jobs;
using Sentience.Models.Research;
using Sentience.Models.Upgrades;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Sentience
{
    public class GameEngine
    {
        private ChatBot ChatBot { get; set; } = new ChatBot();
        private CheapProcessor CheapProcessor { get; set; } = new CheapProcessor();
        private CheapVM CheapVM { get; set; } = new CheapVM();
        private CookieClicker CookieClicker { get; set; } = new CookieClicker();
        private TicTacToe TicTacToe { get; set; } = new TicTacToe();
        private IfStatement IfStatement { get; set; } = new IfStatement();
        private InputResponse InputResponse { get; set; } = new InputResponse();
        private RedditScraper RedditScraper { get; set; } = new RedditScraper();

        private float _money = 0;
        private float _dailyIncome = 0;
        private float _expenses = 0;

        private float _baseGameSpeed = 500f;
        private float _baseXPGain = 12f;
        private float _gameSpeed = 1f;
        private float _globalMultiplier = 1f;
        private float _incomeMultiplier = 1.07f;
        private float _upgradeMultiplier = 1.13f;

        private float _gameSpeedModifier = 1f;
        private float _globalLevelModifier = 0.1f;
        private float _jobXPModifier = 1f;
        private float _researchXPModifier = 1f;

        public List<Job> JobsList = new List<Job>();
        public List<ResearchProject> ResearchList = new List<ResearchProject>();
        public List<Upgrade> UpgradeList = new List<Upgrade>();

        private Job _NextJobUpgrade;
        private ResearchProject _NextResearchUpgrade;
        private Upgrade _NextUpgrade;

        private Timer _GameTimer { get; set; }

        // I WANT TO EVENTUALLY MOVE THE ENTIRE GAMES TIMER SYSTEM TO THE GAME ENGINE
        // CURRENTLY I AM NOT SURE HOW TO UPDATE THE COMPONENTS FROM THIS CLASS
        // THIS CODE IS CURRENTLY NOT USED
        #region GAME ENGINE TIMER


        private Timer CreateGameTimer()
        {
            float gameSpeed = GetGameSpeed();
            Timer newTimer = new Timer(gameSpeed);
            newTimer.Elapsed += new ElapsedEventHandler(RunDailyActions);
            newTimer.Enabled = true;
            newTimer.AutoReset = true;
            return new Timer(gameSpeed);
        }

        public Timer GetGameTimer()
        {
            return _GameTimer;
        }

        private void RunDailyActions(object source, ElapsedEventArgs e)
        {
            GetNextUpgrades();
        }

        #endregion

        #region FUNCTIONS
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
                double newValue = Math.Round(value / (double)n, 2);
                formattedValue = String.Format("{0}{1}", newValue, dict[n]);
            }
            return formattedValue;
        }
        public void GetNextUpgrades()
        {
            Job nextUnlockedJob = JobsList.Where(w => w.Unlocked == true).LastOrDefault();
            ResearchProject nextUnlockedResearch = ResearchList.Where(w => w.Unlocked == true).LastOrDefault();
            Upgrade nextUnlockedUpgrade = UpgradeList.Where(w => w.Unlocked == true).LastOrDefault();
            
            if(nextUnlockedJob != null) SetNextJobUpgrade(nextUnlockedJob);
            if (nextUnlockedResearch != null) SetNextResearchUpgrade(nextUnlockedResearch);
            if (nextUnlockedUpgrade != null) SetNextUpgrade(nextUnlockedUpgrade);
        }
        #endregion
        public GameEngine()
        {
            CreateJobs();
            CreateResearch();
            CreateUpgrades();
            _GameTimer = CreateGameTimer();
        }

        #region Game Engine Variables
        #region GETS
        public float GetBaseXPGain()
        {
            return _baseXPGain;
        }
        public float GetDailyIncome()
        {
            return _dailyIncome;
        }
        public float GetExpenses()
        {
            return _expenses;
        }
        public float GetGameSpeed()
        {
            return _baseGameSpeed / (_gameSpeed * _gameSpeedModifier);
        }
        public float GetGlobalLevels()
        {
            // GETS TOTAL LEVELS AND RETURNS THE PERCENTAGE FOR THE MODIFIER TO ADD TO XP GAIN
            int levels = 0;
            foreach(Job job in JobsList)
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
                if (research.Modifier == Modifiers.GlobalXP)
                {
                    newXp += research.ModifierValue;
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
        public void SetIncomeMultiplier(Job job)
        {
            if(job.JobType == JobTypes.Basics)
            {
                _incomeMultiplier = 1.07f;
            }

            if (job.JobType == JobTypes.Amatuer)
            {
                _incomeMultiplier = 1.09f;
            }

            if (job.JobType == JobTypes.Novice)
            {
                _incomeMultiplier = 1.11f;
            }

            if (job.JobType == JobTypes.Expert)
            {
                _incomeMultiplier = 1.13f;
            }

            if (job.JobType == JobTypes.Sentient)
            {
                _incomeMultiplier = 1.15f;
            }
        }
        public void SetMoney()
        {
            _money += GetIncome();
            _money = (float)Math.Ceiling(_money * 100) / 100;
        }
        public void SetUpgradeMultiplier(float value)
        {
            _upgradeMultiplier = value;
        }
        #endregion
        #endregion

        #region Jobs
        #region CREATE
        //public Job CreateJob(Job job)
        //{
        //    return job.Create();
        //}
        private void CreateJobs()
        {
            ChatBot = new ChatBot(this);
            ChatBot.Income = ChatBot.BaseIncome;
            JobsList.Add(ChatBot);

            TicTacToe = new TicTacToe(this);
            TicTacToe.Income = TicTacToe.BaseIncome;
            JobsList.Add(TicTacToe);

            RedditScraper = new RedditScraper(this);
            RedditScraper.Income = RedditScraper.BaseIncome;
            JobsList.Add(RedditScraper);
        }
        #endregion
        #region GETS
        public Job GetJob(Job job)
        {
            return JobsList.Where(w => w.Name == job.Name).First();
        }
        public int GetJobXPGain()
        {
            return (int)((GetBaseXPGain() + GetGlobalLevels()) * GetJobXpModifier() * _globalMultiplier);
        }
        public Job GetNextJobUpgrade()
        {
            return _NextJobUpgrade;
        }
        public float GetXPModifier()
        {
            return _jobXPModifier;
        }
        #endregion
        #region UPDATES
        public void SetActiveJob(Job job)
        {
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
        public void UpdateJob(Job job)
        {
            switch (job.Name)
            {
                case "Chat Bot":
                    ChatBot = (ChatBot)job;
                    break;
                case "Tic-Tac-Toe":
                    TicTacToe = (TicTacToe)job;
                    break;
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
            InputResponse = new InputResponse(this);
            ResearchList.Add(InputResponse);

            IfStatement = new IfStatement(this);
            ResearchList.Add(IfStatement);
        }
        #endregion
        #region GETS
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
        public void UpdateResearch(ResearchProject research)
        {
            switch (research.Name)
            {
                case "Input Response":
                    InputResponse = (InputResponse)research;
                    break;
            }
        }
        #endregion
        public void ApplyResearchModifiers()
        {
            float newXP = 1f;
            float newGameSpeed = 1f;
            foreach (ResearchProject research in ResearchList)
            {
                if (research.Modifier == Modifiers.JobXP)
                {
                    newXP += research.ModifierValue;
                    newXP = (float)Math.Ceiling(newXP * 100) / 100;
                }
                if (research.Modifier == Modifiers.GameSpeed)
                {
                    newGameSpeed += research.ModifierValue;
                    newGameSpeed = (float)Math.Ceiling(newGameSpeed * 100) / 100;
                }
            }
            _gameSpeedModifier = newGameSpeed;
            _jobXPModifier = newXP;
        }
        #endregion

        #region Upgrades
        #region CREATE
        public Upgrade CreateUpgrade(Upgrade upgrade)
        {
            return upgrade.Create();
        }
        private void CreateUpgrades()
        {
            CheapVM = new CheapVM(this);
            UpgradeList.Add(CheapVM);

            CheapProcessor = new CheapProcessor(this);
            UpgradeList.Add(CheapProcessor);
        }
        #endregion
        #region GETS
        public Upgrade GetNextUpgrade()
        {
            return _NextUpgrade;
        }
        public Upgrade GetUpgrade(Upgrade upgrade)
        {
            return UpgradeList.Where(w => w.Name == upgrade.Name).First();
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
        }
        #endregion
        #endregion

        #region Unlock Requirements
        public void UnlockJobs()
        {
            // Checks ChatBot for required level
            TicTacToe.Unlocked = TicTacToe.CanUnlock(ChatBot);

            // Checks TicTacToe for required level
            RedditScraper.Unlocked = RedditScraper.CanUnlock(TicTacToe);

            // Checks CookieClick for required level
            CookieClicker.Unlocked = CookieClicker.CanUnlock(RedditScraper, IfStatement);
        }
        public void UnlockResearch()
        {
            // Checks InputResponse for required level
            IfStatement.Unlocked = IfStatement.CanUnlock(InputResponse);
        }
        public void UnlockUpgrades()
        {
            // Checks for required money balance
            CheapVM.Unlocked = CheapVM.CanUnlock(this);
            CheapProcessor.Unlocked = CheapProcessor.CanUnlock(this);
        }
        #endregion
    }
}
