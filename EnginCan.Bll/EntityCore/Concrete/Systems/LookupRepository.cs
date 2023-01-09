using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnginCan.Bll.EntityCore.Abstract.Systems;
using EnginCan.Dal.EfCore;
using EnginCan.Dal.EfCore.Concrete;
using EnginCan.Entity.Models.Systems;

namespace EnginCan.Bll.EntityCore.Concrete.Systems
{
    public class LookupRepository : EntityBaseRepository<Lookup>, ILookupRepository
    {
        public LookupRepository(EnginCanContext context) : base(context)
        {
        }
    }
}
