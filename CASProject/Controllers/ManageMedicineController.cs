using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicalDAL.EF;
namespace CASProject.Controllers
{
    public class ManageMedicineController : Controller
    {
        ClinicalDAL.UserRepository userRepo;
        ClinicalDAL.AppointmentRepository aptRepo;

        public ManageMedicineController()
        {
            userRepo = new ClinicalDAL.UserRepository();
            aptRepo = new ClinicalDAL.AppointmentRepository();
        }





        // GET: ManageMedicine
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AddMedicine()
        {
            var Med = userRepo.GetMedicine();


            return View(Med);
        }



        [HttpPost]

        public ActionResult AddMedicine(string u )
        {
            string username = Request["txtuname"];
            Medicine med = new Medicine();
            string name = Request["txtmed"];
            int qty = Convert.ToInt32(Request["txtquan"]);
            decimal tax = Convert.ToDecimal(Request["txttax"]);
            decimal dis = Convert.ToDecimal(Request["txtdis"]);
            string date = Request["txtdate"];
            decimal price = Convert.ToDecimal(Request["txtprice"]);
            decimal taxprice = (price + ((tax / 100) * price));
            decimal fprice = taxprice - ((dis/100)*taxprice);
            med.Name = name;
            med.Quantity = qty;
            med.Price = Convert.ToDouble(fprice);
            med.ExpDate = Convert.ToDateTime(date);


            userRepo.AddMedicine(med);
            
            return RedirectToAction("AddMedicine", "ManageMedicine", new {username=username});
        }

        public ActionResult Delete()
        {
            int mid = Convert.ToInt32(Request.QueryString["mid"]);
            userRepo.DeleteMedicine(mid);
            string username = Request.QueryString["username"];
            return RedirectToAction("Pharmacist", "Profiles", new { username = username });
        }

        public ActionResult EditMedicine()
        {
            string username = Request.QueryString["username"];
            int mid = Convert.ToInt32(Request.QueryString["mid"]);
            var med = userRepo.GetMedicineById(mid);
            return View(med);
        }

        [HttpPost]
        public ActionResult EditMedicine(string str)
        {
            int mid = Convert.ToInt32(Request["txtid"]);
            string username = Request["txtuname"];
            
            int qty = Convert.ToInt32(Request["txtqty"]);
            decimal prc = Convert.ToDecimal(Request["txtprice"]);

            userRepo.UpdateMedicine(mid, qty, prc);

            return RedirectToAction("pharmacist", "profiles", new { username = username });
        }
    }
}