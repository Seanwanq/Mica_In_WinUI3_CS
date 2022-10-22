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
    public sealed partial class TestFullScreen : Window
    {
        private AppWindow _appWindow;
        private Microsoft.UI.Windowing.OverlappedPresenter _overlappedPresenter;
        public TestFullScreen()
        {
            this.InitializeComponent();
            _appWindow = GetAppWindowForCurrentWindow();

            FullScreenButton.Content = "Enter Full Screen";
        }


        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }

        private void FullScreenState_Click(object sender, RoutedEventArgs e)
        {
            FullScreenButton.Content = "Exit Full Screen";
            if (_appWindow.Presenter.Kind == AppWindowPresenterKind.FullScreen)
            {
                _appWindow.SetPresenter(AppWindowPresenterKind.Default);
                FullScreenButton.Content = "Enter Full Screen";
            }
            else
            {
                _appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
                FullScreenButton.Content = "Exit Full Screen";
            }
        }

        private void MoveWindow_Click(object sender, RoutedEventArgs e)
        {
            Windows.Graphics.PointInt32 position;
            position.X = 120;
            position.Y = 120;
            _appWindow.Move(position);
        }

        private bool hasBorderAndTitleBar = true;
        private void SetBorderAndTitleBar_Click(object sender, RoutedEventArgs e)
        {
            if (_appWindow.Presenter.Kind != AppWindowPresenterKind.FullScreen)
            {
                _overlappedPresenter = _appWindow.Presenter as Microsoft.UI.Windowing.OverlappedPresenter;
                if (hasBorderAndTitleBar)
                {
                    _overlappedPresenter?.SetBorderAndTitleBar(false, false);
                    hasBorderAndTitleBar = false;
                    _overlappedPresenter.IsResizable = false;
                    _overlappedPresenter.IsMinimizable = false;

                }
                else
                {
                    _overlappedPresenter?.SetBorderAndTitleBar(true, true);
                    hasBorderAndTitleBar = true;
                    _overlappedPresenter.IsResizable = true;
                    _overlappedPresenter.IsMinimizable = true;

                }

            }
        }
    }
}
