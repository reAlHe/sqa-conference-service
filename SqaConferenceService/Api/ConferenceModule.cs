using Nancy;
using SqaConferenceService.Service;

namespace SqaConferenceService.Api
{
    public sealed class ConferenceModule : NancyModule
    {
        private readonly IConferenceService _conferenceService;
        
        public ConferenceModule(IConferenceService conferenceService) : base("/sqadays")
        {
            _conferenceService = conferenceService;
            
            Get("/{conferencename}/speakers", async parameters =>
            {
                string conferenceName = parameters.conferenceName;
                var speakers = await _conferenceService.FetchSpeakersForConferenceAsync(conferenceName);
                return Response.AsJson(speakers).WithStatusCode(HttpStatusCode.OK);
            });
        }
    }
}
