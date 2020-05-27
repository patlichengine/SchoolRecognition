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

    public class cOfficeLocalGovernmentsRepository : IOfficeLocalGovernmentsRepository, IDisposable
    {

        //private readonly ConnectionString _connectionString;
        //private IPinsRepository _pinService;
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        //public cRecognitionLocalGovernmentsRepository(ConnectionString connectionString)
        //{
        //    //_connectionString = connectionString;
        //    //_pinService = new cPinsRepository(_connectionString);

        //}

        public cOfficeLocalGovernmentsRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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

        public async Task<IEnumerable<OfficeLocalGovernmentsViewDto>> List()
        {
            //Instantiate Return Value
            IEnumerable<OfficeLocalGovernmentsViewDto> returnValue = new List<OfficeLocalGovernmentsViewDto>();
            try
            {
                var dbResult = _context.OfficeLocalGovernments
                        .Include(x => x.Office)
                        .ThenInclude(x => x.OfficeType)
                        .Include(x => x.LocalGovernment)
                        .ThenInclude(y => y.State) as IQueryable<OfficeLocalGovernments>;


                returnValue = _mapper.Map<IEnumerable<OfficeLocalGovernmentsViewDto>>(dbResult);

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PagedList<OfficeLocalGovernmentsViewDto>> PagedList(OfficeLocalGovernmentsResourceParams resourceParams)
        {
            //Instantiate Return Value
            PagedList<OfficeLocalGovernmentsViewDto> returnValue = PagedList<OfficeLocalGovernmentsViewDto>
                        .Create(Enumerable.Empty<OfficeLocalGovernmentsViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context.OfficeLocalGovernments
                        .Include(x => x.Office)
                        .ThenInclude(x => x.OfficeType)
                        .Include(x => x.LocalGovernment)
                        .ThenInclude(y => y.State) as IQueryable<OfficeLocalGovernments>;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        dbResult = dbResult.Where(
                            a => (a.LocalGovernment != null ? a.LocalGovernment.Name : null).ToUpper().Contains(searchQuery)
                            || (a.LocalGovernment != null ? a.LocalGovernment.Code : null).ToUpper().Contains(searchQuery)
                            || (a.Office != null ? a.Office.Name : null).ToUpper().Contains(searchQuery)
                            || (a.Office != null ? a.Office.Address : null).ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var recognitionLocalGovernmentPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<OfficeLocalGovernmentsViewDto, OfficeLocalGovernments>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            recognitionLocalGovernmentPropertyMappingDictionary);
                    }

                    var mappedResult = dbResult.Select(x => new OfficeLocalGovernmentsViewDto()
                    {
                        Id = x.Id,
                        LocalGovernmentName = x.LocalGovernment != null ? x.LocalGovernment.Name : null,
                        LocalGovernmentCode = x.LocalGovernment != null ? x.LocalGovernment.Code : null,
                        StateName = x.LocalGovernment != null && x.LocalGovernment.State != null ? x.LocalGovernment.State.Name : null,
                        StateCode = x.LocalGovernment != null && x.LocalGovernment.Code != null ? x.LocalGovernment.State.Name : null,
                        OfficeName = x.Office != null ? x.Office.Name : null,
                        OfficeAddress = x.Office != null ? x.Office.Address : null,
                        OfficeTypeDescription = x.Office != null && x.Office.OfficeType != null ? x.Office.OfficeType.Description : null,

                    });

                    returnValue = await PagedList<OfficeLocalGovernmentsViewDto>.CreateAsync(mappedResult,
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

        public async Task<OfficeLocalGovernmentsViewDto> Get(Guid id)
        {

            //Instantiate Return Value
            OfficeLocalGovernmentsViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.OfficeLocalGovernments as IQueryable<OfficeLocalGovernments>;

                    var officeLocalGovernments = await dbResult.Include(x => x.Office).Include(x => x.LocalGovernment)
                        .Select(x => new OfficeLocalGovernmentsViewDto()
                        {
                            Id = x.Id,
                            OfficeId = x.OfficeId != null ? x.OfficeId.Value : Guid.Empty,
                            OfficeName = x.Office != null ? x.Office.Name : null,
                            OfficeAddress = x.Office != null ? x.Office.Address : null,
                            LocalGovernmentId = x.LocalGovernmentId != null ? x.LocalGovernmentId.Value : Guid.Empty,

                        })
                        .Where(x => x.Id == id).SingleOrDefaultAsync();
                    //


                    return returnValue = officeLocalGovernments;
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

        public async Task<Guid?> Create(OfficeLocalGovernmentsCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    var existingOfficeLocalGovernment = await _context.OfficeLocalGovernments.Where(x => x.OfficeId == _obj.OfficeId && x.LocalGovernmentId == _obj.LocalGovernmentId).SingleOrDefaultAsync();
                    if (existingOfficeLocalGovernment != null)
                    {
                        return returnValue = existingOfficeLocalGovernment.Id;
                    }
                    OfficeLocalGovernments entity = _mapper.Map<OfficeLocalGovernments>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    await _context.OfficeLocalGovernments.AddAsync(entity);
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

        public async Task<IEnumerable<Guid?>> CreateMultiple(OfficeLocalGovernmentsCreateMultipleDto _obj)
        {
            //Instantiate Return Value
            List<Guid?> returnValue = new List<Guid?>();
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    //Store for list of new entitys to add to the db
                    List<OfficeLocalGovernments> entitys = new List<OfficeLocalGovernments>();

                    //Get list of existing OfficeLocalGovernments from db
                    var existingOfficeLocalGovernments = await _context.OfficeLocalGovernments.Where(x => x.OfficeId == _obj.OfficeId).ToListAsync();

                    //Convert List of LocalGovernmentIds to hashset to prevent duplicates
                    var _localGovernmentIds = new HashSet<Guid>(_obj.LocalGovernmentIds);

                    for (int i = 0; i < _localGovernmentIds.Count(); i++)
                    {
                        //Create new entity
                        OfficeLocalGovernments entity = new OfficeLocalGovernments()
                        {
                            Id = Guid.NewGuid(),
                            OfficeId = _obj.OfficeId,
                            LocalGovernmentId = _obj.LocalGovernmentIds[i]
                        };
                        //Check if an entity with the same LocalGovernmentId and OfficeId exists
                        bool isExistingOfficeLocalGovernment = existingOfficeLocalGovernments.Where(x => x.OfficeId == entity.OfficeId && x.LocalGovernmentId == entity.LocalGovernmentId).Any();

                        ///If entity with the same LocalGovernmentId and Office ID
                        ///does not exist add new entity to the list of new entities
                        if (!isExistingOfficeLocalGovernment)
                        {
                            entitys.Add(entity);
                            //
                            returnValue.Add(entity.Id);
                        }
                    }
                    //OfficeLocalGovernments entity = _mapper.Map<OfficeLocalGovernments>(_obj);
                    ////
                    //entity.Id = Guid.NewGuid();
                    await _context.OfficeLocalGovernments.AddRangeAsync(entitys);
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

        public async Task<OfficeLocalGovernmentsViewDto> Update(OfficeLocalGovernmentsCreateDto _obj)
        {

            //Instantiate Return Value
            OfficeLocalGovernmentsViewDto returnValue = null;
            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<OfficeLocalGovernments>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue = _mapper.Map<OfficeLocalGovernmentsViewDto>(entity);

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
                    var entity = await _context.OfficeLocalGovernments.Where(x => x.Id == id).SingleOrDefaultAsync();
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

        public async Task<bool> Exists(Guid localGovernmentdId, Guid officeId)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (localGovernmentdId != Guid.Empty && officeId != Guid.Empty)
                {
                    var dbResult = await _context.OfficeLocalGovernments.AnyAsync(x => x.LocalGovernmentId == localGovernmentdId && x.OfficeId == officeId);
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
        public async Task<bool> Exists(Guid id, Guid localGovernmentdId, Guid officeId)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (id != Guid.Empty && localGovernmentdId != Guid.Empty && officeId != Guid.Empty)
                {
                    var dbResult = await _context.OfficeLocalGovernments.Where(x => x.Id != id).AnyAsync(x => x.LocalGovernmentId == localGovernmentdId && x.OfficeId == officeId);
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


        public async Task<OfficeLocalGovernmentsCreationDependecyDto> GetCreationDependencys()
        {
            //Instantiate Return Value
            OfficeLocalGovernmentsCreationDependecyDto returnValue = new OfficeLocalGovernmentsCreationDependecyDto();
            try
            {
                var officeTypes = await _context.OfficeTypes.Select(x => new OfficeTypesViewDto()
                {
                    Id = x.Id,
                    TypeDescription = x.Description,
                }).ToListAsync();

                var states = await _context.LocalGovernments.Select(x => new StatesViewDto()
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
