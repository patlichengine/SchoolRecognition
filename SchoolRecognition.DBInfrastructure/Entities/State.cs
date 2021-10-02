using System;
using System.Collections.Generic;

namespace SchoolRecognition.DBInfrastructure.Entities
{
    public partial class State
    {
        public State()
        {
            ApplicationUserStates = new HashSet<ApplicationUserState>();
            OfficeStates = new HashSet<OfficeState>();
        }

        public string StateCode { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<ApplicationUserState> ApplicationUserStates { get; set; }
        public virtual ICollection<OfficeState> OfficeStates { get; set; }
    }
}
