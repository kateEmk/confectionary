using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Confectionery.ViewModels
{
    public class SweetStaffViewModel
    {
        public int? StaffId { get; set; }
        [Display(Name = "Название десерта")]
        [Required(ErrorMessage = "Вы не заполнили поле название десерта")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина названия десерта должна быть от 2 до 50 символов")]
        public string StaffName { get; set; }

        [Display(Name = "Дата поступления десерта")]
        public DateTime DateDeliver { get; set; } = DateTime.Now;
        [Display(Name = "Вес десерта")]
        [Required(ErrorMessage = "Вы не заполнили вес десерта")]
        [Range(0,1000000, ErrorMessage = "Вы ввели некорректный вес(от 0 до 1.000.000)")]
        public float Weight { get; set; }
        [Display(Name = "Цена десерта")]
        [Required(ErrorMessage = "Вы не заполнили цену десерта")]
        [Range(0,10000000,ErrorMessage = "Вы ввели некорректную цену(от 0 до 1.000.000)")]
        public float Price { get; set; }
        [Display(Name = "Калорийность десертаы")]
        [Required(ErrorMessage = "Вы не заполнили калорийность десерта")]
        [Range(0, 10000000, ErrorMessage = "Вы ввели некорректную калорийность(от 0 до 1.000.000)")]
        public float Calories { get; set; }
        [Display(Name = "Классификация десерта")]
        [Required(ErrorMessage = "Вы не заполнили поле тип десерта")]
        [StringLength(maximumLength: 25,MinimumLength = 4,ErrorMessage = "Длина классификаци должна быть от 4 до 25 символов")]
        public string Classification { get; set; }
        [Display(Name = "Id привязанной компании")]
        public int? CompanyId { get; set; }
        [Display (Name = "Выберите изображение")]
        public IFormFile? Image { get; set; }

        public SweetStaffViewModel()
        {
        }

        public SweetStaffViewModel(int? staffId, string staffName, DateTime dateDeliver, float weight, float price, float calories, string classification)
        {
            StaffId = staffId;
            StaffName = staffName;
            DateDeliver = dateDeliver;
            Weight = weight;
            Price = price;
            Calories = calories;
            Classification = classification;
        }

        public SweetStaffViewModel(int? staffId, string staffName, DateTime dateDeliver, float weight, float price, float calories, string classification, int? companyId) : this(staffId, staffName, dateDeliver, weight, price, calories, classification)
        {
            CompanyId = companyId;
        }

        public SweetStaffViewModel(int? staffId, string staffName, DateTime dateDeliver, float weight, float price, float calories, string classification, int? companyId, IFormFile image) : this(staffId, staffName, dateDeliver, weight, price, calories, classification, companyId)
        {
            Image = image;
        }
    }
}
