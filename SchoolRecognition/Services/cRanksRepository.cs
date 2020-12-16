using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class cRanksRepository : ControllerBase, IRanksRepository, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public cRanksRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
        public async Task<IEnumerable<RanksDto>> List()
        {
            return await Task.Run(async () =>
            {
                var result = await _context.Ranks.ToListAsync<Ranks>();
                return _mapper.Map<IEnumerable<RanksDto>>(result);

            });
        }

        public async Task<RanksDto> Create(CreateRanksDto ranks)
        {
            return await Task.Run(async () =>
            {
                if (ranks == null)
                {
                    throw new ArgumentNullException(nameof(ranks));
                }
                var ranksEntity = _mapper.Map<Ranks>(ranks);
                ranksEntity.Id = Guid.NewGuid();

                _context.Ranks.Add(ranksEntity);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<RanksDto>(ranksEntity);
            });
        }

        public async Task<RanksDto> Update(Guid ranksID, UpdateRanksDto ranks)
        {
            return await Task.Run(async () =>
            {
                if (ranks == null)
                {
                    throw new ArgumentNullException(nameof(ranks));
                }

                var ranks1 = await _context.Ranks.FirstOrDefaultAsync(c => c.Id == ranksID);

                if (ranks1 == null)
                {
                    throw new ArgumentNullException(nameof(ranks1));
                }
                var val = _mapper.Map(ranks, ranks1);


                _context.Ranks.Update(val);
                bool save = await Save();

                return _mapper.Map<RanksDto>(val);
            });
        }

        public async Task<RanksDto> ListById(Guid ranksId)
        {
            return await Task.Run(async () =>
            {
                if (ranksId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ranksId));
                }

                var result = await _context.Ranks.FirstOrDefaultAsync(c => c.Id == ranksId);
                //return the mapped object
                return _mapper.Map<RanksDto>(result);
            });
        }

        public async Task<RanksDto> Patch(Guid rankId, JsonPatchDocument<UpdateRanksDto> patchDocument)
        {
            return await Task.Run(async () =>
            {
                if (patchDocument == null)
                {
                    throw new ArgumentNullException(nameof(patchDocument));
                }

                var ranks = await _context.Ranks.FirstOrDefaultAsync(c => c.Id == rankId);

                if (ranks == null)
                {
                    throw new ArgumentNullException(nameof(ranks));
                }

                //map the extracted result with the Update class
                var ranksToPatch = _mapper.Map<UpdateRanksDto>(ranks);

                //apply the patch where there are changes and resolve error
                patchDocument.ApplyTo(ranksToPatch, ModelState);



                if (!TryValidateModel(ranksToPatch))
                {
                    throw new ArgumentNullException(nameof(patchDocument));
                    //return ValidationProblem(ModelState);
                }

                //map back the patched record to the previously extracted record from DB
                _mapper.Map(ranksToPatch, ranks);

                bool save = await Save();

                return _mapper.Map<RanksDto>(ranks);
            });
        }

        public async Task<RanksDto> Delete(Guid rankId)
        {
            return await Task.Run(async () =>
            {
                var ranks = await _context.Ranks.FirstOrDefaultAsync(c => c.Id == rankId);
                if (ranks == null)
                {
                    throw new ArgumentNullException(nameof(rankId));
                }
                // await _context.SaveChangesAsync();
                _context.Ranks.Remove(ranks);
                await Save();

                return _mapper.Map<RanksDto>(ranks);
            });
        }

        public async Task<bool> RanksExists(Guid ranksId)
        {
            return await Task.Run(async () =>
            {
                if (ranksId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ranksId));
                }

                return await _context.Ranks.AnyAsync(a => a.Id == ranksId);
            });
        }

        public async Task<bool> Save()
        {
            return await Task.Run(async () =>
            {
                return (await _context.SaveChangesAsync() >= 0);
            });
        }

        public async Task<PagedList<RanksDto>> List(RanksResourceParams resourceParams)
        {
            return await Task.Run(async () =>
            {
                if (resourceParams == null)
                {
                    throw new ArgumentNullException(nameof(resourceParams));
                }


                var collection = _context.Ranks.Include(x => x.ApplicationUsers)
                     as IQueryable<Ranks>;



                //Search by
                if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                {
                    var searchQuery = resourceParams.SearchQuery.Trim();
                    collection = collection.Where(c => c.Name.Contains(searchQuery)

                   || c.Code.Contains(searchQuery));
                }

                //Order by
                if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                {
                    //get property mapping
                    var usersPropertyMappingDictionary =
                        _propertyMappingService.GetPropertyMapping<RanksDto, Ranks>();

                    collection = collection.ApplySort(resourceParams.OrderBy,
                        usersPropertyMappingDictionary);
                }

                //Get the mapped data
                var mappingData = (_mapper.Map<IEnumerable<RanksDto>>(collection)).AsQueryable();// as IQueryable<AccountsDto>;

                //return the paginated data
                return PagedList<RanksDto>.Create(mappingData, resourceParams.PageNumber, resourceParams.PageSize);
            });
        }
    }
}
