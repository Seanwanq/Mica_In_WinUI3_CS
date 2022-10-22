﻿using Microsoft.UI.Xaml;
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
using Mica_In_WinUI3_CS;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.





namespace Mica_In_WinUI3_CS
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainWindow : Window
    {
        WindowsSystemDispatcherQueueHelper m_wsdqHelper;
        Microsoft.UI.Composition.SystemBackdrops.MicaController m_micaController;
        Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration m_configurationSource;
        bool TrySetMicaBackdrop()
        {
            if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
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

                m_micaController = new Microsoft.UI.Composition.SystemBackdrops.MicaController();

                // Enable the system backdrop.
                // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
                m_micaController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
                m_micaController.SetSystemBackdropConfiguration(m_configurationSource);
                return true; // succeeded
            }

            return false; // Mica is not supported on this system
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            // Make sure any Mica/Acrylic controller is disposed so it doesn't try to
            // use this closed window.
            if (m_micaController != null)
            {
                m_micaController.Dispose();
                m_micaController = null;
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


        public MainWindow()
        {
            this.InitializeComponent();
            TrySetMicaBackdrop();
            Title = "Welcome!";
        }

        int counter = 0;
        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            string timestr;
            if (counter == 1)
            {
                timestr = "time";
            }
            else
            {
                timestr = "times";
            }
            myButton.Content = "You have clicked " + counter + $" {timestr}.";
        }


        private void CreateNewWindow(object sender, RoutedEventArgs e)
        {
            PaintWindow paintWindow = new PaintWindow();
            paintWindow.Activate();
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {

            counter = 0;
            myButton.Content = "Click Me";
        }

        private void CreateUMTWindow(object sender, RoutedEventArgs e)
        {
            UnderstandMatrixTranform umtWindow = new();
            umtWindow.Activate();
        }

        private void CreateWebView(object sender, RoutedEventArgs e)
        {
            webview2 wv2 = new();
            wv2.Activate();
        }

        private void CreateBackground(object sender, RoutedEventArgs e)
        {
            TestBackground tbg = new();
            tbg.Activate();
        }

        private void CreateTestFullScreen(object sender, RoutedEventArgs e)
        {
            TestFullScreen testfullscreen = new();
            testfullscreen.Activate();
        }

        private void CreateAcrylicBlurMicaTransparentWindow(object sender, RoutedEventArgs e)
        {
            AcrylicBlurMicaTransparent abmtWindow = new();
            abmtWindow.Activate();
        }
    }
}
