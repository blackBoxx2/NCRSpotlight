using EntitiesLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ViewModels
{
    
    public class QualityPortionsWithRepViewModel
    {
        public QualityPortion QualityPortion { get; set; }
        public IdentityUser Representative { get; set; } = new IdentityUser();
    }
}
