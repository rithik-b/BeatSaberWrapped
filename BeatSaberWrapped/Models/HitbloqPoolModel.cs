using Newtonsoft.Json;

namespace BeatSaberWrapped.Models
{
    internal class HitbloqPoolModel
    {
        [JsonProperty("cover")]
        public string coverURL;

        [JsonProperty("shown_name")]
        public string shownName;

        [JsonProperty("banner_title_hide")]
        public bool bannerTitleHide;
    }
}
