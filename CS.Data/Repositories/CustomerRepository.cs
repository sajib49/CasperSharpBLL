using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CS.Data.Interfaces;
using CS.Model;

namespace CS.Data.Repositories
{
    public class CustomerRepository:GenericRepository<Customer>,ICustomerRepository
    {

    }
}