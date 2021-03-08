using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicalDAL.EF ;

namespace ClinicalDAL
{
    public class BaseRepository
    {
        protected ClinicalDAL.EF.ClinicalEntities mycontext;
        public BaseRepository()
        {
            mycontext = new EF.ClinicalEntities();
        }
    }
    public class UserRepository : BaseRepository
    {
        public List<EF.User> GetUser()
        {
            IEnumerable<EF.User> query = from usr in mycontext.Users                                         
                                         select usr;

            return query.ToList();
        }
        public List<EF.User> GetDoctor(string  uname)
        {
            IEnumerable<EF.User> query = from usr in mycontext.Users
                                         .Include("Doctors") where (usr.Username.Equals(uname))                                      
                                         select usr;

            return query.ToList();
        }

        public List<EF.Medicine> GetMedicine()
        {

            IEnumerable<EF.Medicine> query = from usr in mycontext.Medicines
                                         select usr;

            return query.ToList();


        }

        public EF.Medicine GetMedicineById(int mid)
        {
            return mycontext.Medicines.Where(u => u.Id == mid).FirstOrDefault();
        }


        public List<EF.Medicine> GetMedicineByname(string name)
        {
            return mycontext.Medicines.Where(u => u.Name.ToLower().StartsWith(name.ToLower())).OrderBy(u => u.Name).ToList();
           // return context.Users.Include("Role").Where(u => u.Name.ToLower().StartsWith(name.ToLower())).OrderBy(u => u.Name).ToList();

        }




        public List<EF.User> GetProfiles(string uname)
        {
            IEnumerable<EF.User> query = from usr in mycontext.Users
                                         where (usr.Username.Equals(uname))
                                         select usr;

            return query.ToList();
        }

        public List<EF.User> GetPatients()
        {
            IEnumerable<EF.User> query = from usr in mycontext.Users
                                         where (usr.RoleId==2)
                                         select usr;

            return query.ToList();
        }
        public void AddUser(EF.User usr)

        {
            
            mycontext.Users.Add(usr);

            int  norowaff = mycontext.SaveChanges();
           
           // EF.User user = GetUserByID(usr.Id);
         

        }


        public void AddMedicine(EF.Medicine med)

        {

            mycontext.Medicines.Add(med);

            int norowaff = mycontext.SaveChanges();

            // EF.User user = GetUserByID(usr.Id);


        }

        public void DeleteMedicine(int id)
        {
            var item = mycontext.Medicines.Where(u => u.Id == id).FirstOrDefault();
            mycontext.Medicines.Remove(item);
            int rowaff = mycontext.SaveChanges();
        }


        public void UpdateMedicine(int id, int qty, decimal price)
        {
            var med = mycontext.Medicines.Where(u => u.Id == id).FirstOrDefault();

            med.Quantity = qty;
            med.Price = Convert.ToDouble(price);

            mycontext.SaveChanges();
        }


        public void AddUser(EF.User usr, EF.Doctor doc)
        {

            mycontext.Users.Add(usr);

            //int norowaff = mycontext.SaveChanges();

            EF.User user = GetUserByID(usr.Id);
            doc.UserId = usr.Id;
            mycontext.Doctors.Add(doc);
           int noeowaff=  mycontext.SaveChanges();

        }


        public EF.User GetUserByID(int id)
        {
            return mycontext.Users.Where(u => u.Id == (id)).FirstOrDefault();
        }
        /*public EF.User GetUserByID(int id)
        {
            return mycontext.Users.Include("Role").Where(u => u.Id == (id)).OrderBy(u => u.Id).FirstOrDefault();
        }*/


         public EF.User getuserbyuname(string uname)
         {

            // return mycontext.Users.Where(u => u.Username.ToLower().StartsWith(uname.ToLower())).FirstOrDefault();

            return mycontext.Users.Where(s => s.Username.Equals(uname)).FirstOrDefault();

         }

        public EF.Appointment getdoctorbyappointment(int id)
        {

            // return mycontext.Users.Where(u => u.Username.ToLower().StartsWith(uname.ToLower())).FirstOrDefault();

            return mycontext.Appointments.Where(s => s.PatientId==id).FirstOrDefault();

        }



        public bool checkpass(string uname, string pass)
        {
           var dbpass= mycontext.Users.Where(u => u.Username.ToLower().StartsWith(uname.ToLower())).FirstOrDefault();

            string newpass = dbpass.Password;
            if(newpass==pass)
            {
                return true;
            }
           
                return false;
            
            //var data = mycontext.Users.Where(s => s.Username.Equals(uname) && s.Password.Equals(pass)).ToList();
        }
        public int  getRoleId(string uname)
        {
            var obj = mycontext.Users.Where(u => u.Username.ToLower().StartsWith(uname.ToLower())).FirstOrDefault();

             return obj.RoleId;            
        }

        public void UpdateUser(string username, string Phone, string Address, string Qualification, string Password)
        {

            var up = mycontext.Users.First(u => u.Username.Equals(username));
             
            up.Phone = Phone;
            up.Address =Address;
            up.Password = Password;

            var ud = mycontext.Doctors.First(u => u.UserId==up.Id);
                 ud.Qualification = Qualification;

            mycontext.SaveChanges();
        
        }
        public void UpdateProfile(string username, string Phone, string Address, string Password)
        {

            var up = mycontext.Users.First(u => u.Username.Equals(username));

            up.Phone = Phone;
            up.Address = Address;
            up.Password = Password;
                 

            mycontext.SaveChanges();

        }

        public List<EF.User> GetDoctorByDept()
        {
           var did = from usr in mycontext.Users
                                         .Include("Doctors") where(usr.RoleId==1)
                    
                                         select usr;

            

            return did.ToList();
        }



    }




}