using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class NCRLog
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

        public NCRStatus Status { get; set; }

        public ICollection<NCRLogHistory> History { get; set; } = new HashSet<NCRLogHistory>();
        

    }
}
