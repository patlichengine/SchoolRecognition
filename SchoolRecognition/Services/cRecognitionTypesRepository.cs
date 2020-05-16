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

    public class cRecognitionTypesRepository : IRecognitionTypesRepository, IDisposable
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

        public cRecognitionTypesRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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



        public async Task<IEnumerable<RecognitionTypesViewDto>> GetAllRecognitionTypesAsync()
        {
            //Instantiate Return Value
            IEnumerable<RecognitionTypesViewDto> returnValue = new List<RecognitionTypesViewDto>();
            try
            {
                var dbResult = await _context.RecognitionTypes.Include(x => x.Pins)
                    .Select(x => new RecognitionTypesViewDto()
                    {
                        Id = x.Id,
                        RecognitionTypeCode = x.Code,
                        RecognitionTypeName = x.Name,
                        PinsCount = x.Pins != null ? x.Pins.Count() : 0,
                        IsActivePinsCount = x.Pins != null ? x.Pins.Where(x => x.IsActive == true).Count() : 0,
                        IsInUsePinsCount = x.Pins != null ? x.Pins.Where(x => x.IsInUse == true).Count() : 0,
                    }).ToListAsync();

                returnValue = dbResult;

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<CustomPagedList<RecognitionTypesViewDto>> GetAllRecognitionTypesAsPagedListAsync(RecognitionTypesResourceParams resourceParams)
        {
            //Instantiate Return Value
            CustomPagedList<RecognitionTypesViewDto> returnValue = CustomPagedList<RecognitionTypesViewDto>
                        .Create(Enumerable.Empty<RecognitionTypesViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);

            try
            {
                if (resourceParams != null)
                {

                    var dbResult = _context.RecognitionTypes as IQueryable<RecognitionTypes>;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        dbResult = dbResult.Where(a => a.Code.ToUpper().Contains(searchQuery)
                            || a.Name.ToUpper().Contains(searchQuery));
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var recognitionTypePropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<RecognitionTypesViewDto, RecognitionTypes>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            recognitionTypePropertyMappingDictionary);
                    }

                    var mappedResult = dbResult.Select(x => new RecognitionTypesViewDto()
                    {
                        Id = x.Id,
                        RecognitionTypeCode = x.Code,
                        RecognitionTypeName = x.Name,
                        PinsCount = x.Pins.Count(),

                    });

                    returnValue = await CustomPagedList<RecognitionTypesViewDto>.CreateAsync(mappedResult,
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

        public async Task<RecognitionTypesViewDto> GetRecognitionTypesSingleOrDefaultAsync(Guid id)
        {

            //Instantiate Return Value
            RecognitionTypesViewDto returnValue = null;
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.RecognitionTypes as IQueryable<RecognitionTypes>;

                    var recognitionTypes = await dbResult.Where(x => x.Id == id).SingleOrDefaultAsync();
                    returnValue = _mapper.Map<RecognitionTypesViewDto>(recognitionTypes);
                    //
                    returnValue.PinsCount = await dbResult.Include(x => x.Pins).Where(x => x.Id == id).SelectMany(x => x.Pins).CountAsync();
                    returnValue.IsActivePinsCount = await dbResult.Include(x => x.Pins).Where(x => x.Id == id).SelectMany(x => x.Pins).Where(x => x.IsActive == true).CountAsync();
                    returnValue.IsInUsePinsCount = await dbResult.Include(x => x.Pins).Where(x => x.Id == id).SelectMany(x => x.Pins).Where(x => x.IsInUse == true).CountAsync();


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

        public async Task<RecognitionTypesViewDto> GetRecognitionTypesAllPinsAsync(Guid id)
        {


            //Instantiate Return Value
            RecognitionTypesViewDto returnValue = null;
            IEnumerable<PinsViewDto> returnValuePins = new List<PinsViewDto>();
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.RecognitionTypes
                        .Include(x => x.Pins)
                        .ThenInclude(x => x.CreatedByNavigation)
                        .Include(x => x.Pins)
                        .ThenInclude(x => x.SchoolPayments)
                        .ThenInclude(y => y.School)
                        .ThenInclude(z => z.Category)
                        .Where(x => x.Id == id) as IQueryable<RecognitionTypes>;


                    returnValue = _mapper.Map<RecognitionTypesViewDto>(await dbResult.SingleOrDefaultAsync());

                    returnValuePins = await dbResult.SelectMany(x => x.Pins).Select(x => new PinsViewDto()
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


                    returnValue.RecognitionTypePins = returnValuePins;

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

        public async Task<RecognitionTypeViewPagedListPinsDto> GetRecognitionTypesPinsAsPagedListAsync(Guid id, PinsResourceParams resourceParams)
        {



            //Instantiate Return Value
            RecognitionTypeViewPagedListPinsDto returnValue = null;

            //Instantiate Return Value
            CustomPagedList<PinsViewDto> returnValuePins = CustomPagedList<PinsViewDto>
                        .Create(Enumerable.Empty<PinsViewDto>().AsQueryable(),
                            resourceParams.PageNumber,
                            resourceParams.PageSize);
            try
            {
                if (id != Guid.Empty)
                {
                    var dbResult = _context.RecognitionTypes
                        .Include(x => x.Pins)
                        .ThenInclude(x => x.CreatedByNavigation)
                        .Include(x => x.Pins)
                        .ThenInclude(x => x.SchoolPayments)
                        .ThenInclude(y => y.School)
                        .ThenInclude(z => z.Category)
                        .Where(x => x.Id == id) as IQueryable<RecognitionTypes>;

                    RecognitionTypes recognitionType = await dbResult.FirstOrDefaultAsync();
                    //
                    var queryablePins = dbResult.SelectMany(x => x.Pins) as IQueryable<Pins>;



                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        var searchQuery = resourceParams.SearchQuery.Trim().ToUpper();

                        queryablePins = queryablePins.Where(a => a.SerialPin.ToUpper().Contains(searchQuery)
                            || (a.DateCreated != null ? a.DateCreated: null).ToString().ToUpper().Contains(searchQuery)
                            || (a.CreatedByNavigation != null ? a.CreatedByNavigation.Surname : null).ToUpper().Contains(searchQuery)
                            || (a.CreatedByNavigation != null ? a.CreatedByNavigation.Othernames: null).ToUpper().Contains(searchQuery)
                            );
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var pinsPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<PinsViewDto, Pins>();

                        queryablePins = queryablePins.ApplySort(resourceParams.OrderBy,
                            pinsPropertyMappingDictionary);
                    }
                    ///Use LINQ to map pins to pinsviewdto
                    var mappedResult = queryablePins.Select(x => new PinsViewDto()
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

                    returnValuePins = await CustomPagedList<PinsViewDto>.CreateAsync(mappedResult,
                        resourceParams.PageNumber,
                        resourceParams.PageSize);


                    returnValue = _mapper.Map<RecognitionTypeViewPagedListPinsDto>(recognitionType);
                    //
                    returnValue.PinsCount = await queryablePins.CountAsync();
                    returnValue.IsActivePinsCount = await queryablePins.Where(x => x.IsActive == true).CountAsync();
                    returnValue.IsInUsePinsCount = await queryablePins.Where(x => x.IsInUse == true).CountAsync();
                    //
                    returnValue.RecognitionTypePins = returnValuePins;


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

        public async Task<Guid?> CreateRecognitionTypeAsync(RecognitionTypesCreateDto _obj)
        {
            //Instantiate Return Value
            Guid? returnValue = null;
            try
            {
                if (_obj != null && _obj.Id == Guid.Empty)
                {
                    RecognitionTypes entity = _mapper.Map<RecognitionTypes>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    await _context.RecognitionTypes.AddAsync(entity);
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

        public async Task<RecognitionTypesViewDto> UpdateRecognitionTypeAsync(RecognitionTypesCreateDto _obj)
        {

            //Instantiate Return Value
            RecognitionTypesViewDto returnValue = null;
            try
            {
                if (_obj != null && _obj.Id != Guid.Empty)
                {
                    var entity = _mapper.Map<RecognitionTypes>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue =_mapper.Map<RecognitionTypesViewDto>(entity);

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

        public async Task DeleteRecognitionTypeAsync(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var entity = await _context.RecognitionTypes.Where(x => x.Id == id).SingleOrDefaultAsync();
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
        public async Task<bool> CheckIfRecognitionTypeExists(string name, string code)
        {
            //Instantiate Return Value
            bool returnValue = true;
            try
            {

                if (!String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(code))
                {

                    name = name.Trim().ToUpper();
                    code = code.Trim().ToUpper();
                    var dbResult = await _context.RecognitionTypes.AnyAsync(x => x.Name.Trim().ToUpper() == name
                    || x.Code.Trim().ToUpper() == code
                    );
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(name));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CheckIfRecognitionTypeExists(Guid id, string name, string code)
        {
            //Instantiate Return Value
            bool returnValue = true;
            try
            {

                if (id != Guid.Empty && !String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(code))
                {

                    name = name.Trim().ToUpper();
                    code = code.Trim().ToUpper();
                    var dbResult = await _context.RecognitionTypes.Where(x => x.Id != id).AnyAsync(x => x.Name.Trim().ToUpper() == name
                    || x.Code.Trim().ToUpper() == code
                    );
                    returnValue = dbResult;

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(name));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
