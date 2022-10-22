using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Runtime.InteropServices;
using WinRT;
using Windows.UI;
using Windows.UI.WindowManagement;
using Windows.Graphics.Imaging;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;
using Mica_In_WinUI3_CS;
using Microsoft.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Mica_In_WinUI3_CS
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestBackground : Window
    {
        WindowsSystemDispatcherQueueHelper m_wsdqHelper;
        Microsoft.UI.Composition.SystemBackdrops.MicaController m_micaController;
        Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController m_acrylicController;
        Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration m_configurationSource;

        bool TrySetAcrylicBackdrop()
        {
            if (Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController.IsSupported())
            {
                m_wsdqHelper = new WindowsSystemDispatcherQueueHelper();
                m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController();

                // Hooking up the policy object
                m_configurationSource = new Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration();
                this.Activated += Window_Activated;
                this.Closed += Window_Closed;
                ((FrameworkElement)this.Content).ActualThemeChanged += Window_ThemeChanged;

                // Initial configuration state.
                m_configurationSource.IsInputActive = true;
                SetConfigurationSourceTheme();

                m_acrylicController = new Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController();

                // Enable the system backdrop.
                // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
                m_acrylicController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
                m_acrylicController.SetSystemBackdropConfiguration(m_configurationSource);
                return true; // succeeded
            }

            return false; // Acrylic is not supported on this system

        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            backgroundColorChangeAutomatically = false;
            // Make sure any Mica/Acrylic controller is disposed so it doesn't try to
            // use this closed window.
            if (m_acrylicController != null)
            {
                m_acrylicController.Dispose();
                m_acrylicController = null;
            }
            this.Activated -= Window_Activated;
            m_configurationSource = null;
        }

        private void Window_ThemeChanged(FrameworkElement sender, object args)
        {
            if (m_configurationSource != null)
            {
                SetConfigurationSourceTheme();
            }
        }

        private void SetConfigurationSourceTheme()
        {
            switch (((FrameworkElement)this.Content).ActualTheme)
            {
                case ElementTheme.Dark: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
                case ElementTheme.Light: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light; break;
                case ElementTheme.Default: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Default; break;
            }
        }


        public TestBackground()
        {
            this.InitializeComponent();

            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            var size = new Windows.Graphics.SizeInt32();
            size.Width = 2500;
            size.Height = 1200;

            appWindow.Resize(size);

            var currentWindowWidth = appWindow.Size.Width;

            TrySetAcrylicBackdrop();



            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            background.Background = backgroundColor;
            colorPicker.Color = winuiColor;
            AppTitleBar.Background = backgroundColor;

            AutoChangeBackground();

        }

        static byte alpha = 255;
        static byte red = 255;
        static byte green = 165;
        static byte blue = 96;
        

        static Color winuiColor = Windows.UI.Color.FromArgb(alpha, red, green, blue);
        Brush backgroundColor = new SolidColorBrush(winuiColor);
        //Brush acrylicBackdropBrush = new;

        bool backgroundColorChangeAutomatically = true;

        bool rPlus = true;
        bool gPlus = true;
        bool bPlus = true;

        private async void AutoChangeBackground()
        {
            while (backgroundColorChangeAutomatically)
            {
                if (red == 255)
                {
                    rPlus = false;
                }
                else if(red == 0)
                {
                    rPlus = true;
                }
                if (green == 255)
                {
                    gPlus = false;
                }
                else if(green == 0)
                {
                    gPlus = true;
                }

                if (blue == 255)
                {
                    bPlus = false;
                }
                else if(blue == 0)
                {
                    bPlus = true;
                }


                if (rPlus)
                {
                    red++;
                }
                else
                {
                    red--;
                }

                if (gPlus)
                {
                    green++;
                }
                else
                {
                    green--;
                }
                if (bPlus)
                {
                    blue++;
                }
                else
                {
                    blue--;
                }



                               
                await Task.Delay(1);
                winuiColor = Windows.UI.Color.FromArgb(alpha, red, green, blue);
                backgroundColor = new SolidColorBrush(winuiColor);
                background.Background = backgroundColor;
                AppTitleBar.Background = backgroundColor;
                colorPicker.Color = Windows.UI.Color.FromArgb(alpha, red, green, blue);
            }
        }


        private void SetBackgrounColor(ColorPicker sender, ColorChangedEventArgs args)
        {
            //await Task.Delay(5);
            //backgroundColorChangeAutomatically = false;
            background.Background = new SolidColorBrush(colorPicker.Color);
            AppTitleBar.Background = new SolidColorBrush(colorPicker.Color);
            red = colorPicker.Color.R;
            green = colorPicker.Color.G;
            blue = colorPicker.Color.B;
            alpha = colorPicker.Color.A;
        }

        private static string autoState = "starts";

        private void ToggleAutoBackground(object sender, RoutedEventArgs e)
        {
            if (backgroundColorChangeAutomatically)
            {
                backgroundColorChangeAutomatically = false;
                autoState = "stops";
            }
            else
            {
                backgroundColorChangeAutomatically = true;
                autoState = "starts";
                AutoChangeBackground();
            }

            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText($"Toggle button set to \"{backgroundColorChangeAutomatically.ToString()}\"")
                .AddText($"Automatic background changing {autoState}")
                .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater
        }
    }
}
