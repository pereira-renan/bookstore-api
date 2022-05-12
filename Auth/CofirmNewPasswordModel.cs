using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bookstore_api
{
    public class ConfirmNewPasswordModel
    {
        [Required(ErrorMessage = "A valid user is required")]
        [DefaultValue("Success")]
        public string? User { get; set; }

        [Required(ErrorMessage = "A valid code is required")]
        [DefaultValue("Success")]
        public string? Code { get; set; }

        [Required(ErrorMessage = "A valid password is required")]
        [DefaultValue("Success")]
        public string? NewPassword { get; set; }
    }

    public class ConfirmNewPasswordModel_OK
    {
        [DefaultValue("Success")]
        public string? Status { get; set; }

        [EmailAddress]
        [DefaultValue("Password reset successfully!")]
        public string? Message { get; set; }
    }

}