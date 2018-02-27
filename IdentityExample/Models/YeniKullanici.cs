using System.ComponentModel.DataAnnotations;//property'e ait olması gereken ek özellikleri barından namespace

namespace IdentityExample.Models
{
    public class YeniKullanici
    {
        [Required(ErrorMessage = "Kullanıcı Adı zorunludur")] //zorunlu alan bilgisini vermektedir.
        public string KullaniciAd { get; set; }
        [Required(ErrorMessage = "Email zorunludur")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre zorunludur")]
        public string Sifre { get; set; }
    }
}
