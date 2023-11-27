using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MedHouse.Models.Data
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите дату регистрации")]
        [Display(Name = "Дата регистрации")]
        public DateTime DateRegistarion {  get; set; }


        //навигационные свойства
    }
}
