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

        [Display(Name = "Engineer Review:")]
        public EngReview EngReview { get; set; } = EngReview.UseAsIs;

        [Display(Name = "Does Customer Require Notifications?")]
        public bool Notif { get; set; } = false;
        public string? Disposition { get; set; } = "";

        [Display(Name = "Does Drawing Require Update?")]
        public bool Update { get; set; } = false;

        [Display(Name = "Updated Rev. No.")]
        public int? RevNumber { get; set; } = 0;

        [Display(Name = "Original Rev. No.")]
        public int? OriginalRevNumber { get; set; } = 0;

        [Display(Name = "Name Of Engineer:")]
        public string OriginalEngineer { get; set; } = "None";

        [Display(Name = "Revision Date:")]
        public DateTime? RevDate { get; set; } = DateTime.Today;

        [Display(Name = "Engineering Representative:")]
        public string? RepID { get; set; } = string.Empty;

        [Display(Name = "Date:")]
        public DateTime? Date { get; set; } = DateTime.Today;


    }
}
