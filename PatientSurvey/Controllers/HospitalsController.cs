using System.Linq;
using System.Web.Mvc;
using PatientSurvey.Models;
using PatientSurvey.Services;

namespace PatientSurvey.Controllers
{
    public class HospitalsController : Controller
    {
        private readonly ISurveyService _surveyService;

        public HospitalsController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        public ActionResult Index()
        {
            var hospitalRatings = _surveyService.GetSurveySummary().ToArray();

            var surveyResults = new SurveyResults()
            {
                Ratings = hospitalRatings
            };
            return View(surveyResults);
        }
	}
}