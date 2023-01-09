using System;
using System.Collections.Generic;
using System.Text;
using EnginCan.Bll.EntityCore.Abstract.Systems;
using EnginCan.Dal.EfCore;
using EnginCan.Dal.EfCore.Concrete;
using EnginCan.Entity.Models.Systems;

namespace EnginCan.Bll.EntityCore.Concrete.Systems
{
    public class LookupTypeRepository : EntityBaseRepository<LookupType>, ILookupTypeRepository
    {
        public LookupTypeRepository(EnginCanContext context) : base(context)
        {
        }
    }
}
