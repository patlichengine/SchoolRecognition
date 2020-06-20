
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
    public class cSchoolsRepository : ISchoolsRepository, IDisposable
    {

        //private readonly ConnectionString _connectionString;
        //private IPinsRepository _pinService;
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly ILocalGovernmentsRepository _localGovernmentsRepository;
        private readonly ISchoolCategoryRepository _schoolCategoryRepository;

        //public cRecognitionTypesRepository(ConnectionString connectionString)
        //{
        //    //_connectionString = connectionString;
        //    //_pinService = new cPinsRepository(_connectionString);

        //}

        public cSchoolsRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService, ILocalGovernmentsRepository localGovernmentsRepository, ISchoolCategoryRepository schoolCategoryRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
            _localGovernmentsRepository = localGovernmentsRepository ?? throw new ArgumentNullException(nameof(localGovernmentsRepository));
            _schoolCategoryRepository = schoolCategoryRepository ?? throw new ArgumentNullException(nameof(schoolCategoryRepository));
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

        public async Task<IEnumerable<SchoolsViewDto>> List()
        {
            //Instantiate Return Value
            IEnumerable<SchoolsViewDto> returnValue = new List<SchoolsViewDto>();
            try
            {
                var dbResult = await _context.Schools
                    .Include(x=>x.Category)
                    .Include(x => x.Office)
                    .Include(x=>x.Lg)
                    .ThenInclude(x=>x.State)
                    .Select(x => new SchoolsViewDto()
                    {
                        Id = x.Id,
                        SchoolName = x.Name,
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
                        //SchoolCategory
                        CategoryId = x.CategoryId,
                        SchoolCategoryName = x.Category != null ? x.Category.Name : null,
                        SchoolCategoryCode = x.Category != null ? x.Category.Code : null,
                        //Office
                        OfficeId = x.OfficeId,
                        OfficeName = x.Office != null ? x.Office.Name : null,
                        //LGA
                        LgId = x.LgId,
                        LgaName = x.Lg != null ? x.Lg.Name : null,
                        LgaCode = x.Lg != null ? x.Lg.Code : null,
                        //State
                        StateName = x.Lg != null & x.Lg.State != null ? x.Lg.State.Name : null,
                        StateCode = x.Lg != null & x.Lg.State != null ? x.Lg.State.Code : null,

                    }).ToListAsync();

                returnValue = dbResult;

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PagedList<SchoolsViewDto>> PagedList(SchoolsResourceParams resourceParams)
        {
            //Instantiate Return Value
            PagedList<SchoolsViewDto> returnValue = PagedList<SchoolsViewDto>
                        .Create(Enumerable.Empty<SchoolsViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context.Schools
                    .Include(x => x.Category)
                    .Include(x => x.Office)
                    .Include(x => x.Lg)
                    .ThenInclude(x => x.State) as IQueryable<Schools>;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        dbResult = dbResult.Where(
                            a => a.Name.ToUpper().Contains(searchQuery)
                            || a.EmailAddress.ToUpper().Contains(searchQuery)
                            || a.PhoneNo.ToUpper().Contains(searchQuery)
                            || (a.YearEstablished != null ? a.YearEstablished.Value.ToString() : "").ToUpper().Contains(searchQuery)
                            || (a.Category != null ? a.Category.Name : "").ToUpper().Contains(searchQuery)
                            || (a.Category != null ? a.Category.Code : "").ToUpper().Contains(searchQuery)
                        );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var recognitionTypePropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<SchoolsViewDto, Schools>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            recognitionTypePropertyMappingDictionary);
                    }

                    var mappedResult = dbResult.Select(x => new SchoolsViewDto()
                    {
                        Id = x.Id,
                        SchoolName = x.Name,
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
                        //SchoolCategory
                        CategoryId = x.CategoryId,
                        SchoolCategoryName = x.Category != null ? x.Category.Name : null,
                        SchoolCategoryCode = x.Category != null ? x.Category.Code : null,
                        //Office
                        OfficeId = x.OfficeId,
                        OfficeName = x.Office != null ? x.Office.Name : null,
                        //LGA
                        LgId = x.LgId,
                        LgaName = x.Lg != null ? x.Lg.Name : null,
                        LgaCode = x.Lg != null ? x.Lg.Code : null,
                        //State
                        StateName = x.Lg != null & x.Lg.State != null ? x.Lg.State.Name : null,
                        StateCode = x.Lg != null & x.Lg.State != null ? x.Lg.State.Code : null,


                    });

                    returnValue = await PagedList<SchoolsViewDto>.CreateAsync(mappedResult,
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

        public async Task<SchoolsViewDto> GetByCentreId(Guid centreId)
        {

            //Instantiate Return Value
            SchoolsViewDto returnValue = null;
            try
            {
                if (centreId != Guid.Empty)
                {
                    var dbResult = _context.Schools
                    .Include(x => x.Category)
                    .Include(x => x.Office)
                    .Include(x => x.Lg)
                    .ThenInclude(x => x.State)
                    .Include(x => x.PinHistories)
                    .Include(x => x.SchoolClassAllocations)
                    .ThenInclude(y => y.Class) 
                    .Include(x=>x.SchoolDeficiencies)
                    .Include(x=>x.SchoolFacilities)
                    .ThenInclude(y=>y.FacilitySetting)
                    .Include(x=>x.SchoolFacilities)
                    .ThenInclude(y=>y.CreatedByNavigation)
                    .Include(x=>x.SchoolPayments)
                    .ThenInclude(y=>y.CreatedByNavigation)
                    .Include(x=>x.SchoolPayments)
                    .ThenInclude(y=>y.Pin)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.Title)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.Category)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.SchoolStaffDegrees)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.SchoolStaffSubjects)
                    as IQueryable<Schools>;

                    var schools = await dbResult
                        .Where(x => x.CentreId == centreId).SingleOrDefaultAsync();
                    returnValue = _mapper.Map<SchoolsViewDto>(schools);
                    //
                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(centreId));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<SchoolsViewDto> GetBySchoolName(string schoolName)
        {

            //Instantiate Return Value
            SchoolsViewDto returnValue = null;
            try
            {
                if (!String.IsNullOrWhiteSpace(schoolName))
                {
                    var dbResult = _context.Schools
                    .Include(x => x.Category)
                    .Include(x => x.Office)
                    .Include(x => x.Lg)
                    .ThenInclude(x => x.State)
                    .Include(x => x.PinHistories)
                    .Include(x => x.SchoolClassAllocations)
                    .ThenInclude(y => y.Class) 
                    .Include(x=>x.SchoolDeficiencies)
                    .Include(x=>x.SchoolFacilities)
                    .ThenInclude(y=>y.FacilitySetting)
                    .Include(x=>x.SchoolFacilities)
                    .ThenInclude(y=>y.CreatedByNavigation)
                    .Include(x=>x.SchoolPayments)
                    .ThenInclude(y=>y.CreatedByNavigation)
                    .Include(x=>x.SchoolPayments)
                    .ThenInclude(y=>y.Pin)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.Title)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.Category)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.SchoolStaffDegrees)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.SchoolStaffSubjects)
                    as IQueryable<Schools>;

                    var schools = await dbResult
                        .Where(x => x.Name == schoolName).SingleOrDefaultAsync();
                    returnValue = _mapper.Map<SchoolsViewDto>(schools);
                    //
                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(schoolName));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        

        public async Task<SchoolsViewPagedListSchoolFacilitiesDto> GetIncludingPagedListOfSchoolFacilities(Guid id, SchoolFacilitiesResourceParams resourceParams)
        {

            //Instantiate Return Value
            SchoolsViewPagedListSchoolFacilitiesDto returnValue = null;

            //Instantiate Return Value
            PagedList<SchoolFacilitiesViewDto> returnValueSchoolFacilities = PagedList<SchoolFacilitiesViewDto>
                        .Create(Enumerable.Empty<SchoolFacilitiesViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Schools
                    .Include(x => x.Category)
                    .Include(x => x.Office)
                    .Include(x => x.Lg)
                    .ThenInclude(x => x.State)
                    .Include(x => x.PinHistories)
                    .Include(x => x.SchoolClassAllocations)
                    .ThenInclude(y => y.Class)
                    .Include(x => x.SchoolDeficiencies)
                    .Include(x => x.SchoolFacilities)
                    .ThenInclude(y => y.FacilitySetting)
                    .ThenInclude(z => z.FacilityItemSettings)
                    .Include(x => x.SchoolFacilities)
                    .ThenInclude(y => y.CreatedByNavigation)
                    .Include(x => x.SchoolPayments)
                    .ThenInclude(y => y.CreatedByNavigation)
                    .Include(x => x.SchoolPayments)
                    .ThenInclude(y => y.Pin)
                    .Include(x => x.SchoolStaffProfiles)
                    .ThenInclude(y => y.Title)
                    .Include(x => x.SchoolStaffProfiles)
                    .ThenInclude(y => y.Category)
                    .Include(x => x.SchoolStaffProfiles)
                    .ThenInclude(y => y.SchoolStaffDegrees)
                    .Include(x => x.SchoolStaffProfiles)
                    .ThenInclude(y => y.SchoolStaffSubjects)
                    .Where(x => x.Id == id) as IQueryable<Schools>;

                    var school = await dbResult.Select(x => new SchoolsViewPagedListSchoolFacilitiesDto()
                      {
                          Id = x.Id,
                          SchoolName = x.Name,
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
                          //SchoolCategory
                          CategoryId = x.CategoryId,
                          SchoolCategoryName = x.Category != null ? x.Category.Name : null,
                          SchoolCategoryCode = x.Category != null ? x.Category.Code : null,
                          //Office
                          OfficeId = x.OfficeId,
                          OfficeName = x.Office != null ? x.Office.Name : null,
                          //LGA
                          LgId = x.LgId,
                          LgaName = x.Lg != null ? x.Lg.Name : null,
                          LgaCode = x.Lg != null ? x.Lg.Code : null,
                          //State
                          StateName = x.Lg != null & x.Lg.State != null ? x.Lg.State.Name : null,
                          StateCode = x.Lg != null & x.Lg.State != null ? x.Lg.State.Code : null,


                      })  
                        .FirstOrDefaultAsync();
                    //
                    var queryableSchoolFacilities = dbResult.SelectMany(x => x.SchoolFacilities) as IQueryable<SchoolFacilities>;



                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        queryableSchoolFacilities = queryableSchoolFacilities.Where(a => a.ValueAupplied.ToString().ToUpper().Contains(searchQuery)             
                            || (a.DateCreated).ToString().ToUpper().Contains(searchQuery)
                            || (a.CreatedByNavigation != null ? a.CreatedByNavigation.Surname : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.CreatedByNavigation != null ? a.CreatedByNavigation.Othernames : null).ToString().ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var pinsPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<SchoolFacilitiesViewDto, SchoolFacilities>();

                        queryableSchoolFacilities = queryableSchoolFacilities.ApplySort(resourceParams.OrderBy,
                            pinsPropertyMappingDictionary);
                    }
                    ///Use LINQ to map pins to pinsviewdto
                    var mappedResult = queryableSchoolFacilities.Select(x => new SchoolFacilitiesViewDto()
                    {
                        Id = x.Id,
                        SchoolId = x.SchoolId,
                        FacilitySettingId = x.FacilitySettingId,
                        ValueAupplied = x.ValueAupplied,
                        CreatedBy = x.CreatedBy,
                        DateCreated = x.DateCreated,
                        //FacilitySettings
                        FacilitySettingPosition = x.FacilitySetting != null ? x.FacilitySetting.Position : null,
                        SubjectId = x.FacilitySetting != null ? x.FacilitySetting.SubjectId : null,
                        FacilitySettingSpecification = x.FacilitySetting != null ? x.FacilitySetting.Specification : null,
                        FacilitySettingQuantity = x.FacilitySetting != null ? x.FacilitySetting.Quantity : null,
                        //FacilityItemSettings
                        FacilityItemSettingsDescription = x.FacilitySetting != null && x.FacilitySetting.FacilityItemSettings != null ? x.FacilitySetting.FacilityItemSettings.Description : null,
                        //Schools
                        SchoolName = x.School != null ? x.School.Name : null,
                        SchoolAddress = x.School != null ? x.School.Address : null,
                        SchoolEmailAddress = x.School != null ? x.School.EmailAddress : null,
                        SchoolPhoneNo = x.School != null ? x.School.PhoneNo : null,
                        YearEstablished = x.School != null ? x.School.YearEstablished : null,
                        SchoolCategoryName = x.School != null && x.School.Category != null ? x.School.Category.Name : null,
                        //CreatedBy
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,

                    });

                    returnValueSchoolFacilities = await PagedList<SchoolFacilitiesViewDto>.CreateAsync(mappedResult,
                        resourceParams.PageNumber,
                        resourceParams.PageSize);


                    returnValue = school;
                    //
                    returnValue.Facilities = returnValueSchoolFacilities;


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



        public async Task<SchoolsViewDto> Get(Guid id)
        {

            //Instantiate Return Value
            SchoolsViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Schools
                    .Include(x => x.Category)
                    .Include(x => x.Office)
                    .Include(x => x.Lg)
                    .ThenInclude(x => x.State)
                    .Include(x => x.PinHistories)
                    .Include(x => x.SchoolClassAllocations)
                    .ThenInclude(y => y.Class) 
                    .Include(x=>x.SchoolDeficiencies)
                    .Include(x=>x.SchoolFacilities)
                    .ThenInclude(y=>y.FacilitySetting)
                    .Include(x=>x.SchoolFacilities)
                    .ThenInclude(y=>y.CreatedByNavigation)
                    .Include(x=>x.SchoolPayments)
                    .ThenInclude(y=>y.CreatedByNavigation)
                    .Include(x=>x.SchoolPayments)
                    .ThenInclude(y=>y.Pin)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.Title)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.Category)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.SchoolStaffDegrees)
                    .Include(x=>x.SchoolStaffProfiles)
                    .ThenInclude(y=>y.SchoolStaffSubjects)
                    as IQueryable<Schools>;

                    var schools = await dbResult
                        .Where(x => x.Id == id).SingleOrDefaultAsync();
                    returnValue = _mapper.Map<SchoolsViewDto>(schools);
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

        public async Task<SchoolsViewDto> GetIncludingListOfSchoolPayments(Guid id)
        {

            //Instantiate Return Value
            SchoolsViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Schools
                    .Include(x => x.Category)
                    .Include(x => x.Office)
                    .Include(x => x.Lg)
                    .ThenInclude(x => x.State)
                    .Include(x => x.PinHistories)
                    .Include(x => x.SchoolClassAllocations)
                    .ThenInclude(y => y.Class)
                    .Include(x => x.SchoolDeficiencies)
                    .Include(x => x.SchoolFacilities)
                    .ThenInclude(y => y.FacilitySetting)
                    .Include(x => x.SchoolFacilities)
                    .ThenInclude(y => y.CreatedByNavigation)
                    .Include(x => x.SchoolPayments)
                    .ThenInclude(y => y.CreatedByNavigation)
                    .Include(x => x.SchoolPayments)
                    .ThenInclude(y => y.Pin)
                    .ThenInclude(z => z.RecognitionType)
                    .Include(x => x.SchoolStaffProfiles)
                    .ThenInclude(y => y.Title)
                    .Include(x => x.SchoolStaffProfiles)
                    .ThenInclude(y => y.Category)
                    .Include(x => x.SchoolStaffProfiles)
                    .ThenInclude(y => y.SchoolStaffDegrees)
                    .Include(x => x.SchoolStaffProfiles)
                    .ThenInclude(y => y.SchoolStaffSubjects)
                    as IQueryable<Schools>;

                    var school = await dbResult.Select(x => new SchoolsViewDto()
                    {
                        Id = x.Id,
                        SchoolName = x.Name,
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
                        //SchoolCategory
                        CategoryId = x.CategoryId,
                        SchoolCategoryName = x.Category != null ? x.Category.Name : null,
                        SchoolCategoryCode = x.Category != null ? x.Category.Code : null,
                        //Office
                        OfficeId = x.OfficeId,
                        OfficeName = x.Office != null ? x.Office.Name : null,
                        //LGA
                        LgId = x.LgId,
                        LgaName = x.Lg != null ? x.Lg.Name : null,
                        LgaCode = x.Lg != null ? x.Lg.Code : null,
                        //State
                        StateName = x.Lg != null & x.Lg.State != null ? x.Lg.State.Name : null,
                        StateCode = x.Lg != null & x.Lg.State != null ? x.Lg.State.Code : null,
                        Payments = x.SchoolPayments.Select(x => new SchoolPaymentsViewDto()
                        {
                            Id = x.Id,
                            AmountPaid = x.Amount,
                            PaymentReceiptNo = x.ReceiptNo,
                            DateCreated = x.DateCreated,
                            PaymentReceiptImage = x.ReceiptImage,
                            //CreatedByNavigation
                            CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                            //school
                            SchoolName = x.School != null ? x.School.Name : null,
                            SchoolCategoryName = x.School != null && x.School.Category != null ? x.School.Name : null,
                            //Pin
                            PinSerialNumber = x.Pin != null ? x.Pin.SerialPin : null,
                            //RecognitionType
                            RecognitionTypeCode = x.Pin != null && x.Pin.RecognitionType != null ? x.Pin.RecognitionType.Code : null,
                            RecognitionTypeName = x.Pin != null && x.Pin.RecognitionType != null ? x.Pin.RecognitionType.Name : null,

                        })

                    })
                    .Where(x => x.Id == id).SingleOrDefaultAsync();
                    //
                    return returnValue = school;
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

        public async Task<Guid?> CreateForCentre(CentresViewDto _obj, Guid? officeId)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            Guid? lgaId = null;
            Guid? schoolCategoryId = null;
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    Schools entity = _mapper.Map<Schools>(_obj);

                    #region Get School Lga

                    char[] charArrayCentreNo = _obj.CentreNo.ToCharArray();

                    //Get characters at positions 2 and 3
                    string stateCode = $"{charArrayCentreNo[1]}{charArrayCentreNo[2]}";

                    //Get characters at positions 4 and 5
                    string lgaCode = $"{charArrayCentreNo[3]}{charArrayCentreNo[4]}";

                    var localGovernment = await _localGovernmentsRepository.GetByStateCodeAndLgaCode(stateCode, lgaCode);
                    if (localGovernment != null)
                    {
                        lgaId = localGovernment.Id;
                    }

                    #endregion

                    #region Get SchoolCategory

                    string _categoryCode = _obj.CentreCategoryCode;

                    var schoolCategory = await _schoolCategoryRepository.GetByCode(_categoryCode);

                    if (schoolCategory != null)
                    {
                        schoolCategoryId = schoolCategory.Id;
                    }


                    #endregion
                    //
                    entity.Id = Guid.NewGuid();
                    //
                    entity.LgId = lgaId;
                    entity.CategoryId = schoolCategoryId;
                    entity.OfficeId = officeId;
                    //
                    await _context.Schools.AddAsync(entity);
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

        public async Task<Guid?> Create(SchoolsCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    Schools entity = _mapper.Map<Schools>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    await _context.Schools.AddAsync(entity);
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

        public async Task<SchoolsViewDto> Update(SchoolsCreateDto _obj)
        {

            //Instantiate Return Value
            SchoolsViewDto returnValue = null;
            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<Schools>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue = _mapper.Map<SchoolsViewDto>(entity);

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
                    var entity = await _context.Schools.Where(x => x.Id == id).SingleOrDefaultAsync();
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

        public async Task<bool> Exists(string schoolName)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(schoolName))
                {

                    var searchQuery = schoolName.Trim().ToUpper();
                    var dbResult = await _context.Schools.AnyAsync(x => x.Name.Trim().ToUpper() == searchQuery);
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(schoolName));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Exists(Guid id, string schoolName)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(schoolName) && id != Guid.Empty)
                {

                    var schoolNameQuery = schoolName.Trim().ToUpper();
                    bool dbResult = await _context.Schools.Where(x => x.Id != id).AnyAsync(x => x.Name.Trim().ToUpper() == schoolNameQuery);
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

        public async Task<bool> Exists(string schoolName, string schoolAddress)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(schoolName))
                {

                    var schoolNameQuery = schoolName.Trim().ToUpper();
                    var schoolAddressQuery = schoolName.Trim().ToUpper();
                    var dbResult = await _context.Schools.AnyAsync(x => x.Name.Trim().ToUpper() == schoolNameQuery && x.Address.Trim().ToUpper() == schoolAddressQuery);
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(schoolName));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Exists(Guid id, string schoolName, string schoolAddress)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(schoolName) && id != Guid.Empty)
                {

                    var schoolNameQuery = schoolName.Trim().ToUpper();
                    var schoolAddressQuery = schoolName.Trim().ToUpper();
                    bool dbResult = await _context.Schools.Where(x => x.Id != id).AnyAsync(x => x.Name.Trim().ToUpper() == schoolNameQuery && x.Address.Trim().ToUpper() == schoolAddressQuery);
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

        public async Task<SchoolsCreationDependecyDto> GetCreationDependencys()
        {
            //Instantiate Return Value
            SchoolsCreationDependecyDto returnValue = new SchoolsCreationDependecyDto();
            try
            {

                var schoolCategorys = await _context.SchoolCategories.Select(x => new SchoolCategorysViewDto()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name

                }).ToListAsync();


                var officeLocalGovernments = await _context.OfficeLocalGovernments
                    .Include(x => x.LocalGovernment)
                    .ThenInclude(y => y.State)
                    .Select(x => new OfficeLocalGovernmentsViewDto()
                    {
                        Id = x.Id,
                        LocalGovernmentId = x.LocalGovernmentId != null ? x.LocalGovernmentId.Value : Guid.Empty,
                        LocalGovernmentCode = x.LocalGovernment != null ? x.LocalGovernment.Code : null,
                        LocalGovernmentName = x.LocalGovernment != null ? x.LocalGovernment.Name : null,
                        StateCode = x.LocalGovernment != null && x.LocalGovernment.State != null ? x.LocalGovernment.State.Code : null,
                        StateName = x.LocalGovernment != null && x.LocalGovernment.State != null ? x.LocalGovernment.State.Name : null,

                    }).ToListAsync();


                returnValue.SchoolCategorys = schoolCategorys;
                returnValue.OfficeLocalGovernments = officeLocalGovernments;


                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
