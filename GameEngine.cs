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
        private CheapVM CheapVM { get; set; } = new CheapVM();
        private NeuralNetwork NeuralNetwork { get; set; } = new NeuralNetwork();
        private MachineLearning MachineLearning { get; set; } = new MachineLearning();
        private ProcessingPower ProcessingPower { get; set; } = new ProcessingPower();

        private float _money = 0;
        private float _dailyIncome = 0;
        private float _expenses = 0;

        private float _baseGameSpeed = 500f;
        private float _baseXPGain = 12f;
        private float _gameSpeed = 1f;
        private float _globalMultiplier = 1f;
        private float _incomeMultiplier = 1.15f;
        private float _upgradeMultiplier = 1.07f;

        private float _gameSpeedModifier = 1f;
        private float _jobXPModifier = 1f;
        private float _researchXPModifier = 1f;

        public List<Job> JobsList = new List<Job>();
        public List<Research> ResearchList = new List<Research>();
        public List<Upgrades> UpgradeList = new List<Upgrades>();

        private Timer _GameTimer { get; set; }

        // I WANT TO EVENTUALLY MOVE THE ENTIRE GAMES TIMER SYSTEM TO THE GAME ENGINE
        // CURRENTLY I AM NOT SURE HOW TO UPDATE THE COMPONENTS FROM THIS CLASS
        // THIS CODE IS CURRENTLY NOT USED
        #region GAME ENGINE TIMER


        //private Timer CreateGameTimer()
        //{
        //    float gameSpeed = GetGameSpeed();
        //    Timer newTimer = new Timer(gameSpeed);
        //    newTimer.Elapsed += new ElapsedEventHandler(RunDailyActions);
        //    newTimer.Enabled = true;
        //    newTimer.AutoReset = true;
        //    return new Timer(gameSpeed);
        //}

        //public Timer GetGameTimer()
        //{
        //    return _GameTimer;
        //}

        //private void RunDailyActions(object source, ElapsedEventArgs e)
        //{
        //    SetMoney();
        //}

        #endregion
        public GameEngine()
        {
            CreateJobs();
            CreateResearch();
            CreateUpgrades();
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
                foreach (Research research in ResearchList)
                {
                    if (research.Modifier == Modifiers.JobXP)
                    {
                        newXp += research.ModifierValue;
                    }
                }
                foreach (Upgrades upgrade in UpgradeList)
                {
                    if (upgrade.Modifier == Modifiers.JobXP && upgrade.Active)
                    {
                        newXp += upgrade.Multiplier;
                    }
                }

            return newXp;
            }
            public float GetMoney()
            {
                return _money;
            }
            public float GetResearchXpModifier()
            {
                float newXp = 1f;
                foreach (Research research in ResearchList)
                {
                    if (research.Modifier == Modifiers.ResearchSpeed)
                    {
                        newXp += research.ModifierValue;
                    }
                }
                return newXp;
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
            }
            public void SetGlobalMulitplier()
            {
                float newXp = 1f;
                foreach (Research research in ResearchList)
                {
                    if (research.Modifier == Modifiers.GlobalXP)
                    {
                        newXp += research.ModifierValue;
                    }
                }
                _globalMultiplier = newXp;
            }
            public void SetExpenses()
            {
                float newExpenses = 0;
                foreach (Upgrades upgrade in UpgradeList)
                {
                    if (upgrade.Active)
                    {
                        newExpenses += upgrade.Expense;
                    }
                }
                _expenses = newExpenses;
            }
            public void SetGameSpeed(int value)
            {
                _gameSpeed = value;
            }
            public void SetMoney()
            {
                _money += GetIncome();
            }
            public void SetUpgradeMultiplier(float value)
            {
                _upgradeMultiplier = value;
            }
        #endregion
        #endregion

        #region Jobs
            #region CREATE
                public Job CreateJob(Job job)
                {
                    return job.Create();
                }
                private void CreateJobs()
                {
                    ChatBot = new ChatBot(this);
                    ChatBot.Income = ChatBot.BaseIncome;
                    JobsList.Add(ChatBot);

                    NeuralNetwork = new NeuralNetwork(this);
                    NeuralNetwork.Income = NeuralNetwork.BaseIncome;
                    JobsList.Add(NeuralNetwork);

                }
            #endregion
            #region GETS
                public Job GetJob(Job job)
                    {
                        return JobsList.Where(w => w.Name == job.Name).First();
                    }
                public int GetJobXPGain()
                {
                    return (int)(GetBaseXPGain() * GetJobXpModifier() * _globalMultiplier);
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
                        case "Neural Network":
                            NeuralNetwork = (NeuralNetwork)job;
                            break;
                    }
                }
            #endregion
        #endregion

        #region Research
            #region CREATE
                public Research CreateResearch(Research research)
                {
                    return research.Create();
                }
                private void CreateResearch()
                {
                    ProcessingPower = new ProcessingPower(this);
                    ResearchList.Add(ProcessingPower);

                    MachineLearning = new MachineLearning(this);
                    ResearchList.Add(MachineLearning);
                }
            #endregion
            #region GETS
                public Research GetResearch(Research research)
                {
                    return ResearchList.Where(w => w.Name == research.Name).First();
                }
                public float GetResearchModifier(Research research)
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
                    return (int)(GetBaseXPGain() * GetResearchXpModifier() * _globalMultiplier);
                }
            #endregion
            #region UPDATES
                public void SetActiveResearch(Research research)
                {
                    foreach (Research j in ResearchList)
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
                public void UpdateResearch(Research research)
                {
                    switch (research.Name)
                    {
                        case "Processing Power":
                            ProcessingPower = (ProcessingPower)research;
                            break;
                    }
                }
        #endregion
        public void ApplyResearchModifiers()
        {
            float newXP = 1f;
            float newGameSpeed = 1f;
            foreach (Research research in ResearchList)
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
            public Upgrades CreateUpgrade(Upgrades upgrade)
            {
                return upgrade.Create();
            }
            private void CreateUpgrades()
            {
                CheapVM = new CheapVM(this);
                UpgradeList.Add(CheapVM);
            }
        #endregion
            #region GETS
        public Upgrades GetUpgrade(Upgrades upgrade)
        {
            return UpgradeList.Where(w => w.Name == upgrade.Name).First();
        }
        public float GetUpgradeModifier(Upgrades upgrade)
        {
            switch (upgrade.Modifier)
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
        #endregion
        #region UPDATES
        public void ToggleActiveUpgrade(Upgrades upgrade)
        {
            upgrade.Active = !upgrade.Active;
        }
        #endregion
        #endregion

        #region Unlock Requirements
        public void UnlockJobs()
        {
            NeuralNetwork.Unlocked = NeuralNetwork.CanUnlock(ChatBot);
        }
        public void UnlockResearch()
        {
            MachineLearning.Unlocked = MachineLearning.CanUnlock(ProcessingPower);
        }
        public void UnlockUpgrades()
        {
            CheapVM.Unlocked = CheapVM.CanUnlock(this);
        }
        #endregion
    }
}
