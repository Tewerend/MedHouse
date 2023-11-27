using System.ComponentModel.DataAnnotations;

namespace MedHouse.ViewModels.Providers
{
    public class EditProviderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите поставщика")]
        [Display(Name = "Поставщик")]
        public string NameProvider { get; set; }
    }
}
