using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CS.Model
{
    [Table("t_User")]
    public class User
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
         [ScaffoldColumn(false)]
        public string UserFullName { get; set; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Enter User Name First")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter Password First")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [ScaffoldColumn(false)]
        public string Salt { get; set; }
        [ScaffoldColumn(false)]
        public string UserSbUs { get; set; }
        [ScaffoldColumn(false)]
        public int? EmployeeId { get; set; }
    }
}