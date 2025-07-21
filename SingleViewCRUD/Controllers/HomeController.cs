using SingleViewCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SingleViewCRUD.Controllers
{
    public class HomeController : Controller
    {
        BalUser obj = new BalUser();
        public ActionResult Index()
        {
            ViewBag.CountryList = GetCountries();
            List<Users> lst = obj.ViewData();
            return View(lst);
        }

        [HttpGet]
        public ActionResult SaveUser()
        {
            ViewBag.CountryList = GetCountries();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUser(Users objMVC)
        {
            if (ModelState.IsValid)
            {
                obj.SaveData(objMVC);
                return Json(new { success = true, message = "User saved successfully!" });
            }

            return Json(new { success = false, message = "Please correct the highlighted errors." });
        }


        private List<SelectListItem> GetCountries()
        {
            DataTable dt = obj.Country();
            List<SelectListItem> countryList = new List<SelectListItem>();

            foreach (DataRow row in dt.Rows)
            {
                countryList.Add(new SelectListItem
                {
                    Value = row["c_id"].ToString(),
                    Text = row["CountryName"].ToString()
                });
            }
            return countryList;
        }

        public JsonResult GetStates(int countryId)
        {
            Users objBal = new Users { CountryId = countryId };
            DataTable dt = obj.State(objBal);

            var stateList = dt.AsEnumerable().Select(row => new
            {
                StateId = row["StateID"].ToString(),
                StateName = row["StateName"].ToString()
            });

            return Json(stateList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCities(int stateId)
        {
            Users objBal = new Users { StateId = stateId };
            DataTable dt = obj.City(objBal);

            var cityList = dt.AsEnumerable().Select(row => new
            {
                CityId = row["CityID"].ToString(),
                CityName = row["CityName"].ToString()
            });

            return Json(cityList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = obj.ViewData().FirstOrDefault(u => u.ID == id);
            if (user == null) return Json(null, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                user.ID,
                user.Name,
                user.Gender,
                user.Phone,
                user.Email,
                user.Password,
                user.Country,
                user.StateId,
                user.CityID
               
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users objUser)
        {
            if (ModelState.IsValid)
            {
                BalUser objBal = new BalUser();
                objBal.Edit(objUser);
                return Json(new { success = true, message = "User updated successfully." });

            }
            ViewBag.CountryList = GetCountries();
            return Json(new { success = false, message = "Invalid user data." });
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            BalUser objBal = new BalUser();
            objBal.DeleteUser(id);
            return RedirectToAction("index", "Home");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}