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
    [Route("api/Pins")]
    [ApiController]
    public class PinsApiController: ControllerBase
    {
        private readonly IPinsRepository _pins;

       

        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;

        [Obsolete]
        public PinsApiController(IPinsRepository pins, IWebHostEnvironment hostingEnvironment)

        {
            _pins = pins;
            _hostingEnvironment = hostingEnvironment;



        }

        [HttpGet]
        public ActionResult<IEnumerable<PinsDto>> Get()
        {


            var result = _pins.GetPins().Result;

            return Ok(result);
        }

        [HttpGet("{recognitionTypeID}")]
        public IActionResult GetPinsByRecognitionTypeID(Guid recognitionTypeID)
        {


            var result = _pins.GetPinsByRecognitionType(recognitionTypeID).Result;

            return Ok(result);
        }


    }
}
