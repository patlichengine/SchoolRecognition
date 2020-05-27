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

    public class cApplicationSettingsRepository : IApplicationSettingsRepository, IDisposable
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

        public cApplicationSettingsRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
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
        public async Task<ApplicationSettingsViewDto> Get()
        {

            //Instantiate Return Value
            ApplicationSettingsViewDto returnValue = null;
            try
            {
                var dbResult = await _context.ApplicationSettings.SingleOrDefaultAsync();

                returnValue = _mapper.Map<ApplicationSettingsViewDto>(dbResult);
                //


                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<ApplicationSettingsViewDto> Create(ApplicationSettingsCreateDto _obj)
        {

            //Instantiate Return Value
            ApplicationSettingsViewDto returnValue = null;
            try
            {
                if (_obj != null)
                {
                    ApplicationSettings entity = _mapper.Map<ApplicationSettings>(_obj);
                    //
                    entity.Id = Guid.NewGuid();
                    await _context.ApplicationSettings.AddAsync(entity);
                    await this.Save();

                    returnValue = _mapper.Map<ApplicationSettingsViewDto>(entity);

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

        public async Task<ApplicationSettingsViewDto> Update(ApplicationSettingsCreateDto _obj)
        {

            //Instantiate Return Value
            ApplicationSettingsViewDto returnValue = null;
            try
            {
                var hasExistingApplicationSetting = await _context.ApplicationSettings.AnyAsync();
                if (hasExistingApplicationSetting == false)
                {
                    returnValue = await this.Create(_obj);

                    return returnValue;

                }
                else if (_obj != null && _obj.Id != Guid.Empty)
                {
                    ApplicationSettings entity = _mapper.Map<ApplicationSettings>(_obj);

                    _context.Update(entity);
                    await this.Save();

                    returnValue = _mapper.Map<ApplicationSettingsViewDto>(entity);

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



    }
}
