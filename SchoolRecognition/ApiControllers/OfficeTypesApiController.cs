using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Entities;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/officeTypes")]
    [ApiController]
    public class OfficeTypesApiController : ControllerBase
    {
        private readonly IOfficeTypesRepository _officeTypesRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public OfficeTypesApiController(IOfficeTypesRepository officeTypesRepository,
            IMapper mapper, IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService)
        {
            _officeTypesRepository = officeTypesRepository ?? 
                throw new ArgumentNullException(nameof(officeTypesRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
              throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
              throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet("", Name = "GetAllOfficeTypes")]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<OfficeTypesViewDto>>> GetOfficeTypes()
        {
            try
            {
                var result = await _officeTypesRepository.GetAllOfficeTypesAsync();
                var links = CreateLinksForOfficeTypes();

                var shapedOfficeTypes = result
                               .ShapeData(null);


                var shapedOfficeTypesWithLinks = shapedOfficeTypes.Select(officeType =>
                {
                    var officeTypeAsDictionary = officeType as IDictionary<string, object>;
                    var officeTypeLinks = CreateLinksForOfficeType((Guid)officeTypeAsDictionary["Id"], null);
                    officeTypeAsDictionary.Add("links", officeTypeLinks);
                    return officeTypeAsDictionary;
                });


                var linkedCollectionResource = new
                {
                    value = shapedOfficeTypesWithLinks,
                    links
                };


                return Ok(linkedCollectionResource);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{officeTypeId:guid}", Name = "GetOfficeTypeDetails")]
        //[Route("{id:guid}")]
        public async Task<ActionResult<OfficeTypesViewDto>> GetOfficeTypeDetails(Guid officeTypeId)
        {
            try
            {
                if (officeTypeId == Guid.Empty)
                {
                    return BadRequest();
                }
                var result = await _officeTypesRepository.GetOfficeTypesSingleOrDefaultAsync(officeTypeId);
                var links = CreateLinksForOfficeType(officeTypeId, null);


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

        [HttpGet("{officeTypeId:guid}/offices", Name = "GetOfficeTypeDetailsIncludingOffices")]
        //[Route("{id:guid}/offices")]
        public async Task<ActionResult<OfficeTypesViewDto>> GetOfficeTypeDetailsIncludingOffices(Guid officeTypeId, [FromQuery] OfficesResourceParams resourceParams)
        {
            try
            {
                if (officeTypeId == Guid.Empty)
                {
                    return BadRequest();
                }
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
                var result = await _officeTypesRepository.GetOfficeTypesOfficesAsPagedListAsync(officeTypeId, resourceParams);
                var links = CreateLinksForOfficeType(officeTypeId, null);
                if (result == null)
                {
                    return NotFound();
                }
                var paginationMetadata = new
                {
                    totalCount = result.OfficeTypeOffices.TotalCount,
                    pageSize = result.OfficeTypeOffices.PageSize,
                    currentPage = result.OfficeTypeOffices.CurrentPage,
                    totalPages = result.OfficeTypeOffices.TotalPages
                };

                Response.Headers.Add("X-Pagination",
                    JsonSerializer.Serialize(paginationMetadata));

                //var officeLinks = CreateLinksForOfficeTypeOffices(resourceParams,
                //    result.OfficeTypeOffices.HasNext,
                //    result.OfficeTypeOffices.HasPrevious);

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

        private string CreateOfficeTypesResourceUri(
           OfficeTypesResourceParams resourceParams,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetOfficeTypes",
                      new
                      {
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber - 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetOfficeTypes",
                      new
                      {
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber + 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.Current:
                default:
                    return Url.Link("GetOfficeTypes",
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

        private string CreateOfficeTypeOfficesResourceUri(
           OfficesResourceParams resourceParams,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetOfficeTypeOffices",
                      new
                      {
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber - 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetOfficeTypeOffices",
                      new
                      {
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber + 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.Current:
                default:
                    return Url.Link("GetOfficeTypeOffices",
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

        private IEnumerable<LinkDto> CreateLinksForOfficeType(Guid officeTypeId, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                  new LinkDto(Url.Link("GetOfficeTypeDetails", new { officeTypeId }),
                  "self",
                  "GET"));
            }
            else
            {
                links.Add(
                  new LinkDto(Url.Link("GetOfficeTypeDetails", new { officeTypeId, fields }),
                  "self",
                  "GET"));
            }

            links.Add(
                  new LinkDto(Url.Link("GetOfficeTypeDetailsIncludingOffices", new { officeTypeId }),
                  "self",
                  "GET"));

            //links.Add(
            //   new LinkDto(Url.Link("DeleteOfficeType", new { authorId }),
            //   "delete_author",
            //   "DELETE"));

            //links.Add(
            //    new LinkDto(Url.Link("CreateCourseForOfficeType", new { authorId }),
            //    "create_course_for_author",
            //    "POST"));

            //links.Add(
            //   new LinkDto(Url.Link("GetCoursesForOfficeType", new { authorId }),
            //   "courses",
            //   "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForOfficeTypes(
            OfficeTypesResourceParams resourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateOfficeTypesResourceUri(
                   resourceParameters, ResourceUriType.Current)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateOfficeTypesResourceUri(
                      resourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateOfficeTypesResourceUri(
                        resourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }
        private IEnumerable<LinkDto> CreateLinksForOfficeTypes()
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
                  new LinkDto(Url.Link("GetAllOfficeTypes", null),
                  "self",
                  "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForOfficeTypeOffices(
            OfficesResourceParams resourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateOfficeTypeOfficesResourceUri(
                   resourceParameters, ResourceUriType.Current)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateOfficeTypeOfficesResourceUri(
                      resourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateOfficeTypeOfficesResourceUri(
                        resourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }
    }
}