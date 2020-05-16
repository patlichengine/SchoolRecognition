using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Models;
using SchoolRecognition.Services;
using Vereyon.Web;

namespace SchoolRecognition.Controllers
{

    [Route("application_settings")]
    public class ApplicationSettingsController : Controller
    {
        private IFlashMessage _flashMessage;
        private IApplicationSettingsRepository _applicationSettingsRepository;
        private readonly IMapper _mapper;


        public ApplicationSettingsController(IFlashMessage flashMessage, IApplicationSettingsRepository applicationSettingsRepository, IMapper mapper)
        {
            _flashMessage = flashMessage ??
                throw new ArgumentNullException(nameof(mapper));
            _applicationSettingsRepository = applicationSettingsRepository ??
               throw new ArgumentNullException(nameof(applicationSettingsRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        [Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index()
        {
            try
            {

                var applicationSetting = await _applicationSettingsRepository.GetApplicationSettingsSingleOrDefaultAsync();

                return View(applicationSetting);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }




        [Route("change_settings")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Update()
        {
            try
            {
                var applicationSetting = await _applicationSettingsRepository.GetApplicationSettingsSingleOrDefaultAsync();
                var model = _mapper.Map<ApplicationSettingsCreateDto>(applicationSetting);
                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("change_settings")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ApplicationSettingsCreateDto model)
        {

            try
            {
                if (ModelState.IsValid)
                {


                    //Set Pins as active 
                    var result = await _applicationSettingsRepository.UpdateApplicationSettingAsync(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "Application Settings Updated Successfully!");
                        return RedirectToAction("Index", "ApplicationSettings");
                    }
                    else
                    {
                        _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                        return View(model);
                    }
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}