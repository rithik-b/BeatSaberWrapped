using BeatSaberWrapped.Sources;
using BeatSaberWrapped.UI;
using Zenject;
using SiraUtil;

namespace BeatSaberWrapped.Installers
{
    internal class WrappedMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<LocalLevelDataSource>().AsSingle();
            Container.Bind<ScoreSaberDataSource>().AsSingle();
            Container.Bind<HitbloqDataSource>().AsSingle();

            Container.BindInterfacesTo<MenuButtonManager>().AsSingle();
            Container.Bind<WrappedFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<WrappedViewController>().FromNewComponentAsViewController().AsSingle();
        }
    }
}
