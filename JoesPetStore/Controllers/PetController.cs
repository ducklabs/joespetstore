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
        // GET: Pet
        public ActionResult Details()
        {
            var petDetailsViewModel = Facade.FindPet();
            return View(petDetailsViewModel);
        }
    }
}