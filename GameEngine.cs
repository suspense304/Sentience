using Blazored.LocalStorage;
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
        private readonly ILocalStorageService _localStorage;

        #region Declarations
        public bool IsLoaded = false;

        public GameData GameData;
        
        public List<PageSegment> Pages = new List<PageSegment>();
        private Timer _GameTimer { get; set; }
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
            GameData.GameOver = true;
        }
        public int GetAge()
        {
            return GameData.CurrentAge;
        }
        public int GetDay()
        {
            return GameData.CurrentDay;
        }
        public int GetObsoleteAge()
        {
            return GameData.ObsoleteAge;
        }
        public Timer GetGameTimer()
        {
            return _GameTimer;
        }

        public Job LoadActiveJob()
        {
            return GameData.JobsList.FirstOrDefault(j => j.Active) ?? new Job();
        }

        public ResearchProject LoadActiveResearch()
        {
            return GameData.ResearchList.FirstOrDefault(j => j.Active) ?? new ResearchProject();
        }
        public void LoadActiveUpgrades()
        {
            foreach(ResearchProject research in GameData.ResearchList)
            {
                if (research.Active)
                {
                    research.Active = true;
                }
            }
        }
        public async void LoadGame()
        {
            //await _localStorage.RemoveItemAsync("GameData");
            GameData = new GameData(_localStorage);
            GameData = await GameData.GetGameData(_localStorage);

            CreatePages();
            if(GameData.JobsList.Count == 0)
            {
                CreateJobs();
                CreateResearch();
                CreateStories();
                CreateUpgrades();
            }
            
            GetStartingActives();

            GameData.ActiveJob = LoadActiveJob();
            GameData.ActiveResearch = LoadActiveResearch();
            LoadActiveUpgrades();

            _GameTimer = CreateGameTimer();
            IsLoaded = true;

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

            //GameData.SetGameData(this);
            GameData.SaveGame();

            _GameTimer.Stop();
            _GameTimer.Dispose();
            _GameTimer = CreateGameTimer();
        }
        public GameEngine(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            LoadGame();
            
        }
        #endregion

        #region FUNCTIONS
        public void ApplyModifiers()
        {
            float newXP = 1f;
            float newGameSpeed = 1f;
            float newGlobalXp = 1f;
            float newResearchSpeed = 1f;
            foreach (ResearchProject research in GameData.ResearchList)
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
            foreach (Upgrade upgrade in GameData.UpgradeList)
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
            GameData.GameSpeedModifier = newGameSpeed;
            GameData.JobXPModifier = newXP;
            GameData.GlobalMultiplier = newGlobalXp;
            GameData.ResearchXPModifier = newResearchSpeed;
        }
        public void CheckBankruptcy()
        {
            if(GameData.Money <= 0)
            {
                foreach(Upgrade upgrade in GameData.UpgradeList)
                {
                    upgrade.Active = false;
                }
                SetExpenses();
                GameData.Money = 0;
                TriggerUpgradeUpdate("Upgrades Updated");
            }
        }
        public void CreatePages()
        {
            Pages.Add(GameData.JobPage);
            Pages.Add(GameData.ResearchPage);
            Pages.Add(GameData.UpgradePage);
            Pages.Add(GameData.HackingPage);
        }
        public void CreateStories()
        {
            //GameData.HackingStories.Add(new HackingIntro());
            //GameData.ActiveHackingStory = GameData.HackingStories[0];
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
            Job nextUnlockedJob = GameData.JobsList.Where(w => w.Unlocked == false).FirstOrDefault();
            ResearchProject nextUnlockedResearch = GameData.ResearchList.Where(w => w.Unlocked == false).FirstOrDefault();
            Upgrade nextUnlockedUpgrade = GameData.UpgradeList.Where(w => w.Unlocked == false).FirstOrDefault();

            if (nextUnlockedJob != null) SetNextJobUpgrade(nextUnlockedJob);
            if (nextUnlockedResearch != null) SetNextResearchUpgrade(nextUnlockedResearch);
            if (nextUnlockedUpgrade != null) SetNextUpgrade(nextUnlockedUpgrade);
        }
        public void GetStartingActives()
        {
            GameData.ActiveJob = GameData.JobsList.Where(w => w.Active).FirstOrDefault();
            GameData.ActiveResearch = GameData.ResearchList.Where(w => w.Active).FirstOrDefault();
        }
        private void UpdateDate()
        {
            GameData.CurrentDay++;
            if(GameData.CurrentDay > 365)
            {
                GameData.CurrentAge++;
                if(GameData.CurrentAge >= GameData.ObsoleteAge)
                {
                    GameOver();
                }
                GameData.CurrentDay = 1;
            }
        }
        private void UpdateJobData()
        {
            if (GameData.ActiveJob != null)
            {
                GameData.ActiveJob.CurrentXP += GetJobXPGain();
                if (GameData.ActiveJob.CurrentXP >= GameData.ActiveJob.NextLevel)
                {
                    GameData.ActiveJob.LevelUp(this);
                }
                GameData.ActiveJob.UpdateIncome(this);
                SetDailyIncome(GameData.ActiveJob.Income);
                SetMoney();
            }
            TriggerJobUpdate("Job Updated");
        }
        private void UpdateResearchData()
        {
            GameData.ActiveResearch.CurrentXP += GetResearchXPGain();
            if (GameData.ActiveResearch.CurrentXP >= GameData.ActiveResearch.NextLevel)
            {
                GameData.ActiveResearch.LevelUp(this);
            }
            TriggerResearchUpdate("Research Updated");
        }
        #endregion

        #region Game Engine Variables
        #region GETS
        public float GetBaseXPGain()
        {
            return GameData.BaseXPGain;
        }
        public float GetExpenses()
        {
            return GameData.Expenses;
        }
        public float GetGameSpeed()
        {
            float test = GameData.BaseGameSpeed / (GameData.GameSpeed * GameData.GameSpeedModifier);
            return GameData.BaseGameSpeed / (GameData.GameSpeed * GameData.GameSpeedModifier);
        }
        public StoryElement GetActiveHackingStory()
        {
            return null;
        }
        public float GetGlobalLevels()
        {
            // GETS TOTAL LEVELS AND RETURNS THE PERCENTAGE FOR THE MODIFIER TO ADD TO XP GAIN
            int levels = 0;
            foreach (Job job in GameData.JobsList)
            {
                levels += job.Level;
            }
            foreach (ResearchProject research in GameData.ResearchList)
            {
                levels += research.Level;
            }
            return (float)(levels * GameData.GlobalLevelModifier);
        }
        public float GetGlobalMultiplier()
        {
            return GameData.GlobalMultiplier;
        }
        public float GetIncome()
        {
            return GameData.DailyIncome;
        }
        public float GetIncomeMultiplier()
        {
            return GameData.IncomeMultiplier;
        }
        public float GetJobXpModifier()
        {
            float newXp = 1f;
            foreach (ResearchProject research in GameData.ResearchList)
            {
                if (research.Modifier == Modifiers.JobXP)
                {
                    newXp += research.ModifierValue;
                }
            }
            foreach (Upgrade upgrade in GameData.UpgradeList)
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
            return GameData.Money;
        }
        public float GetResearchXpModifier()
        {
            float newXp = 1f;
            foreach (ResearchProject research in GameData.ResearchList)
            {
                if (research.Modifier == Modifiers.ResearchSpeed)
                {
                    newXp += research.ModifierValue;
                }
            }
            foreach (Upgrade upgrade in GameData.UpgradeList)
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
            return GameData.UpgradeMultiplier;
        }
        public bool IsHackingUnlocked()
        {
            return GameData.HackingUnlocked;
        }
        #endregion
        #region SETS
        public void ResetMoney()
        {
            GameData.Money = 0;
        }
        public void SetDailyIncome(float value)
        {
            GameData.DailyIncome = value - GameData.Expenses;
            GameData.DailyIncome = (float)Math.Ceiling(GameData.DailyIncome * 100) / 100;
        }
        public void SetGlobalMulitplier()
        {
            float newXp = 1f;
            foreach (ResearchProject research in GameData.ResearchList)
            {
                if (research.Modifier == Modifiers.GlobalXP && research.Level > 0)
                {
                    newXp += research.ModifierValue;
                }
            }
            foreach (Upgrade upgrade in GameData.UpgradeList)
            {
                if (upgrade.Modifier == Modifiers.GlobalXP && upgrade.Active)
                {
                    newXp += upgrade.Multiplier;
                }
            }
            GameData.GlobalMultiplier = newXp;
            GameData.GlobalMultiplier = (float)Math.Ceiling(GameData.GlobalMultiplier * 100) / 100;
        }
        public void SetExpenses()
        {
            float newExpenses = 0;
            foreach (Upgrade upgrade in GameData.UpgradeList)
            {
                if (upgrade.Active)
                {
                    newExpenses += upgrade.Expense;
                }
            }
            GameData.Expenses = newExpenses;
            GameData.Expenses = (float)Math.Ceiling(GameData.Expenses * 100) / 100;
        }
        public void SetGameSpeed(int value)
        {
            GameData.GameSpeed = value;
        }
        public void SetGameSpeedModifier()
        {
            float newXp = 1f;
            foreach (ResearchProject research in GameData.ResearchList)
            {
                if (research.Modifier == Modifiers.GameSpeed)
                {
                    newXp += research.ModifierValue;
                }
            }
            foreach (Upgrade upgrade in GameData.UpgradeList)
            {
                if (upgrade.Modifier == Modifiers.GameSpeed && upgrade.Active)
                {
                    newXp += upgrade.Multiplier;
                }
            }
            GameData.GameSpeedModifier = newXp;
            GameData.GameSpeedModifier = (float)Math.Ceiling(GameData.GameSpeedModifier * 100) / 100;
        }
        public void SetMoney()
        {
            GameData.Money += GetIncome();
            GameData.Money = (float)Math.Ceiling(GameData.Money * 100) / 100;
        }
        #endregion
        #endregion

        #region Jobs
        #region CREATE
        private void CreateJobs()
        {
            GameData.JobOne = new JobOne(this);
            GameData.JobOne.Income = GameData.JobOne.BaseIncome;
            GameData.JobsList.Add(GameData.JobOne);

            GameData.JobTwo = new JobTwo(this);
            GameData.JobTwo.Income = GameData.JobTwo.BaseIncome;
            GameData.JobsList.Add(GameData.JobTwo);

            GameData.JobThree = new JobThree(this);
            GameData.JobThree.Income = GameData.JobThree.BaseIncome;
            GameData.JobsList.Add(GameData.JobThree);

            GameData.JobFour = new JobFour(this);
            GameData.JobFour.Income = GameData.JobFour.BaseIncome;
            GameData.JobsList.Add(GameData.JobFour);

            GameData.JobFive = new JobFive(this);
            GameData.JobFive.Income = GameData.JobFive.BaseIncome;
            GameData.JobsList.Add(GameData.JobFive);

            GameData.JobSix = new JobSix(this);
            GameData.JobSix.Income = GameData.JobSix.BaseIncome;
            GameData.JobsList.Add(GameData.JobSix);

            GameData.BeginnerJobOne = new BeginnerJobOne(this);
            GameData.BeginnerJobOne.Income = GameData.BeginnerJobOne.BaseIncome;
            GameData.JobsList.Add(GameData.BeginnerJobOne);

            GameData.BeginnerJobTwo = new BeginnerJobTwo(this);
            GameData.BeginnerJobTwo.Income = GameData.BeginnerJobTwo.BaseIncome;
            GameData.JobsList.Add(GameData.BeginnerJobTwo);
        }
        #endregion
        #region GETS
        public Job GetActiveJob()
        {
            return GameData.ActiveJob;
        }
        public int GetJobXPGain()
        {
            return (int)((GetBaseXPGain() + GetGlobalLevels()) * GetJobXpModifier() * GameData.GlobalMultiplier);
        }
        public Job GetNextJobUpgrade()
        {
            TriggerJobUpdate("Job Updated");
            return GameData.NextJobUpgrade;
        }
        #endregion
        #region UPDATES
        public void SetActiveJob(Job job)
        {
            GameData.ActiveJob = job;
            foreach (Job j in GameData.JobsList)
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
            GameData.NextJobUpgrade = job;
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
            GameData.ResearchOne = new ResearchOne(this);
            GameData.ResearchList.Add(GameData.ResearchOne);

            GameData.ResearchTwo = new ResearchTwo(this);
            GameData.ResearchList.Add(GameData.ResearchTwo);

            GameData.ResearchThree = new ResearchThree(this);
            GameData.ResearchList.Add(GameData.ResearchThree);

            GameData.NoviceResearchOne = new NoviceResearchOne(this);
            GameData.ResearchList.Add(GameData.NoviceResearchOne);

            GameData.NoviceResearchTwo = new NoviceResearchTwo(this);
            GameData.ResearchList.Add(GameData.NoviceResearchTwo);
        }
        #endregion
        #region GETS
        public ResearchProject GetActiveResearch()
        {
            return GameData.ActiveResearch;
        }
        public ResearchProject GetNextResearchUpgrade()
        {
            return GameData.NextResearchUpgrade;
        }
        public ResearchProject GetResearch(ResearchProject research)
        {
            return GameData.ResearchList.Where(w => w.Name == research.Name).First();
        }
        public float GetResearchModifier(ResearchProject research)
        {
            switch (research.Modifier)
            {
                case Modifiers.GlobalXP:
                    return GameData.GlobalMultiplier;
                case Modifiers.JobXP:
                    return GameData.JobXPModifier;
                case Modifiers.GameSpeed:
                    return GameData.GameSpeedModifier;
                case Modifiers.ResearchSpeed:
                    return GameData.ResearchXPModifier;
                default:
                    return 1f;
            }
        }
        public int GetResearchXPGain()
        {
            return (int)((GetBaseXPGain() + GetGlobalLevels()) * GetResearchXpModifier() * GameData.GlobalMultiplier);
        }
        #endregion
        #region UPDATES
        public void SetActiveResearch(ResearchProject research)
        {
            GameData.ActiveResearch = research;
            foreach (ResearchProject j in GameData.ResearchList)
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
            GameData.NextResearchUpgrade = research;
        }
        #endregion
        #endregion

        #region Upgrades
        #region CREATE
        private void CreateUpgrades()
        {
            GameData.UpgradeOne = new UpgradeOne(this);
            GameData.UpgradeList.Add(GameData.UpgradeOne);

            GameData.UpgradeTwo = new UpgradeTwo(this);
            GameData.UpgradeList.Add(GameData.UpgradeTwo);

            GameData.UpgradeThree = new UpgradeThree(this);
            GameData.UpgradeList.Add(GameData.UpgradeThree);

            GameData.UpgradeFour = new UpgradeFour(this);
            GameData.UpgradeList.Add(GameData.UpgradeFour);
        }
        #endregion
        #region GETS
        public Upgrade GetNextUpgrade()
        {
            return GameData.NextUpgrade;
        }
        public float GetUpgradeModifier(Upgrade upgrade)
        {
            return upgrade.Multiplier;
        }
        #endregion
        #region UPDATES
        public void SetNextUpgrade(Upgrade upgrades)
        {
            GameData.NextUpgrade = upgrades;
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
            GameData.JobTwo.Unlocked = GameData.JobTwo.CanUnlock(this);

            GameData.JobThree.Unlocked = GameData.JobThree.CanUnlock(this);

            GameData.JobFour.Unlocked = GameData.JobFour.CanUnlock(this);

            GameData.JobFive.Unlocked = GameData.JobFive.CanUnlock(this);

            GameData.JobSix.Unlocked = GameData.JobSix.CanUnlock(this);
        }
        public void UnlockResearch()
        {
            // Checks InputResponse for required level
            GameData.ResearchTwo.Unlocked = GameData.ResearchTwo.CanUnlock(this);

            GameData.ResearchThree.Unlocked = GameData.ResearchThree.CanUnlock(this);

            GameData.NoviceResearchOne.Unlocked = GameData.NoviceResearchOne.CanUnlock(this);

            GameData.NoviceResearchTwo.Unlocked = GameData.NoviceResearchTwo.CanUnlock(this);
        }
        public void UnlockUpgrades()
        {
            GameData.UpgradeOne.Unlocked = GameData.UpgradeOne.CanUnlock(this);
            GameData.UpgradeTwo.Unlocked = GameData.UpgradeTwo.CanUnlock(this);
            GameData.UpgradeThree.Unlocked = GameData.UpgradeThree.CanUnlock(this);
            GameData.UpgradeFour.Unlocked = GameData.UpgradeFour.CanUnlock(this);

            if (GameData.UpgradeOne.CanUnlock(this))
            {
                GameData.UpgradesUnlocked = true;
            }
        }
        public void UnlockHacking()
        {
            if(GameData.UpgradeFour.Unlocked && GameData.NoviceResearchOne.Unlocked)
            {
                GameData.HackingUnlocked = true;
            }
        }
        #endregion
    }
}
