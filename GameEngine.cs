using Sentience.Models;
using Sentience.Models.Jobs;
using Sentience.Models.Research;

namespace Sentience
{
    public class GameEngine
    {
        private ChatBot ChatBot { get; set; } = new ChatBot();
        private NeuralNetwork NeuralNetwork { get; set; } = new NeuralNetwork();
        private MachineLearning MachineLearning { get; set; } = new MachineLearning();

        private ProcessingPower ProcessingPower { get; set; } = new ProcessingPower();

        private float _money = 0;
        private int _moneyPerSecond = 0;

        private float _baseGameSpeed = 500f;
        private float _baseXPGain = 12f;
        private float _gameSpeed = 1f;
        private float _globalMultiplier = 1f;
        private float _incomeMultiplier = 1.15f;
        private float _upgradeMultiplier = 1.35f;

        private float _gameSpeedModifier = 1f;
        private float _jobXPModifier = 1f;
        private float _researchXPModifier = 1f;

        public List<Job> JobsList = new List<Job>();
        public List<Research> ResearchList = new List<Research>();
        public GameEngine()
        {
            CreateJobs();
            CreateResearch();
        }

        public float GetBaseXPGain()
        {
            return _baseXPGain;
        }

        public void SetUpgradeMultiplier(float value)
        {
            _upgradeMultiplier = value;
        }

        public float GetUpgradeMultiplier()
        {
            return _upgradeMultiplier;
        }

        #region Jobs

        public Job GetJob(Job job)
        {
            return JobsList.Where(w => w.Name == job.Name).First();
        }

        public float GetXPModifier()
        {
            return _jobXPModifier;
        }

        public Job CreateJob(Job job)
        {
            return job.Create();
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

        #region Research

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
                    break;
                case Modifiers.JobXP:
                    return _jobXPModifier;
                    break;
                case Modifiers.GameSpeed:
                    return _gameSpeedModifier;
                    break;
                default: 
                    return 1f;
            }
        }

        public Research CreateResearch(Research research)
        {
            return research.Create();
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


        #region Game Engine Variables

        public float GetJobXpModifier()
        {
            float newXp = 1f;
            foreach(Research research in ResearchList)
            {
                if(research.Modifier == Modifiers.JobXP)
                {
                    newXp += research.ModifierValue;
                }
            }
            return newXp;
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

        public void SetMoney(float value)
        {
            _money += (float)Math.Ceiling(value * 100) / 100;
        }

        public float GetMoney()
        {
            return _money;
        }

        public float GetIncomeMultiplier()
        {
            return _incomeMultiplier;
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

        public float GetGlobalMultiplier()
        {
            return _globalMultiplier;
        }

        public float GetGameSpeed()
        {
            return _baseGameSpeed / (_gameSpeed * _gameSpeedModifier);
        }

        public void SetGameSpeed(int value)
        {
            _gameSpeed = value;
        }

        #endregion
        // JOBS
        public int GetJobXPGain()
        {
            return (int)(GetBaseXPGain() * GetJobXpModifier() * _globalMultiplier);
        }
        void CreateJobs()
        {
            ChatBot = new ChatBot(this);
            ChatBot.Income = ChatBot.BaseIncome;
            JobsList.Add(ChatBot);

            NeuralNetwork = new NeuralNetwork(this);
            NeuralNetwork.Income = NeuralNetwork.BaseIncome;
            JobsList.Add(NeuralNetwork);

        }

        public void SetActiveJob(Job job)
        {
            foreach(Job j in JobsList)
            {
                if(j == job)
                {
                    j.Active = true;
                }
                else
                {
                    j.Active = false;
                }
            }
        }

        // Research
        public int GetResearchXPGain()
        {
            return (int)(GetBaseXPGain() * GetResearchXpModifier() * _globalMultiplier);
        }
        void CreateResearch()
        {
            ProcessingPower = new ProcessingPower(this);
            ResearchList.Add(ProcessingPower);

            MachineLearning = new MachineLearning(this);
            ResearchList.Add(MachineLearning);
        }
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


        // Unlock Requirements

        public void UnlockJobs()
        {
            NeuralNetwork.Unlocked = NeuralNetwork.CanUnlock(ChatBot);
        }

        public void UnlockResearch()
        {
            MachineLearning.Unlocked = MachineLearning.CanUnlock(ProcessingPower);
        }
    }
}
