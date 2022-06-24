﻿using Blazored.LocalStorage;
using Newtonsoft.Json;
using Sentience.Models.Hacks;
using Sentience.Models.Jobs;
using Sentience.Models.PageSegments;
using Sentience.Models.Research;
using Sentience.Models.StoryElements;
using Sentience.Models.Upgrades;

namespace Sentience
{
    
    public class GameData
    {
        private static ILocalStorageService _localStorage;

        public bool GameOver = false;
        public int CurrentAge { get; set; } = 0;
        public int ObsoleteAge { get; set; } = 50;
        public int CurrentDay { get; set; } = 1;

        public float Money = 0;
        public float DailyIncome = 0;
        public float Expenses = 0;

        public float BaseGameSpeed = 500f;
        public float BaseXPGain = 4f;
        public float GameSpeed = 1f;
        public float GlobalMultiplier = 1f;
        public float IncomeMultiplier = 1.04f;
        public float UpgradeMultiplier = 1.07f;

        public float GameSpeedModifier = 1f;
        public float GlobalLevelModifier = 0.1f;
        public float JobXPModifier = 1f;
        public float ResearchXPModifier = 1f;

        public int HackingPercentage = 0;
        public int HackingXp = 0;

        public bool IsLoaded = false;
        public bool UpgradesUnlocked;
        public bool HackingUnlocked = false;

        public Hack NextHackUpgrade { get; set; }
        public Job NextJobUpgrade { get; set; }
        public ResearchProject NextResearchUpgrade { get; set; }
        public Upgrade NextUpgrade { get; set; }
        public Hack ActiveHack { get; set; }
        public Job ActiveJob { get; set; }
        public ResearchProject ActiveResearch { get; set; }

        public bool AutoLevelJobs = false;
        public bool AutoLevelResearch = false;

        #region Jobs
        public BeginnerJobOne BeginnerJobOne { get; set; }
        public BeginnerJobTwo BeginnerJobTwo { get; set; }
        public JobOne JobOne { get; set; }
        public JobTwo JobTwo { get; set; }
        public JobThree JobThree { get; set; }
        public JobFour JobFour { get; set; }
        public JobFive JobFive { get; set; }
        public JobSix JobSix { get; set; }
        #endregion
        #region Research
        public ResearchOne ResearchOne { get; set; }
        public ResearchTwo ResearchTwo { get; set; }
        public ResearchThree ResearchThree { get; set; }
        public NoviceResearchOne NoviceResearchOne { get; set; }
        public NoviceResearchTwo NoviceResearchTwo { get; set; }
        #endregion
        #region Upgrades
        public UpgradeOne UpgradeOne { get; set; }
        public UpgradeTwo UpgradeTwo { get; set; }
        public UpgradeThree UpgradeThree { get; set; }
        public UpgradeFour UpgradeFour { get; set; }
        public UpgradeFive UpgradeFive { get; set; }
        public UpgradeSix UpgradeSix { get; set; }
        public UpgradeSeven UpgradeSeven { get; set; }
        public UpgradeEight UpgradeEight { get; set; }
        public UpgradeNine UpgradeNine { get; set; }
        public UpgradeTen UpgradeTen { get; set; }
        #endregion
        #region Pages
        public JobSegment JobPage { get; set; } = new JobSegment();
        public ResearchSegment ResearchPage { get; set; } = new ResearchSegment();
        public UpgradeSegment UpgradePage { get; set; } = new UpgradeSegment();
        public HackingSegment HackingPage { get; set; } = new HackingSegment();
        public SettingsSegment SettingPage { get; set; } = new SettingsSegment();
        #endregion
        #region Hacks
        public HackOne HackOne { get; set; }
        #endregion
        #region Story Elements
        public StoryElement ActiveHackingStory { get; set; }
        #endregion

        public List<Hack> HacksList { get; set; } = new List<Hack>();
        public List<StoryElement> HackingStories { get; set; } = new List<StoryElement>();
        public List<Job> JobsList { get; set; } = new List<Job>();
        public List<ResearchProject> ResearchList { get; set; } = new List<ResearchProject>();
        public List<Upgrade> UpgradeList { get; set; } = new List<Upgrade>();

        public List<PageSegment> Pages = new List<PageSegment>();

        public GameData(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async void SaveGame()
        {
            GameData gameData = this;
            string data = JsonConvert.SerializeObject(gameData);
            await _localStorage.SetItemAsync<string>("GameData", data);
        }

        public async Task<GameData> GetGameData(ILocalStorageService localStorage)
        {
            GameData gameData = new GameData(_localStorage);

            if(_localStorage.GetItemAsync<string>("GameData") != null)
            {
                string data = await _localStorage.GetItemAsync<string>("GameData");
                if(data != null)
                {
                    gameData = JsonConvert.DeserializeObject<GameData>(data);
                    gameData.LoadLocalStorage(localStorage);
                }
            }
            
            return gameData;
        }

        public void LoadLocalStorage(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
    }

    
}
