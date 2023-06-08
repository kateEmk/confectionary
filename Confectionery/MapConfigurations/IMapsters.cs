using Confectionery.Models;
using Confectionery.ViewModels;
using LibraryDatabaseCoffe.Models.DB.Tables;
using Mapster;

namespace Confectionery.Mapers
{
    [Mapper]
    public interface IMapsters 
    {
        public SStaffViewModel MapToShortViewStaff(SweetStaff sweetStaff);
        public IEnumerable<SStaffViewModel> MapToShortViewStaffs(IEnumerable<SweetStaff> enumerable);
        public User MapToUser(RegistrationViewModel staffView);
        public AutorisationViewModel MapToViewAutorisationUser(User sweetStaff);
        public FStaffViewModel MapToFullViewStaff(SweetStaff sweetStaff);
        public BascetViewModel MapToViewBascet(DescriptionOrder sweetStaff);
        public IEnumerable<BascetViewModel> MapToViewBascets(IEnumerable<DescriptionOrder> sweetStaff);
        public UserViewModel MapToViewUserAccount(User user);
        public List<CompanyViewModel> MapToViewCompanies(List<Company> company);
        public Company MapToCompany(CompanyViewModel companyView);
        public List<SweetStaffViewModel> MapToViewSweetStaffs(List<SweetStaff> company);
        public SweetStaff MapToSweetStaff(SweetStaffViewModel companyView);
        public AccountViewModel MapToUserAccountView(User user);
        public List<OrderViewModel> MapToOrdersView(List<Order> orders);
        public OrderViewModel MapToOrderView(Order order);
        public Order MapToOrder(OrderViewModel order);
    }
}
