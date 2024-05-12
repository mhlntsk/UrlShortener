using System.ComponentModel.DataAnnotations;

namespace MVC.Models.AccountViewModels
{
    public class LoginViewModel
    {
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
