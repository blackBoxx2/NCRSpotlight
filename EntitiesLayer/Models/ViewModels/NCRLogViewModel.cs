using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models.ViewModels
{
	public class NCRLogViewModel
	{
        public NCRLog NCR { get; set; }

        [Display(Name = "Supplier")]
        public int? SupplierID { get; set; }

        public Supplier? Supplier { get; set; }
    }
}
