using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class CentreSubjectSanctionsViewDto
    {
        public Guid Id { get; set; }
        public bool? IsActive { get; set; }
        //CentreSanctions
        public string CentreSanctionDescription { get; set; }
        public int CentreSanctionYearOfSaction { get; set; }
        public DateTime CentreSanctionDateCreated { get; set; }
        //Centre
        public string CentreNo { get; set; }
        public string CentreName { get; set; }
        public string SchoolCategoryName { get; set; }
        public string SchoolCategoryCode { get; set; }
        //Subject
        public string SubjectCode { get; set; }
        public string SubjectLongName { get; set; }
        public string SubjectShortName { get; set; }
        public bool HasItem { get; set; }
        public bool IsTradeSubject { get; set; }
        public bool IsCoreSubject { get; set; }
    }
}
