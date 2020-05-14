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
    public class cSubjectsRepository : ISubjectsRepository, IDisposable
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

        public cSubjectsRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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

        public async Task<IEnumerable<SubjectsViewDto>> GetAllSubjectsAsync()
        {
            //Instantiate Return Value
            IEnumerable<SubjectsViewDto> returnValue = new List<SubjectsViewDto>();
            try
            {
                var dbResult = await _context.Subjects
                    .Select(x => new SubjectsViewDto()
                    {
                        Id = x.Id,
                        SubjectCode = x.SubjectCode,
                        LongName = x.LongName,
                        ShortName = x.ShortName,
                        HasItem = x.HasItem,
                        IsTradeSubject = x.IsTradeSubject,
                        IsCoreSubject = x.IsCoreSubject
                    }).ToListAsync();

                returnValue = dbResult;

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<CustomPagedList<SubjectsViewDto>> GetAllSubjectsAsPagedListAsync(SubjectsResourceParams resourceParams)
        {
            //Instantiate Return Value
            CustomPagedList<SubjectsViewDto> returnValue = CustomPagedList<SubjectsViewDto>
                        .Create(Enumerable.Empty<SubjectsViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context.Subjects as IQueryable<Subjects>;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        dbResult = dbResult.Where(
                            a=> (a.SubjectCode.ToString()).ToUpper().Contains(searchQuery)

                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var recognitionStatePropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<SubjectsViewDto, Subjects>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            recognitionStatePropertyMappingDictionary);
                    }

                    var mappedResult = dbResult.Select(x => new SubjectsViewDto()
                    {
                        Id = x.Id,
                        SubjectCode = x.SubjectCode,
                        LongName = x.LongName,
                        ShortName = x.ShortName,
                        HasItem = x.HasItem,
                        IsTradeSubject = x.IsTradeSubject,
                        IsCoreSubject = x.IsCoreSubject

                    });

                    returnValue = await CustomPagedList<SubjectsViewDto>.CreateAsync(mappedResult,
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

        public async Task<SubjectsViewDto> GetSubjectsSingleOrDefaultAsync(Guid id)
        {

            //Instantiate Return Value
            SubjectsViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Subjects as IQueryable<Subjects>;

                    var officeStates = await dbResult.Where(x => x.Id == id).SingleOrDefaultAsync();
                    returnValue = _mapper.Map<SubjectsViewDto>(officeStates);
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

        public async Task<SubjectsViewDto> GetSubjectsAllCentreSubjectSanctionsAsync(Guid id)
        {
            //Instantiate Return Value
            SubjectsViewDto returnValue = null;
            IEnumerable<CentreSubjectSanctionsViewDto> returnValueCentreSubjectSanctions = new List<CentreSubjectSanctionsViewDto>();
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Subjects.Include(x => x.CentreSubjectSanctions)
                        .ThenInclude(css => css.CentreSanction)
                        .ThenInclude(cs => cs.Centre)
                        .ThenInclude(c => c.SchoolCategory)
                        .Where(x => x.Id == id) as IQueryable<Subjects>;


                    var pin = await dbResult.SingleOrDefaultAsync();
                    returnValue = _mapper.Map<SubjectsViewDto>(pin);


                    var schoolPayments = await dbResult.SelectMany(x => x.CentreSubjectSanctions).ToListAsync();
                    returnValueCentreSubjectSanctions = _mapper.Map<IEnumerable<CentreSubjectSanctionsViewDto>>(schoolPayments);

                    returnValue.SubjectSanctions = returnValueCentreSubjectSanctions;

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

        public async Task<SubjectViewPagedListCentreSubjectSanctionsDto> GetSubjectsCentreSubjectSanctionsAsPagedListAsync(Guid id, CentreSubjectSanctionsResourceParams resourceParams)
        {

            //Instantiate Return Value
            SubjectViewPagedListCentreSubjectSanctionsDto returnValue = null;

            //Instantiate Return Value
            CustomPagedList<CentreSubjectSanctionsViewDto> returnValueCentreSubjectSanctions = CustomPagedList<CentreSubjectSanctionsViewDto>
                        .Create(Enumerable.Empty<CentreSubjectSanctionsViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Subjects.Include(x => x.CentreSubjectSanctions)
                        .ThenInclude(css => css.CentreSanction)
                        .ThenInclude(cs => cs.Centre)
                        .ThenInclude(c => c.SchoolCategory)
                        .Where(x => x.Id == id) as IQueryable<Subjects>;

                    Subjects pin = await dbResult.FirstOrDefaultAsync();
                    //
                    var queryableCentreSubjectSanctions = dbResult.SelectMany(x => x.CentreSubjectSanctions) as IQueryable<CentreSubjectSanctions>;



                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        queryableCentreSubjectSanctions = queryableCentreSubjectSanctions.Where(
                            //CentreSanctions
                            a => (a.CentreSanction != null ? a.CentreSanction.Description : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.CentreSanction != null ? a.CentreSanction.YearOfSaction.ToString() : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.CentreSanction != null ? a.CentreSanction.DateCreated.ToString() : null).ToString().ToUpper().Contains(searchQuery)
                            //Centre
                            || (a.CentreSanction != null && a.CentreSanction.Centre != null ? a.CentreSanction.Centre.CentreNo : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.CentreSanction != null && a.CentreSanction.Centre != null ? a.CentreSanction.Centre.CentreName : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.CentreSanction != null && a.CentreSanction.Centre != null && a.CentreSanction.Centre.SchoolCategory != null ? a.CentreSanction.Centre.SchoolCategory.Name : null).ToString().ToUpper().Contains(searchQuery)
                            //Subject
                            || (a.Subject != null ? a.Subject.SubjectCode : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.Subject != null ? a.Subject.LongName : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.Subject != null ? a.Subject.ShortName: null).ToString().ToUpper().Contains(searchQuery)
                   
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var pinsPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<CentreSubjectSanctionsViewDto, CentreSubjectSanctions>();

                        queryableCentreSubjectSanctions = queryableCentreSubjectSanctions.ApplySort(resourceParams.OrderBy,
                            pinsPropertyMappingDictionary);
                    }
                    ///Use LINQ to map pins to pinsviewdto
                    var mappedResult = queryableCentreSubjectSanctions.Select(x => new CentreSubjectSanctionsViewDto()
                    {
                        Id = x.Id,
                        IsActive = x.IsActive,
                        //CentreSanctions
                        CentreSanctionDescription = x.CentreSanction != null ? x.CentreSanction.Description : null,
                        CentreSanctionYearOfSaction = x.CentreSanction != null ? x.CentreSanction.YearOfSaction : DateTime.Now.Year,
                        CentreSanctionDateCreated = x.CentreSanction != null ? x.CentreSanction.DateCreated : DateTime.Now,
                        //Centre
                        CentreNo = x.CentreSanction != null && x.CentreSanction.Centre != null ? x.CentreSanction.Centre.CentreNo : null,
                        CentreName = x.CentreSanction != null && x.CentreSanction.Centre != null ? x.CentreSanction.Centre.CentreName : null,
                        SchoolCategoryName = x.CentreSanction != null && x.CentreSanction.Centre != null && x.CentreSanction.Centre.SchoolCategory != null ? x.CentreSanction.Centre.SchoolCategory.Name : null,
                        SchoolCategoryCode = x.CentreSanction != null && x.CentreSanction.Centre != null && x.CentreSanction.Centre.SchoolCategory != null ? x.CentreSanction.Centre.SchoolCategory.Code: null,
                        //Subject
                        SubjectCode = x.Subject != null ? x.Subject.SubjectCode : null,
                        SubjectLongName = x.Subject != null ? x.Subject.LongName : null,
                        SubjectShortName = x.Subject != null ? x.Subject.ShortName: null,
                        HasItem = x.Subject != null ? x.Subject.HasItem : false,
                        IsTradeSubject = x.Subject != null ? x.Subject.IsTradeSubject : false,
                        IsCoreSubject = x.Subject != null ? x.Subject.IsCoreSubject : false,

                    });

                    returnValueCentreSubjectSanctions = await CustomPagedList<CentreSubjectSanctionsViewDto>.CreateAsync(mappedResult,
                        resourceParams.PageNumber,
                        resourceParams.PageSize);


                    returnValue = _mapper.Map<SubjectViewPagedListCentreSubjectSanctionsDto>(pin);
                    //
                    returnValue.CentreSanctions = returnValueCentreSubjectSanctions;


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

        public async Task<Guid?> CreateSubjectAsync(SubjectsCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    Subjects entity = _mapper.Map<Subjects>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    await _context.Subjects.AddAsync(entity);
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

        public async Task<SubjectsViewDto> UpdateSubjectAsync(SubjectsCreateDto _obj)
        {

            //Instantiate Return Value
            SubjectsViewDto returnValue = null;
            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<Subjects>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue = _mapper.Map<SubjectsViewDto>(entity);

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

        public async Task DeleteSubjectAsync(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var entity = await _context.Subjects.Where(x => x.Id == id).SingleOrDefaultAsync();
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


        public async Task<bool> CheckIfSubjectExists(string subjectCode, string longName, string shortName)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(subjectCode) && !String.IsNullOrWhiteSpace(longName) && !String.IsNullOrWhiteSpace(shortName))
                {

                    subjectCode = subjectCode.Trim().ToUpper();
                    longName = longName.Trim().ToUpper();
                    shortName = shortName.Trim().ToUpper();
                    var dbResult = await _context.Subjects.AnyAsync(x => x.SubjectCode.Trim().ToUpper() == subjectCode
                        || x.LongName.Trim().ToUpper() == longName
                        || x.ShortName.Trim().ToUpper() == shortName
                        );

                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(subjectCode));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CheckIfSubjectExists(Guid id, string subjectCode, string longName, string shortName)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (id != Guid.Empty && !String.IsNullOrWhiteSpace(subjectCode) && !String.IsNullOrWhiteSpace(longName) && !String.IsNullOrWhiteSpace(shortName))
                {

                    subjectCode = subjectCode.Trim().ToUpper();
                    longName = longName.Trim().ToUpper();
                    shortName = shortName.Trim().ToUpper();
                    var dbResult = await _context.Subjects.Where(x => x.Id != id).AnyAsync(x => x.SubjectCode.Trim().ToUpper() == subjectCode
                        || x.LongName.Trim().ToUpper() == longName
                        || x.ShortName.Trim().ToUpper() == shortName
                        );

                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(subjectCode));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
