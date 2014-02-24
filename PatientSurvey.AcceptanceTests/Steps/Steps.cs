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

        [Then(@"I should see the following average overall care ratings:")]
        public void ThenIShouldSeeTheFollowingAverageOverallCareRatings(Table table)
        {

            var viewResult = _actionResult as ViewResult;
            var surveyResult = viewResult.Model as SurveyResults;

            foreach (var row in table.Rows)
            {
                var ratings = row["Average rating of the care received"].Split('/');
                var expectedRating = decimal.Parse(ratings[0]) / decimal.Parse(ratings[1]);
                var hospitalRating = surveyResult.Ratings.FirstOrDefault(r => r.HospitalName == row["Hospital"]);
                hospitalRating.OverallCareRating.Should().Be(expectedRating);
            }
        }

        // ReSharper restore PossibleNullReferenceException

    }
}
