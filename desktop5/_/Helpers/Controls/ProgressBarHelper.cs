using sunamo.Helpers.Number;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace desktop
{
    public class ProgressBarHelper : Window, IProgressBarHelper
    {
        ProgressBar pb = null;
        PercentCalculator percentCalculator;
        UIElement ui = null;

        public IProgressBarHelper CreateInstance(ProgressBar pb, double overall, DispatcherObject ui)
        {
            return new ProgressBarHelper(pb, overall, ui);
        }

        public ProgressBarHelper(ProgressBar pb, double overall, DispatcherObject ui)
        {
            this.pb = pb;
            this.ui = pb;
            ui.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, 0d);
            ui.Dispatcher.Invoke(IH.delegateChangeVisibilityUIElementWpf, pb, System.Windows.Visibility.Visible);
            percentCalculator = new PercentCalculator(overall);
        }

        public void Done()
        {
            ui.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, 100d);
            ui.Dispatcher.Invoke(IH.delegateChangeVisibilityUIElementWpf, pb, System.Windows.Visibility.Collapsed);
        }

        public void DonePartially()
        {
            percentCalculator.last += percentCalculator.onePercent;
            ui.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, percentCalculator.last);
        }
    }
}