using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public enum ProcessApplicable
    {
        [Display(Name = "Supplier or Rec-Insp")]
        Supplier,
        [Display(Name = "WIP (Production Order)")]
        WIP

    }
}
