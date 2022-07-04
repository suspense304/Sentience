using System.ComponentModel.DataAnnotations;

namespace Sentience
{
    public enum Modifiers
    {
        [Display(Name = "Job XP")]
        JobXP,
        [Display(Name = "Game Speed")]
        GameSpeed,
        [Display(Name = "Research Speed")]
        ResearchSpeed,
        [Display(Name = "Global XP")]
        GlobalXP,
        [Display(Name = "Income")]
        Income,
    }
}
