using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicalDAL.EF;

namespace ClinicalDAL
{
   public  class RoleRepository : BaseRepository
    {
        public HashSet<Role> GetRoles()
        {
            return mycontext.Roles.OrderBy(r => r.Name).ToHashSet();

        }

    }
}
