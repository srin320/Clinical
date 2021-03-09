using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicalDAL.EF;

namespace CASProject.Controllers
{
    public class ManageUsersController : Controller
    {
        protected ClinicalDAL.EF.ClinicalEntities mycontext;
        ClinicalDAL.UserRepository userRepo;
        ClinicalDAL.RoleRepository roleRepo;
        public ManageUsersController()
        {
            userRepo = new ClinicalDAL.UserRepository();
            roleRepo = new ClinicalDAL.RoleRepository();
            mycontext = new ClinicalDAL.EF.ClinicalEntities();
        }

        [HttpGet]
        public ActionResult Register()
        {

            ViewModel.UserViewModel model = new ViewModel.UserViewModel();

            roleRepo = new ClinicalDAL.RoleRepository();
            model.Roles = roleRepo.GetRoles().ToHashSet();
            return View(model);

        }



        [HttpPost]
        public ActionResult Register(ViewModel.UserViewModel usr)
        {
            if (ModelState.IsValid)
            {

                ClinicalDAL.EF.User u = new ClinicalDAL.EF.User();
                ClinicalDAL.EF.Doctor d = new ClinicalDAL.EF.Doctor();

                u.RoleId = usr.RoleId;
                if (usr.RoleId == 1)
                {
                    u.Name = usr.Name;
                    u.DOB = Convert.ToDateTime(usr.DOB);
                    u.Phone = usr.Phone;
                    u.Email = usr.Email;
                    u.Password = usr.Password;
                    u.Username = usr.Username;
                    u.Address = usr.Address;
                    u.Gender = usr.Gender;
                    d.Qualification = usr.Qualification;

                    d.RegNo = Convert.ToInt32(usr.RegNo);

                    d.DeptName = Request["Departments"];
                    // d.DeptName = doc.DeptName;
                    userRepo.AddUser(u, d);
                }
                else
                {
                    u.Name = usr.Name;
                    u.DOB = Convert.ToDateTime(usr.DOB);
                    u.Phone = usr.Phone;
                    u.Email = usr.Email;
                    u.Password = usr.Password;
                    u.Username = usr.Username;
                    u.Address = usr.Address;
                    u.Gender = usr.Gender;
                    userRepo.AddUser(u);
                }



                return RedirectToAction("Login");


            }
            else
            {

                ViewModel.UserViewModel model = new ViewModel.UserViewModel();
                roleRepo = new ClinicalDAL.RoleRepository();
                model.Roles = roleRepo.GetRoles().ToHashSet();
                return View(model);



            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewModel.UserViewModel model = new ViewModel.UserViewModel();

            return View(model);


        }
        [HttpPost]
        public ActionResult Login(ViewModel.UserViewModel usr)
        {
            ClinicalDAL.EF.User u = new ClinicalDAL.EF.User();

            string uname = usr.Username;
            var user = userRepo.GetProfiles(uname).FirstOrDefault();
            

            //var obj =userRepo.getpassbyuname(uname);
            bool val = userRepo.checkpass(uname, usr.Password);

            //var data = mycontext.Users.Where(s => s.Username.Equals(uname) && s.Password.Equals(pass)).ToList();
            if (val)
            {
                Session["Myname"] = user.Name;
                Session["Myuser"] = uname;
                int rid = userRepo.getRoleId(uname);
                Session["Myrole"] = rid.ToString();

                if (rid == 1)
                {
                    //Session["Myrole"] = rid;
                    //return RedirectToAction("/Profiles/Doctor");
                    return RedirectToAction("Doctor", "Profiles", new { username = uname });
                }
                else if (rid == 2)
                {
                    //Session["Myrole"] = rid;
                    return RedirectToAction("Patient", "Profiles", new { username = uname });
                }
                else if (rid == 3)
                {
                    // Session["Myrole"] = rid;
                    return RedirectToAction("FrontOfficeMember", "Profiles", new { username = uname });
                }
                else
                {
                    // Session["Myrole"] = rid;
                    return RedirectToAction("Pharmacist", "Profiles", new { username = uname });
                }



            }
            else
            {
                ViewBag.Message = "Wrong Password Or Username ! Try Again";
                return View();
                //   return RedirectToAction("Login");
            }


        }
        [HttpGet]
        public ActionResult Logout(ViewModel.UserViewModel usr)
        {



            Session["MyUser"] = null;
            Session["Myrole"] = null;
            return RedirectToAction("index","home");


        }
    }
}


 