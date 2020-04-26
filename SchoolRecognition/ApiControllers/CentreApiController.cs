using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Models;
using SchoolRecognition.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/Centres")]
    [ApiController]
    public class CentreApiController:ControllerBase
    {
        private readonly ICentresRepository _centre;

        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;

        [Obsolete]
        public CentreApiController(ICentresRepository centre, IWebHostEnvironment hostingEnvironment)

        {
            _centre = centre;
            _hostingEnvironment = hostingEnvironment;



        }
        [HttpGet]
        public ActionResult<IEnumerable<CentresDto>> Get()
        {


            var result = _centre.GetAllCentres().Result;

            return Ok(result);
        }

        
        [HttpGet("{centrenumber}")]
        //[Route("{centrenumber:string}")]
        //  [HttpGet("Edit/{categoryId}", Name = "Get")]
        public IActionResult GetCentre(string centrenumber)
        {

            if (centrenumber == string.Empty)
            {
                return NotFound();
            }

            var result = _centre.GetCentreByCentreNumber(centrenumber).Result;
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

            // return await _schoolCategories.GetCategoryById(id);
        }

    }
}
