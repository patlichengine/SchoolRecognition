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
    public class cRecognitionTypesRepository:IRecognitionTypesRepository,IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        public cRecognitionTypesRepository(SchoolRecognitionContext context, IMapper mapper)
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
        public async Task<IEnumerable<RecognitionTypesDto>> GetAllRecognitionTypes()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.RecognitionTypes.ToListAsync<RecognitionTypes>();
                return _mapper.Map<IEnumerable<RecognitionTypesDto>>(result);

            });
        }
    }
}
