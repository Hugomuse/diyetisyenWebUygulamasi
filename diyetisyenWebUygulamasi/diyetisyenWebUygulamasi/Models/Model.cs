using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace diyetisyenWebUygulamasi.Models
{
    public class Diyetisyen
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public string ResimYolu { get; set; }
        public string Sifre {  get; set; }


        public string ilanBaslik { get; set; }
        public string ilanAciklama { get; set; }
    }
    public class Randevu
    {
        public int Id { get; set; }
        public int DiyetisyenID { get; set; }
        public int KullaniciID { get; set; }
        public DateTime tarih { get; set; }
    }
    public class Yorum
    {
        public int Id { get; set; }
        public int DiyetisyenId { get; set; }
        public int KullaniciId { get; set; }
        public int Yildiz {  get; set; }
        public string Icerik { get; set; }
    }
    public class Kullanici
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Sifre { get; set; }
        public string ResimYolu { get; set; }
    }
    public class Siparis
    {
        public int Id { get; set; }
        public int KullaniciID { get; set; }
        public int DiyetisyenID { get; set; }
        public bool YorumYapildi { get; set; }
    }
    public class Post
    {
        public int Id { get; set; }
        public int DiyetisyenId { get; set; }
        public string MedyaYolu { get; set; }
    }
    public class Begeni
    {
        public int Id { get; set; }
        public int DiyetisyenID { get; set; }
        public int KullaniciID { get; set; }
    }
    public class Mesaj
    {
        public int Id { get; set; }
        public string aliciTipi { get; set; }
        public int aliciId { get; set; }
        public int vericiId { get; set; }
        public string metin {  get; set; }
    }
}