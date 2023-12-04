using System.ComponentModel.DataAnnotations;

namespace MedHouse.ViewModels.MeasuringMedications
{
    public class CreateMeasuringMedicationViewModel
    {
        [Required(ErrorMessage = "Введите измерение лекартсва")]
        [Display(Name = "Измерение лекарства")]
        public string Measuring { get; set; }
    }
}
