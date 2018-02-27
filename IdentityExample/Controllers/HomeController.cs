using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityExample.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {

        private UserManager<AppUser> kullaniciYonetimi; /* Microsoft.AspNetCore.Identity namespace altında bulunan AppUser sınıfını uygulayan UserManager sınıfından bir değişken tanımlıyoruz.*/
        public HomeController(UserManager<AppUser> userManager) //Kurucu metot dependency injection işlemi ile UserManager sınıfı ve parametresi kullanımı
        {
            kullaniciYonetimi = userManager; //kendi değişkenimize dependency injection ile aldığımız örnek sınıfı atıyoruz.
        }

        public IActionResult Index()
        {
            return View(kullaniciYonetimi.Users);//kullanıcılarla ilgili bize yardımcı metotlara sahip kullanıcıYonetimi sınıfının Users property'i ile tüm kullanıcıları view'e gönderme işlemi yapmaktayız.
        }

        public IActionResult YeniKullanici()
        {
            return View();
        }

        [HttpPost] //Http Post bildirimi
        public async Task<IActionResult> YeniKullanici(YeniKullanici model)  //Metodun asenkron çalışması için gerekli async ve Task anahtar kelimelerinin kullanımı
        {
            if (ModelState.IsValid)
            {
                AppUser kullanici = new AppUser
                {
                    UserName = model.KullaniciAd,
                    Email = model.Email
                }; //AppUser sınıfı kurulurken kullanıcı tarafından gönderilen değerlerle kuruyoruz.
                IdentityResult sonuc = await kullaniciYonetimi.CreateAsync(kullanici, model.Sifre); //asenkron Create modu çalıştırılırken YeniKullanici Methodunda tanımlanan async anahtar kelimesinin çalışacağı await bildirimi.
                //create işlemi gerçekleştildiğinde sonuc olarak IdentityResult tpinden bir nesne döner ve bu nesne ile İşlemin başarılı olarak yapıldığı ya da alınan hatalar dizisi geri döndürülür.

                if (sonuc.Succeeded)
                {
                    return RedirectToAction("Index"); //işlem başarılı ise index action'a gönderimi
                }
                else
                {
                    foreach (IdentityError hata in sonuc.Errors) //hataların listelenerek view'e gönderilmesi için ModelNesnesine tanımlamalarının eklenmesi
                    {
                        ModelState.AddModelError("", hata.Description);
                    }
                }
            }
            return View(model);
        }

    }
}
