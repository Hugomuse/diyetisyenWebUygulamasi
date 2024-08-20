using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Spatial;

namespace diyetisyenWebUygulamasi.Models
{
    public class datacontext : DbContext
    {
        public DbSet<Diyetisyen> diyetisyens { get; set; }
        public DbSet<Randevu> randevus { get; set; }
        public DbSet<Yorum> yorums { get; set; }
        public DbSet<Kullanici> kullanicis {  get; set; }
        public DbSet<Siparis> siparis { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Begeni> begenis { get; set; }
        public DbSet<Mesaj> mesajs { get; set; }
    }
}