﻿using AutoMapper;
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
                var dbResult = _context.Offices
                    .Include(x => x.OfficeType)
                    .Include(x => x.State)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.State)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.Office) as IQueryable<Offices>;

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
                    OfficeStateOffices = x.OfficeStates.Select(y => new OfficeStatesViewDto()
                    {
                        Id = y.Id,
                        StateId = y.StateId != null ? y.StateId.Value : Guid.Empty,
                        OfficeId = y.OfficeId != null ? y.OfficeId.Value : Guid.Empty,
                        StateName = y.State != null ? y.State.Name : null,
                        StateCode = y.State != null ? y.State.Code : null,
                        OfficeName = y.Office != null ? y.Office.Name : null,
                        OfficeAddress = y.Office != null ? y.Office.Address : null,
                    }),


                }).ToListAsync();

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

                    var dbResult = _context.Offices
                    .Include(x => x.OfficeType)
                    .Include(x => x.State)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.State)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.Office) as IQueryable<Offices>;
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
                        OfficeStateOffices = x.OfficeStates.Select(y => new OfficeStatesViewDto()
                        {
                            Id = y.Id,
                            StateId = y.StateId != null ? y.StateId.Value : Guid.Empty,
                            OfficeId = y.OfficeId != null ? y.OfficeId.Value : Guid.Empty,
                            StateName = y.State != null ? y.State.Name : null,
                            StateCode = y.State != null ? y.State.Code : null,
                            OfficeName = y.Office != null ? y.Office.Name : null,
                            OfficeAddress = y.Office != null ? y.Office.Address : null,
                        }),


                    }); ;

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

        public async Task<OfficesViewDto> GetOfficesAllSchoolsAsync(Guid id)
        {
            //Instantiate Return Value
            OfficesViewDto returnValue = null;
            IEnumerable<SchoolsViewDto> returnValueSchools = new List<SchoolsViewDto>();
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
                    .ThenInclude(y => y.Office).Where(x => x.Id == id) as IQueryable<Offices>;


                    var office = await dbResult.Select(x => new OfficesViewDto()
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
                            StateId = y.StateId != null ? y.StateId.Value : Guid.Empty,
                            OfficeId = y.OfficeId != null ? y.OfficeId.Value : Guid.Empty,
                            StateName = y.State != null ? y.State.Name : null,
                            StateCode = y.State != null ? y.State.Code : null,
                            OfficeName = y.Office != null ? y.Office.Name : null,
                            OfficeAddress = y.Office != null ? y.Office.Address : null,
                        }),


                    }).SingleOrDefaultAsync();

                    returnValue = office;


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
                    var dbResult = _context.Offices
                    .Include(x => x.OfficeType)
                    .Include(x => x.State)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.State)
                    .Include(x => x.OfficeStates)
                    .ThenInclude(y => y.Office)
                    .Where(x => x.Id == id) as IQueryable<Offices>;

                    var office = await dbResult.Select(x => new OfficeViewPagedListSchoolsDto()
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
                            StateId = y.StateId != null ? y.StateId.Value : Guid.Empty,
                            OfficeId = y.OfficeId != null ? y.OfficeId.Value : Guid.Empty,
                            StateName = y.State != null ? y.State.Name : null,
                            StateCode = y.State != null ? y.State.Code : null,
                            OfficeName = y.Office != null ? y.Office.Name : null,
                            OfficeAddress = y.Office != null ? y.Office.Address : null,
                        }),


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

                    returnValueSchools = await CustomPagedList<SchoolsViewDto>.CreateAsync(mappedResult,
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


        public async Task<OfficesViewDto> GetOfficesSingleOrDefaultAsync(Guid id)
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
                            OfficeStateOffices = x.OfficeStates.Select(y => new OfficeStatesViewDto()
                            {
                                Id = y.Id,
                                StateId = y.StateId != null ? y.StateId.Value : Guid.Empty,
                                OfficeId = y.OfficeId != null ? y.OfficeId.Value : Guid.Empty,
                                StateName = y.State != null ? y.State.Name : null,
                                StateCode = y.State != null ? y.State.Code : null,
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
        

        public async Task<Guid?> CreateOfficeAsync(OfficesCreateDto _obj)
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
                    await _context.Offices.AddAsync(entity);

                    //Create OfficeState entity for this Office
                    OfficeStates officeState = new OfficeStates()
                    {
                        Id = Guid.NewGuid(),
                        OfficeId = entity.Id,
                        StateId = _obj.StateAssigned
                    };
                    await _context.OfficeStates.AddAsync(officeState);


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


        ///
        public async Task<bool> CheckIfOfficeExists(string officeName)
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

        public async Task<bool> CheckIfOfficeExists(Guid id, string officeName)
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

        public async Task<OfficeCreationDependecyDto> GetOfficeCreationDepedencys()
        {
            //Instantiate Return Value
            OfficeCreationDependecyDto returnValue = new OfficeCreationDependecyDto();
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
    }
}
