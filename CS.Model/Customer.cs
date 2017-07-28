using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Model
{
    [Table("t_Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerId { get; set; }
        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        public virtual ICollection<CustomerBankGuarantee> BankGuarantees { get; set; }
        public virtual ICollection<CustomerRoi> CustomerRois { get; set; }
    }
}