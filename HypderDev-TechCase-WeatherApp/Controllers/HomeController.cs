using HypderDev_TechCase_WeatherApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HypderDev_TechCase_WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Context model = new Context();//Contexten model türetildi.
            List<City> sehirler = model.Cities.ToList();//DBdeki kayıtlı bütün şehirler önyüze gönderiliyor.
            ViewBag.Sehirler = sehirler;//ViewBag üzerinden gönderilmesinin sebebi aynı sayfada kullanılan bir modelin mevcut oldugundan dolayıdır.

            if (sehirler.Count != 0)
            {
                ViewBag.StartIndex = sehirler[0].CityID;
                ViewBag.EndIndex = sehirler[sehirler.Count - 1].CityID; 
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Detail(String CoordLat, String CoordLon)
        {
            ViewBag.lat = CoordLat;
            ViewBag.lon = CoordLon;
            return View();
        }

        [HttpPost]
        public IActionResult AddCity(City sehir)
        {
            Context model = new Context();//Contextden nesne türetildi.
            City checkSehir = model.Cities.FirstOrDefault(x => x.CityName == sehir.CityName);

            if (checkSehir == null)//Bu şehir DBde kayıtlı değil ise(İsim unique olarak kabul edilmektedir) ekle.
            {
                //Ekleme ve kayıt etme işlemi gerçekleştirildi.
                model.Add(sehir);
                model.SaveChanges();
            }
            

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult DeleteCity(int CityID)
        {
            Context model = new Context();//Contextden nesne türetildi.
            City checkSehir = model.Cities.FirstOrDefault(x => x.CityID == CityID);

            if (checkSehir != null)//Bu şehir DBde kayıtlı ise(İsim unique olarak kabul edilmektedir) sil.
            {
                //Ekleme ve kayıt etme işlemi gerçekleştirildi.
                model.Remove(checkSehir);
                model.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
