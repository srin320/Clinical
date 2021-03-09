using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicalDAL.EF;
using CASProject.Models;
using System.Dynamic;
namespace CASProject.Controllers
{
    public class ProfilesController : Controller
    {
        ClinicalDAL.UserRepository userRepo;
        ClinicalDAL.AppointmentRepository appointmentRepo;
        ClinicalDAL.MessageRepository msgRepo;

        public ProfilesController()
        {
            userRepo = new ClinicalDAL.UserRepository();
            appointmentRepo = new ClinicalDAL.AppointmentRepository();
            msgRepo = new ClinicalDAL.MessageRepository();
        }



        // GET: Profiles
        [HttpGet]       

        public ActionResult Doctor(ViewModel.UserViewModel usr)
        {
            if ((Session["Myuser"]!=null) && (Session["Myrole"].Equals("1")))
            {
                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
                var uname = Request.QueryString["username"];
                var users = userRepo.GetDoctor(uname);
                var med = userRepo.GetMedicine().ToList();

                var did = userRepo.getuserbyuname(uname);

                var appointments = appointmentRepo.GetAppointmentsForDoctor(did.Id);
                var msgs = msgRepo.GetLatestMsgForDoctor(did.Id);

                dynamic mymodel = new ExpandoObject();

                mymodel.Medicine = med;
                mymodel.Doctor = users;
                mymodel.Appointment = appointments;
                mymodel.Message = msgs;

                return View(mymodel);
            }
            else
            {
                Session["MyUser"] = null;
                Session["Myrole"] = null;
                return RedirectToAction("Login", "ManageUsers");
            }

        }

        [HttpPost]
        public ActionResult Doctor(ViewModel.UserViewModel usr,string uname)
        {
            userRepo = new ClinicalDAL.UserRepository();
            ClinicalDAL.EF.User u = new ClinicalDAL.EF.User();
           // string  qryname = Request.QueryString["username"];
            if (!ModelState.IsValid)
            {
                string username = Request["txtuname"];
                string Phone = Request["txtphone"];
                string Address = Request["txtadd"];
                string Qualification = Request["txtqual"];
                string Password = Request["txtpwd"];
                var did = userRepo.getuserbyuname(username);
                var obj = userRepo.GetProfiles(username);
                userRepo.UpdateUser(username, Phone, Address, Qualification, Password);
                var appointments = appointmentRepo.GetAppointmentsForDoctor(did.Id);
                var msgs = msgRepo.GetLatestMsgForDoctor(did.Id);

                var users = userRepo.GetDoctor(username);
                string sname = Request["searchname"];
                if (sname != null)
                {
                    var medname = userRepo.GetMedicineByname(sname);
                    dynamic mymodel = new ExpandoObject();


                    mymodel.Medicine = medname;
                    mymodel.Doctor = users;
                    mymodel.Appointment = appointments;
                    mymodel.Message = msgs;
                    return View(mymodel);
                }
                else
                {
                    var medname = userRepo.GetMedicine().ToList();
                    dynamic mymodel = new ExpandoObject();

                    mymodel.Medicine = medname;
                    mymodel.Doctor = users;
                    mymodel.Appointment = appointments;
                    mymodel.Message = msgs;
                    return View(mymodel);
                }
            }
            else
            {
                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
                return View(model);
            }
           
            
                
            
        }

 

            [HttpGet]
            public ActionResult Patient(ViewModel.UserViewModel usr)
        {
            if (Session["Myuser"] != null && (Session["Myrole"].Equals("2")))
            {
                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
                var uname = Request.QueryString["username"];
                var users = userRepo.GetProfiles(uname);

                var pid = userRepo.getuserbyuname(uname);

                var appointments = appointmentRepo.GetAppointmentsForPatient(pid.Id);
                //var dlist = userRepo.GetDoctorByDept(Request["Departments"]);

                // var dusers = userRepo.GetDoctor(uname);
                var med = userRepo.GetMedicine().ToList();

                dynamic mymodel = new ExpandoObject();

                mymodel.Medicine = med;
                mymodel.User = users;
                mymodel.Appointment = appointments;

                return View(mymodel);

            }
            else
            {
                Session["MyUser"] = null;
                Session["Myrole"] = null;
                return  RedirectToAction("login", "manageusers");
            }

             
        }





        [HttpPost]
        public ActionResult Patient(ViewModel.UserViewModel usr, string uname)
        {

            userRepo = new ClinicalDAL.UserRepository();
            ClinicalDAL.EF.User u = new ClinicalDAL.EF.User();
         
            if (!ModelState.IsValid)
            {

                string username = Request["txtuname"];
                string Phone = Request["txtphone"];
                string Address = Request["txtadd"];                
                string Password = Request["txtpwd"];
                 var pid = userRepo.getuserbyuname(username);
                userRepo.UpdateProfile(username, Phone, Address, Password);

                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
               // var uname = Request.QueryString["username"];
                var users = userRepo.GetProfiles(username);
                var appointments = appointmentRepo.GetAppointmentsForPatient(pid.Id);
               
                var med = userRepo.GetMedicine().ToList();
                dynamic mymodel = new ExpandoObject();

                mymodel.Medicine = med;
                mymodel.User = users;
                mymodel.Appointment = appointments;

                return View(mymodel);

            }
            else
            {
                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
                return View(model);
            }
        }


        public ActionResult Pharmacist(ViewModel.UserViewModel usr)
        {
            if (Session["Myuser"] != null && (Session["Myrole"].Equals("4")))
            {

                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
                var uname = Request.QueryString["username"];
                var users = userRepo.GetProfiles(uname);


                var med = userRepo.GetMedicine().ToList();

                dynamic mymodel = new ExpandoObject();

                mymodel.Medicine = med;
                mymodel.User = users;
                // mymodel.Appointment = appointments;

                return View(mymodel);
            }
            else
            {
                Session["Myuser"] = null;
                Session["Myrole"] = null;
                return  RedirectToAction("login", "manageusers");
            }
        }

        [HttpPost]
        public ActionResult Pharmacist(ViewModel.UserViewModel usr, string u)
        {
            if (Session["Myuser"] != null && (Session["Myrole"].Equals("4")))
            {

                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
                /*var uname = Request.QueryString["username"];
                var users = userRepo.GetProfiles(uname);*/
                string username = Request["txtuname"];
                string Phone = Request["txtphone"];
                string Address = Request["txtadd"];
                string Password = Request["txtpwd"];
                var pid = userRepo.getuserbyuname(username);
                userRepo.UpdateProfile(username, Phone, Address, Password);                
                var users = userRepo.GetProfiles(username);
                var med = userRepo.GetMedicine().ToList();

                dynamic mymodel = new ExpandoObject();

                mymodel.Medicine = med;
                mymodel.User = users;
                return View(mymodel);
            }
            else
            {
                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
                return View(model);
            }
        }


        [HttpGet]
        public ActionResult FrontOfficeMember(ViewModel.UserViewModel usr)
        {
            if (Session["Myuser"] != null && (Session["Myrole"].Equals("3")))
            {
                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
                var uname = Request.QueryString["username"];
                var users = userRepo.GetProfiles(uname);
                var appointments = appointmentRepo.GetAppointmentsForFrontOffice();
                var pusers = userRepo.GetPatients();

                dynamic mymodel = new ExpandoObject();


                mymodel.User = users;
                mymodel.Patient = pusers;
                mymodel.Appointment = appointments;


                return View(mymodel);
            }
            else
            {

                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult FrontOfficeMember(ViewModel.UserViewModel usr, string u)
        {
            if (Session["Myuser"] != null && (Session["Myrole"].Equals("3")))
            {
                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
               // var uname = Request.QueryString["username"];
                string username = Request["txtuname"];
                string Phone = Request["txtphone"];
                string Address = Request["txtadd"];
                string Password = Request["txtpwd"];
                var pid = userRepo.getuserbyuname(username);
                userRepo.UpdateProfile(username, Phone, Address, Password);


                var users = userRepo.GetProfiles(username);
                var appointments = appointmentRepo.GetAppointmentsForFrontOffice();
                var pusers = userRepo.GetPatients();

                dynamic mymodel = new ExpandoObject();
                mymodel.User = users;
                mymodel.Patient = pusers;
                mymodel.Appointment = appointments;


                return View(mymodel);
            }
            else
            {
                Session["MyUser"] = null;
                Session["Myrole"] = null;
                return RedirectToAction("login", "manageusers");
            }
        }

    }
}