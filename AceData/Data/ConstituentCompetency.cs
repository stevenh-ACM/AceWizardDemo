using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AceData.Data
{
    public class ConstituentCompetency
    {
        public int Id {get; set;}
        public Competency MemberCompetency{get; set;}

        [ForeignKey("Competency")]
        public int CompetencyId{get; set;}    
        public Competency Competency{get; set;}        
    }
}