using LibraryDatabaseCoffe.Models.DB.Tables;

namespace Confectionery.ViewModels
{
    public class AccountViewModel 
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public float Total { get; set; }

        public AccountViewModel(string email, string name, float total)
        {
            Email = email;
            Name = name;
            Total = total;
        }
    }
    public class OrderView
    {
        public int Number { get; set; }
        public DateTime DateGives { get; set; }
        public float TotalSpent { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public Dictionary<string, short> Staffs { get; set; }

        public OrderView(int number, DateTime dateGives, float totalSpent, StatusOrder statusOrder, Dictionary<string, short> staffs)
        {
            Number = number;
            DateGives = dateGives;
            TotalSpent = totalSpent;
            StatusOrder = statusOrder;
            Staffs = staffs;
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
    public class UserViewModel
    {
        public AccountViewModel AccountView { get; set; }
        public List<OrderView> Orders { get; set; }

        public UserViewModel(AccountViewModel accountView, List<OrderView> orders)
        {
            AccountView = accountView;
            Orders = orders;
        }
    }
}
