using Hitbloq.Entries;
using SiraUtil;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaberWrapped.Sources
{
    internal class HitbloqDataSource
    {
        private readonly SiraClient siraClient;
        private readonly IPlatformUserModel platformUserModel;
        private const string API_URL = "https://hitbloq.com/api";
        private HitbloqProfileModel cachedUserModel;

        public HitbloqDataSource(SiraClient siraClient, IPlatformUserModel platformUserModel)
        {
            this.siraClient = siraClient;
            this.platformUserModel = platformUserModel;
        }

        public async Task<HitbloqProfileModel> GetUserModel(CancellationToken? cancellationToken)
        {
            if (cachedUserModel == null)
            {
                UserInfo userInfo = await platformUserModel.GetUserInfo();
                try
                {
                    WebResponse webResponse = await siraClient.GetAsync($"{API_URL}/tools/ss_registered/{userInfo.platformUserId}", cancellationToken ?? CancellationToken.None).ConfigureAwait(false);
                    HitbloqUserID hitbloqUserID = Utilities.ParseWebResponse<HitbloqUserID>(webResponse);
                    if (hitbloqUserID.registered)
                    {
                        webResponse = await siraClient.GetAsync($"{API_URL}/users/{hitbloqUserID.id}", cancellationToken ?? CancellationToken.None).ConfigureAwait(false);
                        cachedUserModel = Utilities.ParseWebResponse<HitbloqProfileModel>(webResponse);
                    }
                }
                catch (TaskCanceledException) { }
            }
            return cachedUserModel;
        }
    }
}
