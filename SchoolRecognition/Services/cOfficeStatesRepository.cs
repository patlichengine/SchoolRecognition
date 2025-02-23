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

    public class cOfficeStatesRepository : IOfficeStatesRepository, IDisposable
    {

        //private readonly ConnectionString _connectionString;
        //private IPinsRepository _pinService;
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        //public cRecognitionStatesRepository(ConnectionString connectionString)
        //{
        //    //_connectionString = connectionString;
        //    //_pinService = new cPinsRepository(_connectionString);

        //}

        public cOfficeStatesRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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

        public async Task<IEnumerable<OfficeStatesViewDto>> List()
        {
            //Instantiate Return Value
            IEnumerable<OfficeStatesViewDto> returnValue = new List<OfficeStatesViewDto>();
            try
            {
                var dbResult = await _context.OfficeStates
                    .Include(x => x.Office)
                    .ThenInclude(y => y.OfficeType)
                    .Include(x => x.State)
                    .Select(x => new OfficeStatesViewDto()
                    {
                        Id = x.Id,
                        OfficeId = x.OfficeId != null ? x.OfficeId.Value : Guid.Empty,
                        OfficeName = x.Office != null ? x.Office.Name : null,
                        OfficeAddress = x.Office != null ? x.Office.Address : null,
                        OfficeTypeDescription = x.Office != null && x.Office.OfficeType != null ? x.Office.OfficeType.Description : null,
                        StateId = x.StateId != null ? x.StateId.Value : Guid.Empty,
                        StateName = x.State != null ? x.State.Name : null,
                        StateCode = x.State != null ? x.State.Code : null,
                    }).ToListAsync();

                returnValue = dbResult;

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PagedList<OfficeStatesViewDto>> PagedList(OfficeStatesResourceParams resourceParams)
        {
            //Instantiate Return Value
            PagedList<OfficeStatesViewDto> returnValue = PagedList<OfficeStatesViewDto>
                        .Create(Enumerable.Empty<OfficeStatesViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context.OfficeStates
                    .Include(x => x.Office)
                    .ThenInclude(y => y.OfficeType)
                    .Include(x => x.State) as IQueryable<OfficeStates>;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        dbResult = dbResult.Where(
                            a => (a.State != null ? a.State.Name : null).ToUpper().Contains(searchQuery)
                            || (a.State != null ? a.State.Code : null).ToUpper().Contains(searchQuery)
                            || (a.Office != null ? a.Office.Name : null).ToUpper().Contains(searchQuery)
                            || (a.Office != null ? a.Office.Address : null).ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var recognitionStatePropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<OfficeStatesViewDto, OfficeStates>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            recognitionStatePropertyMappingDictionary);
                    }

                    var mappedResult = dbResult.Select(x => new OfficeStatesViewDto()
                    {
                        Id = x.Id,
                        OfficeId = x.OfficeId != null ? x.OfficeId.Value : Guid.Empty,
                        OfficeName = x.Office != null ? x.Office.Name : null,
                        OfficeAddress = x.Office != null ? x.Office.Address : null,
                        OfficeTypeDescription = x.Office != null && x.Office.OfficeType != null ? x.Office.OfficeType.Description : null,
                        StateId = x.StateId != null ? x.StateId.Value : Guid.Empty,
                        StateName = x.State != null ? x.State.Name : null,
                        StateCode = x.State != null ? x.State.Code : null,

                    });

                    returnValue = await PagedList<OfficeStatesViewDto>.CreateAsync(mappedResult,
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

        public async Task<OfficeStatesViewDto> Get(Guid id)
        {

            //Instantiate Return Value
            OfficeStatesViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.OfficeStates as IQueryable<OfficeStates>;

                    var officeStates = await dbResult.Include(x => x.Office).Include(x => x.State)
                        .Select(x => new OfficeStatesViewDto()
                        {
                            Id = x.Id,
                            OfficeId = x.OfficeId != null ? x.OfficeId.Value : Guid.Empty,
                            OfficeName = x.Office != null ? x.Office.Name : null,
                            OfficeAddress = x.Office != null ? x.Office.Address : null,
                            StateId = x.StateId != null ? x.StateId.Value : Guid.Empty,
                            StateName = x.State != null ? x.State.Name : null,
                            StateCode = x.State != null ? x.State.Code : null,

                        })
                        .Where(x => x.Id == id).SingleOrDefaultAsync();
                    //


                    return returnValue = officeStates;
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

        public async Task<Guid?> Create(OfficeStatesCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    var existingOfficeState = await _context.OfficeStates.Where(x => x.OfficeId == _obj.OfficeId && x.StateId == _obj.StateId).SingleOrDefaultAsync();
                    if (existingOfficeState != null)
                    {
                        return returnValue = existingOfficeState.Id;
                    }
                    OfficeStates entity = _mapper.Map<OfficeStates>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    await _context.OfficeStates.AddAsync(entity);
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

        public async Task<IEnumerable<Guid?>> CreateMultiple(OfficeStatesCreateMultipleDto _obj)
        {
            //Instantiate Return Value
            List<Guid?> returnValue = new List<Guid?>();
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    //Store for list of new entitys to add to the db
                    List<OfficeStates> entitys = new List<OfficeStates>();

                    //Get list of existing OfficeStates from db
                    var existingOfficeStates = await _context.OfficeStates.Where(x => x.OfficeId == _obj.OfficeId).ToListAsync();

                    //Convert List of StateIds to hashset to prevent duplicates
                    var _stateIds = new HashSet<Guid>(_obj.StateIds);

                    for (int i = 0; i < _stateIds.Count(); i++)
                    {
                        //Create new entity
                        OfficeStates entity = new OfficeStates()
                        {
                            Id = Guid.NewGuid(),
                            OfficeId = _obj.OfficeId,
                            StateId = _obj.StateIds[i]
                        };
                        //Check if an entity with the same StateId and OfficeId exists
                        bool isExistingOfficeState = existingOfficeStates.Where(x => x.OfficeId == entity.OfficeId && x.StateId == entity.StateId).Any();

                        ///If entity with the same StateId and Office ID
                        ///does not exist add new entity to the list of new entities
                        if (!isExistingOfficeState)
                        {
                            entitys.Add(entity);
                            //
                            returnValue.Add(entity.Id);
                        }
                    }
                    //OfficeStates entity = _mapper.Map<OfficeStates>(_obj);
                    ////
                    //entity.Id = Guid.NewGuid();
                    await _context.OfficeStates.AddRangeAsync(entitys);
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

        public async Task<OfficeStatesViewDto> Update(OfficeStatesCreateDto _obj)
        {

            //Instantiate Return Value
            OfficeStatesViewDto returnValue = null;
            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<OfficeStates>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue = _mapper.Map<OfficeStatesViewDto>(entity);

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
                    var entity = await _context.OfficeStates.Where(x => x.Id == id).SingleOrDefaultAsync();
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

        public async Task<bool> Exists(Guid statedId, Guid officeId)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (statedId != Guid.Empty && officeId != Guid.Empty)
                {
                    var dbResult = await _context.OfficeStates.AnyAsync(x => x.StateId == statedId && x.OfficeId == officeId);
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(officeId));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> Exists(Guid id, Guid statedId, Guid officeId)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (id != Guid.Empty && statedId != Guid.Empty && officeId != Guid.Empty)
                {
                    var dbResult = await _context.OfficeStates.Where(x => x.Id != id).AnyAsync(x => x.StateId == statedId && x.OfficeId == officeId);
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


        public async Task<OfficeStatesCreationDependecyDto> GetCreationDependencys()
        {
            //Instantiate Return Value
            OfficeStatesCreationDependecyDto returnValue = new OfficeStatesCreationDependecyDto();
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
