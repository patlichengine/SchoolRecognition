using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IPinHistories
    {
       // Task<SchoolPaymentViewModel> CreatePinHistory(SchoolPaymentViewModel pinHmodel);
        Task<PinHistoriesDto> CreatePinHistory(PinHistoriesCreateDto pinmodel);
    }
}
