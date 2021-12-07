using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hitbloq.Entries
{
    internal class HitbloqProfileModel
    {
        [JsonProperty("profile_background")]
        public string profileBackgroundURL;

        [JsonProperty("max_rank")]
        public Dictionary<string, int> maxRanks;
    }

    internal class HitbloqUserID
    {
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public int id = -1;

        [JsonProperty("registered")]
        public bool registered;
    }
}
