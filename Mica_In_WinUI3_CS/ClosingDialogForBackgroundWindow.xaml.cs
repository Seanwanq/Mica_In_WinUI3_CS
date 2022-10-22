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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Mica_In_WinUI3_CS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClosingDialogForBackgroundWindow : Page
    {
        public ClosingDialogForBackgroundWindow()
        {
            this.InitializeComponent();
        }

        //public async void ShowClosingDialog_Click(object sender, RoutedEventArgs e, Window c_window)
        //{
        //    ContentDialog dialog = new ContentDialog();
        //    dialog.XamlRoot = this.XamlRoot;
        //    dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        //    dialog.Title = "Close the window?";
        //    dialog.PrimaryButtonText = "Yes";
        //    dialog.PrimaryButtonClick(c_window.Close());
        //}
    }
}
