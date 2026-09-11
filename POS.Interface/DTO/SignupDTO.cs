
using System.ComponentModel.DataAnnotations;

namespace POS.Interface.DTO
{
    public class SignupDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required] 
        public string PhoneNo { get; set; } = string.Empty;

    }
}
