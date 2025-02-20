﻿using System;
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
        [Display(Name = "NCR No.")]
        public int ID { get; set; }

        [Display(Name = "Quality Description")]
        public int QualityPortionID { get; set; }

        [Display(Name = "Quality Description")]
        public QualityPortion? QualityPortion { get; set; }

        [Display(Name = "Engineering Portion")]
        public int EngPortionID { get; set; }
        public EngPortion? EngPortion { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [NotMapped]
        [Display(Name = "Date")]
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
