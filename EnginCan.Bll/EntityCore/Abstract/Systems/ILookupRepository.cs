using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnginCan.Dal.EfCore.Abstract;
using EnginCan.Entity.Models.Systems;

namespace EnginCan.Bll.EntityCore.Abstract.Systems
{
    public interface ILookupRepository : IEntityBaseRepository<Lookup>
    {
    }
}
