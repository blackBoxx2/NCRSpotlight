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
        [Display(Name = "NCR No.")]
        public int ID { get; set; }

        [Display(Name = "Description of Product (including SAP No.):")]
        public int ProductID { get; set; } = 1;

        public Product? Product { get; set; }

        
        [Display(Name = "Quantity Recieved:")]
        public int? Quantity { get; set; }

        
        [Display(Name = "Quantity Defective:")]
        public int? QuantityDefective { get; set; }

        
        [Display(Name = "Sales Order No.")]
        public string? OrderNumber { get; set; } = " ";

        
        [Display(Name = "Description of Defect:")]
        public string? DefectDescription { get; set; } = " ";

        [Display(Name = "Quality Representative Name:")]
        public string RepID { get; set; }

        [Display(Name = "Identify Process Applicable:")]
        public ProcessApplicable ProcessApplicable { get; set; }

        [Display(Name = "Date:")]
        public DateTime? Created { get; set; }

        public ICollection<QualityPicture> qualityPictures { get; set; } = new HashSet<QualityPicture>();  
    }
}
