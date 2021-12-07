using Newtonsoft.Json;

namespace BeatSaberWrapped.Models
{
    internal class ScoreSaberUserModel
    {
        [JsonProperty("profilePicture", NullValueHandling = NullValueHandling.Ignore)]
        public string profilePictureURL;

        [JsonProperty("pp", NullValueHandling = NullValueHandling.Ignore)]
        public float pp;

        [JsonProperty("globalRank", NullValueHandling = NullValueHandling.Ignore)]
        public int globalRank;

        [JsonProperty("countryRank", NullValueHandling = NullValueHandling.Ignore)]
        public int countryRank;
    }
}
