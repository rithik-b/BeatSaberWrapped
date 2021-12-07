using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using System;

namespace BeatSaberWrapped.UI
{
    [HotReload(RelativePathToLayout = @"..\Views\WrappedView.bsml")]
    [ViewDefinition("BeatSaberWrapped.UI.Views.WrappedView.bsml")]
    internal class WrappedViewController : BSMLAutomaticViewController
    {
        public event Action DismissEvent;

        [UIAction("dismiss-click")]
        public void DismissClicked()
        {
            DismissEvent?.Invoke();
        }
    }
}
