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
    [Route("api/SchoolPayments")]
    [ApiController]
    public class SchoolPaymentsApiController: ControllerBase
    {
        private readonly ISchoolPaymentsRepository _schoolPayments;

        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;

        [Obsolete]
        public SchoolPaymentsApiController(ISchoolPaymentsRepository schoolPayments, IWebHostEnvironment hostingEnvironment)
        {
            _schoolPayments = schoolPayments;
            _hostingEnvironment = hostingEnvironment;



        }
        //[HttpGet]
        //public ActionResult<IEnumerable<RanksDto>> Get()
        //{


        //    var result = _schoolPayments.GetAllCategory().Result;

        //    return Ok(result);




        //}
    }
}
