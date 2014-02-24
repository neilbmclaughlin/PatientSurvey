using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FluentAssertions;
using PatientSurvey.Controllers;
using PatientSurvey.Models;
using PatientSurvey.Services;
using TechTalk.SpecFlow;

// ReSharper disable PossibleNullReferenceException
namespace PatientSurvey.AcceptanceTests.Steps
{
    [Binding]
    public class Steps
    {
        private ActionResult _actionResult;


        [Given(@"the following survey data:")]
        public void GivenTheFollowingSurveyData(Table table)
        {
            var filename = string.Format("{0}/survey_data.csv", Path.GetTempPath());

            File.Delete(filename);
            var sb = new StringBuilder();
            foreach (var row in table.Rows)
            {
                sb.AppendLine(String.Join(",", row.Values));
            }

            File.AppendAllText(filename, sb.ToString());
        }

        [When(@"I view the hospital list")]
        public void WhenIViewTheHospitalList()
        {
            var ratingsProcessingService = new RatingsProcessingService();
            var ratingsRepository = new RatingsRepository();
            var surveyService = new SurveyService(ratingsRepository, ratingsProcessingService);
            var controller = new HospitalsController(surveyService);
            _actionResult = controller.Index();
        }

        [Then(@"hospitals '(.*)' should be in the list")]
        public void ThenHospitalsShouldBeInTheList(string hospitalList)
        {
            _actionResult.Should().BeOfType<ViewResult>();

            var viewResult = _actionResult as ViewResult;

            viewResult.Model.Should().BeOfType<SurveyResults>();

            var surveyResult = viewResult.Model as SurveyResults;

            surveyResult.Should().NotBeNull();
            surveyResult.Ratings.Count().Should().Be(2);

            hospitalList
                .Split(',')
                .ToList()
                .ForEach(h => surveyResult.Ratings.Should().Contain(r => r.HospitalName == h, string.Format("Hospital {0} not found in ratings", h)));

        }
        // ReSharper restore PossibleNullReferenceException

    }
}
