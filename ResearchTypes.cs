using System.ComponentModel.DataAnnotations;

namespace Sentience
{
    public enum ResearchTypes
    {
        [Display(Name = "Fundamentals")]
        Fundamentals,
        [Display(Name = "Novice")]
        Novice,
        [Display(Name = "Expert")]
        Expert,
        [Display(Name = "Prodigy")]
        Prodigy,
        [Display(Name = "Theoretical")]
        Theoretical,
        [Display(Name = "The Machines Are Taking Over")]
        MachinesTakingOver
    }
}
