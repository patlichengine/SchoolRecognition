using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/Ranks")]
    [ApiController]
    public class RanksApiController:ControllerBase
    {
        private readonly IRanksRepository _ranks;

        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;

        [Obsolete]
        public RanksApiController(IRanksRepository ranks, IWebHostEnvironment hostingEnvironment)

        { 
            _ranks = ranks;
            _hostingEnvironment = hostingEnvironment;



        }
    }

}
