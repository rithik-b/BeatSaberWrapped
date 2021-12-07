using System.Collections.Generic;

namespace BeatSaberWrapped.Models
{
    internal class LocalLevelData
    {
        public readonly IPreviewBeatmapLevel beatmapLevel;
        public readonly int playCount;
        public readonly int highScore;
        
        public LocalLevelData(IPreviewBeatmapLevel beatmapLevel, int playCount, int highScore)
        {
            this.beatmapLevel = beatmapLevel;
            this.playCount = playCount;
            this.highScore = highScore;
        }
    }

    internal class PlayCountComparerIHardlyKnowHer : IComparer<LocalLevelData>
    {
        public int Compare(LocalLevelData x, LocalLevelData y)
        {
            return x.playCount - y.playCount;
        }
    }
}
