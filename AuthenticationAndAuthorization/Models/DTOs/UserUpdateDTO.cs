using AuthenticationAndAuthorization.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAndAuthorization.Models.DTOs
{
    public class UserUpdateDTO
    {

        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "User Name")]
        [MinLength(2, ErrorMessage = "En Az 2 karakter olmalidir")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre alani Zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Email alani Zorunludur")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email formati dogru degil")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public UserUpdateDTO()
        {

        }
        public UserUpdateDTO(AppUser user)
        {
            UserName = user.UserName;
            Password = user.PasswordHash;
            Email = user.Email;
        }

    }
}