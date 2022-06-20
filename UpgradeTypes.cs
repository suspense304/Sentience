using System.ComponentModel.DataAnnotations;

namespace Sentience
{
    public enum UpgradeTypes
    {
        [Display(Name = "Scrapyard Parts")]
        Scrapyard,
        [Display(Name = "Refurbished")]
        Refurbished,
        [Display(Name = "Previouse Generation")]
        PreviouseGeneration,
        [Display(Name = "Brand New")]
        BrandNew,
        [Display(Name = "Next Gen")]
        NextGen,
        [Display(Name = "Futuristic")]
        Futuristic
    }
}
