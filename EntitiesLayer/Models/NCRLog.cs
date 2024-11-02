using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    internal class NCRLog
    {
        [Key]
        public int ID { get; set; }

        [Required]
        
        public int QualityPortionID { get; set; }

        [Required]
        [Display(Name = "Quality Rep Portion")]
        public QualityPortion? qualityPortion { get; set; }

        [Display(Name = "Date Created")]
        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public int EngRepID { get; set; }
        [Display(Name = "Engineering Rep")]
        public RoleRep? EngRep { get; set; }

        [Required]
        public int QARepID { get; set; }
        [Display(Name = "Quality Assurance Rep")]
        public RoleRep? QARep { get; set; }

    }
}
