using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PinsCreationDependecyDto
    {
        public IEnumerable<RecognitionTypesViewDto> RecognitionTypes { get; set; }
        public ApplicationSettingsViewDto ApplicationSetting { get; set; }
    }
}
