using System.ComponentModel.DataAnnotations;

namespace MedHouse.ViewModels.Providers
{
    public class CreateProviderViewModel
    {
        [Required(ErrorMessage = "Введите поставщика")]
        [Display(Name = "Поставщик")]
        public string NameProvider { get; set; }
    }
}
