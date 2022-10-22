using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Mica_In_WinUI3_CS
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashScreen : Window
    {
        private AppWindow _appWindow;
        private Microsoft.UI.Windowing.OverlappedPresenter _overlappedPresenter;
        private Microsoft.UI.Windowing.DisplayArea displayArea;


        public SplashScreen()
        {
            this.InitializeComponent();
            Title = "Splash Screen";
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);

            _appWindow = AppWindow.GetFromWindowId(myWndId);
            displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(myWndId, Microsoft.UI.Windowing.DisplayAreaFallback.Nearest);

            Windows.Graphics.SizeInt32 size;
            size.Height = 450;
            size.Width = 800;

            _appWindow.Resize(size);


            var CenteredPosition = _appWindow.Position;
            CenteredPosition.X = ((displayArea.WorkArea.Width - _appWindow.Size.Width) / 2);
            CenteredPosition.Y = ((displayArea.WorkArea.Height - _appWindow.Size.Height) / 2);

            _appWindow.Move(CenteredPosition);

            _overlappedPresenter = _appWindow.Presenter as Microsoft.UI.Windowing.OverlappedPresenter;
            _overlappedPresenter?.SetBorderAndTitleBar(false, false);
            _overlappedPresenter.IsResizable = false;
            _overlappedPresenter.IsMinimizable = false;
        }

    }
}
