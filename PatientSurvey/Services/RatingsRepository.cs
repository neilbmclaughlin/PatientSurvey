using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PatientSurvey.Models;

namespace PatientSurvey.Services
{
    public class RatingsRepository : IRatingsRepository
    {
        public IEnumerable<PatientHospitalRating> GetAll()
        {
            var filename = string.Format("{0}/survey_data.csv", Path.GetTempPath());

            var allLines = File.ReadAllLines(filename);

            return allLines.Select(GetPatientHospitalRating);
            
        }

        private static PatientHospitalRating GetPatientHospitalRating(string line)
        {
            var data = line.Split(',');
            var careRating = decimal.Parse(data[6].Split('/')[0]) / decimal.Parse(data[6].Split('/')[1]);


            return new PatientHospitalRating
            {
                PatientFirstName = data[0],
                PatientSurname = data[1],
                HospitalName = data[2],
                CareRating = careRating
            };
        }
    }
}