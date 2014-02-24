using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientSurvey.Models
{
    public class HospitalRating
    {
        public string HospitalName { get; set; }
        public decimal OverallCareRating { get; set; }
    }
}