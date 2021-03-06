using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CASProject.Controllers
{
    public class ManageMessagesController : Controller
    {
        ClinicalDAL.MessageRepository msgRepo;
        ClinicalDAL.UserRepository userRepo;

        public ManageMessagesController()
        {
            msgRepo = new ClinicalDAL.MessageRepository();
           userRepo = new ClinicalDAL.UserRepository();
        }
        // GET: ManageMessages
        public ActionResult ViewMsgPatient()
        {
            //string uname = Request.QueryString["username"];
            int pid = Convert.ToInt32(Request.QueryString["pid"]);
            //var u = userRepo.getuserbyuname(uname);
            var u = userRepo.GetUserByID(pid);
            var v = userRepo.getdoctorbyappointment(u.Id);
            var pat = userRepo.getuserbyuname(Request.QueryString["username"]);
            
            var msgs = msgRepo.GetMessagesForPatient(pid, v.DoctorId);
            return View(msgs);
        }

        [HttpPost]
        public ActionResult SendMsgPatient()
        {
            string msg = Request["txtMsg"];
            int ssid = Convert.ToInt32(Request["sid"]);
            int rrid = Convert.ToInt32(Request["rid"]);
            int ret = msgRepo.Send(msg, ssid, rrid);

            return RedirectToAction("ViewMsgPatient", new { pid = ssid, did = rrid });
        }

        public ActionResult ViewMsgDoctor()
        {
            //string uname = Request.QueryString["username"];
            var u = userRepo.getuserbyuname(Request.QueryString["username"]);
            var msgs = msgRepo.GetLatestMsgForDoctor(u.Id);
            return View(msgs);
        }

        [HttpGet]
        public ActionResult SendMsgDoctor()
        {
            int did = Convert.ToInt32(Request.QueryString["did"]);
            int pid = Convert.ToInt32(Request.QueryString["pid"]);
            var msgs = msgRepo.GetMessagesForDoctor(did, pid);
            return View(msgs);
        }

        [HttpPost]
        public ActionResult SendMsgDoctor(string str)
        {
            string msg = Request["txtMsg"];
            int ssid = Convert.ToInt32(Request["sid"]);
            int rrid = Convert.ToInt32(Request["rid"]);
            int ret = msgRepo.Send(msg, ssid, rrid);

            return RedirectToAction("SendMsgDoctor", new { pid = rrid, did = ssid });
        }
    }
}