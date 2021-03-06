using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASProject.Models
{
    public class Messages
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int RecieverId { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }

        public byte Status { get; set; }
    }
}