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

        public async Task<OfficesViewDto> GetOfficesAllOfficeStatesAsync(Guid id)
        {
            //Instantiate Return Value
            OfficesViewDto returnValue = null;
            IEnumerable<OfficeStatesViewDto> returnValueOfficeStates = new List<OfficeStatesViewDto>();
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Offices.Include(x => x.OfficeType).Include(x => x.State).Include(x => x.CreatedByNavigation).Where(x => x.Id == id) as IQueryable<Offices>;


                    var pin = await dbResult.SingleOrDefaultAsync();
                    returnValue = _mapper.Map<OfficesViewDto>(pin);


                    var schoolPayments = await dbResult.SelectMany(x => x.OfficeStates).ToListAsync();
                    returnValueOfficeStates = _mapper.Map<IEnumerable<OfficeStatesViewDto>>(schoolPayments);

                    returnValue.StateOffices = returnValueOfficeStates;

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


        public async Task<OfficeViewPagedListOfficeStatesDto> GetOfficesOfficeStatesAsPagedListAsync(Guid id, OfficeStatesResourceParams resourceParams)
        {

            //Instantiate Return Value
            OfficeViewPagedListOfficeStatesDto returnValue = null;

            //Instantiate Return Value
            CustomPagedList<OfficeStatesViewDto> returnValueOfficeStates = CustomPagedList<OfficeStatesViewDto>
                        .Create(Enumerable.Empty<OfficeStatesViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Offices.Include(x => x.OfficeType).Include(x => x.State).Include(x => x.CreatedByNavigation).Where(x => x.Id == id) as IQueryable<Offices>;

                    Offices pin = await dbResult.FirstOrDefaultAsync();
                    //
                    var queryableOfficeStates = dbResult.SelectMany(x => x.OfficeStates) as IQueryable<OfficeStates>;



                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        queryableOfficeStates = queryableOfficeStates.Where(a => (a.State != null ? a.State.Name : null).ToString().Contains(searchQuery)
                            || (a.Office != null ? a.Office.Name : null).ToUpper().Contains(searchQuery)
                            || (a.Office != null ? a.Office.Address : null).ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var pinsPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<OfficeStatesViewDto, OfficeStates>();

                        queryableOfficeStates = queryableOfficeStates.ApplySort(resourceParams.OrderBy,
                            pinsPropertyMappingDictionary);
                    }
                    ///Use LINQ to map pins to pinsviewdto
                    var mappedResult = queryableOfficeStates.Select(x => new OfficeStatesViewDto()
                    {
                        Id = x.Id,
                        StateName = x.State != null ? x.State.Name : null,
                        OfficeName = x.Office != null ? x.Office.Name : null,
                        OfficeAddress = x.Office != null ? x.Office.Address : null

                    });

                    returnValueOfficeStates = await CustomPagedList<OfficeStatesViewDto>.CreateAsync(mappedResult,
                        resourceParams.PageNumber,
                        resourceParams.PageSize);


                    returnValue = _mapper.Map<OfficeViewPagedListOfficeStatesDto>(pin);
                    //
                    returnValue.StateOffices = returnValueOfficeStates;


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

        public async Task<OfficesViewDto> GetOfficesAllSchoolsAsync(Guid id)
        {
            //Instantiate Return Value
            OfficesViewDto returnValue = null;
            IEnumerable<SchoolsViewDto> returnValueSchools = new List<SchoolsViewDto>();
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Offices.Include(x => x.OfficeType).Include(x => x.State).Include(x => x.CreatedByNavigation).Where(x => x.Id == id) as IQueryable<Offices>;


                    var pin = await dbResult.SingleOrDefaultAsync();
                    returnValue = _mapper.Map<OfficesViewDto>(pin);


                    var schoolPayments = await dbResult.SelectMany(x => x.Schools).ToListAsync();
                    returnValueSchools = _mapper.Map<IEnumerable<SchoolsViewDto>>(schoolPayments);

                    returnValue.OfficeSchools = returnValueSchools;

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


        public async Task<OfficeViewPagedListSchoolsDto> GetOfficesSchoolsAsPagedListAsync(Guid id, SchoolsResourceParams resourceParams)
        {

            //Instantiate Return Value
            OfficeViewPagedListSchoolsDto returnValue = null;

            //Instantiate Return Value
            CustomPagedList<SchoolsViewDto> returnValueSchools = CustomPagedList<SchoolsViewDto>
                        .Create(Enumerable.Empty<SchoolsViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Offices.Include(x => x.OfficeType).Include(x => x.State).Include(x => x.CreatedByNavigation).Where(x => x.Id == id) as IQueryable<Offices>;

                    Offices pin = await dbResult.FirstOrDefaultAsync();
                    //
                    var queryableSchools = dbResult.SelectMany(x => x.Schools) as IQueryable<Schools>;



                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        queryableSchools = queryableSchools.Where(a => a.Name.ToString().Contains(searchQuery)
                            || a.Address.ToString().Contains(searchQuery)
                            || a.EmailAddress.ToString().Contains(searchQuery)
                            || a.PhoneNo.ToString().Contains(searchQuery)
                            || (a.YearEstablished != null ? a.YearEstablished : null).ToString().Contains(searchQuery)
                            || (a.Category != null ? a.Category.Name : null).ToString().Contains(searchQuery)
                            || (a.Office != null ? a.Office.Name : null).ToUpper().Contains(searchQuery)
                            || (a.Office != null ? a.Office.Address : null).ToUpper().Contains(searchQuery)
                            || (a.Lg != null ? a.Lg.Name : null).ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var pinsPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<SchoolsViewDto, Schools>();

                        queryableSchools = queryableSchools.ApplySort(resourceParams.OrderBy,
                            pinsPropertyMappingDictionary);
                    }
                    ///Use LINQ to map pins to pinsviewdto
                    var mappedResult = queryableSchools.Select(x => new SchoolsViewDto()
                    {
                        Id = x.Id,
                        SchoolName = x.Name,
                        SchoolCategoryName = x.Category != null ? x.Category.Name : null,
                        OfficeName = x.Office != null ? x.Office.Name : null,
                        LgaName = x.Lg != null ? x.Lg.Name : null,
                        Address = x.Address,
                        EmailAddress = x.EmailAddress,
                        PhoneNo = x.PhoneNo,
                        YearEstablished = x.YearEstablished,
                        IsRecognised = x.IsRecognised,
                        IsVetted = x.IsVetted,
                        IsInspected = x.IsInspected,
                        IsCompleted = x.IsCompleted,
                        IsRecommended = x.IsRecommended,
                        HasDeficientSubject = x.HasDeficientSubject,
                        HasDeficientFacilitiy = x.HasDeficientFacilitiy,

                    });

                    returnValueSchools = await CustomPagedList<SchoolsViewDto>.CreateAsync(mappedResult,
                        resourceParams.PageNumber,
                        resourceParams.PageSize);


                    returnValue = _mapper.Map<OfficeViewPagedListSchoolsDto>(pin);
                    //
                    returnValue.OfficeSchools = returnValueSchools;


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


        public async Task<OfficesViewDto> GetOfficesSingleOrDefaultAsync(Guid id)
        {

            //Instantiate Return Value
            OfficesViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = await _context.Offices.Include(x => x.OfficeType).Include(x => x.State).Include(x => x.CreatedByNavigation).Where(x => x.Id == id).SingleOrDefaultAsync();


                    returnValue = _mapper.Map<OfficesViewDto>(dbResult);


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
        

        public async Task<Guid?> CreateOfficeAsync(OfficesCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null)
                {
                    Offices entity = _mapper.Map<Offices>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    await _context.Offices.AddAsync(entity);
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

        public async Task<OfficesViewDto> UpdateOfficeAsync(OfficesCreateDto _obj)
        {

            //Instantiate Return Value
            OfficesViewDto returnValue = null;
            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<Offices>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue = _mapper.Map<OfficesViewDto>(entity);

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
            try
            {
                if (id != Guid.Empty)
                {
                    var entity = await _context.Offices.Where(x => x.Id == id).SingleOrDefaultAsync();
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



    }
}
