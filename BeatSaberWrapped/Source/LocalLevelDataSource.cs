using BeatSaberWrapped.Models;
using SongCore;
using System.Collections.Generic;

namespace BeatSaberWrapped.Source
{
    internal class LocalLevelDataSource
    {
        private readonly PlayerDataModel playerDataModel;
        private List<LocalLevelData> cachedPlayCountBeatmapLevels;

        public LocalLevelDataSource(PlayerDataModel playerDataModel)
        {
            this.playerDataModel = playerDataModel;
        }

        private List<LocalLevelData> FetchBeatmapLevels()
        {
            if (cachedPlayCountBeatmapLevels != null)
            {
                cachedPlayCountBeatmapLevels = new List<LocalLevelData>();
                foreach (PlayerLevelStatsData levelStatsData in playerDataModel.playerData.levelsStatsData)
                {
                    IPreviewBeatmapLevel beatmapLevel = Loader.GetLevelById(levelStatsData.levelID);
                    cachedPlayCountBeatmapLevels.Add(new LocalLevelData(beatmapLevel, levelStatsData.playCount, levelStatsData.highScore));
                }
            }
            return cachedPlayCountBeatmapLevels;
        }

        public List<LocalLevelData> MapsSortedByPlayCount()
        {
            List<LocalLevelData> playCountBeatmapLevels = new List<LocalLevelData>(FetchBeatmapLevels());
            playCountBeatmapLevels.Sort(new PlayCountComparerIHardlyKnowHer());
            return playCountBeatmapLevels;
        }
    }
}
