using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;

using SchoolRecognition.Services;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/SchoolCategories")]
    [ApiController]
    public class SchoolCategoriesApiController : ControllerBase
    {

       //private readonly SchoolCategoriesService _schoolCategoriesService;
        private readonly ISchoolCategoryRepository _schoolCategories;
        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;

        [Obsolete]
        public SchoolCategoriesApiController(ISchoolCategoryRepository schoolCategories, IWebHostEnvironment hostingEnvironment)
        {
            _schoolCategories = schoolCategories;
            _hostingEnvironment = hostingEnvironment;
            


        }

        // GET: api/SchoolCategories
        [HttpGet]
        public ActionResult<IEnumerable<SchoolCategoryDto>> Get()
        {
           

            var result =  _schoolCategories.GetAllCategory().Result;

            return Ok(result);


          

        }





        // GET: api/SchoolCategories/5
       
        [HttpGet("Edit/{categoryId}", Name = "Get")]
        public IActionResult Get(Guid categoryId)
        {

            if (categoryId == Guid.Empty)
            {
                return NotFound();
            }

            var result = _schoolCategories.GetCategoryById(categoryId).Result;
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

           // return await _schoolCategories.GetCategoryById(id);
        }

        // POST: api/SchoolCategories
        [HttpPost]
        public async Task<ActionResult<SchoolCategoryDto>> Post(SchoolCategories school)
        {

            var result = await _schoolCategories.Create(school);

            return (result);
        }

        //// PUT: api/SchoolCategories/5
        //[HttpPut("{id}")]
        //public void Put(Guid id, [FromBody] SchoolCategories school)
        //{
        //    _schoolCategories.Update(school);
        //}

        //// DELETE: api/ApiWithActions/5
       
        [HttpDelete("Delete/{id}", Name = "Delete")]
        public IActionResult Delete(Guid id)
        {
           
          var result =  _schoolCategories.Delete(id).Result;
           
            return Ok(result);

            
        }
    }
}
