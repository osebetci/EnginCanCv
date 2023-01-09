using EnginCan.Core.Utilities.Results.Abstract;
using EnginCan.Dal.EfCore.Abstract;
using EnginCan.Entity.Models.Abouts;
using System.Collections.Generic;
using System.Linq;

namespace EnginCan.Bll.EntityCore.Abstract.Abouts
{
    public interface IAboutRepository : IEntityBaseRepository<About>
    {
        IDataResult<IQueryable<About>> GetAllAbouts();
        IDataResult<About> GetById(int id);
        IResult AddAbout(About about);
        IResult UpdateAbout(About about);
        IResult DeleteAbout(int id);
    }

}
