using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        #region Summary
        [Display(Name = "Description of Product (including SAP No.):")]
        public string Summary { get 
            {
                return $"{Description} - {SapNo}"     
                    ; } }


        #endregion


        [Required]
        [Display(Name = "Supplier Name:")]
        public int SupplierID { get; set; }

        [Display(Name = "Supplier")]
        public Supplier? Supplier { get; set; }

        [Required]
        [Display(Name = "PO or Prod No:")]
        public string ProductNumber { get; set; }

        [Required]
        [Display(Name = "SAP No.")]
        public string SapNo { get; set; }

        [Required]
        public string Description { get; set; }
        [Display(Name = "Pictures")]
        public ICollection<ProductPicture> ProductPictures { get; set; } = new HashSet<ProductPicture>();

        public ICollection<QualityPortion> QualityPortions { get; set; } = new HashSet<QualityPortion>();

    }
}
