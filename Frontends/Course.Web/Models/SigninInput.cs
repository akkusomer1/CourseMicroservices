using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Course.Web.Models
{
    public class SigninInput
    {
        [Required]
        [Display(Name = "Email adresiniz")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Şifreniz")]
        public string Password { get; set; }

        [Display(Name = "Beni hatırla")]
        [JsonIgnore]
        public bool IsRemember { get; set; }
    }
}
