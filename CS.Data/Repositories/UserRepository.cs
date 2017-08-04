using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CS.Data.Interfaces;
using CS.Model;

namespace CS.Data.Repositories
{
    public class UserRepository:GenericRepository<User>, IUserRepository
    {
        public List<User> GetUserDetails(int nUserId)
        {
            //string sGetUserDetails = @"SELECT * FROM t_User a,t_Employee b
            //                            WHERE  a.EmployeeId=b.EmployeeID
            //                            AND a.UserID = "+nUserId+" ";

            const string sGetUserDetails = @"SELECT * FROM t_User a";

            List<User> anUser;
            using (var oBllDbContext = new BllDbContext())
            {
                anUser = oBllDbContext.Database.SqlQuery<User>(sGetUserDetails).ToList();
            }
            return anUser;
        }
    }
}