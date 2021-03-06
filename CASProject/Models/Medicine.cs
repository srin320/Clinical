using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASProject.Models
{
    public class Medicine
    {
        public int id { get; set; }
        public string Name{ get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime  ExpDate { get; set; }


    }
}