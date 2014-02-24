using System;
using System.Collections.Generic;
using System.Linq;
using PatientSurvey.Models;

namespace PatientSurvey.Services
{
    public class RatingsProcessingService :IRatingsProcessingService
    {

        public IEnumerable<HospitalRating> GetHospitalRatings(List<PatientHospitalRating> patientHospitalRatings)
        {
            return patientHospitalRatings
                .GroupBy(hpr => hpr.HospitalName)
                .Select(g => new HospitalRating()
                {
                    HospitalName = g.Key,
                    OverallCareRating = Decimal.Round(AveragingFunction(g), 1)

                });
        }

        private static decimal AveragingFunction(IEnumerable<PatientHospitalRating> ratings)
        {
            return ratings.Average(r => r.CareRating);
        }

    }
}