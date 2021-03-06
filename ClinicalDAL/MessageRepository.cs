using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalDAL
{
    public class MessageRepository : BaseRepository
    {
        public List<EF.Message> GetMessagesForPatient(int pid, int did)
        {
            return mycontext.Messages.Where(x => (x.SenderId == pid && x.RecieverId == did) || (x.RecieverId == pid && x.SenderId == did)).ToList();
        }

        public List<EF.Message> GetMessagesForDoctor(int did, int pid)
        {
            return mycontext.Messages.Where(x => (x.RecieverId == did && x.SenderId == pid) || (x.SenderId == did && x.RecieverId == pid)).ToList();
        }

        public List<EF.Message> GetLatestMsgForDoctor(int did)
        {
            var msg = (from p in mycontext.Messages
                       where p.RecieverId == did
                       group p by p.SenderId into g
                       let date = g.Max(d => d.Date)
                       from pr in g
                       where pr.Date == date
                       select pr).ToList();

            return msg;
        }

        public int Send(string msg, int sid, int rid)
        {
            EF.Message newMsg = new EF.Message();
            newMsg.SenderId = sid;
            newMsg.RecieverId = rid;
            newMsg.Message1 = msg;
            newMsg.Status = 1;
            newMsg.Date = DateTime.Now;

            mycontext.Messages.Add(newMsg);
            int rowaff = mycontext.SaveChanges();

            return rowaff;
        }
    }
}
