using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class RoleRep
    {
        [Key]
        public int RoleRepID { get; set; }

        [Required]
        public int RoleID { get; set; }
        public Role Role { get; set; }

        [Required]
        public int RepresentativeID { get; set; }
        public Representative Representative { get; set; }
        public ICollection<QualityPortion> QualityPortions{ get; set; } = new HashSet<QualityPortion>();

}
}
