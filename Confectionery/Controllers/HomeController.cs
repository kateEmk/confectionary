using Confectionery.Mapers;
using Confectionery.Models;
using Confectionery.ViewModels;
using LibraryDatabaseCoffe.Models.DB.Request.Repositories;
using LibraryDatabaseCoffe.Models.DB.Tables;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Confectionery.Controllers
{

    public class HomeController : Controller 
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public FStaffViewModel? SweetStaffOrdered;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Main([FromQuery(Name = "productName")] string productName)
        {
            SweetStaffRepository sweetStaffRepository = HttpContext.RequestServices.GetService<SweetStaffRepository>() ?? throw new Exception("Exception Sweetstaff Repository");
            Mapsters mapstersStaff = HttpContext.RequestServices.GetService<Mapsters>() ?? throw new Exception("Exception MapstersStaff");
            if (productName is null)
            {
                var requesGetAllSweets = await sweetStaffRepository.GetAllAsync();
                return View(mapstersStaff.MapToShortViewStaffs(requesGetAllSweets).ToList());
            }
            var sweetsByName = await sweetStaffRepository.SearchByName(productName);
            return View(mapstersStaff.MapToShortViewStaffs(sweetsByName).ToList());

        }
        [HttpPost]
        public async Task<IActionResult> ProductAdd(DescriptionViewModel descriptionView) 
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                DescriptionOrderRepository orderRepository = HttpContext.RequestServices.GetService<DescriptionOrderRepository>() ?? throw new Exception("Exception DescriptionOrderRepository Repository");

                await orderRepository.AddAsync(new DescriptionOrder(Convert.ToInt32(descriptionView.Count), descriptionView.Id ?? -1, Convert.ToInt32((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) ?? new Claim(ClaimTypes.Upn, "-1")).Value)));

                return RedirectToAction("Main","Home");
            }
            else
            {
                return RedirectToAction("Main", "Home");
            }
        }
        [HttpGet]
        [Route("Home/{id:int}")]
        public async Task<IActionResult> Product(int id)
        {
            SweetStaffRepository sweetStaffRepository = HttpContext.RequestServices.GetService<SweetStaffRepository>() ?? throw new Exception("Exception Sweetstaff Repository");
            Mapsters mapstersStaff = HttpContext.RequestServices.GetService<Mapsters>() ?? throw new Exception("Exception MapstersStaff");

            var productsJoin = await sweetStaffRepository.GetFullAsync(id);

            FStaffViewModel? product = mapstersStaff.MapToFullViewStaff(productsJoin.FirstOrDefault());
            SweetStaffOrdered = product;
            DescriptionViewModel orderView = new DescriptionViewModel(product);

            return View(orderView);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}