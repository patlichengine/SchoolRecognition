using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Controllers
{
    public class SchoolPaymentsController: Controller
    {
        ICentresRepository _centreRepository;
        IRecognitionTypesRepository _recognitionTypeRepository;
        IPinsRepository _pinRepository;
        public SchoolPaymentsController(ICentresRepository centreRepo,IRecognitionTypesRepository recognitionTypesRepo,IPinsRepository pinsRepo)
        {
            _centreRepository = centreRepo ??
               throw new ArgumentNullException(nameof(centreRepo));

            _recognitionTypeRepository = recognitionTypesRepo ??
              throw new ArgumentNullException(nameof(recognitionTypesRepo));

            _pinRepository = pinsRepo ??
             throw new ArgumentNullException(nameof(recognitionTypesRepo));
        }


    }
}
