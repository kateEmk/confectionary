using System.ComponentModel.DataAnnotations;

namespace Confectionery.Models
{
    public class RegistrationViewModel
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Вы не заполнили поле Имя")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов")]
        public string Name { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вы не заполнили поле пароль")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Длина пароля должна быть от 10 до 50 символов")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Вы не заполнили поле пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Длина пароля должна быть от 10 до 50 символов")]
        public string RepeatPassword { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        [Required(ErrorMessage = "Вы не заполнили поле Email")]
        [RegularExpression(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", ErrorMessage = "Вы ввели некорректный Email")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Длина Email должна быть от 5  символов")]
        public string Email { get; set; }
        public RegistrationViewModel(string name, string password, string email, string repeatPassword)
        {
            Name = name;
            Password = password;
            Email = email;
            RepeatPassword = repeatPassword;
        }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public RegistrationViewModel()
        {
        }
    }
}
