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
    [Route("api/RecognitionTypes")]
    [ApiController]
    public class RecognitionTypesApiController: ControllerBase
    {
        private readonly IRecognitionTypesRepository _recognitionTypes;

        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;

        [Obsolete]
        public RecognitionTypesApiController(IRecognitionTypesRepository recognitionTypes, IWebHostEnvironment hostingEnvironment)
        {
            _recognitionTypes = recognitionTypes;
            _hostingEnvironment = hostingEnvironment;



        }
        [HttpGet]
        public ActionResult<IEnumerable<RecognitionTypesDto>> GetRecognitionTypes()
        {


            var result = _recognitionTypes.GetAllRecognitionTypes().Result;

            return Ok(result);
        }



        

    }
}
