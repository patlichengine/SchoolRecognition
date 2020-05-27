using AutoMapper;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cSchoolPaymentsRepository: ISchoolPaymentsRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        public cSchoolPaymentsRepository(SchoolRecognitionContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

        //public async Task<IEnumerable<RanksDto>> GetAllCategory()
        //{
        //    return await Task.Run(async () =>
        //    {
        //        var result = await _context.SchoolPayments.ToListAsync<SchoolPayments>();
        //        return _mapper.Map<IEnumerable<RanksDto>>(result);

        //    });
        //}
    }
}
