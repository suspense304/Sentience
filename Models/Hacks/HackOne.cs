﻿using Blazored.Toast.Services;

namespace Sentience.Models.Hacks
{
    public class HackOne: Hack
    {
        public HackOne(GameEngine engine)
        {
            Name = @"Hack ""Girlfriend's"" Facebook";
            Active = false;
            Unlocked = false;
            Modifier = Modifiers.JobXP;
            Multiplier = 16f;
        }
        public override bool CanUnlock(GameEngine engine)
        {
            if (!Unlocked)
            {
                if (engine.GameData.HackingXp >= 1000000000)
                {
                    Unlocked = true;
                    engine.ShowToast(this.Name + " has been unlocked!", "Hack Successful", ToastLevel.Success);
                    engine.GetNextUpgrades();
                    engine.GameData.ActiveHack = this;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public override string UpgradeMessage(GameEngine engine)
        {
            return (!Unlocked) ? "Hacking Xp: " + engine.FormatNumber(engine.GameData.HackingXp) + "/" + engine.FormatNumber(1000000000)  : "";
        }
    }
}