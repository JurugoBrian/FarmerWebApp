using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Farmer.Data;
using Farmer.Service;
using Farmer.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// Farmer Controller has all ActionResult methods for the end-user interface of operations

namespace Farmer.Web.Controllers
{

    public class FarmerController : Controller
    {
        private readonly IFarmerService farmerService;
        private readonly IFarmerProfileService farmerProfileService;

        public FarmerController(IFarmerService farmerService, IFarmerProfileService farmerProfileService)
        {
            this.farmerService = farmerService;
            this.farmerProfileService = farmerProfileService;
        }
        
        // This method returns an Index View with data which is dispalyed when the web app
        // initialy loads
        [HttpGet]
        public IActionResult Index()
        {
            List<FarmerViewModel> model = new List<FarmerViewModel>();
            farmerService.GetFarmers().ToList().ForEach(u =>
            {
                FarmerProfile farmerProfile = farmerProfileService.GetFarmerProfile(u.Id);
                FarmerViewModel farmer = new FarmerViewModel
                {
                    Id = u.Id,
                    Name = $"{farmerProfile.FirstName} {farmerProfile.LastName}",
                    Email = u.Email,
                    Address = farmerProfile.Address
                };
                model.Add(farmer);
            });

            return View(model);
        }

        [HttpGet]
        public ActionResult AddFarmer()
        {
            FarmerViewModel model = new FarmerViewModel();

            return PartialView("_AddFarmer", model);
        }

        // The action method AddUser helps to return a view to add a farmer to the system

        [HttpPost]
        public ActionResult AddFarmer(FarmerViewModel model)
        {
            Farmers farmersEntity = new Farmers
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                AddedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                FarmerProfile = new FarmerProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    AddedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                }
            };
            farmerService.InsertFarmer(farmersEntity);
            if (farmersEntity.Id > 0)
            {
                return RedirectToAction("index");
            }
            return View(model);
        }

        [HttpGet]
        public PartialViewResult DeleteFarmer(int id)
        {
            FarmerProfile farmerProfile = farmerProfileService.GetFarmerProfile(id);
            string name = $"{farmerProfile.FirstName} {farmerProfile.LastName}";
            return PartialView("_DeleteFarmer", name);
        }

        [HttpPost]
        public ActionResult DeleteFarmer(long id, FormCollection form)
        {
            farmerService.DeleteFarmer(id);
            return RedirectToAction("Index");
        }
    }
}
