using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JoesPetStore.Models;

namespace JoesPetStore.Controllers
{
    public class PetController : Controller
    {
        // GET: PetId
        public ActionResult Details()
        {
            var petDetailsViewModel = Facade.FindPet();
            return View(petDetailsViewModel);
        }

        public ActionResult Request()
        {
            var petDetailsViewModel = Facade.FindPet();
            return View(petDetailsViewModel);
        }

        public ActionResult Approve()
        {
            var petDetailsViewModel = Facade.FindPet();
            return View(petDetailsViewModel);
        }

        public ActionResult PurchasePage()
        {
            var petDetailsViewModel = Facade.FindPet();
            return View();
        }

        [HttpPost]
        public ActionResult Purchase(string petid)
        {
            Facade.PurchasePet();
            return RedirectToAction("Index", "Home", new { area = "" });
        }


    }
}