using System.Collections.Generic;
using PatientSurvey.Models;

namespace PatientSurvey.Services
{
    public interface IRatingsProcessingService
    {
        IEnumerable<HospitalRating> GetHospitalRatings(List<PatientHospitalRating> patientHospitalRatings);
    }
}