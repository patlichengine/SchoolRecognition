using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Models;

namespace SchoolRecognition.Data
{
    public class SchoolRecognitionAppContext : DbContext
    {
        public SchoolRecognitionAppContext (DbContextOptions<SchoolRecognitionAppContext> options)
            : base(options)
        {
        }

        public DbSet<SchoolRecognition.Models.SchoolCategories> SchoolCategories { get; set; }


    }


}
