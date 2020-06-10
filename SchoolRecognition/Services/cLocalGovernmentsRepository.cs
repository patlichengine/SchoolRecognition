using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{

    public class cLocalGovernmentsRepository : ILocalGovernmentsRepository, IDisposable
    {

        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;


        public cLocalGovernmentsRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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


        public async Task<IEnumerable<LocalGovernmentsViewDto>> List()
        {

            //Instantiate Return Value
            IEnumerable<LocalGovernmentsViewDto> returnValue = new List<LocalGovernmentsViewDto>();
            try
            {
                var dbResult = _context
                    .LocalGovernments
                    .Include(x => x.State)
                    .Include(y => y.Schools) as IQueryable<LocalGovernments>;

                returnValue = await dbResult.Select(x => new LocalGovernmentsViewDto()
                {
                    Id = x.Id,
                    LgaName = x.Name,
                    LgaCode = x.Code,
                    SchoolsCount = x.Schools != null ? x.Schools.Count() : 0
                }).ToListAsync();

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<LocalGovernmentsViewDto>> ListByStateId(Guid stateId)
        {


            //Instantiate Return Value
            IEnumerable<LocalGovernmentsViewDto> returnValue = new List<LocalGovernmentsViewDto>();
            try
            {
                if (stateId != Guid.Empty)
                {
                    var dbResult = _context
                   .LocalGovernments
                   .Include(x => x.State)
                   .Include(y => y.Schools)
                   .Where(x => x.StateId == stateId) as IQueryable<LocalGovernments>;

                    returnValue = await dbResult.Select(x => new LocalGovernmentsViewDto()
                    {
                        Id = x.Id,
                        LgaName = x.Name,
                        LgaCode = x.Code,
                        SchoolsCount = x.Schools != null ? x.Schools.Count() : 0
                    }).ToListAsync();

                }
                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<LocalGovernmentsViewDto> GetByStateIdAndLgaCode(Guid stateId, string code)
        {


            //Instantiate Return Value
            LocalGovernmentsViewDto returnValue = new LocalGovernmentsViewDto();
            try
            {
                if (stateId != Guid.Empty && !String.IsNullOrWhiteSpace(code))
                {
                    string _code = code.Trim().ToUpper();
                    var dbResult = _context
                   .LocalGovernments
                   .Include(x => x.State)
                   .Include(y => y.Schools)
                   .Where(x => x.StateId == stateId && x.Code.Trim().ToUpper() == _code) as IQueryable<LocalGovernments>;

                    returnValue = await dbResult.Select(x => new LocalGovernmentsViewDto()
                    {
                        Id = x.Id,
                        LgaName = x.Name,
                        LgaCode = x.Code,
                        SchoolsCount = x.Schools != null ? x.Schools.Count() : 0
                    }).SingleOrDefaultAsync();

                }
                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<LocalGovernmentsViewDto> GetByStateCodeAndLgaCode(string stateCode, string lgaCode)
        {


            //Instantiate Return Value
            LocalGovernmentsViewDto returnValue = new LocalGovernmentsViewDto();
            try
            {
                if (!String.IsNullOrWhiteSpace(stateCode) && !String.IsNullOrWhiteSpace(lgaCode))
                {
                    string _lgaCode = lgaCode.Trim().ToUpper();
                    string _stateCode = stateCode.Trim().ToUpper();
                    var dbResult = _context
                   .LocalGovernments
                   .Include(x => x.State)
                   .Include(y => y.Schools)
                   .Where(
                        x => x.Code.Trim().ToUpper() == _lgaCode
                        && (x.State != null ? x.State.Code : null) == _stateCode
                        ) as IQueryable<LocalGovernments>;

                    returnValue = await dbResult.Select(x => new LocalGovernmentsViewDto()
                    {
                        Id = x.Id,
                        LgaName = x.Name,
                        LgaCode = x.Code,
                        SchoolsCount = x.Schools != null ? x.Schools.Count() : 0
                    }).SingleOrDefaultAsync();

                }
                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
