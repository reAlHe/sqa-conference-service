using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using SqaConferenceService.Models;
using Xunit;

namespace SqaConferenceService.Test.Integration
{
    public class SqaConferenceServiceIntegrationTest : IClassFixture<SqaConferenceServiceIntegrationTestSetup>
    {
        private SqaConferenceServiceIntegrationTestSetup _sqaConferenceServiceIntegrationTestSetup;

        public SqaConferenceServiceIntegrationTest(
            SqaConferenceServiceIntegrationTestSetup sqaConferenceServiceIntegrationTestSetup)
        {
            _sqaConferenceServiceIntegrationTestSetup = sqaConferenceServiceIntegrationTestSetup;
        }

        [Fact]
        public async Task GetConferenceSpeakersSuccessfully()
        {
            var speakerDetails1 = new Speaker(1, "Alexander", "Henze", "MaibornWolff");
            var speakerDetails2 = new Speaker(2, "Maik", "Nogens", "MaibornWolff");
            var speakerDetails3 = new Speaker(3, "Joachim", "Basler", "MaibornWolff");

            var browser = new Browser(new SqaConferenceServiceBootstrapper());
            _sqaConferenceServiceIntegrationTestSetup.SpeakersServiceMock.Given("There are speakers details available for speakers with id 1, 2 and 3")
                .UponReceiving("A request to retrieve information about the speakers")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/speakers",
                    Query = "id=1,2,3"
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        {"Content-Type", "application/json; charset=utf-8"}
                    },
                    Body = new[]
                    {
                        speakerDetails1, 
                        speakerDetails2, 
                        speakerDetails3
                    }
                });

            var response = await browser.Get("/sqadays/sqa-days-minsk/speakers");
            var body = JArray.Parse(response.Body.AsString());

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            body.Count.Should().Be(3);
            body[0]["id"].Value<string>().Should().Be("1");
            body[0]["name"].Value<string>().Should().Be("Alexander");
            body[0]["surname"].Value<string>().Should().Be("Henze");
            body[0]["company"].Value<string>().Should().Be("MaibornWolff");
            
            body[1]["id"].Value<string>().Should().Be("2");
            body[1]["name"].Value<string>().Should().Be("Maik");
            body[1]["surname"].Value<string>().Should().Be("Nogens");
            body[1]["company"].Value<string>().Should().Be("MaibornWolff");
            
            body[2]["id"].Value<string>().Should().Be("3");
            body[2]["name"].Value<string>().Should().Be("Joachim");
            body[2]["surname"].Value<string>().Should().Be("Basler");
            body[2]["company"].Value<string>().Should().Be("MaibornWolff");
        }
    }

    public class SqaConferenceServiceIntegrationTestSetup : IDisposable
    {
        public readonly IMockProviderService SpeakersServiceMock;

        private readonly PactBuilder _pactBuilder;

        public SqaConferenceServiceIntegrationTestSetup()
        {
            _pactBuilder = new PactBuilder(new PactConfig {SpecificationVersion = "2.0.0"});
            _pactBuilder.ServiceConsumer("sqa-conference-service")
                .HasPactWith("sqa-speakers-service");
            SpeakersServiceMock = _pactBuilder.MockService(
                5001, new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
        }

        public void Dispose()
        {
            _pactBuilder.Build();
        }
    }
}
