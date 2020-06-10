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
    public class cSchoolPaymentsRepository : ISchoolPaymentsRepository, IDisposable
    {

        //private readonly ConnectionString _connectionString;
        //private IPinsRepository _pinService;
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly ICentresRepository _centresRepository;
        private readonly ISchoolsRepository _schoolsRepository;
        private readonly IPinsRepository _pinsRepository;

        //public cRecognitionTypesRepository(ConnectionString connectionString)
        //{
        //    //_connectionString = connectionString;
        //    //_pinService = new cPinsRepository(_connectionString);

        //}

        public cSchoolPaymentsRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService, ICentresRepository centresRepository, IPinsRepository pinsRepository, ISchoolsRepository schoolsRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
            _centresRepository = centresRepository ?? throw new ArgumentNullException(nameof(centresRepository));
            _schoolsRepository = schoolsRepository ?? throw new ArgumentNullException(nameof(schoolsRepository));
            _pinsRepository = pinsRepository ?? throw new ArgumentNullException(nameof(pinsRepository));
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

        public async Task<IEnumerable<SchoolPaymentsViewDto>> List()
        {
            //Instantiate Return Value
            IEnumerable<SchoolPaymentsViewDto> returnValue = new List<SchoolPaymentsViewDto>();
            try
            {
                var dbResult = await _context.SchoolPayments
                    .Include(x=>x.Pin)
                    .ThenInclude(y=>y.RecognitionType)
                    .Include(x=>x.School)
                    .ThenInclude(y=>y.Category)
                    .Include(x=>x.CreatedByNavigation)
                    .Select(x => new SchoolPaymentsViewDto()
                    {
                        Id = x.Id,
                        AmountPaid = x.Amount,
                        PaymentReceiptNo = x.ReceiptNo,
                        //PaymentReceiptImage = x.ReceiptImage,
                        DateCreated = x.DateCreated,
                        //Pin
                        PinId = x.PinId,
                        PinSerialNumber = x.Pin != null ? x.Pin.SerialPin : null,
                        //RecognitionType
                        RecognitionTypeName = x.Pin != null && x.Pin.RecognitionType != null ? x.Pin.RecognitionType.Name : null,
                        RecognitionTypeCode = x.Pin != null && x.Pin.RecognitionType != null ? x.Pin.RecognitionType.Code : null,
                        //School
                        SchoolId = x.SchoolId,
                        SchoolName = x.School != null ? x.School.Name : null,
                        //SchoolCategory
                        SchoolCategoryName = x.School != null && x.School.Category != null ? x.School.Category.Name : null,
                        //CreatedBy
                        CreatedBy = x.CreatedBy,
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname} {x.CreatedByNavigation.Othernames}" : null



                    }).ToListAsync();

                returnValue = dbResult;

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PagedList<SchoolPaymentsViewDto>> PagedList(SchoolPaymentsResourceParams resourceParams)
        {
            //Instantiate Return Value
            PagedList<SchoolPaymentsViewDto> returnValue = PagedList<SchoolPaymentsViewDto>
                        .Create(Enumerable.Empty<SchoolPaymentsViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context.SchoolPayments
                    .Include(x => x.Pin)
                    .ThenInclude(y => y.RecognitionType)
                    .Include(x => x.School)
                    .ThenInclude(y => y.Category)
                    .Include(x => x.CreatedByNavigation) as IQueryable<SchoolPayments>;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        dbResult = dbResult.Where(
                            a => a.ReceiptNo.ToUpper().Contains(searchQuery)
                            || (a.Amount != null ? a.Amount.Value.ToString() : "").ToUpper().Contains(searchQuery)
                            || (a.Pin != null ? a.Pin.SerialPin : "").ToUpper().Contains(searchQuery)
                            || (a.School != null ? a.School.Name : "").ToUpper().Contains(searchQuery)
                            || (a.School != null && a.School.Category != null ? a.School.Category.Code : "").ToUpper().Contains(searchQuery)
                            || (a.School != null && a.School.Category != null ? a.School.Category.Name : "").ToUpper().Contains(searchQuery)
                        );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var recognitionTypePropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<SchoolPaymentsViewDto, SchoolPayments>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            recognitionTypePropertyMappingDictionary);
                    }

                    var mappedResult = dbResult.Select(x => new SchoolPaymentsViewDto()
                    {
                        Id = x.Id,
                        AmountPaid = x.Amount,
                        PaymentReceiptNo = x.ReceiptNo,
                        //PaymentReceiptImage = x.ReceiptImage,
                        DateCreated = x.DateCreated,
                        //Pin
                        PinId = x.PinId,
                        PinSerialNumber = x.Pin != null ? x.Pin.SerialPin : null,
                        //RecognitionType
                        RecognitionTypeName = x.Pin != null && x.Pin.RecognitionType != null ? x.Pin.RecognitionType.Name : null,
                        RecognitionTypeCode = x.Pin != null && x.Pin.RecognitionType != null ? x.Pin.RecognitionType.Code : null,
                        //School
                        SchoolId = x.SchoolId,
                        SchoolName = x.School != null ? x.School.Name : null,
                        //SchoolCategory
                        SchoolCategoryName = x.School != null && x.School.Category != null ? x.School.Category.Name : null,
                        //CreatedBy
                        CreatedBy = x.CreatedBy,
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname} {x.CreatedByNavigation.Othernames}" : null

                    });

                    returnValue = await PagedList<SchoolPaymentsViewDto>.CreateAsync(mappedResult,
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

        public async Task<SchoolPaymentsViewDto> Get(Guid id)
        {

            //Instantiate Return Value
            SchoolPaymentsViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.SchoolPayments
                    .Include(x => x.Pin)
                    .ThenInclude(y => y.RecognitionType)
                    .Include(x => x.School)
                    .ThenInclude(y => y.Category)
                    .Include(x => x.CreatedByNavigation)
                    as IQueryable<SchoolPayments>;

                    var schoolPayments = await dbResult
                        .Where(x => x.Id == id).SingleOrDefaultAsync();

                    returnValue = _mapper.Map<SchoolPaymentsViewDto>(schoolPayments);
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

        public async Task<Guid?> Create(SchoolPaymentsCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            Guid? _schoolId = null;
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    IEnumerable<PinsViewDto> activePinsNOTInUse = await _pinsRepository.ListActivePinsNOTInUseByRecognitionTypeId(_obj.RecognitionTypeId.Value);
                    List<PinsViewDto> listOfActivePinsNOTInUse = activePinsNOTInUse.ToList();
                    //
                    int totalListOfActivePinsNOTInUse = listOfActivePinsNOTInUse.Count();

                    Random random = new Random();
                    int randomNumber = random.Next(-1, totalListOfActivePinsNOTInUse);

                    var pin = listOfActivePinsNOTInUse[randomNumber];


                    SchoolPayments entity = _mapper.Map<SchoolPayments>(_obj);
                    _schoolId = _obj.SchoolId;
                    //
                    #region Consolidate Centre with Existing School otherwise Create School Table row for the Centre

                    if (!String.IsNullOrWhiteSpace(_obj.CentreNo))
                    {
                        CentresViewDto _centre = await _centresRepository.GetByCentreNumber(_obj.CentreNo);
                        if (_centre != null)
                        {
                            string centreName = _centre.CentreName.Trim().ToUpper();
                            SchoolsViewDto _school = await _schoolsRepository.GetBySchoolName(centreName);
                            if (_school != null)
                            {
                                _schoolId = _school.Id;
                            }
                            else
                            {
                                _schoolId = await _schoolsRepository.CreateForCentre(_centre, _obj.OfficeId);
                            }
                        }
                    }

                    #endregion

                    if (_schoolId == null || (_schoolId != null && _schoolId.Value == Guid.Empty))
                    {
                        SchoolsCreateDto schoolObj = _mapper.Map<SchoolsCreateDto>(_obj);

                        _schoolId = await _schoolsRepository.Create(schoolObj);
                    }
                    entity.Id = Guid.NewGuid();
                    entity.PinId = pin.Id;
                    entity.DateCreated = DateTime.Now;
                    entity.SchoolId = _schoolId;

                    await _context.SchoolPayments.AddAsync(entity);
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

        public async Task<SchoolPaymentsViewDto> Update(SchoolPaymentsCreateDto _obj)
        {

            //Instantiate Return Value
            SchoolPaymentsViewDto returnValue = null;
            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<SchoolPayments>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue = _mapper.Map<SchoolPaymentsViewDto>(entity);

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
                    var entity = await _context.SchoolPayments.Where(x => x.Id == id).SingleOrDefaultAsync();
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

        public async Task<bool> Exists(string receiptNo)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(receiptNo))
                {

                    var searchQuery = receiptNo.Trim().ToUpper();
                    var dbResult = await _context.SchoolPayments.AnyAsync(x => x.ReceiptNo.Trim().ToUpper() == searchQuery);
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(receiptNo));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<bool> Exists(Guid id, string receiptNo)
        {
            //Instantiate Return Value
            bool returnValue = false;
            try
            {

                if (!String.IsNullOrWhiteSpace(receiptNo))
                {

                    var searchQuery = receiptNo.Trim().ToUpper();
                    var dbResult = await _context.SchoolPayments.Where(x => x.Id != id).AnyAsync(x => x.ReceiptNo.Trim().ToUpper() == searchQuery);
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

        public async Task<SchoolPaymentsCreationDependecyDto> GetCreationDependencys()
        {
            //Instantiate Return Value
            SchoolPaymentsCreationDependecyDto returnValue = new SchoolPaymentsCreationDependecyDto();
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
                        LocalGovernmentId  = x.LocalGovernmentId != null ? x.LocalGovernmentId.Value : Guid.Empty,
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
