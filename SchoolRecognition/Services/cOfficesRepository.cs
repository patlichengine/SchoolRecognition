using AutoMapper;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cOfficesRepository : IOfficesRepository, IDisposable
    {

        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;


        public cOfficesRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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

        public Task<bool> CreateMultipleOfficeAsync(OfficesCreateDto _obj)
        {
            throw new NotImplementedException();
        }

        public Task<Guid?> CreateOfficeAsync(OfficesCreateDto _obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOfficeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomPagedList<OfficesViewDto>> GetAllOfficesAsPagedListAsync(OfficesResourceParams resourceParams)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OfficesViewDto>> GetAllOfficesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OfficesViewDto> GetOfficesAllOfficeHistoriesAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OfficesViewDto> GetOfficesAllOfficeStatesAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OfficeViewPagedListOfficeStatesDto> GetOfficesOfficeStatesAsPagedListAsync(Guid id, OfficeStatesResourceParams resourceParams)
        {
            throw new NotImplementedException();
        }

        public Task<OfficesViewDto> GetOfficesSingleOrDefaultAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OfficesViewDto> UpdateOfficeAsync(OfficesCreateDto _obj)
        {
            throw new NotImplementedException();
        }
    }
}
