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


        public async Task<IEnumerable<OfficesViewDto>> List()
        {

            //Instantiate Return Value
            IEnumerable<OfficesViewDto> returnValue = new List<OfficesViewDto>();
            try
            {
                var dbResult = _context.Offices
                    .Include(x => x.OfficeType)
                    .Include(x => x.State)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x=>x.OfficeLocalGovernments)
                    .Include(x=>x.Schools)
                    .Include(x => x.OfficeStates) as IQueryable<Offices>;

                returnValue = await dbResult.Select(x => new OfficesViewDto()
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
                    OfficeStatesCount = x.OfficeStates != null ? x.OfficeStates.Count() : 0,
                    OfficeLocalGovernmentsCount = x.OfficeLocalGovernments != null ? x.OfficeLocalGovernments.Count() : 0,
                    SchoolsCount = x.Schools != null ? x.Schools.Count() : 0,


                }).ToListAsync();

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<OfficesViewDto>> ListByOfficeTypeId(Guid officeTypeId)
        {

            //Instantiate Return Value
            IEnumerable<OfficesViewDto> returnValue = new List<OfficesViewDto>();
            try
            {
                if (officeTypeId != Guid.Empty)
                {
                    var dbResult = _context.Offices
                    .Include(x => x.OfficeType)
                    .Include(x => x.State)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x => x.OfficeLocalGovernments)
                    .Include(x => x.Schools)
                    .Include(x => x.OfficeStates)
                    .Where(x => x.OfficeTypeId == officeTypeId) as IQueryable<Offices>;

                    returnValue = await dbResult.Select(x => new OfficesViewDto()
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
                        OfficeStatesCount = x.OfficeStates != null ? x.OfficeStates.Count() : 0,
                        OfficeLocalGovernmentsCount = x.OfficeLocalGovernments != null ? x.OfficeLocalGovernments.Count() : 0,
                        SchoolsCount = x.Schools != null ? x.Schools.Count() : 0,


                    }).ToListAsync();
                }

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<PagedList<OfficesViewDto>> PagedList(OfficesResourceParams resourceParams)
        {
            //Instantiate Return Value
            PagedList<OfficesViewDto> returnValue = PagedList<OfficesViewDto>
                        .Create(Enumerable.Empty<OfficesViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context.Offices
                    .Include(x => x.OfficeType)
                    .Include(x => x.State)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x => x.OfficeLocalGovernments)
                    .Include(x => x.Schools)
                    .Include(x => x.OfficeStates) as IQueryable<Offices>;
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
                        DateCreated = x.DateCreated,
                        OfficeStatesCount = x.OfficeStates != null ? x.OfficeStates.Count() : 0,
                        OfficeLocalGovernmentsCount = x.OfficeLocalGovernments != null ? x.OfficeLocalGovernments.Count() : 0,
                        SchoolsCount = x.Schools != null ? x.Schools.Count() : 0,


                    }); ;

                    returnValue = await PagedList<OfficesViewDto>.CreateAsync(mappedResult,
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


        public async Task<OfficesViewDto> Get(Guid id)
        {

            //Instantiate Return Value
            OfficesViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = await _context.Offices.Include(x => x.OfficeType)
                    .Include(x => x.State)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.State)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.Office)
                    .Include(x => x.OfficeLocalGovernments)
                    .ThenInclude(y => y.LocalGovernment)
                    .Include(x => x.OfficeLocalGovernments)
                    .ThenInclude(y => y.Office)
                        .Select(x => new OfficesViewDto()
                        {
                            Id = x.Id,
                            OfficeName = x.Name,
                            OfficeAddress = x.Address,
                            StateName = x.State != null ? x.State.Name : null,
                            OfficeImage = x.OfficeImage,
                            Longitude = x.Longitute,
                            Latitude = x.Latitude,
                            OfficeTypeDescription = x.OfficeType != null ? x.OfficeType.Description : null,
                            //IsActive = x.IsActive,
                            StateId = x.StateId,
                            OfficeTypeId = x.OfficeTypeId,
                            //IsInUse = x.IsInUse,
                            CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                            DateCreated = x.DateCreated,
                            OfficeStateStates = x.OfficeStates.Select(y => new OfficeStatesViewDto()
                            {
                                Id = y.Id,
                                StateId = y.StateId != null ? y.StateId.Value : Guid.Empty,
                                OfficeId = y.OfficeId != null ? y.OfficeId.Value : Guid.Empty,
                                StateName = y.State != null ? y.State.Name : null,
                                StateCode = y.State != null ? y.State.Code : null,
                                OfficeName = y.Office != null ? y.Office.Name : null,
                                OfficeAddress = y.Office != null ? y.Office.Address : null,
                            }),
                            OfficeLgas = x.OfficeLocalGovernments.Select(y => new OfficeLocalGovernmentsViewDto()
                            {
                                Id = y.Id,
                                LocalGovernmentId = y.LocalGovernmentId != null ? y.LocalGovernmentId.Value : Guid.Empty,
                                OfficeId = y.OfficeId != null ? y.OfficeId.Value : Guid.Empty,
                                LocalGovernmentName = y.LocalGovernment != null ? y.LocalGovernment.Name : null,
                                LocalGovernmentCode = y.LocalGovernment != null ? y.LocalGovernment.Code : null,
                                OfficeName = y.Office != null ? y.Office.Name : null,
                                OfficeAddress = y.Office != null ? y.Office.Address : null,
                            }),

                        })
                        .Where(x => x.Id == id).SingleOrDefaultAsync();


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

        public async Task<OfficesViewDto> GetIncludingListOfOfficeStates(Guid id)
        {

            //Instantiate Return Value
            OfficesViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = await _context.Offices.Include(x => x.OfficeType)
                    .Include(x => x.State)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.State)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.Office)
                    .Include(x => x.OfficeLocalGovernments)
                    .ThenInclude(y => y.LocalGovernment)
                    .Include(x => x.OfficeLocalGovernments)
                    .ThenInclude(y => y.Office)
                        .Select(x => new OfficesViewDto()
                        {
                            Id = x.Id,
                            OfficeName = x.Name,
                            OfficeAddress = x.Address,
                            StateName = x.State != null ? x.State.Name : null,
                            OfficeImage = x.OfficeImage,
                            Longitude = x.Longitute,
                            Latitude = x.Latitude,
                            OfficeTypeDescription = x.OfficeType != null ? x.OfficeType.Description : null,
                            //IsActive = x.IsActive,
                            StateId = x.StateId,
                            OfficeTypeId = x.OfficeTypeId,
                            //IsInUse = x.IsInUse,
                            CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                            DateCreated = x.DateCreated,
                            OfficeStateStates = x.OfficeStates.Select(y => new OfficeStatesViewDto()
                            {
                                Id = y.Id,
                                StateId = y.StateId != null ? y.StateId.Value : Guid.Empty,
                                OfficeId = y.OfficeId != null ? y.OfficeId.Value : Guid.Empty,
                                StateName = y.State != null ? y.State.Name : null,
                                StateCode = y.State != null ? y.State.Code : null,
                                OfficeName = y.Office != null ? y.Office.Name : null,
                                OfficeAddress = y.Office != null ? y.Office.Address : null,
                            })

                        })
                        .Where(x => x.Id == id).SingleOrDefaultAsync();


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


        public async Task<OfficesViewPagedListSchoolsDto> GetIncludingPagedListOfSchools(Guid id, SchoolsResourceParams resourceParams)
        {

            //Instantiate Return Value
            OfficesViewPagedListSchoolsDto returnValue = null;

            //Instantiate Return Value
            PagedList<SchoolsViewDto> returnValueSchools = PagedList<SchoolsViewDto>
                        .Create(Enumerable.Empty<SchoolsViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Offices
                    .Include(x => x.OfficeType)
                    .Include(x => x.State)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.State)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.Office)
                    .Include(x => x.OfficeLocalGovernments)
                    .ThenInclude(y => y.LocalGovernment)
                    .Include(x => x.OfficeLocalGovernments)
                    .ThenInclude(y => y.Office)
                    .Include(x => x.Schools)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.Schools)
                    .ThenInclude(x => x.Lg)
                    .Include(x => x.Schools)
                    .ThenInclude(x => x.Office)
                    .Where(x => x.Id == id) as IQueryable<Offices>;

                    var office = await dbResult.Select(x => new OfficesViewPagedListSchoolsDto()
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


                    }).FirstOrDefaultAsync();
                    //
                    var queryableSchools = dbResult.SelectMany(x => x.Schools) as IQueryable<Schools>;



                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        queryableSchools = queryableSchools.Where(a => a.Name.ToString().ToUpper().Contains(searchQuery)
                            || a.Address.ToString().ToUpper().Contains(searchQuery)
                            || a.EmailAddress.ToString().ToUpper().Contains(searchQuery)
                            || a.PhoneNo.ToString().ToUpper().Contains(searchQuery)
                            || (a.YearEstablished != null ? a.YearEstablished : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.Category != null ? a.Category.Name : null).ToString().ToUpper().Contains(searchQuery)
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

                    returnValueSchools = await PagedList<SchoolsViewDto>.CreateAsync(mappedResult,
                        resourceParams.PageNumber,
                        resourceParams.PageSize);


                    returnValue = office;
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
        
        
        public async Task<OfficeViewPagedListOfficeLocalGovernmentsDto> GetIncludingPagedListOfOfficeLocalGovernments(Guid id, OfficeLocalGovernmentsResourceParams resourceParams)
        {

            //Instantiate Return Value
            OfficeViewPagedListOfficeLocalGovernmentsDto returnValue = null;

            //Instantiate Return Value
            PagedList<OfficeLocalGovernmentsViewDto> returnValueOfficeLocalGovernments = PagedList<OfficeLocalGovernmentsViewDto>
                        .Create(Enumerable.Empty<OfficeLocalGovernmentsViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Offices
                    .Include(x => x.OfficeType)
                    .Include(x => x.State)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.State)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.Office)
                    .Include(x => x.OfficeLocalGovernments)
                    .ThenInclude(y => y.LocalGovernment)
                    .ThenInclude(z => z.State)
                    .Include(x => x.OfficeLocalGovernments)
                    .ThenInclude(y => y.Office)
                    .ThenInclude(z => z.OfficeType)
                    .Where(x => x.Id == id) as IQueryable<Offices>;

                    var office = await dbResult.Select(x => new OfficeViewPagedListOfficeLocalGovernmentsDto()
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


                    }).FirstOrDefaultAsync();
                    //
                    var queryableOfficeLocalGovernments = dbResult.SelectMany(x => x.OfficeLocalGovernments) as IQueryable<OfficeLocalGovernments>;



                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        queryableOfficeLocalGovernments = queryableOfficeLocalGovernments.Where(
                            a => (a.LocalGovernment != null ? a.LocalGovernment.Name : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.LocalGovernment != null ? a.LocalGovernment.Code : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.LocalGovernment != null && a.LocalGovernment.State != null ? a.LocalGovernment.State.Name : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.LocalGovernment != null && a.LocalGovernment.State != null ? a.LocalGovernment.State.Code : null).ToString().ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var pinsPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<OfficeLocalGovernmentsViewDto, OfficeLocalGovernments>();

                        queryableOfficeLocalGovernments = queryableOfficeLocalGovernments.ApplySort(resourceParams.OrderBy,
                            pinsPropertyMappingDictionary);
                    }
                    ///Use LINQ to map pins to pinsviewdto
                    var mappedResult = queryableOfficeLocalGovernments.Select(x => new OfficeLocalGovernmentsViewDto()
                    {
                        Id = x.Id,
                        LocalGovernmentName = x.LocalGovernment != null ? x.LocalGovernment.Name : null,
                        LocalGovernmentCode = x.LocalGovernment != null ? x.LocalGovernment.Code : null,
                        StateName = x.LocalGovernment != null && x.LocalGovernment.State != null ? x.LocalGovernment.State.Name : null,
                        StateCode = x.LocalGovernment != null && x.LocalGovernment.Code != null ? x.LocalGovernment.State.Name : null,
                        OfficeName = x.Office != null ? x.Office.Name : null,
                        OfficeAddress = x.Office != null ? x.Office.Address : null,
                    });

                    returnValueOfficeLocalGovernments = await PagedList<OfficeLocalGovernmentsViewDto>.CreateAsync(mappedResult,
                        resourceParams.PageNumber,
                        resourceParams.PageSize);


                    returnValue = office;
                    //
                    returnValue.OfficeLgas = returnValueOfficeLocalGovernments;


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

        public async Task<Guid?> Create(OfficesCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    Offices entity = _mapper.Map<Offices>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    entity.DateCreated = DateTime.Now;
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

        public async Task<OfficesViewDto> Update(OfficesCreateDto _obj)
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

        public async Task Delete(Guid id)
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


        ///
        public async Task<bool> Exists(string officeName)
        {
            //Instantiate Return Value
            bool returnValue = true;
            try
            {

                if (!String.IsNullOrWhiteSpace(officeName))
                {

                    var searchQuery = officeName.Trim().ToUpper();
                    var dbResult = await _context.Offices.AnyAsync(x => x.Name.Trim().ToUpper() == searchQuery);
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(officeName));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Exists(Guid id, string officeName)
        {
            //Instantiate Return Value
            bool returnValue = true;
            try
            {

                if (!String.IsNullOrWhiteSpace(officeName) && id != Guid.Empty)
                {

                    var searchQuery = officeName.Trim().ToUpper();
                    var dbResult = await _context.Offices.Where(x => x.Id != id).AnyAsync(x => x.Name.Trim().ToUpper() == searchQuery);
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

        public async Task<OfficesCreationDependecyDto> GetCreationDependencys()
        {
            //Instantiate Return Value
            OfficesCreationDependecyDto returnValue = new OfficesCreationDependecyDto();
            try
            {
                var officeTypes = await _context.OfficeTypes.Select(x => new OfficeTypesViewDto()
                    {
                        Id = x.Id,
                        TypeDescription = x.Description,
                    }).ToListAsync();

                var states = await _context.States.Select(x => new StatesViewDto()
                {
                    Id = x.Id,
                    StateCode = x.Code,
                    StateName = x.Name

                }).ToListAsync();
                                             

                returnValue.OfficeTypes = officeTypes;
                returnValue.States = states;

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public async Task<Guid> GetCurrentUserOfficeId()
        {
            Guid returnValue = Guid.Empty;
            try
            {

                var dbResult = await _context.Offices.Select(x => x.Id).FirstOrDefaultAsync();
                returnValue = dbResult;

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //

    }
}
