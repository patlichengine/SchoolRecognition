using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class PaginationLinksDto
    {
        public IEnumerable<int> Pages { get; set; }
        public int PageNumber { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string PrevDisabled { get; set; }
        public string NextDisabled { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Guid? Id { get; set; }
    }
}
