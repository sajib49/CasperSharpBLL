using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CS.Data.Interfaces;

namespace CS.Data.Repositories
{
    public class BaseRepository<TEntity> : GenericRepository<TEntity>, IBaseRepository<TEntity> where TEntity : class
    {

    }
}