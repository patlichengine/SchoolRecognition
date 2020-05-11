using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
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


        public async Task<IEnumerable<OfficesViewDto>> GetAllOfficesAsync()
        {

            //Instantiate Return Value
            IEnumerable<OfficesViewDto> returnValue = new List<OfficesViewDto>();
            try
            {
                var dbResult = await _context.Offices.Include(x => x.OfficeType).Include(x => x.State).Include(x => x.CreatedByNavigation).ToListAsync();

                returnValue = _mapper.Map<IEnumerable<OfficesViewDto>>(dbResult);

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<CustomPagedList<OfficesViewDto>> GetAllOfficesAsPagedListAsync(OfficesResourceParams resourceParams)
        {
            //Instantiate Return Value
            CustomPagedList<OfficesViewDto> returnValue = CustomPagedList<OfficesViewDto>
                        .Create(Enumerable.Empty<OfficesViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context.Offices.Include(x => x.State).Include(x => x.OfficeType) as IQueryable<Offices>;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        dbResult = dbResult.Where(a => a.Name.ToUpper().Contains(searchQuery)
                            || a.Address.ToUpper().Contains(searchQuery)
                            || (a.DateCreated != null ? a.DateCreated : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.State != null ? a.State.Name : null).ToUpper().Contains(searchQuery)
                            || (a.OfficeType != null ? a.OfficeType.Description : null).ToUpper().Contains(searchQuery)
                            || (a.CreatedByNavigation != null ? a.CreatedByNavigation.Surname : null).ToUpper().Contains(searchQuery)
                            || (a.CreatedByNavigation != null ? a.CreatedByNavigation.Othernames : null).ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var pinPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<OfficesViewDto, Offices>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            pinPropertyMappingDictionary);
                    }

                    var mappedResult = dbResult.Select(x => new OfficesViewDto()
                    {
                        Id = x.Id,
                        OfficeName = x.Name,
                        OfficeAddress = x.Address,
                        StateName = x.State != null ? x.State.Name : null,
                        OfficeImage = x.OfficeImage,
                        Longitude = x.Longitute,
                        Latitude = x.Latitude,
                        OfficeTypeDescription = x.OfficeType != null ? x.OfficeType.Description : null,
                        //
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                        DateCreated = x.DateCreated

                    });

                    returnValue = await CustomPagedList<OfficesViewDto>.CreateAsync(mappedResult,
                        resourceParams.PageNumber,
                        resourceParams.PageSize);

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(resourceParams));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<Guid?> CreateOfficesAsync(OfficesCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null)
                {
                    //Create a PINs Object to be stored in the db
                    var entity = _mapper.Map<Offices>(_obj);

                    returnValue = entity.Id;

                    await _context.Offices.AddAsync(entity);
                    await this.Save();

                    return returnValue;

                }
                else
                {
                    throw new ArgumentNullException(nameof(_obj));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task DeleteOfficeAsync(Guid id)
        {
            throw new NotImplementedException();
        }


        public async Task<OfficesViewDto> GetOfficesAllOfficeStatesAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OfficeViewPagedListOfficeStatesDto> GetOfficesOfficeStatesAsPagedListAsync(Guid id, OfficeStatesResourceParams resourceParams)
        {
            throw new NotImplementedException();
        }

        public async Task<OfficesViewDto> GetOfficesSingleOrDefaultAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OfficesViewDto> UpdateOfficeAsync(OfficesCreateDto _obj)
        {
            throw new NotImplementedException();
        }
    }
}
