using System;

namespace CS.Model.ViewModels
{
   
    public class CustomerRoiDetails
    {      
        public string AreaName { get; set; }
        public string TerritoryName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public DateTime ExpMonth { get; set; }
        public DateTime Tmonth { get; set; }
        public decimal CommisionInc { get; set; }
        public decimal KpiInc { get; set; }
        public decimal CollectionInc { get; set; }
        public decimal VehicleSubsidiary { get; set; }
        public decimal OthersInc { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal MgrSalary { get; set; }
        public decimal SaSalary { get; set; }
        public decimal RaSalary { get; set; }
        public decimal DriverSalary { get; set; }
        public decimal VehicleExp { get; set; }
        public decimal OfficeRent { get; set; }
        public decimal Maintenance { get; set; }
        public decimal OthersExp { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal StockInc { get; set; }
        public decimal CreditToMkt { get; set; }
        public decimal PromRepInc { get; set; }
        public decimal TotalInvestment { get; set; }
        public decimal Income { get; set; }
        public decimal Roi { get; set; }

    }
}