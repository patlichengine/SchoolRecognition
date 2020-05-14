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
    public class cOfficeTypesRepository : IOfficeTypesRepository, IDisposable
    {

        //private readonly ConnectionString _connectionString;
        //private IPinsRepository _pinService;
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        //public cRecognitionTypesRepository(ConnectionString connectionString)
        //{
        //    //_connectionString = connectionString;
        //    //_pinService = new cPinsRepository(_connectionString);

        //}

        public cOfficeTypesRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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

        public async Task<IEnumerable<OfficeTypesViewDto>> GetAllOfficeTypesAsync()
        {
            //Instantiate Return Value
            IEnumerable<OfficeTypesViewDto> returnValue = new List<OfficeTypesViewDto>();
            try
            {
                var dbResult = await _context.OfficeTypes.Include(x => x.Offices)
                    .Select(x => new OfficeTypesViewDto()
                    {
                        Id = x.Id,
                        TypeDescription = x.Description,
                        OfficesCount = x.Offices != null ? x.Offices.Count() : 0,
                    }).ToListAsync();

                returnValue = dbResult;

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<CustomPagedList<OfficeTypesViewDto>> GetAllOfficeTypesAsPagedListAsync(OfficeTypesResourceParams resourceParams)
        {
            //Instantiate Return Value
            CustomPagedList<OfficeTypesViewDto> returnValue = CustomPagedList<OfficeTypesViewDto>
                        .Create(Enumerable.Empty<OfficeTypesViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context.OfficeTypes as IQueryable<OfficeTypes>;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        dbResult = dbResult.Where(a => a.Description.ToUpper().Contains(searchQuery));
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var recognitionTypePropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<OfficeTypesViewDto, OfficeTypes>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            recognitionTypePropertyMappingDictionary);
                    }

                    var mappedResult = dbResult.Select(x => new OfficeTypesViewDto()
                    {
                        Id = x.Id,
                        TypeDescription = x.Description,
                        OfficesCount = x.Offices != null ? x.Offices.Count() : 0

                    });

                    returnValue = await CustomPagedList<OfficeTypesViewDto>.CreateAsync(mappedResult,
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

        public async Task<OfficeTypesViewDto> GetOfficeTypesSingleOrDefaultAsync(Guid id)
        {

            //Instantiate Return Value
            OfficeTypesViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.OfficeTypes as IQueryable<OfficeTypes>;

                    var officeTypes = await dbResult.Where(x => x.Id == id).SingleOrDefaultAsync();
                    returnValue = _mapper.Map<OfficeTypesViewDto>(officeTypes);
                    //
                    returnValue.OfficesCount = await dbResult.Include(x => x.Offices).Where(x => x.Id == id).SelectMany(x => x.Offices).CountAsync();


                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(id));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<OfficeTypesViewDto> GetOfficeTypesAllOfficesAsync(Guid id)
        {


            //Instantiate Return Value
            OfficeTypesViewDto returnValue = null;
            IEnumerable<OfficesViewDto> returnValueOffices = new List<OfficesViewDto>();
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.OfficeTypes
                        .Include(x => x.Offices)
                        .ThenInclude(y => y.OfficeStates)
                        .Include(x => x.Offices)
                        .ThenInclude(y => y.OfficeType)
                        .Include(x => x.Offices)
                        .ThenInclude(y => y.CreatedByNavigation)
                        .Where(x => x.Id == id) as IQueryable<OfficeTypes>;


                    returnValue = _mapper.Map<OfficeTypesViewDto>(await dbResult.SingleOrDefaultAsync());

                    returnValueOffices = await dbResult.SelectMany(x => x.Offices).Select(x => new OfficesViewDto()
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
                        DateCreated = x.DateCreated,
                        OfficeStateOffices = x.OfficeStates.Select(y => new OfficeStatesViewDto()
                        {
                            Id = y.Id,
                            StateName = y.State != null ? y.State.Name : null,
                            StateCode = y.State != null ? y.State.Code : null,
                            OfficeName = y.Office != null ? y.Office.Name : null,
                            OfficeAddress = y.Office != null ? y.Office.Address : null,
                        }),


                    }).ToListAsync();


                    returnValue.OfficeTypeOffices = returnValueOffices;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(id));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<OfficeTypeViewPagedListOfficesDto> GetOfficeTypesOfficesAsPagedListAsync(Guid id, OfficesResourceParams resourceParams)
        {



            //Instantiate Return Value
            OfficeTypeViewPagedListOfficesDto returnValue = null;

            //Instantiate Return Value
            CustomPagedList<OfficesViewDto> returnValueOffices = CustomPagedList<OfficesViewDto>
                        .Create(Enumerable.Empty<OfficesViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.OfficeTypes
                        .Include(x => x.Offices)
                        .ThenInclude(y => y.OfficeStates)
                        .Include(x => x.Offices)
                        .ThenInclude(y => y.OfficeType)
                        .Include(x => x.Offices)
                        .ThenInclude(y => y.CreatedByNavigation)
                        .Where(x => x.Id == id) as IQueryable<OfficeTypes>;

                    OfficeTypes officeType = await dbResult.FirstOrDefaultAsync();
                    //
                    var queryableOffices = dbResult.SelectMany(x => x.Offices) as IQueryable<Offices>;



                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        queryableOffices = queryableOffices.Where(a => a.Name.ToUpper().Contains(searchQuery)
                            || a.Address.ToUpper().Contains(searchQuery)
                            || (a.DateCreated != null ? a.DateCreated : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.CreatedByNavigation != null ? a.CreatedByNavigation.Surname : null).ToUpper().Contains(searchQuery)
                            || (a.State != null ? a.State.Name : null).ToUpper().Contains(searchQuery)
                            || (a.OfficeType != null ? a.OfficeType.Description : null).ToUpper().Contains(searchQuery)
                            || (a.CreatedByNavigation != null ? a.CreatedByNavigation.Othernames : null).ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var pinsPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<OfficesViewDto, Offices>();

                        queryableOffices = queryableOffices.ApplySort(resourceParams.OrderBy,
                            pinsPropertyMappingDictionary);
                    }
                    ///Use LINQ to map pins to pinsviewdto
                    var mappedResult = queryableOffices.Select(x => new OfficesViewDto()
                    {
                        Id = x.Id,
                        OfficeName = x.Name,
                        OfficeAddress = x.Address,
                        StateName = x.State != null ? x.State.Name : null,
                        OfficeImage =x.OfficeImage,
                        Longitude = x.Longitute,
                        Latitude = x.Latitude,
                        OfficeTypeDescription  = x.OfficeType != null ? x.OfficeType.Description : null,
                        //IsActive = x.IsActive,
                        //IsInUse = x.IsInUse,
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                        DateCreated = x.DateCreated,

                        OfficeStateOffices = x.OfficeStates.Select(y => new OfficeStatesViewDto()
                        {
                            Id = y.Id,
                            StateName = y.State != null ? y.State.Name : null,
                            StateCode = y.State != null ? y.State.Code : null,
                            OfficeName = y.Office != null ? y.Office.Name : null,
                            OfficeAddress = y.Office != null ? y.Office.Address : null,
                        }),

                    });

                    returnValueOffices = await CustomPagedList<OfficesViewDto>.CreateAsync(mappedResult,
                        resourceParams.PageNumber,
                        resourceParams.PageSize);


                    returnValue = _mapper.Map<OfficeTypeViewPagedListOfficesDto>(officeType);
                    //
                    returnValue.OfficesCount = await queryableOffices.CountAsync();
                    //
                    returnValue.OfficeTypeOffices = returnValueOffices;


                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(id));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public async Task<Guid?> CreateOfficeTypeAsync(OfficeTypesCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    OfficeTypes entity = _mapper.Map<OfficeTypes>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    await _context.OfficeTypes.AddAsync(entity);
                    await this.Save();

                    return returnValue = entity.Id;
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

        public async Task<OfficeTypesViewDto> UpdateOfficeTypeAsync(OfficeTypesCreateDto _obj)
        {

            //Instantiate Return Value
            OfficeTypesViewDto returnValue = null;
            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<OfficeTypes>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue = _mapper.Map<OfficeTypesViewDto>(entity);

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

        public async Task DeleteOfficeTypeAsync(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var entity = await _context.OfficeTypes.Where(x => x.Id == id).SingleOrDefaultAsync();
                    if (entity != null)
                    {
                        _context.Remove(entity);
                        await this.Save();

                    }

                }
                else
                {
                    throw new ArgumentNullException(nameof(id));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CheckIfOfficeTypeExists(string officeTypeDescription)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(officeTypeDescription))
                {

                    var searchQuery = officeTypeDescription.Trim().ToUpper();
                    var dbResult = await _context.OfficeTypes.AnyAsync(x => x.Description.Trim().ToUpper() == searchQuery);
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(officeTypeDescription));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CheckIfOfficeTypeExists(Guid id, string officeTypeDescription)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(officeTypeDescription))
                {

                    var searchQuery = officeTypeDescription.Trim().ToUpper();
                    var dbResult = await _context.OfficeTypes.Where(x => x.Id != id).AnyAsync(x => x.Description.Trim().ToUpper() == searchQuery);
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(id));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
