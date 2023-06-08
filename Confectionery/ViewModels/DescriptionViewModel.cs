using System.ComponentModel.DataAnnotations;

namespace Confectionery.ViewModels
{
    public class DescriptionViewModel
    {
        public int? Id { get; set; }
        public FStaffViewModel Staff { get; set; }
        [Display(Name = "Колличество")]
        [Required(ErrorMessage = "Вы не заполнили поле колличество")]
        [Range(1,100,ErrorMessage ="Введите корректное корректное колличетсво от 1 до 100")]
        public int Count { get; set; }
        public DescriptionViewModel(FStaffViewModel staff, int count = 1)
        {
            Staff = staff;
            Count = count;
        }

        public DescriptionViewModel(int count, FStaffViewModel id)
        {
            Count = count;
            Id = id.StaffId;
        }

        public DescriptionViewModel()
        {
        }
    }
}
