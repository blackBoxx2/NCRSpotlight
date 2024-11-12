using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int EngPortionID { get; set; }
        public EngPortion? EngPortion { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [NotMapped]
        public string DateSummary
        {
            get
            {
                return this.DateCreated.ToShortDateString();
            }
        }

        public NCRStatus Status { get; set; }

        public ICollection<NCRLogHistory> History { get; set; } = new HashSet<NCRLogHistory>();
        

    }
}
