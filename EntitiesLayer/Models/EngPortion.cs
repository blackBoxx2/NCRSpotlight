using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class EngPortion
    {

        [Key]
        public int ID { get; set; }
        public EngReview EngReview { get; set; } = EngReview.UseAsIs;
        public bool Notif { get; set; } = false;

        public string Disposition { get; set; } = "";

        public bool Update { get; set; } = false;

        public int RevNumber { get; set; } = 0;

        public DateTime RevDate { get; set; } = DateTime.Today;       
        public string RepID { get; set; }


    }
}
