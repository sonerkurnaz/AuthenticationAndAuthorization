using System.ComponentModel.DataAnnotations;

namespace AuthenticationAndAuthorization.Models.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username alanı zorunludur.")]
        [Display(Name = "User Name")]
        [MinLength(2, ErrorMessage = "Username alanı en az 2 karakter olmalıdır")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "email adresini yanlıs girdiniz")]
        [Display(Name = "Email Adress")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "TcKimlik No")]
        public string TcNo { get; set; }

    }
}
