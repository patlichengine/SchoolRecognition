using SchoolRecognition.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class OfficeTypesCreateDto
    {
        public Guid Id { get; set; }
        public string TypeDescription { get; set; }
        public bool IsActive { get; set; }
    }

    public class OfficeTypesViewDto
    {
        public Guid Id { get; set; }
        public string TypeDescription { get; set; }
        public bool? IsActive { get; set; }
        public Int64 OfficesCount { get; set; }
        public virtual IEnumerable<OfficesViewDto> OfficeTypeOffices { get; set; }
    }

    public class OfficeTypeViewPagedListOfficesDto
    {
        public Guid Id { get; set; }
        public string TypeDescription { get; set; }
        public bool? IsActive { get; set; }
        public Int64 OfficesCount { get; set; }
        public virtual PagedList<OfficesViewDto> OfficeTypeOffices { get; set; }
    }
}
