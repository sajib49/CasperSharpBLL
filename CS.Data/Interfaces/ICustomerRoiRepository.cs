using System.Collections.Generic;
using CS.Model;
using CS.Model.ViewModels;

namespace CS.Data.Interfaces
{
    public interface ICustomerRoiRepository:IRepository<CustomerRoi>
    {
        List<CustomerRoiDetails> GetCustomerRoiDetailse();
    }
}
