using System.ComponentModel.DataAnnotations;

namespace MedHouse.ViewModels.UniqueStorages
{
    public class EditUniqueStorageViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название склада")]
        [Display(Name = "Название склада")]
        public string NameStorage { get; set; }

        [Required(ErrorMessage = "Введите адресс склада")]
        [Display(Name = "Адресс склада")]
        public string AdressStorage { get; set; }
    }
}
