using System.Collections.Generic;
using System.Threading.Tasks;
using SqaConferenceService.Models;

namespace SqaConferenceService.Service
{
    public interface IConferenceService
    {
        Task<IEnumerable<Speaker>> FetchSpeakersForConferenceAsync(string conferenceName);
    }
}
