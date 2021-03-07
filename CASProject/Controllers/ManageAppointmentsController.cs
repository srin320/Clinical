using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CASProject.Controllers
{
    public class ManageAppointmentsController : Controller
    {
        ClinicalDAL.AppointmentRepository appointmentRepo;
        ClinicalDAL.UserRepository userRepo;
        public ManageAppointmentsController()
        {
            appointmentRepo = new ClinicalDAL.AppointmentRepository();
            userRepo = new ClinicalDAL.UserRepository();
        }
        // GET: ManageAppointment
        public ActionResult GetAppPatient()
        {

            var u = userRepo.getuserbyuname(Request.QueryString["username"]);
            var appointments = appointmentRepo.GetAppointmentsForPatient(u.Id);
            return View(appointments);
        }

        public ActionResult GetAppDoctor()
        {
            var u = userRepo.getuserbyuname(Request.QueryString["username"]);
            var appointments = appointmentRepo.GetAppointmentsForDoctor(u.Id);
            return View(appointments);
        }

        public ActionResult GetAppFrontOffice()
        {
            var appointments = appointmentRepo.GetAppointmentsForFrontOffice();
            return View(appointments);
        }

        public ActionResult Approve()
        {
            int appid = Convert.ToInt32(Request.QueryString["aid"]);

            appointmentRepo.ApproveAppointment(appid);

            string username = Request.QueryString["username"];
            return RedirectToAction("FrontOfficeMember", "Profiles", new { username = username });
        }

        public ActionResult Deny()
        {
            int appid = Convert.ToInt32(Request.QueryString["aid"]);

            appointmentRepo.DenyAppointment(appid);
            string username = Request.QueryString["username"];
            return RedirectToAction("FrontOfficeMember", "Profiles", new {username=username});
        }
    }
}