using System;
using System.Collections.Generic;
using System.Linq;
using Homent.Models;
using System.Data.Entity;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Homent.Controllers
{
    public class HomeController : Controller
    {
        private MasterHomentDBEntities db = new MasterHomentDBEntities();

        public ActionResult OnBoarding()
        {
            return View();

        }


        public ActionResult Index(string searchValue)
        {
            try
            {

                if (searchValue != null)
                {
                    searchValue = searchValue.ToLower();
                    var HouseList = db.tblHouses.Where(h => h.Address.ToLower().Contains(searchValue)).ToList();
                    ViewBag.HouseList = HouseList;
                }
                else
                {
                    var HouseList = db.tblHouses.ToList();
                    ViewBag.HouseList = HouseList;
                }

                int houseCount = db.tblHouses.Count();
                ViewBag.HouseCount = houseCount;
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
            }
            return View();
        }

        public ActionResult House(int? houseID)
        {
            try
            {
                if (houseID == null)
                {
                    return RedirectToAction("PageNotFound", "House");
                }
                var house = db.tblHouses.Where(h => h.HouseID == houseID).ToList();
                int ownerID = Convert.ToInt32(house[0].OwnerID);
                var ownerDetails = db.tblOwners.Where(o => o.ID == ownerID).ToList();
                ViewBag.OwnerInfo = ownerDetails;
                if (house == null)
                {
                    return RedirectToAction("PageNotFound");
                }
                return View(house.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.InnerException;
            }
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }


}