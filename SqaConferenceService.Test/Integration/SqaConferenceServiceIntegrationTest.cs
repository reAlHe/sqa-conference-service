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
