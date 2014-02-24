using System.Collections.Generic;
using PatientSurvey.Models;

namespace PatientSurvey.Services
{
    public interface IRatingsRepository
    {
        IEnumerable<PatientHospitalRating> GetAll();
    }
}