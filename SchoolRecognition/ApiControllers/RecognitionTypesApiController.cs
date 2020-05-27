using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SchoolRecognition.Entities;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using SchoolRecognition.Services;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/recognitionTypes")]
    [ApiController]
    public class RecognitionTypesApiController : ControllerBase
    {

        private readonly IRecognitionTypesRepository _recognitionTypesRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public RecognitionTypesApiController(IRecognitionTypesRepository recognitionTypesRepository,
            IMapper mapper, IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService)
        {
            _recognitionTypesRepository = recognitionTypesRepository ??
                throw new ArgumentNullException(nameof(recognitionTypesRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
              throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
              throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet("", Name = "GetAllRecognitionTypes")]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<RecognitionTypesViewDto>>> List()
        {
            try
            {
                var result = await _recognitionTypesRepository.List();
                var links = CreateLinksForRecognitionTypes();

                var shapedRecognitionTypes = result
                               .ShapeData(null);


                var pinsResourceParams = new PinsResourceParams();

                var shapedRecognitionTypesWithLinks = shapedRecognitionTypes.Select(recognitionType =>
                {
                    var recognitionTypeAsDictionary = recognitionType as IDictionary<string, object>;
                    var recognitionTypeLinks = CreateLinksForRecognitionType((Guid)recognitionTypeAsDictionary["Id"], null, pinsResourceParams);
                    recognitionTypeAsDictionary.Add("links", recognitionTypeLinks);
                    return recognitionTypeAsDictionary;
                });


                var linkedCollectionResource = new
                {
                    value = shapedRecognitionTypesWithLinks,
                    links
                };


                return Ok(linkedCollectionResource);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{recognitionTypeId:guid}", Name = "GetRecognitionTypeDetails")]
        //[Route("{id:guid}")]
        public async Task<ActionResult<RecognitionTypesViewDto>> Get(Guid recognitionTypeId)
        {
            try
            {
                if (recognitionTypeId == Guid.Empty)
                {
                    return BadRequest();
                }
                var result = await _recognitionTypesRepository.Get(recognitionTypeId);

                var pinsResourceParams = new PinsResourceParams();

                var links = CreateLinksForRecognitionType(recognitionTypeId, null, pinsResourceParams);


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


        [HttpGet("{recognitionTypeId:guid}/pins", Name = "GetRecognitionTypeDetailsIncludingPins")]
        //[Route("{id:guid}/pins")]
        public async Task<ActionResult<RecognitionTypesViewDto>> GetIncludingListOfPins(Guid recognitionTypeId, [FromQuery] PinsResourceParams resourceParams)
        {
            try
            {
                if (recognitionTypeId == Guid.Empty)
                {
                    return BadRequest();
                }
                //Check for Valid Ordering Mappings
                if (!_propertyMappingService.ValidMappingExistsFor<PinsViewDto, Pins>
                    (resourceParams.OrderBy))
                {
                    return BadRequest();
                }
                if (!_propertyCheckerService.TypeHasProperties<PinsViewDto>
                  (resourceParams.Fields))
                {
                    return BadRequest();
                }
                var result = await _recognitionTypesRepository.GetIncludingPagedListOfPins(recognitionTypeId, resourceParams);
                var links = CreateLinksForRecognitionType(recognitionTypeId, null, resourceParams);
                if (result == null)
                {
                    return NotFound();
                }
                var paginationMetadata = new
                {
                    totalCount = result.RecognitionTypePins.TotalCount,
                    pageSize = result.RecognitionTypePins.PageSize,
                    currentPage = result.RecognitionTypePins.CurrentPage,
                    totalPages = result.RecognitionTypePins.TotalPages
                };

                Response.Headers.Add("X-Pagination",
                    JsonSerializer.Serialize(paginationMetadata));

                //var recognitionLinks = CreateLinksForRecognitionTypePins(resourceParams,
                //    result.RecognitionTypePins.HasNext,
                //    result.RecognitionTypePins.HasPrevious);

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


        [HttpPost(Name = "CreateRecognitionType")]
        public async Task<ActionResult> Create([FromBody]RecognitionTypesCreateDto model)
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

                var result = await _recognitionTypesRepository.Create(model);

                if (result == null)
                {
                    var message = "Entry unsuccessful!";
                    return StatusCode(StatusCodes.Status501NotImplemented, message);
                }

                return CreatedAtRoute("GetRecognitionTypeDetails", new { recognitionTypeId = result.Value });


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPut("{recognitionTypeId:guid}", Name = "UpdateRecognitionType")]
        public async Task<ActionResult> Update(Guid recognitionTypeId, [FromBody]RecognitionTypesCreateDto model)
        {
            try
            {
                if (recognitionTypeId == null || recognitionTypeId == Guid.Empty)
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

                var result = await _recognitionTypesRepository.Update(model);

                if (result == null)
                {
                    var message = "Entry unsuccessful!";
                    return StatusCode(StatusCodes.Status501NotImplemented, message);
                }

                return CreatedAtRoute("GetRecognitionTypeDetails", new { recognitionTypeId = result.Id });


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{recognitionTypeId:guid}", Name = "DeleteRecognitionType")]
        public async Task<ActionResult> Delete(Guid recognitionTypeId)
        {
            try
            {
                if (recognitionTypeId == null || recognitionTypeId == Guid.Empty)
                {
                    return BadRequest("Invalid route parameters");

                }

                await _recognitionTypesRepository.Delete(recognitionTypeId);

                return RedirectToRoute("GetAllRecognitionTypes");


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        private string CreateRecognitionTypesResourceUri(
           RecognitionTypesResourceParams resourceParams,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetRecognitionTypes",
                      new
                      {
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber - 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetRecognitionTypes",
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
                    return Url.Link("GetRecognitionTypes",
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

        private string CreateRecognitionTypePinsResourceUri(
           PinsResourceParams resourceParams,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetRecognitionTypePins",
                      new
                      {
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber - 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetRecognitionTypePins",
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
                    return Url.Link("GetRecognitionTypePins",
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

        private IEnumerable<LinkDto> CreateLinksForRecognitionType(
            Guid recognitionTypeId,
            string fields,
            PinsResourceParams resourceParams)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                  new LinkDto(Url.Link("GetRecognitionTypeDetails", new { recognitionTypeId }),
                  "self",
                  "GET"));
            }
            else
            {
                links.Add(
                  new LinkDto(Url.Link("GetRecognitionTypeDetails", new { recognitionTypeId, fields }),
                  "self",
                  "GET"));
            }

            links.Add(
                  new LinkDto(Url.Link("GetRecognitionTypeDetailsIncludingPins", new { recognitionTypeId, resourceParams }),
                  "self",
                  "GET"));

            links.Add(
                new LinkDto(Url.Link("CreateRecognitionType", new { }),
                "create_recognition_type",
                "POST"));

            links.Add(
                new LinkDto(Url.Link("UpdateRecognitionType", new { recognitionTypeId }),
                "update_recognition_type",
                "PUT"));

            links.Add(
               new LinkDto(Url.Link("DeleteRecognitionType", new { recognitionTypeId }),
               "delete_recognition_type",
               "DELETE"));


            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForRecognitionTypes(
            RecognitionTypesResourceParams resourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateRecognitionTypesResourceUri(
                   resourceParameters, ResourceUriType.CurrentPage)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateRecognitionTypesResourceUri(
                      resourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateRecognitionTypesResourceUri(
                        resourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForRecognitionTypes()
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
                  new LinkDto(Url.Link("GetAllRecognitionTypes", null),
                  "self",
                  "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForRecognitionTypePins(
            PinsResourceParams resourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateRecognitionTypePinsResourceUri(
                   resourceParameters, ResourceUriType.CurrentPage)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateRecognitionTypePinsResourceUri(
                      resourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateRecognitionTypePinsResourceUri(
                        resourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }
    }
}