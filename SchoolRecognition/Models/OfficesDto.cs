using SchoolRecognition.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class OfficesCreateDto
    {

        public Guid Id { get; set; }

        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string OfficeName { get; set; }
        [StringLength(300, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string OfficeAddress { get; set; }
        [Required]
        public Guid? StateId { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public Guid? CreatedBy { get; set; }
        public byte[] OfficeImage { get; set; }
        public double? Longitute { get; set; }
        public double? Latitude { get; set; }
        [Required]
        public Guid? OfficeTypeId { get; set; }
        //StateAssigned id the id of the State the Office is assigned to
        //which is used to auto-generate the OfficeStates entity
    }
    public class OfficesViewDto
    {
        public Guid Id { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string StateName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedByUser { get; set; }
        public Guid? CreatedBy { get; set; }
        public byte[] OfficeImage { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string OfficeTypeDescription { get; set; }

        public Guid? StateId { get; set; }
        public Guid? OfficeTypeId { get; set; }
        //Child collections
        public Int64 OfficeLocalGovernmentsCount { get; set; }
        public Int64 OfficeStatesCount { get; set; }
        public Int64 SchoolsCount { get; set; }
        public virtual IEnumerable<OfficeLocalGovernmentsViewDto> OfficeLgas { get; set; }
        public virtual IEnumerable<OfficeStatesViewDto> OfficeStateStates { get; set; }
        public virtual IEnumerable<SchoolsViewDto> OfficeSchools { get; set; }
    }
    public class OfficeViewPagedListOfficeLocalGovernmentsDto
    {
        public Guid Id { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string StateName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedByUser { get; set; }
        public byte[] OfficeImage { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string OfficeTypeDescription { get; set; }
        //public virtual IEnumerable<OfficeStatesViewDto> StateOffices { get; set; }
        public virtual IEnumerable<OfficeStatesViewDto> OfficeStateStates { get; set; }
        public virtual PagedList<OfficeLocalGovernmentsViewDto> OfficeLgas { get; set; }
    }

    public class OfficesViewPagedListSchoolsDto
    {
        public Guid Id { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string StateName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedByUser { get; set; }
        public byte[] OfficeImage { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string OfficeTypeDescription { get; set; }
        //public virtual IEnumerable<OfficeStatesViewDto> StateOffices { get; set; }
        public virtual IEnumerable<OfficeLocalGovernmentsViewDto> OfficeLgas { get; set; }
        public virtual IEnumerable<OfficeStatesViewDto> OfficeStateStates { get; set; }
        public virtual PagedList<SchoolsViewDto> OfficeSchools { get; set; }
    }


    public class OfficesCreationDependecyDto
    {
        public IEnumerable<StatesViewDto> States { get; set; }
        public IEnumerable<OfficeTypesViewDto> OfficeTypes { get; set; }
    }


}
