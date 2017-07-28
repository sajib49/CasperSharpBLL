using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Data.Interfaces
{
    public interface IBaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

    }
}
