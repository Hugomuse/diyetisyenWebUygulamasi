using diyetisyenWebUygulamasi.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace diyetisyenWebUygulamasi.Controllers
{
    public class HesabimController : Controller
    {
        string LinkeDonustur(string donusturulecek)
        {
            return (donusturulecek
                .Replace("İ", "I")
                .Replace("ı", "i")
                .Replace("Ğ", "G")
                .Replace("ğ", "g")
                .Replace("Ü", "U")
                .Replace("ü", "u")
                .Replace("Ş", "S")
                .Replace("ş", "s")
                .Replace("Ö", "O")
                .Replace("ö", "o")
                .Replace("Ç", "C")
                .Replace("ç", "c")
                .Replace("#", "sharp")
                .Replace(" ", "-")
                .Replace(">", "-")
                .Replace("<", "-")
                .Replace("(", "-")
                .Replace(")", "-")
                .Replace("&", "-")
                .Replace("=", "")
                .Replace(":", "")
                .Replace("?", "")
                .Replace(">", "")
                .Replace("<", "")
                .Replace("%", "")
                .Replace("+", "")
                .Replace("^", "")
                .Replace("\'", "")
                .Replace("\"", "")
                .ToLower());
        }
        string GetYetki()
        {
            if (User.IsInRole("Diyetisyen"))
                return "Diyetisyen";
            else if (User.IsInRole("Kullanıcı"))
                return "Kullanıcı";
            else
                return "Giriş Yapmamış";
        }

        datacontext dc = new datacontext();
        public ActionResult Detay(string ad)
        {
            if (string.IsNullOrEmpty(ad))
            {
                ViewBag.Mesaj = "Hesap bulunamadı";
                return View("Yetkisiz");
            }


            if (GetYetki() == "Diyetisyen")
            {
                return View("DiyetisyenDetay", dc.diyetisyens.FirstOrDefault(d => d.Ad == ad));
            }
            else if (GetYetki() == "Kullanıcı")
            {
                return View("KullanıcıDetay", dc.kullanicis.FirstOrDefault(d => d.Ad == ad));
            }
            else
            {
                ViewBag.Mesaj = "Hesap bulunamadı";
                return View("Yetkisiz");
            }
        }

        [HttpPost]
        public ActionResult Detay(int id, string sifre, HttpPostedFileBase resim, string resimRadio, string aciklama)
        {
            if (GetYetki() == "Diyetisyen")
            {
                var diyetisyen = dc.diyetisyens.Find(id);
                diyetisyen.Sifre = sifre;
                diyetisyen.Aciklama = aciklama;

                if (resimRadio == "true")
                {
                    if (resim != null)
                    {
                        string resimYolu = "/Content/images/profilFotolari/user.png";


                        Random rnd = new Random();
                        string diyetisyenLink = rnd.Next(0, 999999).ToString();

                        if (!Directory.Exists(Server.MapPath("/Content/images/profilFotolari/diyetisyen/")))
                            Directory.CreateDirectory(Server.MapPath("/Content/images/profilFotolari/diyetisyen/"));

                        if (System.IO.File.Exists(Server.MapPath(diyetisyen.ResimYolu)))
                            System.IO.File.Delete(Server.MapPath(diyetisyen.ResimYolu));

                        string dosyaAdi = Path.GetFileName(resim.FileName);
                        string yol = Path.Combine(Server.MapPath("~/Content/images/profilFotolari/diyetisyen/"), diyetisyenLink + Path.GetExtension(resim.FileName));
                        resim.SaveAs(yol);

                        resimYolu = "/Content/images/profilFotolari/diyetisyen/" + diyetisyenLink + Path.GetExtension(resim.FileName);
                        diyetisyen.ResimYolu = resimYolu;
                    }
                }

                dc.diyetisyens.AddOrUpdate(diyetisyen);
                dc.SaveChanges();
                return Detay(diyetisyen.Ad);
            }
            else if (GetYetki() == "Kullanıcı")
            {
                var kullanici = dc.kullanicis.Find(id);
                kullanici.Sifre = sifre;
                if (resimRadio == "true")
                {
                    if (resim != null)
                    {
                        string resimYolu = "/Content/images/profilFotolari/user.png";

                        Random rnd = new Random();

                        string kullaniciLink = rnd.Next(0,999999).ToString();

                        if (!Directory.Exists(Server.MapPath("/Content/images/profilFotolari/kullanıcı/")))
                            Directory.CreateDirectory(Server.MapPath("/Content/images/profilFotolari/kullanıcı/"));
                        

                        string dosyaAdi = Path.GetFileName(resim.FileName);
                        string yol = Path.Combine(Server.MapPath("~/Content/images/profilFotolari/kullanıcı/"), kullaniciLink + Path.GetExtension(resim.FileName));
                        resim.SaveAs(yol);

                        resimYolu = "/Content/images/profilFotolari/kullanıcı/" + kullaniciLink + Path.GetExtension(resim.FileName);
                        kullanici.ResimYolu = resimYolu;
                    }
                }

                dc.kullanicis.AddOrUpdate(kullanici);
                dc.SaveChanges();
                return Detay(kullanici.Ad);
            }
            else
                return View("Error");
        }


        [Authorize(Roles = "Diyetisyen")]
        public ActionResult Postlar()
        {
            var diyetisyen = dc.diyetisyens.FirstOrDefault(d=>d.Ad==User.Identity.Name);
            return View(dc.posts.Where(d => d.DiyetisyenId == diyetisyen.Id).ToList());
        }


        [HttpPost]
        [Authorize(Roles = "Diyetisyen")]
        public ActionResult Postlar(string btn, HttpPostedFileBase resim, int postId = 0)
        {
            var diyetisyen = dc.diyetisyens.FirstOrDefault(d => d.Ad == User.Identity.Name);
            if (btn == "ekle")
            {
                if (resim.ContentLength <= 0 || resim == null || string.IsNullOrEmpty(resim.FileName))
                {
                    ViewBag.Mesaj = "Lütfen geçerli bir görsel seçiniz";
                    return View("Yetkisiz");
                }


                
                string diyetisyenLink = LinkeDonustur(diyetisyen.Ad);

                if (!Directory.Exists(Server.MapPath("/Content/images/postlar/" + diyetisyenLink + "/")))
                    Directory.CreateDirectory(Server.MapPath("/Content/images/postlar/" + diyetisyenLink + "/"));


                string dosyaAdi = Path.GetFileName(resim.FileName);
                if (System.IO.File.Exists(Server.MapPath("/Content/images/postlar/" + diyetisyenLink + "/" + dosyaAdi)))
                {
                    ViewBag.Mesaj = "Bu adda bir resim zaten var. Lütfen ismi değiştirip tekrar deneyin";
                    return View("Yetkisiz");
                }

                Random random = new Random();
                int randomSayi = random.Next(0,999999999);
                string yol = Server.MapPath("~/Content/images/postlar/"+ diyetisyenLink + "/"+ randomSayi.ToString() + Path.GetExtension(resim.FileName));
                resim.SaveAs(yol);

                string resimYolu = "/Content/images/postlar/" + diyetisyenLink + "/" + randomSayi.ToString() + Path.GetExtension(resim.FileName);
                var post = new Post
                {
                    DiyetisyenId = diyetisyen.Id,
                    MedyaYolu = resimYolu
                };

                dc.posts.Add(post);
                dc.SaveChanges();



            }
            else if (btn == "sil")
            {
                var post = dc.posts.Find(postId);
                dc.posts.Remove(post);
                dc.SaveChanges();
            }
            return View(dc.posts.Where(d => d.DiyetisyenId == diyetisyen.Id).ToList());



        }


        public class RandevuViewModel
        {
            public Randevu randevu { get; set; }
            public Kullanici kullanici { get; set; }
            public Diyetisyen diyetisyen { get; set; }
        }

        public ActionResult Randevularim()
        {
            var modelList = new List<RandevuViewModel>();
            if (GetYetki() == "Diyetisyen")
            {
                var diyetisyen = dc.diyetisyens.FirstOrDefault(d => d.Ad == User.Identity.Name);
                var randevular = dc.randevus.Where(k => k.DiyetisyenID == diyetisyen.Id).ToList();

                foreach (var randevu in randevular)
                {
                    var model = new RandevuViewModel
                    {
                        kullanici = dc.kullanicis.FirstOrDefault(k => k.Id == randevu.KullaniciID),
                        randevu = randevu
                    };
                    modelList.Add(model);
                }
            }
            else if (GetYetki() =="Kullanıcı")
            {
                var kullanici = dc.kullanicis.FirstOrDefault(d => d.Ad == User.Identity.Name);
                var randevular = dc.randevus.Where(k => k.KullaniciID == kullanici.Id).ToList();

                foreach (var randevu in randevular)
                {
                    var model = new RandevuViewModel
                    {
                        diyetisyen = dc.diyetisyens.FirstOrDefault(k => k.Id == randevu.DiyetisyenID),
                        randevu = randevu
                    };
                    modelList.Add(model);
                }
            }
            else
            {
                ViewBag.Mesaj = "Randevularıma gitmek için giriş yapmanız gerekli";
                return View("Yetkisiz");
            }


            return View(modelList);

        }



    }
}