using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SqaConferenceService.Models;

namespace SqaConferenceService.Service
{
    /// <summary>
    /// This class is responsible for retrieving conference details like speakers.
    /// </summary>
    public class ConferenceService : IConferenceService
    {
        private readonly HttpClient _client;

        private const string SpeakerDetailsServiceUrl = "http://localhost:5001/speakers";

        public ConferenceService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        /// <summary>
        /// Fetches all speakers with details.
        /// </summary>
        /// <param name="conferenceName">The name of the conference</param>
        /// <returns>A list containing all speakers with details</returns>
        public async Task<IEnumerable<Speaker>> FetchSpeakersForConferenceAsync(string conferenceName)
        {
            var speakerIds = FetchSpeakerIdsForConference(conferenceName);
            return await FetchSpeakerDetailsAsync(speakerIds);
        }

        /// <summary>
        /// Fetches speakers details for a speaker ids.
        /// </summary>
        /// <param name="speakerIds">A list of speaker ids</param>
        /// <returns>Speaker details for given speakers</returns>
        private async Task<IEnumerable<Speaker>> FetchSpeakerDetailsAsync(IEnumerable<int> speakerIds)
        {
            var queryUrl = string.Concat(SpeakerDetailsServiceUrl, $"?id={string.Join(",", speakerIds)}");
            var response = await _client.GetAsync(queryUrl);
            return JsonConvert.DeserializeObject<IEnumerable<Speaker>>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Lists all speaker ids for given conference.
        /// </summary>
        /// <param name="conferenceName">The name of the conference</param>
        /// <returns>A list of all speaker ids</returns>
        private static IEnumerable<int> FetchSpeakerIdsForConference(string conferenceName)
        {
            // Do something depending on the conference name, skipped here for readability reasons
            return new[] {1, 2, 3};
        }
    }
}
