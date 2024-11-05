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

        public int QualityPortionID { get; set; }
        public QualityPortion? QualityPortion { get; set; }

        [Display(Name = "Date Created")]
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public NCRStatus Status { get; set; }

        public ICollection<NCRLogHistory> History { get; set; } = new HashSet<NCRLogHistory>();
        

    }
}
