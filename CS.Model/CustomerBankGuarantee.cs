using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Model
{
    [Table("t_CustomerBankGuarantee")]
    public class CustomerBankGuarantee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TranId { get; set; }
        [Required(ErrorMessage = "Please Enter Customer Id")]
        [Display(Name="Customer")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Please Enter Bg Amount")]
        [Display(Name = "Bg Amount")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter Valid Bg Amount")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public double BgAmount { get; set; }
        [Required(ErrorMessage = "Please Enter Opening Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Opening Date")]
        public DateTime OpeningDate { get; set; }
        [Required(ErrorMessage = "Please Enter Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        [Required(ErrorMessage = "Please Enter Is Active")]
       
        [Display(Name = "Is Active")]
        public int IsActive { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Cutomer { get; set; }

    }
}