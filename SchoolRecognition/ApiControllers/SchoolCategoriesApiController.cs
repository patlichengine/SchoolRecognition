﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using SchoolRecognition.Services;

namespace SchoolRecognition.ApiControllers
{
    [Route("api/SchoolCategories")]
    [ApiController]
    public class SchoolCategoriesApiController : ControllerBase
    {

       private readonly SchoolCategoriesService _schoolCategoriesService;
       
        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;

        [Obsolete]
        public SchoolCategoriesApiController(SchoolCategoriesService schoolCategoriesService, IWebHostEnvironment hostingEnvironment)
        {
            _schoolCategoriesService = schoolCategoriesService;
            _hostingEnvironment = hostingEnvironment;
            


        }

        // GET: api/SchoolCategories
        [HttpGet]
        public async Task<ActionResult<List<SchoolCategories>>> Get()
        {
            return await _schoolCategoriesService.ListAll();
        }

        // GET: api/SchoolCategories/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<SchoolCategories>> Get(Guid id)
        {
            return await _schoolCategoriesService.GetBySchoolCategoriesId(id);
        }

        // POST: api/SchoolCategories
        //[HttpPost]
        //public void Post([FromBody] SchoolCategories school)
        //{

        //     _schoolCategories.Create(school);
        //}

        //// PUT: api/SchoolCategories/5
        //[HttpPut("{id}")]
        //public void Put(Guid id, [FromBody] SchoolCategories school)
        //{
        //    _schoolCategories.Update(school);
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(Guid id)
        //{
        //    _schoolCategories.Delete(id);
        //}
    }
}
