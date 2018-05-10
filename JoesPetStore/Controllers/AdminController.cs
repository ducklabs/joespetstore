using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using JoesPetStore.Models;
using JoesPetStore.ViewModels;
using NUnit.Framework;

namespace JoesPetStore.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Approval()
        {
            //var petDetailsViewModel = Facade.FindPurchaseReceipt();
            var allApprovalViewModels = new AllApprovalsViewModel(){ApprovalViewModels = new List<ApprovalViewModel>() };
            allApprovalViewModels.ApprovalViewModels.AddRange( Facade.GetApprovals() );
            return View(allApprovalViewModels);
        }
    }
}