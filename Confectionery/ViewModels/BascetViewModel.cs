using System.ComponentModel.DataAnnotations;

namespace Confectionery.ViewModels
{
    public class BascetViewModel
    {
        [Display(Name = "Колличетсво")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Вы не заполнили поле колличество")]
        [Range(1, 100, ErrorMessage = "Колличество можнт быть ровно от 1 до 100")]
        public int DesciptionId { get; set; }
        public int AmountSweetStaff { get; set; }
        public string StaffName { get; set; }
        public float Weight { get; set; }
        public float Price { get; set; }
        public float Calories { get; set; }
        [Required]
        public int IdStaff { get; set; }

        public BascetViewModel(int desciptionid, int amountSweetStaff, string staffName, float weight, float price, float calories, int idStaff)
        {
            DesciptionId = desciptionid;
            AmountSweetStaff = amountSweetStaff;
            StaffName = staffName;
            Weight = weight;
            Price = price;
            Calories = calories;
            IdStaff = idStaff;
        }
    }
}
