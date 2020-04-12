using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AceData.Data
{
    public class SkillLevel
    {
        public int Id{get; set;}
        public string Name{get; set;}
        public string Description {get; set;}

        [Display(Name = "Cartesian Index")]
        public int CartesianIndex {get; set;}           
    }
}