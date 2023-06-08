using Confectionery.Models;
using Confectionery.ViewModels;
using LibraryDatabaseCoffe.Models.DB.Tables;
using Mapster;

namespace Confectionery.Mappers
{
    public class RegistrationMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SweetStaff, SStaffViewModel>()
                  .MapWith(x => new SStaffViewModel((int)x.StaffId,x.StaffName,x.Classification,x.Price))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<RegistrationViewModel, User>()
                  .MapWith(x => new User(x.Name, x.Email, x.Password))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<User, AutorisationViewModel>()
                  .MapWith(x => new AutorisationViewModel(x.EmailUser, x.Password))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<SweetStaff, FStaffViewModel>()
                  .MapWith(x => new FStaffViewModel((int)x.StaffId,x.StaffName, x.DateDeliver, x.Weight, x.Price, x.Calories, x.Classification, x.Company == null ? "Undefind company" :  x.Company.CompanyName ))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<DescriptionOrder, BascetViewModel>()
                  .MapWith(x => new BascetViewModel((int)x.DescriptionId,x.AmountSweetStaff, x.SweetStaff.StaffName, x.SweetStaff.Weight, x.SweetStaff.Price, x.SweetStaff.Calories,x.StaffId))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<User, UserViewModel>()
                  .IgnoreNullValues(true)
                  .IgnoreIf((x,d) => x.Orders.Count == 0,x => x.Orders)
                  .MapWith(x => new UserViewModel(new AccountViewModel(x.EmailUser, x.NameUser, x.TotalSpent),
                                                  x.Orders != null ?
                                                      x.Orders.Select(x => new OrderView(x.OrderId ?? -1, x.DateOrder, x.Total, x.StatusOrder, 
                                                      x.DescriptionOrders != null ?
                                                        x.DescriptionOrders.ToDictionary(u => u.SweetStaff.StaffName, u => (short)u.AmountSweetStaff)
                                                      :new Dictionary<string, short>())).ToList() 
                                                  : new List<OrderView>()))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<Company, CompanyViewModel>()
                  .MapWith(x => new CompanyViewModel(x.CompanyId, x.CompanyName, x.Owner, x.Telephone, x.BankingAccount))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<CompanyViewModel, Company>()
                  .MapWith(x => new Company(x.CompanyId, x.CompanyName, x.Owner, x.Telephone, x.BankingAccount))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<SweetStaffViewModel, SweetStaff>()
                  .MapWith(x => new SweetStaff(x.StaffId,x.StaffName,x.DateDeliver,x.Weight,x.Price,x.Calories,x.Classification,x.CompanyId))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<SweetStaff, SweetStaffViewModel > ()
                  .MapWith(x => new SweetStaffViewModel(x.StaffId, x.StaffName, x.DateDeliver, x.Weight, x.Price, x.Calories, x.Classification,x.CompanyId))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<User, AccountViewModel>()
                  .MapWith(x => new AccountViewModel(x.EmailUser,x.NameUser,x.TotalSpent))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<Order, OrderViewModel>()
                  .MapWith(x => new OrderViewModel(x.OrderId,x.UserId,x.DateOrder,x.Total,(short)x.StatusOrder))
                  .RequireDestinationMemberSource(true);

            config.NewConfig<OrderViewModel, Order>()
                  .MapWith(x => new Order((int)x.OrderId, x.UserId, x.DateOrder, x.Total, (short)x.StatusOrder))
                  .RequireDestinationMemberSource(true);
        }   
    }
}

