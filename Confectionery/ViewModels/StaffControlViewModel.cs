namespace Confectionery.ViewModels
{
    public class StaffControlViewModel
    {
        public SweetStaffViewModel? NewStaff { get; set; }
        public List<SweetStaffViewModel> Staffs { get; set; } = new List<SweetStaffViewModel>();
        public List<int> Companies { get; set; } = new List<int>();
        public AccountViewModel? User { get; set; }
        public StaffControlViewModel()
        {
        }
        public StaffControlViewModel(List<SweetStaffViewModel> staffsList, AccountViewModel user)
        {
            Staffs = staffsList;
            User = user;
        }

        public StaffControlViewModel(List<SweetStaffViewModel> staffs, List<int> company, AccountViewModel? user)
        {
            Staffs = staffs;
            Companies = company;
            User = user;
        }
    }
}
