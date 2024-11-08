﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class QualityPortion
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public int ProductID { get; set; }

        public Product? Product { get; set; }

        [Required]
        [Display(Name = "Quantity Ordered")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Quantity Defective")]
        public int QuantityDefective { get; set; }

        [Required]
        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }

        [Required]
        [Display(Name = "Defect Description")]
        public string DefectDescription { get; set; }

        [Required]
        public int RoleRepID { get; set; }
        [Display(Name = "Quality Assurance Rep")]
        public RoleRep? RoleRep { get; set; }

        public ICollection<QualityPicture> qualityPictures { get; set; } = new HashSet<QualityPicture>();  
    }
}
