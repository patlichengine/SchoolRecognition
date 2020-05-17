using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Classes;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cPinsRepository : IPinsRepository, IDisposable
    {

        private const string WAECCODEPREFIX = "WC";
        //private const string CHARS = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

   
        public cPinsRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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

        private async Task<string> GenerateSerialPinAsync(Guid recognitionTypeId)
        {
            try
            {


                //
                string strGeneratedSerialPin = null;
                //The strings below will combine with the TotalNumberOfPins to give the final generated pin
                string strRecogntionTypeCode = null;
                string strAlphanumericSecurityCode = null;
                string strYearCode = null;
                //
                int intTotalNumberOfPins = 0;


                if (recognitionTypeId != Guid.Empty)
                {

                    //Get RecognitionType
                    var _recognitionType = await _context.RecognitionTypes.Include(x => x.Pins).Where(x => x.Id == recognitionTypeId).SingleOrDefaultAsync();
                    //Get List of Pins of the given RecognitionType currently in the DB

                    if (_recognitionType != null)
                    {
                        //Resolve RecognitionTypes Code
                        strRecogntionTypeCode = _recognitionType.Code;
                        //Resolve Current Total Number of Pins in DB
                        intTotalNumberOfPins = _recognitionType.Pins.ToList().Count;
                        //Increment TotalNumber of PINs
                        intTotalNumberOfPins++;
                        //Generate Random 3 character Alphanumeric suffix
                        var guid = Guid.NewGuid();
                        strAlphanumericSecurityCode = guid.ToString().Substring(0, 3);
                        //Generate code for year of pin was generated
                        string currentYear = DateTime.Now.Year.ToString();
                        strYearCode = currentYear.Remove(0, 1); /*Remove the first digit of the year*/
                        //Combine components of the GeneratedSerialPin
                        strGeneratedSerialPin = String.Format("{0}{1}{2}{3,5:00000}{4}",
                            WAECCODEPREFIX, strRecogntionTypeCode, strYearCode, intTotalNumberOfPins, strAlphanumericSecurityCode);

                    }
                }

                return strGeneratedSerialPin.ToUpper();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private async Task<List<string>> GenerateMultipleSerialPinsAsync(Guid recognitionTypeId, int numberOfPins)
        {
            try
            {
                //
                List<string> strGeneratedSerialPins = new List<string>();
                //The strings below will combine with the TotalNumberOfPins to give the final generated pin
                string strRecogntionTypeCode = null;
                string strAlphanumericSecurityCode = null;
                string strYearCode = null;
                //
                int intTotalNumberOfPins = 0;


                if (recognitionTypeId != Guid.Empty)
                {
                    //Get RecognitionType
                    var _recognitionType = await _context.RecognitionTypes.Include(x => x.Pins).Where(x => x.Id == recognitionTypeId).SingleOrDefaultAsync();
                    //Get List of Pins of the given RecognitionType currently in the DB

                    if (_recognitionType != null)
                    {

                        //Resolve RecognitionTypes Code
                        strRecogntionTypeCode = _recognitionType.Code;
                        //Resolve Current Total Number of Pins in DB
                        intTotalNumberOfPins = _recognitionType.Pins.ToList().Count;

                        for (int i = 0; i < numberOfPins; i++)
                        {
                            //Resolve RecognitionTypes Code
                            strRecogntionTypeCode = _recognitionType.Code;
                            //Resolve Current Total Number of Pins in DB
                            intTotalNumberOfPins = _recognitionType.Pins.ToList().Count;
                            //Increment TotalNumber of PINs
                            intTotalNumberOfPins++;
                            //Generate Random 3 character Alphanumeric suffix
                            var guid = Guid.NewGuid();
                            strAlphanumericSecurityCode = guid.ToString().Substring(0, 3);
                            //Generate code for year of pin was generated
                            string currentYear = DateTime.Now.Year.ToString();
                            strYearCode = currentYear.Remove(0, 1); /*Remove the first digit of the year*/
                            //Combine components of the GeneratedSerialPin
                            string _strGeneratedSerialPin = String.Format("{0}{1}{2}{3,5:00000}{4}",
                                WAECCODEPREFIX, strRecogntionTypeCode, strYearCode, intTotalNumberOfPins, strAlphanumericSecurityCode);

                            //Add to list of strings
                            strGeneratedSerialPins.Add(_strGeneratedSerialPin.ToUpper());
                        }

                    }
                }

                return strGeneratedSerialPins;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<IEnumerable<PinsViewDto>> GetAllPinsAsync()
        {

            //Instantiate Return Value
            IEnumerable<PinsViewDto> returnValue = new List<PinsViewDto>();
            try
            {
                var dbResult = _context.Pins.Include(x => x.RecognitionType)
                        .Include(x => x.CreatedByNavigation)
                        .Include(x => x.SchoolPayments)
                        .ThenInclude(y => y.School)
                        .ThenInclude(z => z.Category) as IQueryable<Pins>;

                returnValue = await dbResult.Select(x => new PinsViewDto()
                {
                    Id = x.Id,
                    RecognitionTypeName = x.RecognitionType != null ? x.RecognitionType.Name : null,
                    SerialNumber = x.SerialPin,
                    IsActive = x.IsActive,
                    IsInUse = x.IsInUse,
                    CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                    DateCreated = x.DateCreated,
                    Payments = x.SchoolPayments.Select(x => new SchoolPaymentsViewDto()
                    {
                        Id = x.Id,
                        AmountPaid = x.Amount,
                        PaymentReceiptNo = x.ReceiptNo,
                        DateCreated = x.DateCreated,
                        PaymentReceiptImage = x.ReceiptImage,
                        //CreatedByNavigation
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                        SchoolName = x.School != null ? x.School.Name : null,
                        SchoolCategoryName = x.School != null && x.School.Category != null ? x.School.Name : null,
                        PinSerialNumber = x.Pin != null ? x.Pin.SerialPin : null

                    })
                }).ToListAsync();

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<CustomPagedList<PinsViewDto>> GetAllPinsAsPagedListAsync(PinsResourceParams resourceParams)
        {
            //Instantiate Return Value
            CustomPagedList<PinsViewDto> returnValue = CustomPagedList<PinsViewDto>
                        .Create(Enumerable.Empty<PinsViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context.Pins.Include(x => x.RecognitionType)
                        .Include(x => x.CreatedByNavigation)
                        .Include(x => x.SchoolPayments)
                        .ThenInclude(y => y.School)
                        .ThenInclude(z => z.Category) as IQueryable<Pins>;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        dbResult = dbResult.Where(a => a.SerialPin.ToUpper().Contains(searchQuery)
                            || (a.DateCreated != null ? a.DateCreated : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.CreatedByNavigation != null ? a.CreatedByNavigation.Surname : null).ToUpper().Contains(searchQuery)
                            || (a.CreatedByNavigation != null ? a.CreatedByNavigation.Othernames : null).ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var pinPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<PinsViewDto, Pins>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            pinPropertyMappingDictionary);
                    }

                    var mappedResult = dbResult.Select(x => new PinsViewDto()
                    {
                        Id = x.Id,
                        RecognitionTypeName = x.RecognitionType != null ? x.RecognitionType.Name : null,
                        SerialNumber = x.SerialPin,
                        IsActive = x.IsActive,
                        IsInUse = x.IsInUse,
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                        DateCreated = x.DateCreated,
                        Payments = x.SchoolPayments.Select(x => new SchoolPaymentsViewDto()
                        {
                            Id = x.Id,
                            AmountPaid = x.Amount,
                            PaymentReceiptNo = x.ReceiptNo,
                            DateCreated = x.DateCreated,
                            PaymentReceiptImage = x.ReceiptImage,
                            //CreatedByNavigation
                            CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                            SchoolName = x.School != null ? x.School.Name : null,
                            SchoolCategoryName = x.School != null && x.School.Category != null ? x.School.Name : null,
                            PinSerialNumber = x.Pin != null ? x.Pin.SerialPin : null

                        })
                    });

                    returnValue = await CustomPagedList<PinsViewDto>.CreateAsync(mappedResult,
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


        public async Task<PinsViewDto> GetPinsSingleOrDefaultAsync(Guid id)
        {

            //Instantiate Return Value
            PinsViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Pins
                        .Include(x => x.RecognitionType)
                        .Include(x => x.CreatedByNavigation)
                        .Include(x => x.SchoolPayments)
                        .ThenInclude(y => y.School)
                        .ThenInclude(z => z.Category)
                        .Where(x => x.Id == id) as IQueryable<Pins>;


                    returnValue = await dbResult.Select(x => new PinsViewDto()
                    {
                        Id = x.Id,
                        RecognitionTypeName = x.RecognitionType != null ? x.RecognitionType.Name : null,
                        SerialNumber = x.SerialPin,
                        IsActive = x.IsActive,
                        IsInUse = x.IsInUse,
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                        DateCreated = x.DateCreated,
                        Payments = x.SchoolPayments.Select(x => new SchoolPaymentsViewDto()
                        {
                            Id = x.Id,
                            AmountPaid = x.Amount,
                            PaymentReceiptNo = x.ReceiptNo,
                            DateCreated = x.DateCreated,
                            PaymentReceiptImage = x.ReceiptImage,
                            //CreatedByNavigation
                            CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                            SchoolName = x.School != null ? x.School.Name : null,
                            SchoolCategoryName = x.School != null && x.School.Category != null ? x.School.Name : null,
                            PinSerialNumber = x.Pin != null ? x.Pin.SerialPin : null

                        })

                    }).SingleOrDefaultAsync();


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


        public async Task<PinsViewDto> GetPinsAllPinHistoriesAsync(Guid id)
        {
            //Instantiate Return Value
            PinsViewDto returnValue = null;
            IEnumerable<PinHistoriesViewDto> returnValuePinHistorys = new List<PinHistoriesViewDto>();
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Pins.Include(x => x.RecognitionType)
                        .Include(x => x.CreatedByNavigation)
                        .Include(x => x.SchoolPayments)
                        .ThenInclude(y => y.School)
                        .ThenInclude(z => z.Category)
                        .Include(x => x.PinHistories)
                        .Where(x => x.Id == id) as IQueryable<Pins>;

                    returnValue = await dbResult.Select(x => new PinsViewDto()
                    {
                        Id = x.Id,
                        RecognitionTypeName = x.RecognitionType != null ? x.RecognitionType.Name : null,
                        SerialNumber = x.SerialPin,
                        IsActive = x.IsActive,
                        IsInUse = x.IsInUse,
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                        DateCreated = x.DateCreated,
                        Payments = x.SchoolPayments.Select(x => new SchoolPaymentsViewDto()
                        {
                            Id = x.Id,
                            AmountPaid = x.Amount,
                            PaymentReceiptNo = x.ReceiptNo,
                            DateCreated = x.DateCreated,
                            PaymentReceiptImage = x.ReceiptImage,
                            //CreatedByNavigation
                            CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                            SchoolName = x.School != null ? x.School.Name : null,
                            SchoolCategoryName = x.School != null && x.School.Category != null ? x.School.Name : null,
                            PinSerialNumber = x.Pin != null ? x.Pin.SerialPin : null

                        })
                    }).SingleOrDefaultAsync();

                    var pinHistorys = await dbResult.SelectMany(x => x.PinHistories).ToListAsync();
                    returnValuePinHistorys = _mapper.Map<IEnumerable<PinHistoriesViewDto>>(pinHistorys);

                    returnValue.Histories = returnValuePinHistorys;


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

        public async Task<PinsViewDto> GetPinsAllSchoolPaymentsAsync(Guid id)
        {
            //Instantiate Return Value
            PinsViewDto returnValue = null;
            IEnumerable<SchoolPaymentsViewDto> returnValueSchoolPayments = new List<SchoolPaymentsViewDto>();
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Pins
                        .Include(x => x.RecognitionType)
                        .Include(x => x.CreatedByNavigation)
                        .Include(x => x.SchoolPayments)
                        .ThenInclude(y => y.School)
                        .ThenInclude(z => z.Category).Where(x => x.Id == id) as IQueryable<Pins>;


                    returnValue = await dbResult.Select(x => new PinsViewDto()
                    {
                        Id = x.Id,
                        RecognitionTypeName = x.RecognitionType != null ? x.RecognitionType.Name : null,
                        SerialNumber = x.SerialPin,
                        IsActive = x.IsActive,
                        IsInUse = x.IsInUse,
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                        DateCreated = x.DateCreated,
                        Payments = x.SchoolPayments.Select(x => new SchoolPaymentsViewDto()
                        {
                            Id = x.Id,
                            AmountPaid = x.Amount,
                            PaymentReceiptNo = x.ReceiptNo,
                            DateCreated = x.DateCreated,
                            PaymentReceiptImage = x.ReceiptImage,
                            //CreatedByNavigation
                            CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                            SchoolName = x.School != null ? x.School.Name : null,
                            SchoolCategoryName = x.School != null && x.School.Category != null ? x.School.Name : null,
                            PinSerialNumber = x.Pin != null ? x.Pin.SerialPin : null

                        })
                    }).SingleOrDefaultAsync();



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


        public async Task<PinsViewPagedListPinHistoriesDto> GetPinsPinHistoriesAsPagedListAsync(Guid id, PinHistoriesResourceParams resourceParams)
        {

            //Instantiate Return Value
            PinsViewPagedListPinHistoriesDto returnValue = null;

            //Instantiate Return Value
            CustomPagedList<PinHistoriesViewDto> returnValuePins = CustomPagedList<PinHistoriesViewDto>
                        .Create(Enumerable.Empty<PinHistoriesViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.Pins.Include(x => x.RecognitionType)
                        .Include(x => x.CreatedByNavigation)
                        .Include(x => x.SchoolPayments)
                        .ThenInclude(y => y.School)
                        .ThenInclude(z => z.Category)
                        .Include(x => x.PinHistories).Where(x => x.Id == id) as IQueryable<Pins>;

                    //Pins pin = await dbResult.SingleOrDefaultAsync();
                    //
                    var queryablePins = dbResult.SelectMany(x => x.PinHistories) as IQueryable<PinHistories>;



                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        queryablePins = queryablePins.Where(a =>
                            (a.DateActive != null ? a.DateActive : null).ToString().ToUpper().Contains(searchQuery)
                            || (a.Pin != null ? a.Pin.SerialPin : null).ToUpper().Contains(searchQuery)
                            || (a.School != null ? a.School.Name : null).ToUpper().Contains(searchQuery)
                            || (a.School != null && a.School.Category != null ? a.School.Category.Name : null).ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var pinsPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<PinHistoriesViewDto, PinHistories>();

                        queryablePins = queryablePins.ApplySort(resourceParams.OrderBy,
                            pinsPropertyMappingDictionary);
                    }
                    ///Use LINQ to map pins to pinsviewdto
                    var mappedResult = queryablePins.Select(x => new PinHistoriesViewDto()
                    {
                        Id = x.Id,
                        DateActive = x.DateActive,
                        SchoolName = x.School != null ? x.School.Name : null,
                        SchoolCategoryName = x.School != null && x.School.Category != null ? x.School.Name : null,
                        PinSerialNumber = x.Pin != null ? x.Pin.SerialPin : null

                    });

                    returnValuePins = await CustomPagedList<PinHistoriesViewDto>.CreateAsync(mappedResult,
                        resourceParams.PageNumber,
                        resourceParams.PageSize);


                    returnValue = await dbResult.Select(x => new PinsViewPagedListPinHistoriesDto()
                    {
                        Id = x.Id,
                        RecognitionTypeName = x.RecognitionType != null ? x.RecognitionType.Name : null,
                        SerialNumber = x.SerialPin,
                        IsActive = x.IsActive,
                        IsInUse = x.IsInUse,
                        CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                        DateCreated = x.DateCreated,
                        Payments = x.SchoolPayments.Select(x => new SchoolPaymentsViewDto()
                        {
                            Id = x.Id,
                            AmountPaid = x.Amount,
                            PaymentReceiptNo = x.ReceiptNo,
                            DateCreated = x.DateCreated,
                            PaymentReceiptImage = x.ReceiptImage,
                            //CreatedByNavigation
                            CreatedByUser = x.CreatedByNavigation != null ? $"{x.CreatedByNavigation.Surname}, {x.CreatedByNavigation.Othernames}" : null,
                            SchoolName = x.School != null ? x.School.Name : null,
                            SchoolCategoryName = x.School != null && x.School.Category != null ? x.School.Name : null,
                            PinSerialNumber = x.Pin != null ? x.Pin.SerialPin : null

                        })
                    }).SingleOrDefaultAsync();
                    //
                    returnValue.Histories = returnValuePins;


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
        

        public async Task<Guid?> CreatePinsAsync(PinsCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null)
                {
                    //Variable to store the SerialPin to be generated
                    string serialPin = null;

                    if (_obj.RecognitionTypeId != Guid.Empty)
                    {
                        //Generate a SerialPin
                        serialPin = await GenerateSerialPinAsync(_obj.RecognitionTypeId);
                    }
                    //Create a PINs Object to be stored in the db
                    var entity = new Pins()
                    {
                        Id = Guid.NewGuid(),
                        RecognitionTypeId = _obj.RecognitionTypeId,
                        IsActive = _obj.IsActive,
                        CreatedBy = _obj.CreatedBy,
                        DateCreated = DateTime.Now
                    };

                    returnValue = entity.Id;

                    await _context.Pins.AddAsync(entity);
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

        public async Task<bool> CreateMultiplePinsAsync(PinsCreateDto _obj)
        {

            try
            {
                List<string> strGeneratedSerialPins = new List<string>();

                List<Pins> listPinsEntities = new List<Pins>();

                int numberOfPinsToGenerate = _obj.NoOfPinToGenerate;

                Guid? returnId = Guid.Empty;
                if (_obj != null)
                {
                    //Generate a List of Custom SerialPin
                    if (_obj.RecognitionTypeId != null && _obj.RecognitionTypeId != Guid.Empty)
                    {
                        strGeneratedSerialPins = await GenerateMultipleSerialPinsAsync(_obj.RecognitionTypeId, numberOfPinsToGenerate);
                    }

                    //Assign Custom SerialPins to Pin objects
                    for (int i = 0; i < numberOfPinsToGenerate; i++)
                    {
                        //Get string at position "i" in the string array
                        string strGeneratedSerialPin = strGeneratedSerialPins[i];

                        Pins pinsEntity = new Pins()
                        {
                            //Id = Guid.NewGuid(),
                            //
                            RecognitionTypeId = _obj.RecognitionTypeId,
                            SerialPin = strGeneratedSerialPin,
                            IsActive = _obj.IsActive,
                            //IsInUse = _obj.IsInUse,
                            CreatedBy = _obj.CreatedBy,
                            DateCreated = DateTime.Now,
                        };

                        listPinsEntities.Add(pinsEntity);
                    }

                    await _context.Pins.AddRangeAsync(listPinsEntities);
                    await this.Save();

                    return true;

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

        public async Task<PinsViewDto> UpdatePinAsync(PinsUpdateDto _obj)
        {

            //Instantiate Return Value
            PinsViewDto returnValue = null;
            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<Pins>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue = _mapper.Map<PinsViewDto>(entity);

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

        public async Task DeletePinAsync(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var entity = await _context.Pins.Where(x => x.Id == id).SingleOrDefaultAsync();
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

        /// <summary>
        /// The method "CheckNumberOfActivePinsNOTInUseAsync" checks the number of pins that are Active but NOT in use
        /// i.e. such pins can be used to make SchoolPayments. If less than 0 (zero)
        /// the are no pins avaliable for SchoolPayments
        /// </summary>
        /// <returns></returns>
        public async Task<int> CheckNumberOfActivePinsNOTInUseAsync()
        {
            //Instantiate Return Value
            int returnValue = 0;
            try
            {
                var dbResult = await _context.Pins.Where(x => x.IsActive == true && x.IsInUse == false).CountAsync();


                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PinsStatisticsSummaryDto> GetPinsStatisticSummaryAsync()
        {
            //Instantiate Return Value
            PinsStatisticsSummaryDto returnValue = new PinsStatisticsSummaryDto();
            try
            {
                var dbResult = _context.Pins as IQueryable<Pins>;

                returnValue.PinsCount = await dbResult.CountAsync();
                returnValue.IsActivePinsCount = await dbResult.Where(x => x.IsActive == true).CountAsync();
                returnValue.IsInUsePinsCount = await dbResult.Where(x => x.IsInUse == true).CountAsync();

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
