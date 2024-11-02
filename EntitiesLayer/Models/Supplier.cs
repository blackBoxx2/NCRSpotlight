using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class Supplier
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "A supplier name is required")]
        public string SupplierName { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
