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

        [Required]
        public int SupplierID { get; set; }

        public Supplier? Supplier { get; set; }

        [Required]
        public string ProductNumber { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<ProductPicture> ProductPictures { get; set; } = new HashSet<ProductPicture>();

        public ICollection<QualityPortion> QualityPortions { get; set; } = new HashSet<QualityPortion>();

    }
}
