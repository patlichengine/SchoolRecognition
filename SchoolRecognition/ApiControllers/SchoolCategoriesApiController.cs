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


            var result = _schoolCategories.GetAllCategory().Result;

            return Ok(result);




        }





        // GET: api/SchoolCategories/5
       [HttpGet]
        [Route("{categoryId:guid}", Name = "GetSchoolCategory")]
      //  [HttpGet("Edit/{categoryId}", Name = "Get")]
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
        public ActionResult<SchoolCategoryDto>  Post(CreateSchoolCategoryDto school)
        {

            var result = _schoolCategories.Create(school).Result;
            //Return the named user using the specified URI name

            var final = CreatedAtRoute("GetSchoolCategory",
                new { userId = result.Id }, result);
            return Ok(final);
        }

        //// PUT: api/SchoolCategories/5
        [HttpPut("Update/{id}")]
        public ActionResult Update(Guid id, UpdateSchoolCategoryDto usersUpdate)
        {
            
            if (!_schoolCategories.SchoolCategoriesExists(id).Result)
            {
                return NotFound();
            }
           
            var result = _schoolCategories.Update(id, usersUpdate).Result;

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        //// DELETE: api/ApiWithActions/5

        [HttpDelete("Delete/{id}")]
        
        public ActionResult Delete(Guid id)
        {
            if (! _schoolCategories.SchoolCategoriesExists(id).Result)
            {
                return NotFound();
            }

            var result = _schoolCategories.DeleteSchoolCategory(id);
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }



    }
}
