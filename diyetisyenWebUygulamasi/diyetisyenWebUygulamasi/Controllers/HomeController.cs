using diyetisyenWebUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace diyetisyenWebUygulamasi.Controllers
{
    public class HomeController : Controller
    {
        private readonly datacontext dc = new datacontext();

        public class DiyetisyenViewModel
        {
            public Diyetisyen diyetisyen { get; set; }
            public double puan { get; set; }
        }

        public ActionResult Index()
        {
            var diyetisyenListesi = dc.diyetisyens.ToList();
            var modelList = new List<DiyetisyenViewModel>();

            for (int i = 0; i < diyetisyenListesi.Count; i++)
            {
                var diyetisyen = diyetisyenListesi[i];
                var diyetisyenModel = new DiyetisyenViewModel();
                if (dc.yorums.Count(y => y.DiyetisyenId == diyetisyen.Id)==0)
                {
                    diyetisyenModel.puan=0;
                }
                else
                {
                    int toplam = dc.yorums.Where(y => y.DiyetisyenId == diyetisyen.Id).Sum(s => s.Yildiz);
                    double ortalama = toplam / dc.yorums.Count(y => y.DiyetisyenId == diyetisyen.Id);

                    diyetisyenModel.puan = ortalama;
                }
                diyetisyenModel.diyetisyen = diyetisyen;

                


                modelList.Add(diyetisyenModel);
            }
            modelList = modelList.OrderByDescending(m => m.puan).ToList();
            return View(modelList);
        }
    }
}