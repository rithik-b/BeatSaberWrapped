using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeatSaberWrapped.Models
{
    internal class ScoreSaberEntry
    {
        [JsonProperty]
        public ScoreSaberScoreModel score;

        [JsonProperty]
        public ScoreSaberLeaderboardModel leaderboard;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            score.accuracy = score.modifiedScore / (leaderboard.maxScore * score.multiplier);
        }
    }

    internal class ScoreSaberScoreModel
    {
        [JsonProperty]
        public int rank;

        [JsonProperty]
        public int modifiedScore;

        [JsonProperty]
        public int multiplier;

        [JsonProperty]
        public float pp;

        [JsonProperty]
        public int badCuts;

        [JsonProperty]
        public int missedNotes;

        [JsonProperty]
        public int maxCombo;

        [JsonProperty]
        public bool fullCombo;

        [JsonProperty]
        public DateTime timeSet;

        [JsonIgnore]
        public float accuracy;
    }

    public class ScoreSaberLeaderboardModel
    {
        [JsonProperty]
        public string songName;

        [JsonProperty]
        public string songSubName;

        [JsonProperty] 
        public string songAuthorName;

        [JsonProperty]
        public string levelAuthorName;

        [JsonProperty]
        public ScoreSaberDifficultyModel difficulty;

        [JsonProperty] 
        public int maxScore;

        [JsonProperty]
        public bool ranked;

        [JsonProperty] 
        public DateTime rankedDate;

        [JsonProperty] 
        public string coverImage;
    }

    public class ScoreSaberDifficultyModel
    {
        [JsonProperty]
        public int leaderboardId;

        [JsonProperty] 
        public int difficulty;

        [JsonProperty] 
        public string gameMode;

        [JsonProperty] 
        public string difficultyRaw;
    }

    internal class LeaderboardRankComparer : IComparer<ScoreSaberEntry>
    {
        public int Compare(ScoreSaberEntry x, ScoreSaberEntry y)
        {
            return x.score.rank - y.score.rank;
        }
    }

    internal class LeaderboardAccuracyComparer : IComparer<ScoreSaberEntry>
    {
        public int Compare(ScoreSaberEntry x, ScoreSaberEntry y)
        {
            if (x.score.accuracy > y.score.accuracy)
            {
                return 1;
            }
            if (x.score.accuracy < y.score.accuracy)
            {
                return -1;
            }
            return 0;
        }
    }
}
