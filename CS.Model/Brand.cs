using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Model
{
    [Table("t_Brand")]
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        public string BrandCode { get; set; }
        public string BrandDesc{ get; set; }
        public int IsActive { get; set; }
        public int BrandLevel{ get; set; }
        public int ParentId{ get; set; }

        public int BrandPos { get; set; }

        public int UploadStatus{ get; set; }

        public DateTime UploadDate { get; set; }

        public DateTime DownloadDate { get; set; }

        public int RowStatus{ get; set; }

        public int Terminal{ get; set; }
        
    }
}
