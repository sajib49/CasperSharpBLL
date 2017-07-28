using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Model
{
    [Table("t_CustomerROI")]
    public class CustomerRoi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ScaffoldColumn(false)]
        public int TranId { get; set; }
        [Required(ErrorMessage = "Please Enter Customer Code")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        //[Required(ErrorMessage = "Please Enter Last Update Date")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        //[DataType(DataType.Date)]
        //[Display(Name = "Update Date")]
        public DateTime LastUpdateDate { get; set; }

        
        [Required(ErrorMessage = "Please Enter Expense Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime ExpMonth { get; set; }

        
        [Display(Name = "Commision Incentive")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid Commission Incentive")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal CommisionInc { get; set; }

        
        [Display(Name = "KPI Incentive")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid KPI Incentive")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal KPIInc { get; set; }

        
        [Display(Name = "Collection Incentive")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid Collection Incentive")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal CollectionInc { get; set; }


        
        [Display(Name = "Vehicle Subsidiary")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid Vehicle Subsidiary")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal VehicleSubsidiary { get; set; }

        
        [Display(Name = "Others Incentive")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal OthersInc { get; set; }

        
        [Display(Name = "Mgr Salary")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid Mgr Salary")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal MgrSalary { get; set; }

        
        [Display(Name = "SA Salary")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid SA Salary")]
        public decimal SaSalary { get; set; }

        
        [Display(Name = "RA Salary")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid RA Salary")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal RaSalary { get; set; }

        
        [Display(Name = "Driver Salary")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid Driver Salary")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal DriverSalary { get; set; }

        
        [Display(Name = "Others Expense")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal OthersExp { get; set; }

        
        [Display(Name = "Vehicle Expense")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid vehicale Expense")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal VehicleExp { get; set; }


        
        [Display(Name = "Office Rent")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid Office Rent")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal OfficeRent { get; set; }

        
        [Display(Name = "Maintenance Expense")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid Maintenance Expense")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal Maintenance { get; set; }

        
        [Display(Name = "Stock Investment")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid Stock Investment")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal StockInc { get; set; }

        
        [Display(Name = "Credit To MKT")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal CreditToMkt { get; set; }


        [Display(Name = "Promotion/Rep Investment")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal PromRepInc { get; set; }

        
        [Display(Name = "BG")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal BG { get; set; }

        [Display(Name= "BGExp" )]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal BGExp { get; set; }

        public int IsCurrent { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public decimal? ProfitRoi { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Cutomer { get; set; }

    }
}