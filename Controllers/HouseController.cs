using Homent.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homent.Controllers
{
    public class HouseController : Controller
    {
        private MasterHomentDBEntities db = new MasterHomentDBEntities();

        // GET: House
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("PageNotFound");
            }
            var house = db.tblHouses.Where(h => h.HouseID == id);
            if (house == null)
            {
                return RedirectToAction("PageNotFound");
            }
            return View(house.ToList());
        }

        public ActionResult AddHouse()
        {
            try
            {

                var Type = db.tblTypes.ToList();
                ViewBag.Type = Type;

                var Room = db.tblRooms.ToList();
                ViewBag.Room = Room;

                var State = db.tblStates.ToList();
                ViewBag.State = State;
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddHouse(tblHouse house)
        {
            try
            {
                var Type = db.tblTypes.ToList();
                ViewBag.Type = Type;

                var Room = db.tblRooms.ToList();
                ViewBag.Room = Room;

                var State = db.tblStates.ToList();
                ViewBag.State = State;

                string fileName = Path.GetFileNameWithoutExtension(house.Image.FileName);
                string extension = Path.GetExtension(house.Image.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                house.ImagePath = "~/Content/HouseImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/HouseImages/"), fileName);
                house.Image.SaveAs(fileName);

                db.tblHouses.Add(house);
                db.SaveChanges();

                return RedirectToAction("Owner", "Account");

            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ViewBag.Exception = eve;

                    foreach (var ve in eve.ValidationErrors)
                    {
                        ViewBag.Exception = ve;
                    }
                }

            }
            return View();

        }

        public ActionResult EditHouse(int? id)
        {
            try
            {
                var Type = db.tblTypes.ToList();
                ViewBag.Type = Type;

                var Room = db.tblRooms.ToList();
                ViewBag.Room = Room;

                var State = db.tblStates.ToList();
                ViewBag.State = State;

                if (id == null)
                {
                    return RedirectToAction("PageNotFound");
                }
                tblHouse house = db.tblHouses.Find(id);
                if (house == null)
                {
                    return RedirectToAction("PageNotFound");
                }
                return View(house);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditHouse(tblHouse house)
        {
            try
            {
                var Type = db.tblTypes.ToList();
                ViewBag.Type = Type;

                var Room = db.tblRooms.ToList();
                ViewBag.Room = Room;

                var State = db.tblStates.ToList();
                ViewBag.State = State;

                db.Entry(house).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Owner", "Account");

            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.InnerException;
            }
            return View(house);
        }

        public ActionResult DeleteHouse(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("PageNotFound");
            }
            var house = db.tblHouses.Where(h => h.HouseID == id);
            if (house == null)
            {
                return RedirectToAction("PageNotFound");
            }
            return View(house.ToList());
        }

        [HttpPost]
        public ActionResult DeleteHouse(int id)
        {
            tblHouse house = db.tblHouses.Find(id);
            var fileName = Server.MapPath(house.ImagePath);

            db.tblHouses.Remove(house);
            db.SaveChanges();
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
            return RedirectToAction("Owner", "Account");
        }

        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}