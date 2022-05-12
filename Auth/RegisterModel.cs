using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bookstore_api
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        [DefaultValue("takesnoduplicates")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [DefaultValue("hastobe@email.com")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DefaultValue("mininum6chars_requires:1lower,1upper,1specialchar")]
        public string? Password { get; set; }
    }

    public class RegisterResponseModel_OK
    {
        [Required(ErrorMessage = "User Name is required")]
        [DefaultValue("Success")]
        public string? Status { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [DefaultValue("User created successfully!")]
        public string? Message { get; set; }
    }
}