using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using BeatSaberWrapped.UI;
using System;
using Zenject;

namespace BeatSaberWrapped
{
    internal class MenuButtonManager : IInitializable, IDisposable
    {
        private readonly MenuButton menuButton;
        private readonly MainFlowCoordinator mainFlowCoordinator;
        private readonly WrappedFlowCoordinator wrappedFlowCoordinator;

        public MenuButtonManager(MainFlowCoordinator mainFlowCoordinator, WrappedFlowCoordinator wrappedFlowCoordinator)
        {
            menuButton = new MenuButton("Wrapped", "Your year in Beat Saber!", MenuButtonClicked, true);
            this.mainFlowCoordinator = mainFlowCoordinator;
            this.wrappedFlowCoordinator = wrappedFlowCoordinator;
        }

        public void Initialize()
        {
            MenuButtons.instance.RegisterButton(menuButton);
        }
        public void Dispose()
        {
            if (MenuButtons.IsSingletonAvailable)
            {
                MenuButtons.instance.UnregisterButton(menuButton);
            }
        }

        private void MenuButtonClicked()
        {
            mainFlowCoordinator.PresentFlowCoordinator(wrappedFlowCoordinator);
        }
    }
}
