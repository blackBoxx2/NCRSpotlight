using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public enum NCRPhase
    {

        [Display(Name = "Quality Assurance")]
        QA,
        [Display(Name = "Engineering")]
        ENG,
        [Display(Name = "Purchasing")]
        PO,
        Complete

    }
}
