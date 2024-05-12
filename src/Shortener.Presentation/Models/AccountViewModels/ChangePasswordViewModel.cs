using System.ComponentModel.DataAnnotations;

namespace MVC.Models.AccountViewModels
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
    }
}
