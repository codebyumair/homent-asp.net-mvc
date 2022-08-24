using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homent.Models;

namespace Homent.Controllers
{
    public class AccountController : Controller
    {
        private MasterHomentDBEntities db = new MasterHomentDBEntities();


        // GET: Account 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tenantData = db.tblTenants.Where(td => td.Email.Equals(data.Email) && td.Password.Equals(data.Password)).FirstOrDefault();
                    var ownerData = db.tblOwners.Where(od => od.Email.Equals(data.Email) && od.Password.Equals(data.Password)).FirstOrDefault();


                    if (tenantData != null)
                    {
                        HttpCookie tenantCookie = new HttpCookie("tenant");
                        tenantCookie.Value = tenantData.Firstname.ToString() + " " + tenantData.Lastname.ToString();
                        tenantCookie.Expires = DateTime.Now.AddDays(10);
                        Response.Cookies.Add(tenantCookie);

                        return RedirectToAction("Index", "Home");
                    }
                    else if (ownerData != null)
                    {

                        HttpCookie ownerCookie = new HttpCookie("owner");
                        ownerCookie.Value = ownerData.Firstname.ToString() + " " + ownerData.Lastname.ToString();
                        ownerCookie.Expires = DateTime.Now.AddDays(10);

                        HttpCookie ownerIDCookie = new HttpCookie("ownerID");
                        ownerIDCookie.Value = ownerData.ID.ToString();
                        ownerIDCookie.Expires = DateTime.Now.AddDays(10);


                        /*int id = Convert.ToInt32(ownerData.ID);
                        var totalHouse = db.tblHouses.Where(h => h.OwnerID == (id)).Count();
                        ViewBag.TotalHouse = totalHouse.ToString();*/

                        Response.Cookies.Add(ownerCookie);
                        Response.Cookies.Add(ownerIDCookie);
                        return RedirectToAction("Owner");
                    }
                    else if (ownerData == null && tenantData == null)
                    {
                        ViewBag.LoginErrorMessage = "Incorrect username or password";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Exception = ex.Message;
                }
            }
            return View();
        }

        //GET: Account/SignUpTenant
        public ActionResult SignUpTenant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUpTenant(tblTenant tenant)
        {
            try
            {
                db.tblTenants.Add(tenant);
                db.SaveChanges();
                ViewBag.SuccessMsg = "Account created successfully";
                return RedirectToAction("Login");

            }
            catch (Exception e)
            {
                ViewBag.Exception = e.Message.ToString();
            }
            return View(tenant);
        }

        //GET: Account/SignUpOwner
        public ActionResult SignUpOwner()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUpOwner(tblOwner owner)
        {
            try
            {
                db.tblOwners.Add(owner);
                db.SaveChanges();
                ViewBag.SuccessMsg = "Account created successfully";
                return RedirectToAction("Login");

            }
            catch (Exception e)
            {
                ViewBag.Exception = e.Message;
            }
            return View(owner);
        }

        //GET: Account/Login
        public ActionResult Logout()
        {
            Response.Cookies["tenant"].Expires = DateTime.Now;
            Response.Cookies["owner"].Expires = DateTime.Now;
            Response.Cookies["ownerID"].Expires = DateTime.Now;
            Response.Cookies["totalHouse"].Expires = DateTime.Now;
            return RedirectToAction("Login", "Account");
        }


        //GET: Account/Owner
        public ActionResult Owner()
        {
            try
            {
                if (Request.Cookies["ownerID"] != null)
                {
                    int ownerID = Convert.ToInt32(Request.Cookies["ownerID"].Value.ToString());
                    var result = db.tblHouses.Where(h => h.OwnerID == ownerID);
                    var totalHouse = db.tblHouses.Where(h => h.OwnerID == (ownerID)).Count();
                    ViewBag.TotalHouse = totalHouse.ToString();
                    return View(result.ToList());
                }
            }
            catch (Exception e)
            {
                ViewBag.Exception = e.Message;
            }
            return View();
        }
    }
}