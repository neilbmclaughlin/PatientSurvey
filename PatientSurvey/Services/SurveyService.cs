using System.Collections.Generic;
using System.Linq;
using PatientSurvey.Controllers;
using PatientSurvey.Models;

namespace PatientSurvey.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly IRatingsRepository _ratingsRepository;
        private readonly IRatingsProcessingService _ratingsProcessingService;

        public SurveyService(IRatingsRepository ratingsRepository, IRatingsProcessingService ratingsProcessingService)
        {
            _ratingsRepository = ratingsRepository;
            _ratingsProcessingService = ratingsProcessingService;
        }

        public IEnumerable<HospitalRating> GetSurveySummary()
        {
            var list = _ratingsRepository.GetAll().ToList();



            var hospitalRatings = _ratingsProcessingService.GetHospitalRatings(list);

            return hospitalRatings;

        }


    }
}