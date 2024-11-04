using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class QualityPicture : UploadedFile
    {

        [Display(Name = "Quality Portion")]
        public int QualityPortionID { get; set; }

        public QualityPortion? QualityPortion { get; set; }

    }
}
