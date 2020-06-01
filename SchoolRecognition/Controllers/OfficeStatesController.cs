using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;
using Vereyon.Web;


namespace SchoolRecognition.Controllers
{

    [Route("manage_offices/states_controlled")]
    public class OfficeStatesController : Controller
    {

        private IFlashMessage _flashMessage;
        private IOfficesRepository _officesRepository;
        private IOfficeStatesRepository _officeStatesRepository;
        private IStatesRepository _statesRepository;
        private readonly IMapper _mapper;

        public OfficeStatesController(IFlashMessage flashMessage, IOfficesRepository officesRepository, IOfficeStatesRepository officeStatesRepository, IStatesRepository statesRepository, IMapper mapper)
        {
            _flashMessage = flashMessage ??
                throw new ArgumentNullException(nameof(mapper));
            _officesRepository = officesRepository ??
               throw new ArgumentNullException(nameof(officesRepository));
            _officeStatesRepository = officeStatesRepository ??
               throw new ArgumentNullException(nameof(officeStatesRepository));
            _statesRepository = statesRepository ??
               throw new ArgumentNullException(nameof(statesRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }



        [Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index(string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {



                IEnumerable<int> pages = new List<int>();

                var resourceParams = new OfficeStatesResourceParams()
                {
                    PageNumber = pageNumber != null ? pageNumber.Value : 1,
                    SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                    OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "Id",
                };
                //Instantiate PagedList
                PagedList<OfficeStatesViewDto> pins = PagedList<OfficeStatesViewDto>
                         .Create(Enumerable.Empty<OfficeStatesViewDto>().AsQueryable(),
                             resourceParams.PageNumber,
                             resourceParams.PageSize);
                switch (orderBy)
                {
                    case "id_desc":
                        resourceParams.OrderBy = "IdDesc";
                        break;
                    case "id":
                        resourceParams.OrderBy = "Id";
                        break;
                    default:
                        resourceParams.OrderBy = "Id";
                        break;
                }

                var result = await _officeStatesRepository.PagedList(resourceParams);

                if (result != null)
                {
                    var totalPages = result.TotalPages;

                    pages = Enumerable.Range(1, totalPages);

                    if (totalPages > 5)
                    {

                        if ((resourceParams.PageNumber + 2) >= totalPages)
                        {
                            pages = pages.Skip(totalPages - 5).Take(5).ToList();
                        }
                        else if ((resourceParams.PageNumber - 2) <= 1)
                        {
                            pages = pages.Take(5).ToList();
                        }
                        else
                        {
                            pages = pages.Skip(resourceParams.PageNumber - 2).Take(5).ToList();
                        }
                    }
                }

                pins = result;

                ViewData["Pages"] = pages;
                ViewData["OrderBy"] = orderBy;
                ViewData["SearchQuery"] = searchQuery;


                return PartialView(pins);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("assign_state_to_office")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> CreateMultiple()
        {
            try
            {
                //
                var creationDependencys = await _officesRepository.GetCreationDependencys();
                var officeTypes = creationDependencys.OfficeTypes;
                var states = creationDependencys.States;
                //
                ViewData["OfficeTypes"] = officeTypes.OrderBy(x => x.TypeDescription).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.TypeDescription,
                     Value = x.Id.ToString(),
                 }).ToList();
                //
                ViewData["States"] = states.OrderBy(x => x.StateCode).Select(x =>
                 new SelectListItem()
                 {
                     Text = $"{x.StateCode} {x.StateName}",
                     Value = x.Id.ToString(),
                 }).ToList();

                return PartialView();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("assign_state_to_office")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMultiple(OfficeStatesCreateMultipleDto model)
        {

            try
            {
                //
                var creationDependencys = await _officesRepository.GetCreationDependencys();
                var officeTypes = creationDependencys.OfficeTypes;
                var states = creationDependencys.States;
                //
                ViewData["OfficeTypes"] = officeTypes.OrderBy(x => x.TypeDescription).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.TypeDescription,
                     Value = x.Id.ToString(),
                 }).ToList();
                //
                ViewData["States"] = states.OrderBy(x => x.StateCode).Select(x =>
                 new SelectListItem()
                 {
                     Text = $"{x.StateCode} {x.StateName}",
                     Value = x.Id.ToString(),
                 }).ToList();

                var url = Url.Action("CreateMultiple");
                if (ModelState.IsValid)
                {
                    if (model.StateIds == null || model.StateIds.Count() == 0)
                    {
                        _flashMessage.Danger("Oops...Something went wrong!", "You cannot send an empty list to the database!");
                        return Json(url);
                    }
                    var result = await _officeStatesRepository.CreateMultiple(model);

                    if (result != null)
                    {
                        _flashMessage.Confirmation("Operation Completed", "States Assigned to Office Successfully!");
                        url = Url.Action("Details", "Offices", new { id = model.OfficeId });
                        return Json(url);
                    }
                    else
                    {
                        _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                        return Json(url);
                    }
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Form filled incorrectly...");
                return Json(url);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        // GET: Offices/Delete/5
        [Route("delete/{id?}")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var officeState = await _officeStatesRepository.Get(id.Value);

                if (officeState == null)
                {
                    return NotFound();
                }




                return PartialView(officeState);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: Offices/Delete/5
        [Route("delete/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(OfficeStatesViewDto model)
        {
            try
            {
                var url = Url.Action("Delete", new { id = model.Id });
                if (ModelState.IsValid)
                {
                    await _officeStatesRepository.Delete(model.Id);

                    _flashMessage.Info("Delete Successful", "State removed from Office's control!");
                    url = Url.Action("Details", "Offices", new { id = model.OfficeId });
                    return Json(url);
                }
                _flashMessage.Danger("Oops...Something went wrong!", "Invalid operation parameters!");
                return Json(url);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        #region SelectListResolver


        [Route("list_offices")]
        [HttpGet]
        public async Task<IActionResult> GetOfficesList(Guid officeTypeId)
        {
            IEnumerable<OfficesViewDto> _offices = new List<OfficesViewDto>();
            IEnumerable<SelectListItem> selectList = new List<SelectListItem>();
            if (officeTypeId != Guid.Empty)
            {
                _offices = await _officesRepository.ListByOfficeTypeId(officeTypeId);
                selectList = _offices.OrderBy(x => x.OfficeName).Select(x =>
                 new SelectListItem()
                 {
                     Text = x.OfficeName,
                     Value = x.Id.ToString(),
                 }).ToList();

            }
            return Json(selectList);
        }

        [Route("previously_added_states")]
        [HttpGet]
        public async Task<IActionResult> GetOfficeStates(Guid officeId)
        {

            if (officeId == null)
            {
                return NotFound();
            }

            var office = await _officesRepository.Get(officeId);

            if (office == null)
            {
                return BadRequest();
            }
            List<Guid> statesPreviouslyAdded = office.OfficeStateStates.Select(x => x.StateId).ToList();
            return Json(statesPreviouslyAdded);
        }


        #endregion


    }


}