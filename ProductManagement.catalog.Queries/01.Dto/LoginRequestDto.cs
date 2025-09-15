using System.ComponentModel.DataAnnotations;

namespace Catalog.QueriesService._01.Dto
{
    public class LoginRequestDto
    {
        [Required()]
        public string Username { get; set; }

        [Required()]
        public string Password { get; set; }
    }
}
   
