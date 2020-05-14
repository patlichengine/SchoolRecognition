using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cStatesRepository : IStatesRepository, IDisposable
    {

        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;


        public cStatesRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }


        #region Base Methods


        async Task<bool> Save()
        {
            return await Task.Run(async () => {
                return (await _context.SaveChangesAsync() >= 0);
            });
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


        #endregion


        public async Task<IEnumerable<StatesViewDto>> GetAllStatesAsync()
        {

            //Instantiate Return Value
            IEnumerable<StatesViewDto> returnValue = new List<StatesViewDto>();
            try
            {
                var dbResult = await _context.States.ToListAsync();

                returnValue = _mapper.Map<IEnumerable<StatesViewDto>>(dbResult);

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
