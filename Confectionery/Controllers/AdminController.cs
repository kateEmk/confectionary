using Confectionery.Filters;
using Confectionery.Mapers;
using Confectionery.ViewModels;
using LibraryDatabaseCoffe.Models.DB.Request.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Confectionery.Controllers
{
    [TypeFilter(typeof(FilterAdmin))]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHost;
        public AdminController(ILogger<HomeController> logger, IConfiguration configuration,IWebHostEnvironment webHost)
        {
            _logger = logger;
            _configuration = configuration;
            _webHost = webHost;
        }
        public async Task<IActionResult> PanelCompany()
        {
            var CompanyRepository = HttpContext.RequestServices.GetService<CompanyRepository>();
            var userRepository = HttpContext.RequestServices.GetService<UserRepository>();
            var mapper = HttpContext.RequestServices.GetService<Mapsters>();

            var usersData = await userRepository.GetAsync(Convert.ToInt32(User.Claims.FirstOrDefault().Value));
            var user = mapper.MapToUserAccountView(usersData);

            var companiesDB = await CompanyRepository.GetAllAsync();
            List<CompanyViewModel> CompanyList = mapper.MapToViewCompanies(companiesDB.ToList());

            CompanyControlViewModel companyControlView = new CompanyControlViewModel(CompanyList,user);

            return View(companyControlView);
        }
        public async Task<IActionResult> AddCompany(CompanyControlViewModel companyControlView) 
        {
            var CompanyRepository = HttpContext.RequestServices.GetService<CompanyRepository>();
            var userRepository = HttpContext.RequestServices.GetService<UserRepository>();
            var mapper = HttpContext.RequestServices.GetService<Mapsters>();

            var usersData = await userRepository.GetAsync(Convert.ToInt32(User.Claims.FirstOrDefault().Value));
            var user = mapper.MapToUserAccountView(usersData);
            if (ModelState.IsValid)
            {
                var company = mapper.MapToCompany(companyControlView.NewCompany);
                await CompanyRepository.AddAsync(company);
            }
            else 
            {
                companyControlView.User = user;
                return View("PanelCompany", companyControlView);
            }

            var companiesDB = await CompanyRepository.GetAllAsync();
            List<CompanyViewModel> CompanyList = mapper.MapToViewCompanies(companiesDB.ToList());
            CompanyControlViewModel updateCompanyControlView = new CompanyControlViewModel(CompanyList,user);

            return Json(updateCompanyControlView);
        }
        [Route("/DeleteCompany/{IdDeleteCompany:int}")]
        public async Task<IActionResult> DeleteCompany(int IdDeleteCompany)
        {
            var CompanyRepository = HttpContext.RequestServices.GetService<CompanyRepository>();
            var userRepository = HttpContext.RequestServices.GetService<UserRepository>();
            var mapper = HttpContext.RequestServices.GetService<Mapsters>();

            await CompanyRepository.DeleteAsync(IdDeleteCompany);

            var usersData = await userRepository.GetAsync(Convert.ToInt32(User.Claims.FirstOrDefault().Value));
            var user = mapper.MapToUserAccountView(usersData);

            var companiesDB = await CompanyRepository.GetAllAsync();
            List<CompanyViewModel> CompanyList = mapper.MapToViewCompanies(companiesDB.ToList());
            CompanyControlViewModel updateCompanyControlView = new CompanyControlViewModel(CompanyList,user);

            return View("PanelCompany", updateCompanyControlView);
        }
        public async Task<IActionResult> ChangeCompany(CompanyControlViewModel companyControlView)
        {
            var CompanyRepository = HttpContext.RequestServices.GetService<CompanyRepository>();
            var userRepository = HttpContext.RequestServices.GetService<UserRepository>();
            var mapper = HttpContext.RequestServices.GetService<Mapsters>();

            var usersData = await userRepository.GetAsync(Convert.ToInt32(User.Claims.FirstOrDefault().Value));
            var user = mapper.MapToUserAccountView(usersData);

            if (ModelState.IsValid)
            {
                var company = mapper.MapToCompany(companyControlView.NewCompany);

                await CompanyRepository.UpdateAsync((int)company.CompanyId, company);
            }
            else 
            {
                var companiesOld = await CompanyRepository.GetAllAsync();
                List<CompanyViewModel> CompanyListOld = mapper.MapToViewCompanies(companiesOld.ToList());
                CompanyControlViewModel OldCompanyControlView = new CompanyControlViewModel(CompanyListOld,user);
                return View("PanelCompany", OldCompanyControlView);
            }

            var companiesDB = await CompanyRepository.GetAllAsync();
            List<CompanyViewModel> CompanyList = mapper.MapToViewCompanies(companiesDB.ToList());

            CompanyControlViewModel updateCompanyControlView = new CompanyControlViewModel(CompanyList,user);

            return View("PanelCompany", updateCompanyControlView);
        }
        public async Task<IActionResult> PanelSweetStaff()
        {
            var SweetStaffRepository = HttpContext.RequestServices.GetService<SweetStaffRepository>();
            var CompanyRepository = HttpContext.RequestServices.GetService<CompanyRepository>();
            var userRepository = HttpContext.RequestServices.GetService<UserRepository>();
            var mapper = HttpContext.RequestServices.GetService<Mapsters>();

            var usersData = await userRepository.GetAsync(Convert.ToInt32(User.Claims.FirstOrDefault().Value));
            var user = mapper.MapToUserAccountView(usersData);

            var idCompanies = await CompanyRepository.GetAllId();

            var SweetStaffsDB = await SweetStaffRepository.GetAllAsync();

            List<SweetStaffViewModel> SweetStaffList = mapper.MapToViewSweetStaffs(SweetStaffsDB.ToList());
            StaffControlViewModel SweetStaffControlView = new StaffControlViewModel(SweetStaffList,idCompanies.ToList(),user);

            return View("PanelSweetStaff",SweetStaffControlView);
        }
        [HttpPost]
        public async Task<IActionResult> AddSweetStaff(StaffControlViewModel staffControlView) 
        {
            var SweetStaffRepository = HttpContext.RequestServices.GetService<SweetStaffRepository>();
            var CompanyRepository = HttpContext.RequestServices.GetService<CompanyRepository>();
            var userRepository = HttpContext.RequestServices.GetService<UserRepository>();
            var mapper = HttpContext.RequestServices.GetService<Mapsters>();

            if (staffControlView.NewStaff is not null && ModelState.IsValid)
            {
                if ((await SweetStaffRepository.SelectByName(staffControlView.NewStaff.StaffName)).Count() == 0)
                {
                    var newStaff = mapper.MapToSweetStaff(staffControlView.NewStaff);
                    await SweetStaffRepository.AddAsync(newStaff);
                    if (staffControlView.NewStaff.Image is not null)
                    {
                        staffControlView.NewStaff.Image.SaveAsImage(_webHost.WebRootPath + FormFileExtensions.PathFolder + newStaff.StaffName.ToString());
                    }
                }
                else 
                {
                    ModelState.AddModelError("NewStaff.StaffName", "Десерт с таким названием уже существует");
                }
            }

            var usersData = await userRepository.GetAsync(Convert.ToInt32(User.Claims.FirstOrDefault().Value));
            var user = mapper.MapToUserAccountView(usersData);

            var idCompanies = await CompanyRepository.GetAllId();

            var SweetStaffsDB = await SweetStaffRepository.GetAllAsync();

            List<SweetStaffViewModel> SweetStaffList = mapper.MapToViewSweetStaffs(SweetStaffsDB.ToList());
            StaffControlViewModel SweetStaffControlView = new StaffControlViewModel(SweetStaffList, idCompanies.ToList(), user);

            return View("PanelSweetStaff", SweetStaffControlView);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeSweetStaff(StaffControlViewModel staffControlView)
        {
            var SweetStaffRepository = HttpContext.RequestServices.GetService<SweetStaffRepository>();
            var CompanyRepository = HttpContext.RequestServices.GetService<CompanyRepository>();
            var userRepository = HttpContext.RequestServices.GetService<UserRepository>();
            var mapper = HttpContext.RequestServices.GetService<Mapsters>();

            if (staffControlView.NewStaff is not null && ModelState.IsValid)
            {
                var Exists = await SweetStaffRepository.SelectByName(staffControlView.NewStaff.StaffName);
                var Existss = await SweetStaffRepository.SelectByNameAndId(staffControlView.NewStaff.StaffName,(int)staffControlView.NewStaff.StaffId);
                if (Exists.Count() == 0 || (Existss.Count() == 1 && Exists.Count() == 1))
                {
                    var newStaff = mapper.MapToSweetStaff(staffControlView.NewStaff);
                    await SweetStaffRepository.UpdateAsync((int)newStaff.StaffId, newStaff);
                    if (staffControlView.NewStaff.Image is not null)
                    {
                        staffControlView.NewStaff.Image.SaveAsImage(_webHost.WebRootPath + FormFileExtensions.PathFolder + newStaff.StaffName.ToString());
                    }
                }
                else 
                {
                    ModelState.AddModelError("NewStaff.StaffName", "Десерт с таким названием уже существует");
                }
            }

            var usersData = await userRepository.GetAsync(Convert.ToInt32(User.Claims.FirstOrDefault().Value));
            var user = mapper.MapToUserAccountView(usersData);

            var idCompanies = await CompanyRepository.GetAllId();

            var SweetStaffsDB = await SweetStaffRepository.GetAllAsync();

            List<SweetStaffViewModel> SweetStaffList = mapper.MapToViewSweetStaffs(SweetStaffsDB.ToList());
            StaffControlViewModel SweetStaffControlView = new StaffControlViewModel(SweetStaffList, idCompanies.ToList(), user);

            return View("PanelSweetStaff", SweetStaffControlView);
        }
        [Route("/DeleteSweetStaff/{IdDeleteStaff:int}")]
        public async Task<IActionResult> DeleteSweetStaff(int IdDeleteStaff)
        {
            var SweetStaffRepository = HttpContext.RequestServices.GetService<SweetStaffRepository>();
            var CompanyRepository = HttpContext.RequestServices.GetService<CompanyRepository>();
            var userRepository = HttpContext.RequestServices.GetService<UserRepository>();
            var mapper = HttpContext.RequestServices.GetService<Mapsters>();
            var DescpriptionRepository = HttpContext.RequestServices.GetService<DescriptionOrderRepository>();

            if (await DescpriptionRepository.Search(IdDeleteStaff))
            {
                await SweetStaffRepository.DeleteAsync(IdDeleteStaff);
            }

            var usersData = await userRepository.GetAsync(Convert.ToInt32(User.Claims.FirstOrDefault().Value));
            var user = mapper.MapToUserAccountView(usersData);

            var idCompanies = await CompanyRepository.GetAllId();

            var SweetStaffsDB = await SweetStaffRepository.GetAllAsync();

            List<SweetStaffViewModel> SweetStaffList = mapper.MapToViewSweetStaffs(SweetStaffsDB.ToList());
            StaffControlViewModel SweetStaffControlView = new StaffControlViewModel(SweetStaffList, idCompanies.ToList(), user);

            return View("PanelSweetStaff", SweetStaffControlView);
        }
        public async Task<IActionResult> PanelOrder()
        {
            var orderRepository = HttpContext.RequestServices.GetService<OrderRepository>();
            var userRepository = HttpContext.RequestServices.GetService<UserRepository>();
            var mapper = HttpContext.RequestServices.GetService<Mapsters>();

            var usersData = await userRepository.GetAsync(Convert.ToInt32(User.Claims.FirstOrDefault().Value));
            var user = mapper.MapToUserAccountView(usersData);

            var orders = await orderRepository.GetAllAsync();

            List<OrderViewModel> ordersView = mapper.MapToOrdersView(orders.ToList());
            OrderControllViewModel orderControllView = new OrderControllViewModel(user,ordersView);

            return View("PanelOrder", orderControllView);
        }
        public async Task<IActionResult> ChangeOrder(OrderControllViewModel orderControllView) 
        {
            var orderRepository = HttpContext.RequestServices.GetService<OrderRepository>();
            var userRepository = HttpContext.RequestServices.GetService<UserRepository>();
            var mapper = HttpContext.RequestServices.GetService<Mapsters>();

            if (ModelState.IsValid) 
            {
                var changeOrder = mapper.MapToOrder(orderControllView.ChangeOrder);
                await orderRepository.UpdateAsync((int)changeOrder.OrderId,changeOrder);
            }

            var usersData = await userRepository.GetAsync(Convert.ToInt32(User.Claims.FirstOrDefault().Value));
            var user = mapper.MapToUserAccountView(usersData);

            var orders = await orderRepository.GetAllAsync();

            List<OrderViewModel> ordersView = mapper.MapToOrdersView(orders.ToList());
            OrderControllViewModel updateOrderControllView = new OrderControllViewModel(user, ordersView);

            return View("PanelOrder", updateOrderControllView);
        }
    }
}
