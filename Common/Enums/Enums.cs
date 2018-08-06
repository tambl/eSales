using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum GenderEnum
    {
        [Display( Name = "მდედრობითი")]
        Female = 1,
        [Display( Name = "მამრობითი")]
        Male = 2
    }
}
