using System.ComponentModel.DataAnnotations;


namespace POS.Interface.DTO
{
    public class LoginReqDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
    public class LoginResDTO
    {

        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
     
    }
}
