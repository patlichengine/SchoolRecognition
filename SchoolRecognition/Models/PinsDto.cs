using SchoolRecognition.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinsDto
    {
        public Guid Id { get; set; }
        public Guid RecognitionTypeId { get; set; }
        public string SerialPin { get; set; }
        public bool IsActive { get; set; }
        public bool IsInUse { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class PinsCreateDto
    {
        public Guid RecognitionTypeId { get; set; }
        public int NoOfPinToGenerate { get; set; }
        public bool IsActive { get; set; }
        public Guid? CreatedBy { get; internal set; }
    }
    public class PinsUpdateDto
    {
        public Guid Id { get; set; }
        public Guid? RecognitionTypeId { get; set; }
        public string SerialNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsInUse { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
    }
    public class PinsViewDto
    {
        public Guid Id { get; set; }
        public String RecognitionTypeName { get; set; }
        public string SerialNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsInUse { get; set; }
        public String CreatedByUser { get; set; }
        public DateTime? DateCreated { get; set; }
        public string AssignedSchoolName
        {
            get
            {
                string schoolAssigned = null;

                if (Payments != null && Payments.Count() > 0)
                {
                    var schoolPayment = Payments.SingleOrDefault();
                    if (schoolPayment != null)
                    {
                        schoolAssigned = $"{schoolPayment.SchoolName}";
                    }
                }

                return schoolAssigned;
            }
        }
        public string AssignedSchoolCategoryName
        {
            get
            {
                string schoolAssigned = null;

                if (Payments != null && Payments.Count() > 0)
                {
                    var schoolPayment = Payments.SingleOrDefault();
                    if (schoolPayment != null)
                    {
                        schoolAssigned = $"{schoolPayment.SchoolCategoryName}";
                    }
                }

                return schoolAssigned;
            }
        }
        public IEnumerable<PinHistoriesViewDto> Histories { get; set; }
        public IEnumerable<SchoolPaymentsViewDto> Payments { get; set; }
    }
    public class PinsApiListViewDto
    {
        public virtual IEnumerable<PinsViewDto> RecognitionTypePins { get; set; }
        public Int64 RangeFrom { get; set; }
        public Int64 RangeTo { get; set; }
        public Int64 RangeTotalPins { get; set; }
    }
    public class PinsCreationDependecyDto
    {
        public IEnumerable<RecognitionTypesViewDto> RecognitionTypes { get; set; }
        public ApplicationSettingsViewDto ApplicationSetting { get; set; }
    }
    public class PinsStatisticsSummaryDto
    {
        public Int64 PinsCount { get; set; }
        public Int64 IsActivePinsCount { get; set; }
        public Int64 IsInUsePinsCount { get; set; }
    }
    public class PinsViewPagedListOfficeStatesDto
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
        public virtual PagedList<OfficeStatesViewDto> StateOffices { get; set; }
        //public virtual IEnumerable<SchoolsViewDto> OfficeSchools { get; set; }
    }

    public class PinsViewPagedListPinHistoriesDto
    {
        public Guid Id { get; set; }
        public String RecognitionTypeName { get; set; }
        public string SerialNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsInUse { get; set; }
        public String CreatedByUser { get; set; }
        public DateTime? DateCreated { get; set; }
        public string AssignedSchoolName
        {
            get
            {
                string schoolAssigned = null;

                if (Payments != null && Payments.Count() > 0)
                {
                    var schoolPayment = Payments.SingleOrDefault();
                    if (schoolPayment != null)
                    {
                        schoolAssigned = $"{schoolPayment.SchoolName}";
                    }
                }

                return schoolAssigned;
            }
        }
        public string AssignedSchoolCategoryName
        {
            get
            {
                string schoolAssigned = null;

                if (Payments != null && Payments.Count() > 0)
                {
                    var schoolPayment = Payments.SingleOrDefault();
                    if (schoolPayment != null)
                    {
                        schoolAssigned = $"{schoolPayment.SchoolCategoryName}";
                    }
                }

                return schoolAssigned;
            }
        }

        public IEnumerable<SchoolPaymentsViewDto> Payments { get; set; }
        public PagedList<PinHistoriesViewDto> Histories { get; set; }
        //public virtual ICollection<SchoolPayments> SchoolPayments { get; set; }
    }

}
