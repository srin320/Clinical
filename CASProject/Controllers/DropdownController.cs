using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicalDAL.EF;

namespace CASProject.Controllers
{

    public class DropdownController : Controller
    {
        
         ClinicalDAL.UserRepository userRepo;
        ClinicalDAL.AppointmentRepository aptRepo;

        public DropdownController()
        {
            userRepo= new ClinicalDAL.UserRepository();
            aptRepo = new ClinicalDAL.AppointmentRepository();
        }



        [HttpGet]
        public ActionResult ShowDropDown()
        {
           var obj= userRepo.GetDoctorByDept();
           return View(obj);
        }

        [HttpGet]
        public ActionResult Bookappointment()
        {

            var obj = userRepo.GetDoctorByDept();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Bookappointment(string u)
        {
            Appointment appoint = new Appointment();
            var pid = Convert.ToInt32(Request["txtpid"]);
            appoint.PatientId = pid;
            appoint.DoctorId = Convert.ToInt32(Request["txtdid"]);
            appoint.Date = Convert.ToDateTime(Request["txtdoa"]);
            appoint.Status = 0;
            aptRepo.AddAppointmnet(appoint);
            var obj = userRepo.GetUserByID(pid);
            //var obj = userRepo.GetDoctorByDept();
            return RedirectToAction("patient", "Profiles", new { username = obj.Username });
        }


    }
}