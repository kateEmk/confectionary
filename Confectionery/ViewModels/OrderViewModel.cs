using LibraryDatabaseCoffe.Models.DB.Tables;
using System.ComponentModel.DataAnnotations;

namespace Confectionery.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        [Display(Name = "Номер заказа")]
        public int? OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Дата заказа")]
/*        [Range(typeof(DateTime), "1/1/2022", "10/10/2100",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]*/
        public DateTime DateOrder { get; set; }
        [Required]
        [Display(Name = "Общая сстоимость заказа")]
        [Range(0, 100000000)]
        public long Total { get; set; } = 0;
        [Required]
        [Display(Name = "Статус заказа")]
        [Range(-1,3)]
        public StatusOrder StatusOrder { get; set; } = StatusOrder.error;

        public OrderViewModel(int? orderId, int userId, DateTime dateOrder, long total, short statusOrder)
        {
            OrderId = orderId;
            UserId = userId;
            DateOrder = dateOrder;
            Total = total;
            StatusOrder = (StatusOrder)statusOrder;
        }

        public OrderViewModel()
        {
        }
        public string ToStringStatus()
        {
            switch (StatusOrder)
            {
                case StatusOrder.error: return "Некорректный заказ";
                case StatusOrder.expectation: return "Ожидание подтверждения";
                case StatusOrder.expecyation_get: return "Ожидания получения";
                case StatusOrder.success: return "Выполненный заказ";
                default: throw new Exception("Error");
            }
        }
    }
}
