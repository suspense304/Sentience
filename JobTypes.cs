using System.ComponentModel.DataAnnotations;

namespace Sentience
{
    public enum JobTypes
    {
        [Display(Name = "The Basics")]
        Basics,
        [Display(Name = "Amatuer")]
        Amatuer,
        [Display(Name = "Novice")]
        Novice,
        [Display(Name = "Expert")]
        Expert,
        [Display(Name = "Super Human")]
        SuperHuman,
        [Display(Name = "Sentient")]
        Sentient
    }
}
