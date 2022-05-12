using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bookstore_api
{
    public class ResetPasswordModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [DefaultValue("hastobe@email.com")]
        public string? Email { get; set; }

    }

    public class ResetPasswordModel_OK
    {
        [Required(ErrorMessage = "A valid email is required")]
        [DefaultValue("Success")]
        public string? Status { get; set; }


        [Required(ErrorMessage = "A valid email is required")]
        [DefaultValue("Success")]
        public string? Code { get; set; }


        [Required(ErrorMessage = "A valid email is required")]
        [DefaultValue("Success")]
        public string? Callback_URL { get; set; }


        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [DefaultValue("User created successfully!")]
        public string? Message { get; set; }
    }
}