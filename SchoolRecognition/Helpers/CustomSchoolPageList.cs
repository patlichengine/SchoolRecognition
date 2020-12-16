using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Helpers
{
    public class CustomSchoolPageList
    {
        public Guid Id { get; set; }
     
        public string Name { get; set; }

        
        public string Code { get; set; }

    
        public virtual IEnumerable<CentresViewDto> ApprovedExamCentres { get; set; }
        public virtual PagedList<SchoolsViewDto> ApprovedSchools { get; set; }
    }
}
