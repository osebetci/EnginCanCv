using System;
using System.Collections.Generic;
using System.Text;
using EnginCan.Dal.EfCore.Abstract;
using EnginCan.Entity.Models.Systems;

namespace EnginCan.Bll.EntityCore.Abstract.Systems
{
    public interface ILookupTypeRepository : IEntityBaseRepository<LookupType>
    {
    }
}
