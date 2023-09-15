using System.ComponentModel.DataAnnotations;

namespace WebApiBasicAuth.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email boş geçilemez!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez!")]
        public string Password { get; set; }
    }
}
