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
        
        // This method returns an Index View with data which is dispalyed when the web app initialy loads

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

        // The action method AddFarmer helps to return a view to add a farmer to the system

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

        // The action method EditFarmer helps to return a view to edit a farmers details in the system

        public ActionResult EditFarmer(int? id)
        {
            FarmerViewModel model = new FarmerViewModel();
            if (id.HasValue && id != 0)
            {
                Farmers farmerEntity = farmerService.GetFarmer(id.Value);
                FarmerProfile farmerProfileEntity = farmerProfileService.GetFarmerProfile(id.Value);
                model.FirstName = farmerProfileEntity.FirstName;
                model.LastName = farmerProfileEntity.LastName;
                model.Address = farmerProfileEntity.Address;
                model.Email = farmerEntity.Email;
            }
            return PartialView("_EditFarmer", model);
        }

        [HttpPost]
        public ActionResult EditFarmer(FarmerViewModel model)
        {
            Farmers farmerEntity = farmerService.GetFarmer(model.Id);
            farmerEntity.Email = model.Email;
            farmerEntity.ModifiedDate = DateTime.UtcNow;
            FarmerProfile farmerProfileEntity = farmerProfileService.GetFarmerProfile(model.Id);
            farmerProfileEntity.FirstName = model.FirstName;
            farmerProfileEntity.LastName = model.LastName;
            farmerProfileEntity.Address = model.Address;
            farmerProfileEntity.ModifiedDate = DateTime.UtcNow;
            farmerEntity.FarmerProfile = farmerProfileEntity;
            farmerService.UpdateFarmer(farmerEntity);
            if (farmerEntity.Id > 0)
            {
                return RedirectToAction("index");
            }
            return View(model);
        }

        // DeleteFarmer action method returns a view to delete a farmer

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
