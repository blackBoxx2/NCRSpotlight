﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class NCRLogHistory
    {
        [Key]
        public int ID { get; set; }

        public string ChangedBy { get; set; }

        public DateTime ChangedOn { get; set; }

        public string Comments { get; set; }

        public int NCRLogID { get; set; }
        public NCRLog NCRLog { get; set; }
    }
}