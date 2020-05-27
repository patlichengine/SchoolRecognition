using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
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
        public string StateAssigned
        {
            get
            {
                string stateAssigned = null;

                if (OfficeStateOffices != null && OfficeStateOffices.Count() > 0)
                {
                    var officeState = OfficeStateOffices.SingleOrDefault();
                    if (officeState != null)
                    {
                        stateAssigned = $"{officeState.StateCode} {officeState.StateName}";
                    }
                }

                return stateAssigned;
            }
        }
        [JsonIgnore]
        public virtual IEnumerable<OfficeStatesViewDto> OfficeStateOffices { get; set; }
        public virtual IEnumerable<SchoolsViewDto> OfficeSchools { get; set; }
    }
}
