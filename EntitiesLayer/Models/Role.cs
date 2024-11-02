using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    internal class Role
    {

        [Key]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Must include a name for a role")]
        [MaxLength(30)]
        public string RoleName { get; set; }

    }
}
