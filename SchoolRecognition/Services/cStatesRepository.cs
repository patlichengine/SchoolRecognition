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
                var dbResult = _context
                    .States
                    .Include(x => x.LocalGovernments)
                    .Include(x => x.OfficeStates) as IQueryable<States>;

                returnValue = await dbResult.Select(x => new StatesViewDto() {
                    Id = x.Id,
                    StateName = x.Name,
                    StateCode = x.Code,
                    LocalGovernmentsCount = x.LocalGovernments != null ? x.LocalGovernments.Count() : 0,
                    OfficeStatesCount = x.OfficeStates != null ? x.OfficeStates.Count() : 0,
                }).ToListAsync();

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<CustomPagedList<StatesViewDto>> GetAllStatesAsPagedListAsync(StatesResourceParams resourceParams)
        {
            //Instantiate Return Value
            CustomPagedList<StatesViewDto> returnValue = CustomPagedList<StatesViewDto>
                        .Create(Enumerable.Empty<StatesViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context
                        .States
                        .Include(x => x.LocalGovernments)
                        .ThenInclude(y => y.Schools)
                        .ThenInclude(z => z.Category)
                        .Include(x => x.OfficeStates)
                        .ThenInclude(y => y.Office)
                        .ThenInclude(z => z.OfficeType)
                        .Include(x => x.OfficeStates)
                        .ThenInclude(y => y.State)
                        as IQueryable<States>;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        dbResult = dbResult.Where(a => a.Name.ToUpper().Contains(searchQuery)
                            || a.Code.ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var pinPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<StatesViewDto, States>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            pinPropertyMappingDictionary);
                    }

                    var mappedResult = dbResult.Select(x => new StatesViewDto()
                    {
                        Id = x.Id,
                        StateName = x.Name,
                        StateCode = x.Code,
                        LocalGovernmentsCount = x.LocalGovernments != null ? x.LocalGovernments.Count() : 0,
                        OfficeStatesCount = x.OfficeStates != null ? x.OfficeStates.Count() : 0,

                    }); ;

                    returnValue = await CustomPagedList<StatesViewDto>.CreateAsync(mappedResult,
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


        public async Task<StatesViewDto> GetStatesSingleOrDefaultAsync(Guid id)
        {

            //Instantiate Return Value
            StatesViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = await _context
                        .States
                        .Include(x => x.LocalGovernments)
                        .ThenInclude(y => y.State)
                        .Include(x => x.LocalGovernments)
                        .ThenInclude(y => y.Schools)
                        .ThenInclude(z => z.Category)
                        .Include(x => x.OfficeStates)
                        .ThenInclude(y => y.Office)
                        .ThenInclude(z => z.OfficeType)
                        .Include(x => x.OfficeStates)
                        .ThenInclude(y => y.State)
                        .Select(x => new StatesViewDto()
                        {
                            Id = x.Id,
                            StateName = x.Name,
                            StateCode = x.Code,
                            LocalGovernmentsCount = x.LocalGovernments != null ? x.LocalGovernments.Count() : 0,
                            OfficeStatesCount= x.OfficeStates != null ? x.OfficeStates.Count() : 0,
                            StateLGAs = x.LocalGovernments.Select(x=> new LocalGovernmentsViewDto() { 
                                Id = x.Id,
                                LgaName = x.Name,
                                LgaCode = x.Code,
                                StateName = x.State != null ? $"{x.State.Code} {x.State.Name}" : null,
                                SchoolsCount = x.Schools != null ? x.Schools.Count() : 0
                            }),
                            StateOfficeStates = x.OfficeStates.Select(y => new OfficeStatesViewDto()
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



        public async Task<Guid?> CreateStateAsync(StatesCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    States entity = _mapper.Map<States>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    await _context.States.AddAsync(entity);
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

        public async Task<StatesViewDto> UpdateStateAsync(StatesCreateDto _obj)
        {

            //Instantiate Return Value
            StatesViewDto returnValue = null;
            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<States>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue = _mapper.Map<StatesViewDto>(entity);

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

        public async Task DeleteStateAsync(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var entity = await _context.States.Where(x => x.Id == id).SingleOrDefaultAsync();
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

        public async Task<bool> CheckIfStateExists(string stateName)
        {

            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(stateName))
                {

                    var searchQuery = stateName.Trim().ToUpper();
                    var dbResult = await _context.States.AnyAsync(x => x.Name.Trim().ToUpper() == searchQuery);
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(stateName));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public async Task<bool> CheckIfStateExists(Guid id, string stateName)
        {

            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(stateName))
                {

                    var searchQuery = stateName.Trim().ToUpper();
                    var dbResult = await _context.States.Where(x => x.Id != id).AnyAsync(x => x.Name.Trim().ToUpper() == searchQuery);
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(stateName));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
