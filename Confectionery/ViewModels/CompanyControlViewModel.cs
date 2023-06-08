using System.ComponentModel.DataAnnotations;

namespace Confectionery.ViewModels
{
    public class CompanyViewModel
    {
        public int? CompanyId { get; set; }
        [Display(Name = "Название компании")]
        [Required(ErrorMessage = "Вы не заполнили поле название компании")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов")]
        public string CompanyName { get; set; }
        [Display(Name = "Имя владельца")]
        [Required(ErrorMessage = "Вы не заполнили поле имя владельца")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени владельца должна быть от 2 до 50 символов")]
        public string Owner { get; set; }
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Вы не заполнили поле номер телефона")]
        [RegularExpression(@"^\s*\+?375((33\d{7})|(29\d{7})|(44\d{7}|)|(25\d{7}))\s*$", ErrorMessage = "Вы ввели некорректный телефон")]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "Вы не заполнили поле номер телефона")]
        [Display(Name = "Банковский счет")]
        [Range(100000000, 1000000000000, ErrorMessage = "Длина банковского счета должна быть от 10 до 25 символов")]
        public long BankingAccount { get; set; }

        public CompanyViewModel(int? companyId, string companyName, string owner, string telephone, long bankingAccount)
        {
            CompanyId = companyId;
            CompanyName = companyName;
            Owner = owner;
            Telephone = telephone;
            BankingAccount = bankingAccount;
        }

        public CompanyViewModel()
        {
        }
    }
    public class CompanyControlViewModel
    {
        public CompanyViewModel? NewCompany { get; set; }
        public List<CompanyViewModel> CompanyList { get; set; } = new List<CompanyViewModel>();
        public AccountViewModel? User { get; set; }

        public CompanyControlViewModel()
        {
        }

        public CompanyControlViewModel(List<CompanyViewModel> companyList, AccountViewModel user)
        {
            CompanyList = companyList;
            User = user;
        }
    }
}
