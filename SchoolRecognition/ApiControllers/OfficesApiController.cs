using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.Entities;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;

namespace SchoolRecognition.ApiControllers
{

    [Route("api/offices")]
    [ApiController]
    public class OfficesApiController : ControllerBase
    {
        private readonly IOfficesRepository _officesRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public OfficesApiController(IOfficesRepository officesRepository,
            IMapper mapper, IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService)
        {
            _officesRepository = officesRepository ??
                throw new ArgumentNullException(nameof(officesRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
              throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
              throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet("", Name = "GetAllOffices")]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<OfficesViewDto>>> PagedList(OfficesResourceParams resourceParams)
        {
            try
            {

                //Check for Valid Ordering Mappings
                if (!_propertyMappingService.ValidMappingExistsFor<OfficesViewDto, Offices>
                    (resourceParams.OrderBy))
                {
                    return BadRequest();
                }
                if (!_propertyCheckerService.TypeHasProperties<OfficesViewDto>
                  (resourceParams.Fields))
                {
                    return BadRequest();
                }

                var result = await _officesRepository.PagedList(resourceParams);

                var links = CreateLinksForOffices(resourceParams, result.HasNext, result.HasPrevious);

                var shapedOffices = result.AsEnumerable()
                               .ShapeData(resourceParams.Fields);


                var schoolsResourceParams = new SchoolsResourceParams();
                var officeLocalGovernmentsResourceParams = new OfficeLocalGovernmentsResourceParams();

                var shapedOfficesWithLinks = shapedOffices.Select(office =>
                {
                    var officeAsDictionary = office as IDictionary<string, object>;
                    var officeLinks = CreateLinksForOffice((Guid)officeAsDictionary["Id"], null, schoolsResourceParams, officeLocalGovernmentsResourceParams);
                    officeAsDictionary.Add("links", officeLinks);
                    return officeAsDictionary;
                });


                var linkedCollectionResource = new
                {
                    value = shapedOfficesWithLinks,
                    links
                };


                return Ok(linkedCollectionResource);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
   
        [HttpGet("{officeId:guid}", Name = "GetOfficeDetails")]
        //[Route("{id:guid}")]
        public async Task<ActionResult<OfficesViewDto>> Get(Guid officeId)
        {
            try
            {
                if (officeId == Guid.Empty)
                {
                    return BadRequest();
                }
                var result = await _officesRepository.Get(officeId);

                var schoolsResourceParams = new SchoolsResourceParams();
                var officeLocalGovernmentsResourceParams = new OfficeLocalGovernmentsResourceParams();

                var links = CreateLinksForOffice(officeId, null, schoolsResourceParams, officeLocalGovernmentsResourceParams);


                if (result == null)
                {
                    return NotFound();
                }

                var fullResourceToReturn = result
                               .ShapeData(null) as IDictionary<string, object>;

                fullResourceToReturn.Add("links", links);

                return Ok(fullResourceToReturn);


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{officeId:guid}/schools", Name = "GetOfficeDetailsIncludingSchools")]
        //[Route("{id:guid}/offices")]
        public async Task<ActionResult<OfficesViewDto>> GetIncludingPagedListOfSchools(Guid officeId, [FromQuery] SchoolsResourceParams resourceParams)
        {
            try
            {
                if (officeId == Guid.Empty)
                {
                    return BadRequest();
                }
                //Check for Valid Ordering Mappings
                if (!_propertyMappingService.ValidMappingExistsFor<SchoolsViewDto, Schools>
                    (resourceParams.OrderBy))
                {
                    return BadRequest();
                }
                if (!_propertyCheckerService.TypeHasProperties<SchoolsViewDto>
                  (resourceParams.Fields))
                {
                    return BadRequest();
                }
                var result = await _officesRepository.GetIncludingPagedListOfSchools(officeId, resourceParams);
                var links = CreateLinksForOfficeSchools(officeId, resourceParams, result.OfficeSchools.HasNext, result.OfficeSchools.HasPrevious);
                if (result == null)
                {
                    return NotFound();
                }
                var paginationMetadata = new
                {
                    totalCount = result.OfficeSchools.TotalCount,
                    pageSize = result.OfficeSchools.PageSize,
                    currentPage = result.OfficeSchools.CurrentPage,
                    totalPages = result.OfficeSchools.TotalPages
                };

                Response.Headers.Add("X-Pagination",
                    JsonSerializer.Serialize(paginationMetadata));

                //var officeLinks = CreateLinksForOfficeSchools(resourceParams,
                //    result.OfficeSchools.HasNext,
                //    result.OfficeSchools.HasPrevious);

                var fullResourceToReturn = result
                               .ShapeData(null) as IDictionary<string, object>;

                fullResourceToReturn.Add("links", links);

                return Ok(fullResourceToReturn);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{officeId:guid}/localGovernmentsControlled", Name = "GetOfficeDetailsIncludingOfficeLocalGovernments")]
        //[Route("{id:guid}/offices")]
        public async Task<ActionResult<OfficesViewDto>> GetIncludingPagedListOfOfficeLocalGovernments(Guid officeId, [FromQuery] OfficeLocalGovernmentsResourceParams resourceParams)
        {
            try
            {
                if (officeId == Guid.Empty)
                {
                    return BadRequest();
                }
                //Check for Valid Ordering Mappings
                if (!_propertyMappingService.ValidMappingExistsFor<OfficeLocalGovernmentsViewDto, OfficeLocalGovernments>
                    (resourceParams.OrderBy))
                {
                    return BadRequest();
                }
                if (!_propertyCheckerService.TypeHasProperties<OfficeLocalGovernmentsViewDto>
                  (resourceParams.Fields))
                {
                    return BadRequest();
                }
                var result = await _officesRepository.GetIncludingPagedListOfOfficeLocalGovernments(officeId, resourceParams);
                var links = CreateLinksForOfficeLocalGovernments(officeId, resourceParams, result.OfficeLgas.HasNext, result.OfficeLgas.HasPrevious);
                if (result == null)
                {
                    return NotFound();
                }
                var paginationMetadata = new
                {

                    totalCount = result.OfficeLgas.TotalCount,
                    pageSize = result.OfficeLgas.PageSize,
                    currentPage = result.OfficeLgas.CurrentPage,
                    totalPages = result.OfficeLgas.TotalPages
                };

                Response.Headers.Add("X-Pagination",
                    JsonSerializer.Serialize(paginationMetadata));

                //var officeLinks = CreateLinksForOfficeLocalGovernments(resourceParams,
                //    result.OfficeLocalGovernments.HasNext,
                //    result.OfficeLocalGovernments.HasPrevious);

                var fullResourceToReturn = result
                               .ShapeData(null) as IDictionary<string, object>;

                fullResourceToReturn.Add("links", links);

                return Ok(fullResourceToReturn);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost(Name = "CreateOffice")]
        public async Task<ActionResult> Create([FromBody]OfficesCreateDto model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Model is null or empty!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is invalid!");
                }
                if (await _officesRepository.Exists(model.OfficeName))
                {
                    var message = "An item with this description already exists in the system!";
                    return StatusCode(StatusCodes.Status409Conflict, message);
                }

                var result = await _officesRepository.Create(model);

                if (result == null)
                {
                    var message = "Entry unsuccessful!";
                    return StatusCode(StatusCodes.Status501NotImplemented, message);
                }

                return CreatedAtRoute("GetOfficeDetails", new { officeId = result.Value });


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPut("{officeId:guid}", Name = "UpdateOffice")]
        public async Task<ActionResult> Update(Guid officeId, [FromBody]OfficesCreateDto model)
        {
            try
            {
                if (officeId == null || officeId == Guid.Empty)
                {
                    return BadRequest("Invalid route parameters");

                }
                if (model == null)
                {
                    return BadRequest("Model is null or empty!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is invalid!");
                }
                if (await _officesRepository.Exists(model.Id, model.OfficeName))
                {
                    var message = "An item with this description already exists in the system!";
                    return StatusCode(StatusCodes.Status409Conflict, message);
                }

                var result = await _officesRepository.Update(model);

                if (result == null)
                {
                    var message = "Entry unsuccessful!";
                    return StatusCode(StatusCodes.Status501NotImplemented, message);
                }

                return CreatedAtRoute("GetOfficeDetails", new { officeId = result.Id });


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{officeId:guid}", Name = "DeleteOffice")]
        public async Task<ActionResult> Delete(Guid officeId)
        {
            try
            {
                if (officeId == null || officeId == Guid.Empty)
                {
                    return BadRequest("Invalid route parameters");

                }

                await _officesRepository.Delete(officeId);

                return RedirectToRoute("GetAllOffices");


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        private string CreateOfficesResourceUriType(
           OfficesResourceParams resourceParams,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetOffices",
                      new
                      {
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber - 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetOffices",
                      new
                      {
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber + 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.CurrentPage:
                default:
                    return Url.Link("GetOffices",
                    new
                    {
                        fields = resourceParams.Fields,
                        orderBy = resourceParams.OrderBy,
                        pageNumber = resourceParams.PageNumber,
                        pageSize = resourceParams.PageSize,
                        searchQuery = resourceParams.SearchQuery
                    });
            }

        }

        private string CreateOfficeSchoolsResourceUriType(
            Guid officeId,
           SchoolsResourceParams resourceParams,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetOfficeDetailsIncludingSchools",
                      new
                      {
                          OfficeId = officeId,
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber - 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetOfficeDetailsIncludingSchools",
                      new
                      {
                          OfficeId = officeId,
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber + 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.CurrentPage:
                default:
                    return Url.Link("GetOfficeDetailsIncludingSchools",
                    new
                    {
                        OfficeId = officeId,
                        fields = resourceParams.Fields,
                        orderBy = resourceParams.OrderBy,
                        pageNumber = resourceParams.PageNumber,
                        pageSize = resourceParams.PageSize,
                        searchQuery = resourceParams.SearchQuery
                    });
            }

        }

        private string CreateOfficeLocalGovernmentsResourceUriType(
            Guid officeId,
           OfficeLocalGovernmentsResourceParams resourceParams,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetOfficeDetailsIncludingOfficeLocalGovernments",
                      new
                      {
                          OfficeId = officeId,
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber - 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetOfficeDetailsIncludingOfficeLocalGovernments",
                      new
                      {
                          OfficeId = officeId,
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber + 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.CurrentPage:
                default:
                    return Url.Link("GetOfficeDetailsIncludingOfficeLocalGovernments",
                    new
                    {
                        OfficeId = officeId,
                        fields = resourceParams.Fields,
                        orderBy = resourceParams.OrderBy,
                        pageNumber = resourceParams.PageNumber,
                        pageSize = resourceParams.PageSize,
                        searchQuery = resourceParams.SearchQuery
                    });
            }

        }

        private IEnumerable<LinkDto> CreateLinksForOffice(
            Guid officeId, 
            string fields,
            SchoolsResourceParams schoolResourceParameters,
            OfficeLocalGovernmentsResourceParams officeLocalGovernmentsResourceParameters
            )
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                  new LinkDto(Url.Link("GetOfficeDetails", new { officeId }),
                  "self",
                  "GET"));
            }
            else
            {
                links.Add(
                  new LinkDto(Url.Link("GetOfficeDetails", new { officeId, fields }),
                  "self",
                  "GET"));
            }

            links.Add(
                  new LinkDto(Url.Link("GetOfficeDetailsIncludingSchools", new { officeId, schoolResourceParameters }),
                  "self",
                  "GET"));

            links.Add(
                  new LinkDto(Url.Link("GetOfficeDetailsIncludingOfficeLocalGovernments", new { officeId, officeLocalGovernmentsResourceParameters }),
                  "self",
                  "GET"));

            links.Add(
                new LinkDto(Url.Link("CreateOffice", new { }),
                "create_office",
                "POST"));

            links.Add(
               new LinkDto(Url.Link("UpdateOffice", new { officeId }),
               "update_office",
               "GET"));

            links.Add(
               new LinkDto(Url.Link("DeleteOffice", new { officeId }),
               "delete_office",
               "DELETE"));


            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForOffices(
            OfficesResourceParams resourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateOfficesResourceUriType(
                   resourceParameters, ResourceUriType.CurrentPage)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateOfficesResourceUriType(
                      resourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateOfficesResourceUriType(
                        resourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            links.Add(
                new LinkDto(Url.Link("CreateOffice", new { }),
                "create_office",
                "POST"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForOfficeSchools(
            Guid officeId,
            SchoolsResourceParams resourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateOfficeSchoolsResourceUriType(
                   officeId,
                   resourceParameters, ResourceUriType.CurrentPage)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateOfficeSchoolsResourceUriType(
                      officeId,
                      resourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateOfficeSchoolsResourceUriType(
                        officeId,
                        resourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForOfficeLocalGovernments(
            Guid officeId,
            OfficeLocalGovernmentsResourceParams resourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateOfficeLocalGovernmentsResourceUriType(
                   officeId,
                   resourceParameters, ResourceUriType.CurrentPage)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateOfficeLocalGovernmentsResourceUriType(
                      officeId,
                      resourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateOfficeLocalGovernmentsResourceUriType(
                        officeId,
                        resourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }
    }
}
