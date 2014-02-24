using System.Collections.Generic;
using PatientSurvey.Models;

namespace PatientSurvey.Services
{
    public interface ISurveyService
    {
        IEnumerable<HospitalRating> GetSurveySummary();
    }
}