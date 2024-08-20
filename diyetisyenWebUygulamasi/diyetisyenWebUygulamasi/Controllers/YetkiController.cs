using diyetisyenWebUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace diyetisyenWebUygulamasi.Controllers
{
    public class YetkiController : Controller
    {
        private datacontext dc = new datacontext();


        [HttpGet]
        public ActionResult KayitOl()
        {
            return View();
        }


        [HttpPost]
        public ActionResult KayitOl(string ad, string sifre, string yetkiRadio)
        {
            if (yetkiRadio=="kullanici")
            {
                if (dc.kullanicis.Count(k=>k.Ad==ad)>0)
                {
                    ViewBag.Mesaj = "Bu adda bir kullanıcı zaten var";
                    return View("Yetkisiz");
                }
                if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(sifre))
                {
                    ViewBag.Mesaj = "Lütfen tüm bilgileri eksiksiz giriniz";
                    return View("Yetkisiz");
                }

                var kullanici = new Kullanici
                {
                    Ad = ad,
                    Sifre = sifre,
                    ResimYolu = "/Content/images/profilFotolari/user.png"
                };

                dc.kullanicis.Add(kullanici);
                dc.SaveChanges();
            }
            else if (yetkiRadio == "diyetisyen")
            {
                if (dc.diyetisyens.Count(k => k.Ad == ad) > 0)
                {
                    ViewBag.Mesaj = "Bu adda bir diyetisyen zaten var";
                    return View("Yetkisiz");
                }
                if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(sifre))
                {
                    ViewBag.Mesaj = "Lütfen tüm bilgileri eksiksiz giriniz";
                    return View("Yetkisiz");
                }

                var diyetisyen = new Diyetisyen
                {
                    Ad = ad,
                    Sifre = sifre,
                    ResimYolu = "/Content/images/profilFotolari/user.png"
                };

                dc.diyetisyens.Add(diyetisyen);
                dc.SaveChanges();
            }
            else
            {
                ViewBag.Mesaj = "Lütfen kayıt tipiniz seçiniz";
                return View("Yetkisiz");
            }

            return View("GirisYap");
        }


            [HttpGet]
        public ActionResult GirisYap()
        {
            return View();
        }

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult GirisYap(string ad, string sifre, string yetkiRadio)
        {
            var kullanici = dc.kullanicis.FirstOrDefault(k => k.Ad == ad && k.Sifre == sifre);
            if (yetkiRadio == "kullanici" && kullanici != null)
            {
                FormsAuthentication.SetAuthCookie(ad, false);
                string yetkiler = "Kullanıcı";

                var authTicket = new FormsAuthenticationTicket
                (
                    1,
                    ad,
                    DateTime.Now,
                    DateTime.Now.AddYears(5),
                    true,
                    yetkiler
                );
                string sifreliTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, sifreliTicket);
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
            }
            else if (yetkiRadio == "diyetisyen" && dc.diyetisyens.FirstOrDefault(k => k.Ad == ad && k.Sifre == sifre) != null)
            {
                FormsAuthentication.SetAuthCookie(ad, false);

                var authTicket = new FormsAuthenticationTicket
                (
                    1,
                    ad,
                    DateTime.Now,
                    DateTime.Now.AddYears(5),
                    true,
                    "Diyetisyen"
                );
                string sifreliTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, sifreliTicket);
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı adı veya şifre yanlış";
                return View("Yetkisiz");
            }
                
            return RedirectToAction("Index", "Home");
        }
    }
}