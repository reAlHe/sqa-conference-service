using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using FluentAssertions;
using RichardSzalay.MockHttp;
using SqaConferenceService.Models;
using SqaConferenceService.Service;
using SqaConferenceService.Test.Extensions;
using Xunit;

namespace SqaConferenceService.Test.Service
{
    public class ConferenceServiceTest
    {
        [Fact]
        public async Task FetchSpeakersAsyncReturnsListOfSpeakersCorrectly()
        {
            const string conferenceName = "sqa-days-minsk";
            var speakerDetails1 = new Speaker(1, "Alexander", "Henze", "MaibornWolff");
            var speakerDetails2 = new Speaker(2, "Maik", "Nogens", "MaibornWolff");
            var speakerDetails3 = new Speaker(3, "Joachim", "Basler", "MaibornWolff");

            var speakersDetails = new[] {speakerDetails1, speakerDetails2, speakerDetails3};

            var httpMessageHandlerMock = new MockHttpMessageHandler();
            httpMessageHandlerMock.When("http://localhost:5001/speakers?id=1,2,3").Respond(HttpStatusCode.OK,
                MediaTypeNames.Application.Json, speakersDetails.AsJson());
            var client = httpMessageHandlerMock.ToHttpClient();

            var conferenceService = new ConferenceService(client);

            var speakersList = await conferenceService.FetchSpeakersForConferenceAsync(conferenceName);

            speakersList.Count().Should().Be(3);
            speakersList.Should().Contain(speakerDetails1);
            speakersList.Should().Contain(speakerDetails2);
            speakersList.Should().Contain(speakerDetails3);
        }
    }
}
