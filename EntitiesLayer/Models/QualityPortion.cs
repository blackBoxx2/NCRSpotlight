using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class QualityPortion
    {

        [Key]
        public int ID { get; set; }


        public int ProductID { get; set; } = 1;

        public Product? Product { get; set; }

        
        [Display(Name = "Quantity Ordered")]
        public int? Quantity { get; set; }

        
        [Display(Name = "Quantity Defective")]
        public int? QuantityDefective { get; set; }

        
        [Display(Name = "Order Number")]
        public string? OrderNumber { get; set; } = " ";

        
        [Display(Name = "Defect Description")]
        public string? DefectDescription { get; set; } = " ";

        [Display(Name = "Created By")]
        public string RepID { get; set; }

        public ICollection<QualityPicture> qualityPictures { get; set; } = new HashSet<QualityPicture>();  
    }
}
