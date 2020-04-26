using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cPinsRepository:IPinsRepository,IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        public cPinsRepository(SchoolRecognitionContext context, IMapper mapper)
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
        public async Task<IEnumerable<PinsDto>> GetPins()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.Pins.ToListAsync<Pins>();
                return _mapper.Map<IEnumerable<PinsDto>>(result);

            });
        }

        public async Task<IEnumerable<PinsDto>> GetPinsByRecognitionType(Guid recognitionTypeID)
        {
            return await Task.Run(async () =>
            {
                var result = await _context.Pins.Where(c=>c.RecognitionTypeId==recognitionTypeID).ToListAsync<Pins>();
                return _mapper.Map<IEnumerable<PinsDto>>(result);

            });
        }


    }
}
