using SchoolRecognition.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class SubjectsViewPagedListCentreSubjectSanctionsDto
    {
        public Guid Id { get; set; }
        public string SubjectCode { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public bool HasItem { get; set; }
        public bool IsTradeSubject { get; set; }
        public bool IsCoreSubject { get; set; }
        public virtual CustomPagedList<CentreSubjectSanctionsViewDto> CentreSanctions { get; set; }
    }
}
