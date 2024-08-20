using diyetisyenWebUygulamasi.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace diyetisyenWebUygulamasi.Controllers
{
    public class DiyetisyenDetayModel
    {
        public Diyetisyen diyetisyen { get; set; }
        public List<Post> posts { get; set; }
    }

    public class DiyetisyenController : Controller
    {

        public datacontext dc = new datacontext();


        [Authorize(Roles = "Kullanıcı")]
        public ActionResult Detay(string ad)
        {
            if (ad == "**")
            {
                ViewBag.Mesaj = "Lütfen geçerli bir diyetisyen seçiniz";
                return View("Yetkisiz");
            }
            var diyetisyenSec = dc.diyetisyens.FirstOrDefault(d => d.Ad == ad);

            var postListe = dc.posts.Where(p => p.DiyetisyenId == diyetisyenSec.Id).ToList();


            var model = new DiyetisyenDetayModel
            {
                diyetisyen = diyetisyenSec,
                posts = postListe
            };

            ViewBag.YorumYapabilir = false;
            Kullanici kullanici = new Kullanici();
            if (User.IsInRole("Kullanıcı"))
            {
                kullanici = dc.kullanicis.FirstOrDefault(k => k.Ad == User.Identity.Name);
                if (dc.siparis.Count(s => s.KullaniciID == kullanici.Id && s.DiyetisyenID == diyetisyenSec.Id) > 0)
                    ViewBag.YorumYapabilir = true;

                if (dc.begenis.Count(b => b.KullaniciID == kullanici.Id && b.DiyetisyenID == diyetisyenSec.Id) > 0)
                    ViewBag.Begendi = true;
                else
                    ViewBag.Begendi = false;
            }
            else
            {
                ViewBag.YorumYapabilir = false;
                ViewBag.Begendi = false;
            }


            var yorumlar = dc.yorums.Where(y => y.DiyetisyenId == diyetisyenSec.Id).ToList();
            var toplam = 0;
            double ortalama = 0;
            if (yorumlar.Count()>0)
            {
                toplam = yorumlar.Sum(y => y.Yildiz);
                ortalama = toplam / yorumlar.Count();
            }
            



            ViewBag.puan = ortalama;
            ViewBag.begeni = dc.begenis.Count(b => b.DiyetisyenID == diyetisyenSec.Id);

            return View(model);
        }




        [HttpPost]
        [Authorize(Roles = "Kullanıcı")]
        public ActionResult RandevuAl(int id, DateTime randevuTarihi)
        {
            var kullanici = dc.kullanicis.FirstOrDefault(k => k.Ad == User.Identity.Name);
            var randevu = new Randevu
            {
                DiyetisyenID = id,
                KullaniciID = kullanici.Id,
                tarih = randevuTarihi
            };


            if (dc.siparis.Count(s => s.DiyetisyenID == id && s.KullaniciID == kullanici.Id) <= 0)
            {
                var siparis = new Siparis
                {
                    DiyetisyenID = id,
                    KullaniciID = kullanici.Id,
                    YorumYapildi = false,
                };
                dc.siparis.Add(siparis);
            }


            dc.randevus.Add(randevu);

            dc.SaveChanges();
            return Redirect("/Diyetisyen/Detay/" + dc.diyetisyens.Find(id).Ad);
        }


        [HttpPost]
        [Authorize(Roles = "Kullanıcı")]
        public ActionResult YorumYap(int id, string icerik, int yildiz)
        {
            var kullanici = dc.kullanicis.FirstOrDefault(k => k.Ad == User.Identity.Name);

            if (dc.yorums.Count(s => s.DiyetisyenId == id && s.KullaniciId == kullanici.Id) > 0)
            {
                var yorum = dc.yorums.FirstOrDefault(s => s.DiyetisyenId == id && s.KullaniciId == kullanici.Id);
                yorum.Icerik = icerik;
                yorum.Yildiz = yildiz;

                dc.yorums.AddOrUpdate(yorum);
            }
            else
            {
                var yorum = new Yorum
                {
                    DiyetisyenId = id,
                    KullaniciId = kullanici.Id,
                    Icerik = icerik,
                    Yildiz = yildiz
                };
                dc.yorums.AddOrUpdate(yorum);
            }

            dc.SaveChanges();

            return Redirect("/Diyetisyen/Detay/" + dc.diyetisyens.Find(id).Ad);
        }

        [HttpPost]
        [Authorize(Roles = "Kullanıcı")]
        public ActionResult Begeni(int id, string btn)
        {
            var kullanici = dc.kullanicis.FirstOrDefault(k => k.Ad == User.Identity.Name);

            if (btn == "ekle")
            {
                var begeni = new Begeni
                {
                    DiyetisyenID = id,
                    KullaniciID = kullanici.Id,
                };
                dc.begenis.Add(begeni);
            }
            else if (btn == "kaldir")
            {
                var begeni = dc.begenis.FirstOrDefault(b => b.DiyetisyenID == id && b.KullaniciID == kullanici.Id);
                dc.begenis.Remove(begeni);
            }
            dc.SaveChanges();
            return Redirect("/Diyetisyen/Detay/" + dc.diyetisyens.Find(id).Ad);
        }





    }
}