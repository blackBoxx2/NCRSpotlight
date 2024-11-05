using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class ProductPicture : UploadedFile
    {
        [Display(Name = "Product")]
        public int ProductID { get; set; }

        public Product product { get; set; }
    }
}
