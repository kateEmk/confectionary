namespace Confectionery.ViewModels
{
    public class OrderControllViewModel
    {
        public AccountViewModel? User { get; set; }
        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
        public OrderViewModel? ChangeOrder { get; set; }

        public OrderControllViewModel(AccountViewModel? user, List<OrderViewModel> orders)
        {
            User = user;
            Orders = orders;
        }

        public OrderControllViewModel(AccountViewModel? user, List<OrderViewModel> orders, OrderViewModel? changeOrder) : this(user, orders)
        {
            ChangeOrder = changeOrder;
        }

        public OrderControllViewModel()
        {
        }
    }
}
