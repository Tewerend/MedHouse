using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedHouse.Models.Data
{
    public class UniqueStorage
    {
        // Key - поле первичный ключ
        // DatabaseGenerated(DatabaseGeneratedOption.Identity) - поле автоинкрементное
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название склада")]
        [Display(Name = "Название склада")]
        public string NameStorage { get; set; }

        [Required(ErrorMessage = "Введите адресс склада")]
        [Display(Name = "Адресс склада")]
        public string AdressStorage { get; set; }
    }
}
