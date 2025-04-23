using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Catalog.Model.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public IList<string> Roles { get; set; }
    }
}