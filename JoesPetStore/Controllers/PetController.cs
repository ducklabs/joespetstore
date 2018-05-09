﻿using System;
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

        public ActionResult Offer()
        {
            var petDetailsViewModel = Facade.FindPet();
            return View(petDetailsViewModel);
        }

        public ActionResult Approve()
        {
            var petDetailsViewModel = Facade.FindPet();
            return View(petDetailsViewModel);
        }

        public ActionResult Purchase()
        {
            var petDetailsViewModel = Facade.FindPet();

            return View();
        }


    }
}