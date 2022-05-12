using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bookstore_api
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        [DefaultValue("developer")]
        public string? Username { get; set; }

        [DefaultValue("DEVb00tc@mp")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }

    public class LoginResponseModel_OK
    {
        [Required(ErrorMessage = "User Name is required")]
        [DefaultValue("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZGV2ZWxvcGVyIiwianRpIjoiNWMwNWMxYjAtYTg3OS00MTc3LTljNzktMDc4ZDU5MTJkN2Y0IiwiZXhwIjoxNjQ5OTI0OTI1LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.5aJDfxoRJJGNwXkI1t7w6sD6SN1oCT1xYyh7vmtij4k")]
        public string? Token { get; set; }

        [DefaultValue("2022-04-14T08:29:21Z")]
        [Required(ErrorMessage = "Password is required")]
        public DateTime? Expiration { get; set; }
    }
}