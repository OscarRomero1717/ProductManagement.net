using System.ComponentModel.DataAnnotations;

namespace Catalog.QueriesService._01.Dto
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
   
