using System;
using System.Collections.Generic;
using System.Linq;
using CS.Data.Interfaces;
using CS.Model;
using CS.Model.ViewModels;

namespace CS.Data.Repositories
{
    public class CustomerRoiRepository : GenericRepository<CustomerRoi>, ICustomerRoiRepository
    {
        private readonly BllDbContext _db = new BllDbContext();
        public List<CustomerRoiDetails> GetCustomerRoiDetailse()
        {
            const string sCustRoiDetail = @"
                                    select AreaName, TerritoryName, CustomerCode, CustomerName, ExpMonth,TYear,
                                    TranMonth=SUBSTRING(DateName(mm,DATEADD(mm,TMonth,-1)),1,3),
                                    CommisionInc, KPIInc, CollectionInc,VehicleSubsidiary , OthersInc ,
                                    TotalIncome, MgrSalary , SASalary , RASalary , DriverSalary ,  VehicleExp , 
                                    OfficeRent , Maintenance ,OthersExp ,BGExp,TotalExpense, StockInc , CreditToMKT ,
                                    PromRepInc , BG, TotalInvestment, Income, ROI
                                    from
                                    (
                                    select CustomerID, ExpMonth,TYear,  TMonth, CommisionInc, KPIInc, CollectionInc,
                                    VehicleSubsidiary , OthersInc , TotalIncome, MgrSalary , SASalary , RASalary , DriverSalary ,  VehicleExp , OfficeRent , 
                                    Maintenance ,OthersExp ,BGExp,TotalExpense, StockInc , CreditToMKT , PromRepInc ,BG, TotalInvestment, Income,
                                    sum(case when TotalInvestment >0 then ((Income/TotalInvestment)*100) else 0 end) as ROI
                                    from
                                    (
                                    select CustomerID, ExpMonth,TYear,  TMonth, CommisionInc, KPIInc, CollectionInc,
                                    VehicleSubsidiary , OthersInc , TotalIncome, MgrSalary , SASalary , RASalary , DriverSalary ,  VehicleExp , OfficeRent , 
                                    Maintenance ,OthersExp ,BGExp,TotalExpense, StockInc , CreditToMKT , PromRepInc ,BG, TotalInvestment, sum (TotalIncome-TotalExpense) as Income
                                    from
                                    (
                                    select CustomerID, ExpMonth,year(ExpMonth) as TYear, month(ExpMonth) as TMonth, CommisionInc, KPIInc, CollectionInc,
                                    VehicleSubsidiary , OthersInc ,sum(CommisionInc + KPIInc + CollectionInc +
                                    VehicleSubsidiary + OthersInc) as TotalIncome, MgrSalary , SASalary , RASalary , DriverSalary ,  VehicleExp , OfficeRent , 
                                    Maintenance ,OthersExp ,BGExp, sum(MgrSalary + SASalary + RASalary + DriverSalary +  VehicleExp + OfficeRent + 
                                    Maintenance + OthersExp+BGExp) as TotalExpense, StockInc , CreditToMKT , PromRepInc ,BG, sum(StockInc + CreditToMKT + PromRepInc) as TotalInvestment
                                    from t_customerroi
                                    where ExpMonth between '01-Jan-2017' and '01-Jan-2018' and ExpMonth < '01-Jan-2018'
                                    group by CustomerID, ExpMonth, CommisionInc, KPIInc, CollectionInc,
                                    VehicleSubsidiary , OthersInc , MgrSalary , SASalary , RASalary , DriverSalary , OthersExp , VehicleExp , OfficeRent , 
                                    Maintenance , StockInc , CreditToMKT , PromRepInc,BGExp,BG  
                                    ) as Main 
                                    group by CustomerID, ExpMonth,TYear,  TMonth, CommisionInc, KPIInc, CollectionInc,
                                    VehicleSubsidiary , OthersInc , TotalIncome, MgrSalary , SASalary , RASalary , DriverSalary ,  VehicleExp , OfficeRent , 
                                    Maintenance ,OthersExp ,BGExp,TotalExpense, StockInc , CreditToMKT , PromRepInc ,BG, TotalInvestment
                                    ) as Final
                                    group by CustomerID, ExpMonth,TYear, TMonth, CommisionInc, KPIInc, CollectionInc,
                                    VehicleSubsidiary , OthersInc , TotalIncome, MgrSalary , SASalary , RASalary , DriverSalary ,  VehicleExp , OfficeRent , 
                                    Maintenance ,OthersExp ,BGExp,TotalExpense, StockInc , CreditToMKT , PromRepInc ,BG,TotalInvestment, Income
                                    ) as Roi 
                                    inner join (Select AreaName, TerritoryName, CustomerCode, CustomerName, CustomerID from v_customerdetails) as Cust on Roi.CustomerID=Cust.CustomerID";

            return _db.Database.SqlQuery<CustomerRoiDetails>(sCustRoiDetail).ToList();
        }
    }
}