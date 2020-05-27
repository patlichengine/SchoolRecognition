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

    [Route("api/pins")]
    [ApiController]
    public class PinsApiController : ControllerBase
    {
        private readonly IPinsRepository _pinsRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public PinsApiController(IPinsRepository pinsRepository,
            IMapper mapper, IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService)
        {
            _pinsRepository = pinsRepository ??
                throw new ArgumentNullException(nameof(pinsRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
              throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
              throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet("", Name = "GetAllPins")]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<PinsViewDto>>> PagedList(PinsResourceParams resourceParams)
        {
            try
            {

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

                var result = await _pinsRepository.PagedList(resourceParams);

                var links = CreateLinksForPins(resourceParams, result.HasNext, result.HasPrevious);

                var shapedPins = result.AsEnumerable()
                               .ShapeData(resourceParams.Fields);


                var pinHistoriesResourceParams = new PinHistoriesResourceParams();

                var shapedPinsWithLinks = shapedPins.Select(pin =>
                {
                    var pinAsDictionary = pin as IDictionary<string, object>;
                    var pinLinks = CreateLinksForPin((Guid)pinAsDictionary["Id"], null, pinHistoriesResourceParams);
                    pinAsDictionary.Add("links", pinLinks);
                    return pinAsDictionary;
                });


                var linkedCollectionResource = new
                {
                    value = shapedPinsWithLinks,
                    links
                };


                return Ok(linkedCollectionResource);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{pinId:guid}", Name = "GetPinDetails")]
        //[Route("{id:guid}")]
        public async Task<ActionResult<PinsViewDto>> Get(Guid pinId)
        {
            try
            {
                if (pinId == Guid.Empty)
                {
                    return BadRequest();
                }
                var result = await _pinsRepository.Get(pinId);

                var pinHistoriesResourceParams = new PinHistoriesResourceParams();

                var links = CreateLinksForPin(pinId, null, pinHistoriesResourceParams);


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


        [HttpGet("{pinId:guid}/pinHistories", Name = "GetPinDetailsIncludingPinHistories")]
        //[Route("{id:guid}/pins")]
        public async Task<ActionResult<PinsViewDto>> GetIncludingPagedListOfPinHistories(Guid pinId, [FromQuery] PinHistoriesResourceParams resourceParams)
        {
            try
            {
                if (pinId == Guid.Empty)
                {
                    return BadRequest();
                }
                //Check for Valid Ordering Mappings
                if (!_propertyMappingService.ValidMappingExistsFor<PinHistoriesViewDto, PinHistories>
                    (resourceParams.OrderBy))
                {
                    return BadRequest();
                }
                if (!_propertyCheckerService.TypeHasProperties<PinHistoriesViewDto>
                  (resourceParams.Fields))
                {
                    return BadRequest();
                }
                var result = await _pinsRepository.GetIncludingPagedListOfPinHistories(pinId, resourceParams);
                var links = CreateLinksForPinHistories(pinId, resourceParams, result.Histories.HasNext, result.Histories.HasPrevious);
                if (result == null)
                {
                    return NotFound();
                }
                var paginationMetadata = new
                {

                    totalCount = result.Histories.TotalCount,
                    pageSize = result.Histories.PageSize,
                    currentPage = result.Histories.CurrentPage,
                    totalPages = result.Histories.TotalPages
                };

                Response.Headers.Add("X-Pagination",
                    JsonSerializer.Serialize(paginationMetadata));

                //var pinLinks = CreateLinksForPinHistories(resourceParams,
                //    result.PinHistories.HasNext,
                //    result.PinHistories.HasPrevious);

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


        [HttpPost(Name = "CreatePin")]
        public async Task<ActionResult> Create([FromBody]PinsCreateDto model)
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

                var result = await _pinsRepository.CreateMultiple(model);

                if (result == true)
                {
                    var message = "Entry unsuccessful!";
                    return StatusCode(StatusCodes.Status501NotImplemented, message);
                }

                return CreatedAtRoute("GetAllPins", new {  });


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPut("{pinId:guid}", Name = "UpdatePin")]
        public async Task<ActionResult> Update(Guid pinId, [FromBody]PinsUpdateDto model)
        {
            try
            {
                if (pinId == null || pinId == Guid.Empty)
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

                var result = await _pinsRepository.Update(model);

                if (result == null)
                {
                    var message = "Entry unsuccessful!";
                    return StatusCode(StatusCodes.Status501NotImplemented, message);
                }

                return CreatedAtRoute("GetPinDetails", new { pinId = result.Id });


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{pinId:guid}", Name = "DeletePin")]
        public async Task<ActionResult> Delete(Guid pinId)
        {
            try
            {
                if (pinId == null || pinId == Guid.Empty)
                {
                    return BadRequest("Invalid route parameters");

                }

                await _pinsRepository.Delete(pinId);

                return RedirectToRoute("GetAllPins");


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        private string CreatePinsResourceUriType(
           PinsResourceParams resourceParams,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetPins",
                      new
                      {
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber - 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetPins",
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
                    return Url.Link("GetPins",
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

        private string CreatePinSchoolPaymentsResourceUriType(
            Guid pinId,
           SchoolPaymentsResourceParams resourceParams,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetPinDetailsIncludingSchoolPayments",
                      new
                      {
                          PinId = pinId,
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber - 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetPinDetailsIncludingSchoolPayments",
                      new
                      {
                          PinId = pinId,
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber + 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.CurrentPage:
                default:
                    return Url.Link("GetPinDetailsIncludingSchoolPayments",
                    new
                    {
                        PinId = pinId,
                        fields = resourceParams.Fields,
                        orderBy = resourceParams.OrderBy,
                        pageNumber = resourceParams.PageNumber,
                        pageSize = resourceParams.PageSize,
                        searchQuery = resourceParams.SearchQuery
                    });
            }

        }

        private string CreatePinHistoriesResourceUriType(
            Guid pinId,
           PinHistoriesResourceParams resourceParams,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetPinDetailsIncludingPinHistories",
                      new
                      {
                          PinId = pinId,
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber - 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetPinDetailsIncludingPinHistories",
                      new
                      {
                          PinId = pinId,
                          fields = resourceParams.Fields,
                          orderBy = resourceParams.OrderBy,
                          pageNumber = resourceParams.PageNumber + 1,
                          pageSize = resourceParams.PageSize,
                          searchQuery = resourceParams.SearchQuery
                      });
                case ResourceUriType.CurrentPage:
                default:
                    return Url.Link("GetPinDetailsIncludingPinHistories",
                    new
                    {
                        PinId = pinId,
                        fields = resourceParams.Fields,
                        orderBy = resourceParams.OrderBy,
                        pageNumber = resourceParams.PageNumber,
                        pageSize = resourceParams.PageSize,
                        searchQuery = resourceParams.SearchQuery
                    });
            }

        }

        private IEnumerable<LinkDto> CreateLinksForPin(
            Guid pinId,
            string fields,
            PinHistoriesResourceParams pinHistoriesResourceParameters
            )
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                  new LinkDto(Url.Link("GetPinDetails", new { pinId }),
                  "self",
                  "GET"));
            }
            else
            {
                links.Add(
                  new LinkDto(Url.Link("GetPinDetails", new { pinId, fields }),
                  "self",
                  "GET"));
            }


            links.Add(
                  new LinkDto(Url.Link("GetPinDetailsIncludingPinHistories", new { pinId, pinHistoriesResourceParameters }),
                  "self",
                  "GET"));

            links.Add(
                new LinkDto(Url.Link("CreatePin", new { }),
                "create_pin",
                "POST"));

            links.Add(
               new LinkDto(Url.Link("UpdatePin", new { pinId }),
               "update_pin",
               "GET"));

            links.Add(
               new LinkDto(Url.Link("DeletePin", new { pinId }),
               "delete_pin",
               "DELETE"));


            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForPins(
            PinsResourceParams resourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreatePinsResourceUriType(
                   resourceParameters, ResourceUriType.CurrentPage)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreatePinsResourceUriType(
                      resourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreatePinsResourceUriType(
                        resourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            links.Add(
                new LinkDto(Url.Link("CreatePin", new { }),
                "create_pin",
                "POST"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForPinSchoolPayments(
            Guid pinId,
            SchoolPaymentsResourceParams resourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreatePinSchoolPaymentsResourceUriType(
                   pinId,
                   resourceParameters, ResourceUriType.CurrentPage)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreatePinSchoolPaymentsResourceUriType(
                      pinId,
                      resourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreatePinSchoolPaymentsResourceUriType(
                        pinId,
                        resourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForPinHistories(
            Guid pinId,
            PinHistoriesResourceParams resourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreatePinHistoriesResourceUriType(
                   pinId,
                   resourceParameters, ResourceUriType.CurrentPage)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreatePinHistoriesResourceUriType(
                      pinId,
                      resourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreatePinHistoriesResourceUriType(
                        pinId,
                        resourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }
    }
}