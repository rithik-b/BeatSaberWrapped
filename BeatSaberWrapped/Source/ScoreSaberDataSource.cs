using BeatSaberWrapped.Models;
using SiraUtil;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaberWrapped.Source
{
    internal class ScoreSaberDataSource
    {
        private readonly SiraClient siraClient;
        private readonly IPlatformUserModel platformUserModel;
        private const string API_URL = "https://scoresaber.com/api";

        private ScoreSaberUserModel cachedUserModel;
        private List<ScoreSaberEntry> cachedEntries;

        public ScoreSaberDataSource(SiraClient siraClient, IPlatformUserModel platformUserModel)
        {
            this.siraClient = siraClient;
            this.platformUserModel = platformUserModel;
            cachedEntries = new List<ScoreSaberEntry>();
        }

        public async Task<ScoreSaberUserModel> GetUserModel(CancellationToken? cancellationToken)
        {
            if (cachedUserModel == null)
            {
                UserInfo userInfo = await platformUserModel.GetUserInfo();
                try
                {
                    WebResponse webResponse = await siraClient.GetAsync($"{API_URL}/player/{userInfo.platformUserId}/full", cancellationToken ?? CancellationToken.None).ConfigureAwait(false);
                    cachedUserModel = Utilities.ParseWebResponse<ScoreSaberUserModel>(webResponse);
                }
                catch (TaskCanceledException) { }
            }
            return cachedUserModel;
        }

        private async Task<List<ScoreSaberEntry>> GetRankedScoreSaberEntries(CancellationToken? cancellationToken)
        {
            if (cachedEntries.Count == 0)
            {
                UserInfo userInfo = await platformUserModel.GetUserInfo();
                bool unrankedReached = false;
                int page = 1;
                
                while (!unrankedReached)
                {
                    try
                    {
                        WebResponse webResponse = await siraClient.GetAsync($"{API_URL}/player/{userInfo.platformUserId}/scores?limit=100&sort=top&page={page}", cancellationToken ?? CancellationToken.None).ConfigureAwait(false);
                        List<ScoreSaberEntry> scoreSaberEntries = Utilities.ParseWebResponse<List<ScoreSaberEntry>>(webResponse);

                        foreach (ScoreSaberEntry scoreSaberEntry in scoreSaberEntries)
                        {
                            if (scoreSaberEntry.leaderboard.ranked && scoreSaberEntry.score.timeSet.Year == DateTime.Now.Year)
                            {
                                cachedEntries.Add(scoreSaberEntry);
                            }
                            else
                            {
                                unrankedReached = true;
                                break;
                            }
                        }
                    }
                    catch (TaskCanceledException) { }
                    page++;
                }
            }
            return cachedEntries;
        }

        public async Task<List<ScoreSaberEntry>> GetEntriesSortedByPP(CancellationToken? cancellationToken) =>
            new List<ScoreSaberEntry>(await GetRankedScoreSaberEntries(cancellationToken));

        public async Task<List<ScoreSaberEntry>> GetEntriesSortedByGlobalRank(CancellationToken? cancellationToken)
        {
            List<ScoreSaberEntry> scoreSaberEntries = new List<ScoreSaberEntry>(await GetRankedScoreSaberEntries(cancellationToken));
            scoreSaberEntries.Sort(new LeaderboardRankComparer());
            return scoreSaberEntries;
        }

        public async Task<List<ScoreSaberEntry>> GetEntriesSortedByAccuracy(CancellationToken? cancellationToken)
        {
            List<ScoreSaberEntry> scoreSaberEntries = new List<ScoreSaberEntry>(await GetRankedScoreSaberEntries(cancellationToken));
            scoreSaberEntries.Sort(new LeaderboardAccuracyComparer());
            return scoreSaberEntries;
        }
    }
}
