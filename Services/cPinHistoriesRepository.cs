using AutoMapper;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    
    public class cPinHistoriesRepository : IPinHistories, IDisposable
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;

        public cPinHistoriesRepository(SchoolRecognitionContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PinHistoriesDto> CreatePinHistory(PinHistoriesCreateDto pinmodel)
        {
            return await Task.Run(async () =>
            {
                if (pinmodel == null)
                {
                    throw new ArgumentNullException(nameof(pinmodel));
                }
                var schoolsEntity = _mapper.Map<PinHistories>(pinmodel);
                 schoolsEntity.Id = Guid.NewGuid();

                //PinHistories ph = new PinHistories();
                //ph.PinId = pinmodel.PinID;
                //ph.SchoolId = pinmodel.SchoolID; ;


                _context.PinHistories.Add(schoolsEntity);

                bool saveResult = await Save();


                return _mapper.Map<PinHistoriesDto>(schoolsEntity);
            });
        }

        public async Task<bool> Save()
        {
            return await Task.Run(async () =>
            {
                return (await _context.SaveChangesAsync() >= 0);
            });

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

    }

}
