using System.ComponentModel.DataAnnotations;

namespace MVC.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
