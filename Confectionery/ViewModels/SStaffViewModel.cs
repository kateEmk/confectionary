using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Confectionery.Models
{
    public class SStaffViewModel
    {
        [Display(Name = "ID кондицерского изделия:")]
        [Required(ErrorMessage = "Вы не заполнили поле id unknown")]
        public int Id { get; set; }
        [Display(Name = "Имя кондицерского изделия:")]
        [Required(ErrorMessage = "Вы не заполнили поле имя кондицерского изделия:")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Длина пароля должна быть от 10 до 50 символов")]
        public string StaffName { get; set; }
        [Display(Name = "Классификация кондицерского изделия:")]
        [Required(ErrorMessage = "Вы не заполнили поле классификация кондицерского изделия")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Длина классификации должна быть от 10 до 50 символов")]
        public string Classification { get; set; }
        public float Price { get; set; }

        public SStaffViewModel(int id, string staffName, string classification, float price)
        {
            Id = id;
            StaffName = staffName;
            Classification = classification;
            Price = price;
        }
    }
}
