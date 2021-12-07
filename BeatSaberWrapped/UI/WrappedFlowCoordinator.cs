using HMUI;
using BeatSaberMarkupLanguage;
using Zenject;
using BeatSaberMarkupLanguage.Attributes;

namespace BeatSaberWrapped.UI
{
    internal class WrappedFlowCoordinator : FlowCoordinator
    {
        private MainFlowCoordinator mainFlowCoordinator;
        private WrappedViewController wrappedViewController;

        [Inject]
        public void Construct(MainFlowCoordinator mainFlowCoordinator, WrappedViewController wrappedViewController)
        {
            this.mainFlowCoordinator = mainFlowCoordinator;
            this.wrappedViewController = wrappedViewController;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            ProvideInitialViewControllers(wrappedViewController);
            wrappedViewController.DismissEvent += DismissClicked;
        }

        protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
            wrappedViewController.DismissEvent -= DismissClicked;

        }

        public void DismissClicked()
        {
            mainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}
