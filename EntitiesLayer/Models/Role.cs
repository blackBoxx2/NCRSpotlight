using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class Role
    {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Must include a name for a role")]
        [MaxLength(30)]
        public string RoleName { get; set; }

        public ICollection<RoleRep> RoleReps { get; set; } = new HashSet<RoleRep>();

    }
}
