using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public enum EngReview
    {
        [Display(Name = "Use As Is")]
        UseAsIs,
        Repair,
        Rework,
        Scrap

    }
}
