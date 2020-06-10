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

    public class cCentresRepository : ICentresRepository, IDisposable
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

        public cCentresRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }




        #region Base Methods

        async Task<bool> Save()
        {
            return await Task.Run(async () =>
            {
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

        public async Task<IEnumerable<CentresViewDto>> List()
        {
            //Instantiate Return Value
            IEnumerable<CentresViewDto> returnValue = new List<CentresViewDto>();
            try
            {
                var dbResult = await _context.Centres
                    .Include(x => x.SchoolCategory)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x => x.CentreSanctions)
                    .Select(x => new CentresViewDto()
                    {
                        Id = x.Id,
                        CentreNo = x.CentreNo,
                        CentreName = x.CentreName,
                        Longitude = x.Longitude,
                        Latitude = x.Latitude,
                        IsActive = x.IsActive,
                        DateCreated = x.DateCreated != null ? x.DateCreated.Value : DateTime.Now,
                        //SchoolCategory
                        SchoolCategoryId = x.SchoolCategoryId != null ? x.SchoolCategoryId.Value : Guid.Empty,
                        SchoolCategoryName = x.SchoolCategory != null ? x.SchoolCategory.Name : null,
                        SchoolCategoryCode = x.SchoolCategory != null ? x.SchoolCategory.Code : null,
                        //CreatedBy
                        CreatedBy = x.CreatedBy,
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname} {x.CreatedByNavigation.Othernames}" : null,
                        //
                        TotalActiveCentreSanctions = x.CentreSanctions != null ? x.CentreSanctions.Count() : 0

                    }).ToListAsync();

                returnValue = dbResult;

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PagedList<CentresViewDto>> PagedList(CentresResourceParams resourceParams)
        {
            //Instantiate Return Value
            PagedList<CentresViewDto> returnValue = PagedList<CentresViewDto>
                        .Create(Enumerable.Empty<CentresViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context.Centres as IQueryable<Centres>;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        dbResult = dbResult.Where(
                            a => a.CentreNo.ToUpper().Contains(searchQuery)
                            || a.CentreName.ToUpper().Contains(searchQuery)
                            || (a.SchoolCategory != null ? a.SchoolCategory.Name : "").ToUpper().Contains(searchQuery)
                            || (a.SchoolCategory != null ? a.SchoolCategory.Code : "").ToUpper().Contains(searchQuery)
                        );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var recognitionTypePropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<CentresViewDto, Centres>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            recognitionTypePropertyMappingDictionary);
                    }

                    var mappedResult = dbResult.Select(x => new CentresViewDto()
                    {
                        Id = x.Id,
                        CentreNo = x.CentreNo,
                        CentreName = x.CentreName,
                        Longitude = x.Longitude,
                        Latitude = x.Latitude,
                        IsActive = x.IsActive,
                        DateCreated = x.DateCreated != null ? x.DateCreated.Value : DateTime.Now,
                        //SchoolCategory
                        SchoolCategoryId = x.SchoolCategoryId != null ? x.SchoolCategoryId.Value : Guid.Empty,
                        SchoolCategoryName = x.SchoolCategory != null ? x.SchoolCategory.Name : null,
                        SchoolCategoryCode = x.SchoolCategory != null ? x.SchoolCategory.Code : null,
                        //CreatedBy
                        CreatedBy = x.CreatedBy,
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname} {x.CreatedByNavigation.Othernames}" : null,
                        //
                        TotalActiveCentreSanctions = x.CentreSanctions != null ? x.CentreSanctions.Count() : 0


                    });

                    returnValue = await PagedList<CentresViewDto>.CreateAsync(mappedResult,
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

        public async Task<CentresViewDto> Get(Guid id)
        {

            //Instantiate Return Value
            CentresViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Centres
                        .Include(x => x.SchoolCategory)
                        .Include(x => x.CreatedByNavigation)
                        .Include(x => x.CentreSanctions)
                    as IQueryable<Centres>;

                    var centres = await dbResult
                        .Where(x => x.Id == id).SingleOrDefaultAsync();
                    returnValue = _mapper.Map<CentresViewDto>(centres);
                    //
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

        public async Task<CentresViewDto> GetByCentreNumber(string centerNo)
        {


            //Instantiate Return Value
            CentresViewDto returnValue = null;
            try
            {
                if (!String.IsNullOrWhiteSpace(centerNo))
                {
                    var dbResult = _context.Centres
                        .Include(x => x.SchoolCategory)
                        .Include(x => x.CreatedByNavigation)
                        .Include(x => x.CentreSanctions)
                    as IQueryable<Centres>;

                    var centres = await dbResult
                        .Where(x => x.CentreNo == centerNo).SingleOrDefaultAsync();
                    returnValue = _mapper.Map<CentresViewDto>(centres);
                    //
                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(centerNo));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Guid?> Create(CentresCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    Centres entity = _mapper.Map<Centres>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    await _context.Centres.AddAsync(entity);
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

        public async Task<CentresViewDto> Update(CentresCreateDto _obj)
        {

            //Instantiate Return Value
            CentresViewDto returnValue = null;
            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<Centres>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue = _mapper.Map<CentresViewDto>(entity);

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
                    var entity = await _context.Centres.Where(x => x.Id == id).SingleOrDefaultAsync();
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

        public async Task<bool> Exists(string centreName)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(centreName))
                {

                    var searchQuery = centreName.Trim().ToUpper();
                    var dbResult = await _context.Centres.AnyAsync(x => x.CentreName.Trim().ToUpper() == searchQuery);
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(centreName));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Exists(Guid id, string centreName)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(centreName))
                {

                    var searchQuery = centreName.Trim().ToUpper();
                    var dbResult = await _context.Centres.Where(x => x.Id != id).AnyAsync(x => x.CentreName.Trim().ToUpper() == searchQuery);
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

        public async Task<bool> Exists(string centreName, string centreNo)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(centreName))
                {

                    var centreNameQuery = centreName.Trim().ToUpper();
                    var centreNoQuery = centreNo.Trim().ToUpper();
                    var dbResult = await _context.Centres.AnyAsync(x => x.CentreName.Trim().ToUpper() == centreNameQuery && x.CentreNo.Trim().ToUpper() == centreNoQuery);
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(centreName));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Exists(Guid id, string centreName, string centreNo)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(centreName))
                {

                    var centreNameQuery = centreName.Trim().ToUpper();
                    var centreNoQuery = centreNo.Trim().ToUpper();
                    var dbResult = await _context.Centres.Where(x => x.Id != id).AnyAsync(x => x.CentreName.Trim().ToUpper() == centreNameQuery && x.CentreNo.Trim().ToUpper() == centreNoQuery);
                    returnValue = dbResult;
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
