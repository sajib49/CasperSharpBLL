using System.Collections.Generic;
using CS.Model;

namespace CS.Data.Interfaces
{
    public interface IUserRepository :IRepository<User>
    {
        List<User> GetUserDetails(int nUserId);
    }
}
