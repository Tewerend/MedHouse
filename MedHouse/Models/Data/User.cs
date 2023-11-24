using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MedHouse.Models.Data
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Введите фамилию")]

        //отображение Фамилия вместо LastName
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите дату регистрации")]
        [Display(Name = "Дата регистрации")]
        public DateTime Date_of_registarion {  get; set; }
        //навигационные свойства
    }
}
