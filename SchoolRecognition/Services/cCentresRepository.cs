using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{

    public class cCentresRepository :ICentresRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        public cCentresRepository(SchoolRecognitionContext context, IMapper mapper)
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
        public async Task<CentresDto> GetCentreByCentreNumber(string centreNumber)
        {
            return await Task.Run(async () =>
            {
                var result = await _context.Centres.FirstOrDefaultAsync(c => c.CentreNo == centreNumber); 
                return _mapper.Map<CentresDto>(result);

            });
        }

        public async Task<IEnumerable<CentresDto>> GetAllCentres()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.Centres.ToListAsync<Centres>();
                return _mapper.Map<IEnumerable<CentresDto>>(result);

            });
        }
    }
}
