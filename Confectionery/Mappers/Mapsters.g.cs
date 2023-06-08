using System.Collections.Generic;
using System.Linq;
using Confectionery.Mapers;
using Confectionery.Models;
using Confectionery.ViewModels;
using LibraryDatabaseCoffe.Models.DB.Tables;

namespace Confectionery.Mapers
{
    public partial class Mapsters : IMapsters
    {
        public SStaffViewModel MapToShortViewStaff(SweetStaff x)
        {
            return new SStaffViewModel((int)x.StaffId, x.StaffName, x.Classification, x.Price);
        }
        public IEnumerable<SStaffViewModel> MapToShortViewStaffs(IEnumerable<SweetStaff> p1)
        {
            return p1 == null ? null : p1.Select<SweetStaff, SStaffViewModel>(funcMain1);
        }
        public User MapToUser(RegistrationViewModel x)
        {
            return new User(x.Name, x.Email, x.Password);
        }
        public AutorisationViewModel MapToViewAutorisationUser(User x)
        {
            return new AutorisationViewModel(x.EmailUser, x.Password);
        }
        public FStaffViewModel MapToFullViewStaff(SweetStaff x)
        {
            return new FStaffViewModel((int)x.StaffId, x.StaffName, x.DateDeliver, x.Weight, x.Price, x.Calories, x.Classification, x.Company == null ? "Undefind company" : x.Company.CompanyName);
        }
        public BascetViewModel MapToViewBascet(DescriptionOrder x)
        {
            return new BascetViewModel((int)x.DescriptionId, x.AmountSweetStaff, x.SweetStaff.StaffName, x.SweetStaff.Weight, x.SweetStaff.Price, x.SweetStaff.Calories, x.StaffId);
        }
        public IEnumerable<BascetViewModel> MapToViewBascets(IEnumerable<DescriptionOrder> p3)
        {
            return p3 == null ? null : p3.Select<DescriptionOrder, BascetViewModel>(funcMain2);
        }
        public UserViewModel MapToViewUserAccount(User x)
        {
            return new UserViewModel(new AccountViewModel(x.EmailUser, x.NameUser, x.TotalSpent), x.Orders != null ? x.Orders.Select<Order, OrderView>(funcMain3).ToList<OrderView>() : new List<OrderView>());
        }
        public List<CompanyViewModel> MapToViewCompanies(List<Company> p5)
        {
            if (p5 == null)
            {
                return null;
            }
            List<CompanyViewModel> result = new List<CompanyViewModel>(p5.Count);
            
            int i = 0;
            int len = p5.Count;
            
            while (i < len)
            {
                Company item = p5[i];
                result.Add(new CompanyViewModel(item.CompanyId, item.CompanyName, item.Owner, item.Telephone, item.BankingAccount));
                i++;
            }
            return result;
            
        }
        public Company MapToCompany(CompanyViewModel x)
        {
            return new Company(x.CompanyId, x.CompanyName, x.Owner, x.Telephone, x.BankingAccount);
        }
        public List<SweetStaffViewModel> MapToViewSweetStaffs(List<SweetStaff> p6)
        {
            if (p6 == null)
            {
                return null;
            }
            List<SweetStaffViewModel> result = new List<SweetStaffViewModel>(p6.Count);
            
            int i = 0;
            int len = p6.Count;
            
            while (i < len)
            {
                SweetStaff item = p6[i];
                result.Add(new SweetStaffViewModel(item.StaffId, item.StaffName, item.DateDeliver, item.Weight, item.Price, item.Calories, item.Classification, item.CompanyId));
                i++;
            }
            return result;
            
        }
        public SweetStaff MapToSweetStaff(SweetStaffViewModel x)
        {
            return new SweetStaff(x.StaffId, x.StaffName, x.DateDeliver, x.Weight, x.Price, x.Calories, x.Classification, x.CompanyId);
        }
        public AccountViewModel MapToUserAccountView(User x)
        {
            return new AccountViewModel(x.EmailUser, x.NameUser, x.TotalSpent);
        }
        public List<OrderViewModel> MapToOrdersView(List<Order> p7)
        {
            if (p7 == null)
            {
                return null;
            }
            List<OrderViewModel> result = new List<OrderViewModel>(p7.Count);
            
            int i = 0;
            int len = p7.Count;
            
            while (i < len)
            {
                Order item = p7[i];
                result.Add(new OrderViewModel(item.OrderId, item.UserId, item.DateOrder, item.Total, (short)item.StatusOrder));
                i++;
            }
            return result;
            
        }
        public OrderViewModel MapToOrderView(Order x)
        {
            return new OrderViewModel(x.OrderId, x.UserId, x.DateOrder, x.Total, (short)x.StatusOrder);
        }
        public Order MapToOrder(OrderViewModel x)
        {
            return new Order((int)x.OrderId, x.UserId, x.DateOrder, (float)x.Total, (short)x.StatusOrder);
        }
        
        private SStaffViewModel funcMain1(SweetStaff p2)
        {
            return new SStaffViewModel((int)p2.StaffId, p2.StaffName, p2.Classification, p2.Price);
        }
        
        private BascetViewModel funcMain2(DescriptionOrder p4)
        {
            return new BascetViewModel((int)p4.DescriptionId, p4.AmountSweetStaff, p4.SweetStaff.StaffName, p4.SweetStaff.Weight, p4.SweetStaff.Price, p4.SweetStaff.Calories, p4.StaffId);
        }
        
        private OrderView funcMain3(Order x)
        {
            return new OrderView(x.OrderId ?? -1, x.DateOrder, (float)x.Total, x.StatusOrder, x.DescriptionOrders != null ? x.DescriptionOrders.ToDictionary<DescriptionOrder, string, short>(funcMain4, funcMain5) : new Dictionary<string, short>());
        }
        
        private string funcMain4(DescriptionOrder u)
        {
            return u.SweetStaff.StaffName;
        }
        
        private short funcMain5(DescriptionOrder u)
        {
            return (short)u.AmountSweetStaff;
        }
    }
}