using EnginCan.Core.Utilities.Results.Abstract;
using EnginCan.Dal.EfCore.Abstract;
using EnginCan.Entity.Models.Systems;
using System.Collections.Generic;
using System.Linq;

namespace EnginCan.Bll.EntityCore.Abstract.Systems
{
    public interface IPageRepository : IEntityBaseRepository<Page>
    {
        IDataResult<IQueryable<Page>> GetAllPagePermissions();
        IDataResult<Page> GetById(int id);
        IResult AddPage(Page page);
        IResult UpdatePage(Page page);
        IResult DeletePage(int id);
        IDataResult<List<Page>> GetPermissionPage();

    }
}
