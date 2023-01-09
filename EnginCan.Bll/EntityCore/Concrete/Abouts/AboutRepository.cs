using EnginCan.Bll.EntityCore.Abstract.Users;
using EnginCan.Dal.EfCore.Concrete;
using EnginCan.Dal.EfCore;
using EnginCan.Entity.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using EnginCan.Entity.Models.Abouts;
using EnginCan.Bll.EntityCore.Abstract.Abouts;
using EnginCan.Bll.Helpers;
using EnginCan.Core.Utilities.Results.Abstract;
using EnginCan.Core.Utilities.Results.Concrete;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EnginCan.Entity.Shared;

namespace EnginCan.Bll.EntityCore.Concrete.Abouts
{
    public class AboutRepository : EntityBaseRepository<About>, IAboutRepository
    {
        public AboutRepository(EnginCanContext context) : base(context)
        {
        }
        /// <summary>
        /// Yeni about oluşturur
        /// </summary>
        public IResult AddAbout(About about)
        {
            try
            {
                Add(about);
                Commit();
                return new SuccessResult(SystemConstants.AddedMessage);
            }
            catch (Exception)
            {
                return new ErrorResult(SystemConstants.AddedErrorMessage);
            }
        }


        /// <summary>
        /// About siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IResult DeleteAbout(int id)
        {
            var result = FindBy(a => a.Id == id).FirstOrDefault();
            if (result == null)
                return new ErrorResult(SystemConstants.NoData);

            else
            {
                try
                {
                    Delete(result);
                    Commit();
                    return new SuccessResult(SystemConstants.DeletedMessage);
                }
                catch (Exception)
                {
                    return new ErrorResult(SystemConstants.DeletedErrorMessage);
                }
            }
        }

        /// <summary>
        /// Tekil bilgisine göre sayfa getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IDataResult<About> GetById(int id)
        {
            var result = FindBy(m => m.Id == id && m.DataStatus == DataStatus.Activated)
                        .AsNoTracking()
                       .FirstOrDefault();

            if (result != null)
                return new SuccessDataResult<About>(result);
            else
                return new ErrorDataResult<About>(null, SystemConstants.NoData);
        }

        /// <summary>
        /// Yetkili sayfaları döndürür
        /// </summary>
        /// <returns></returns>
        public IDataResult<IQueryable<About>> GetAllAbouts()
        {
            var result = FindBy(m => m.DataStatus == DataStatus.Activated);
            return new SuccessDataResult<IQueryable<About>>(result);
        }

        /// <summary>
        /// Sayfa günceller
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IResult UpdateAbout(About about)
        {
            var hasData = FindBy(m => m.Id == about.Id && m.DataStatus == DataStatus.Activated)
                .AsNoTracking()
                         .FirstOrDefault();
            if (hasData == null)
                return new ErrorDataResult<About>(null, SystemConstants.NoData);

            try
            {
                Update(about);
                Commit();

                return new SuccessResult(SystemConstants.UpdatedMessage);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<About>(null, SystemConstants.UpdatedErrorMessage);
            }

        }

    }
}
