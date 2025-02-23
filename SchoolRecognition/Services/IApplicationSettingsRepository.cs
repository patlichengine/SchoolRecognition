﻿using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IApplicationSettingsRepository
    {


        Task<ApplicationSettingsViewDto> Get();
        Task<ApplicationSettingsViewDto> Update(ApplicationSettingsCreateDto _obj);
    }
}
