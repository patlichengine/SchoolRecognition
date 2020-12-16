using Microsoft.AspNetCore.JsonPatch;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IClassSettingsRepository
    {
        public Task<PagedList<ClassSettingsDto>> List(ClassSettingsResourceParams itemsResourceParams);
        public Task<ClassSettingsDto> Create(ClassSettingsCreateDto create);
        public Task<ClassSettingsDto> Update(Guid classSettingID, ClassSettingsUpdateDto update);
        public Task<ClassSettingsDto> ListById(Guid listByID);
        public Task<ClassSettingsDto> Patch(Guid classSettingID, JsonPatchDocument<ClassSettingsUpdateDto> patchDocument);
        public Task<ClassSettingsDto> Delete(Guid delete);
        public Task<bool> ClassSettingsExists(Guid ItemsID);

    }
}