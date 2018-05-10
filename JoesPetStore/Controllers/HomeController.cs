using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JoesPetStore.Models;

namespace JoesPetStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var petDetailsViewModel = Facade.FindPurchaseReceipt();
            var petDetailsViewModel = Facade.FindPet();
            return View(petDetailsViewModel);
        }
    }
}