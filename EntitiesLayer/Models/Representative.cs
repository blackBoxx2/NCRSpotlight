using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class Representative
    {

        [Key]
        public int ID { get; set; }

        #region summary

        public string FullName
        {
            get { return $"{FirstName} {MiddleInitial} {LastName}"; }
        }


        #endregion

        [Required(ErrorMessage = "A first name is required")]
        public string FirstName { get; set; }

        public string? MiddleInitial {  get; set; }
        
        [Required(ErrorMessage = "A last name is required")]
        public string LastName { get; set; }

        public ICollection<RoleRep> RoleReps { get; set; } = new HashSet<RoleRep>();

    }
}
