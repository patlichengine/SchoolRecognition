using System;
using System.Collections.Generic;

namespace SchoolRecognition.Entities
{
    public partial class RecognitionTypes
    {
        public RecognitionTypes()
        {
            Pins = new HashSet<Pins>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Pins> Pins { get; set; }
    }
}
