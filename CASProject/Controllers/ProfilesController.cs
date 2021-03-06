﻿using System;
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

        [HttpPost]
        public ActionResult Doctor(ViewModel.UserViewModel usr,string uname)
        {
            userRepo = new ClinicalDAL.UserRepository();
            ClinicalDAL.EF.User u = new ClinicalDAL.EF.User();
            string  qryname = Request.QueryString["username"];
            if (!ModelState.IsValid)
            {
                string username = Request["txtuname"];
                string Phone = Request["txtphone"];
                string Address = Request["txtadd"];
                string Qualification = Request["txtqual"];
                string Password = Request["txtpwd"];

                var obj = userRepo.GetProfiles(username);
                userRepo.UpdateUser(qryname, Phone, Address, Qualification, Password);


                var users = userRepo.GetDoctor(qryname);
                string sname = Request["searchname"];
                if (sname != null)
                {
                    var medname = userRepo.GetMedicineByname(sname);
                    dynamic mymodel = new ExpandoObject();

                    mymodel.Medicine = medname;
                    mymodel.Doctor = users;
                    return View(mymodel);
                }
                else
                {
                    var medname = userRepo.GetMedicine().ToList();
                    dynamic mymodel = new ExpandoObject();

                    mymodel.Medicine = medname;
                    mymodel.Doctor = users;
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

                userRepo.UpdateProfile(username, Phone, Address, Password);

                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
               // var uname = Request.QueryString["username"];
                var users = userRepo.GetProfiles(username);


                return View(users);


            }
            else
            {
                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
                return View(model);
            }
        }

        
















            public ActionResult Pharmacist(ViewModel.UserViewModel usr)
        {
            ViewModel.UserViewModel model = new ViewModel.UserViewModel();
            var uname = Request.QueryString["username"];
            var users = userRepo.GetProfiles(uname);


            return View(users);

        }
        public ActionResult FrontOfficeMember(ViewModel.UserViewModel usr)
        {
            ViewModel.UserViewModel model = new ViewModel.UserViewModel();
            var uname = Request.QueryString["username"];
            var users = userRepo.GetProfiles(uname);


            return View(users);

        }

    }
}