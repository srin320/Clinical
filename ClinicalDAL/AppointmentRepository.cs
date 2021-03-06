using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalDAL
{
    public class AppointmentRepository : BaseRepository
    {
        UserRepository userRepo;
        public List<ClinicalDAL.EF.Appointment> GetAppointmentsForPatient(int pid)
        {
            //List<ClinicalDAL.EF.Appointment> res = mycontext.Appointments.Where(x => x.PatientId == pid).ToList();
            return mycontext.Appointments.Include("User").Where(u => u.PatientId == pid).ToList();
        }

        public List<ClinicalDAL.EF.Appointment> GetAppointmentsForDoctor(int did)
        {
            //return mycontext.Appointments.Where(x => x.DoctorId == did).ToList();
            return mycontext.Appointments.Include("User").Where(u => u.DoctorId == did).ToList();
        }

        public List<ClinicalDAL.EF.Appointment> GetAppointmentsForFrontOffice()
        {
            return mycontext.Appointments.Include("User").ToList();
        }

        public EF.Appointment GetAppointmentById(int id)
        {
            return mycontext.Appointments.Where(u => u.Id == id).FirstOrDefault();
        }

        public void ApproveAppointment(int id)
        {
            EF.Appointment app = GetAppointmentById(id);
            app.Status = 1;
            int rowaff = mycontext.SaveChanges();
        }

        public void DenyAppointment(int id)
        {
            EF.Appointment app = GetAppointmentById(id);
            mycontext.Appointments.Remove(app);
            int rowaff = mycontext.SaveChanges();
        }

        public void AddAppointmnet(EF.Appointment apt)
        {

            mycontext.Appointments.Add(apt);


            int noeowaff = mycontext.SaveChanges();

        }
    }
}
