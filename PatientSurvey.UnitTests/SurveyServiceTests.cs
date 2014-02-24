using System.Linq;
using FluentAssertions;
using Moq;
using PatientSurvey.Models;
using PatientSurvey.Services;
using Xunit;


namespace PatientSurvey.UnitTests
{
    public class SurveyServiceTests
    {
        // ReSharper disable PossibleMultipleEnumeration
        // ReSharper disable PossibleNullReferenceException

        [Fact]
        public void SurveyServiceShouldSummariseSurveyResults()
        {
            //Arrange
            var ratingsRepository = new Mock<IRatingsRepository>();
            var ratings = new []
            {
                new PatientHospitalRating
                {
                    HospitalName = "A",
                    PatientFirstName = "Bob",
                    PatientSurname = "Smith",
                    CareRating = 0.7m
                },
                new PatientHospitalRating
                {
                    HospitalName = "A",
                    PatientFirstName = "Bob",
                    CareRating = 0.7m
                },
                new PatientHospitalRating
                {
                    HospitalName = "B",
                    PatientFirstName = "Fred",
                    CareRating = 0.6m
                },
                new PatientHospitalRating
                {
                    HospitalName = "A",
                    PatientFirstName = "Jeff",
                    CareRating = 0.9m
                }
            };
            ratingsRepository.Setup(r => r.GetAll()).Returns(ratings);

            var surveyService = new SurveyService(ratingsRepository.Object, new RatingsProcessingService());

            //Act
            var hospitalRatings = surveyService.GetSurveySummary();

            //Assert
            hospitalRatings.Count().Should().Be(2);
            hospitalRatings
                .FirstOrDefault(r => r.HospitalName == "A")
                .OverallCareRating.Should().Be(0.8m);
            hospitalRatings
                .FirstOrDefault(r => r.HospitalName == "B")
                .OverallCareRating.Should().Be(0.6m);

        }
    }
    // ReSharper restore PossibleNullReferenceException
    // ReSharper restore PossibleMultipleEnumeration
}