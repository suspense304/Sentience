﻿namespace Sentience.Models.Upgrades
{
    public class Upgrades
    {
        public bool Active;
        public bool Unlocked;
        public bool Purchased;
        public float Multiplier { get; set; }
        public float Expense { get; set; }
        public Modifiers Modifier { get; set; }
        public string Name { get; set; } = "";

        public Upgrades Create()
        {
            return new Upgrades();
        }

        public void PurchasedUpgrade()
        {
            Purchased = true;
        }
    }
    
}