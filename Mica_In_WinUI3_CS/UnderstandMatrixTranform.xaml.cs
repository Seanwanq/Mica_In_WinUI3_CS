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
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UnderstandMatrixTranform : Window
    {
        public UnderstandMatrixTranform()
        {
            this.InitializeComponent();
            Title = "Understand MatrixTransform";
        }

        double m11_value = 1;
        double m12_value = 0;
        double m21_value = 0;
        double m22_value = 1;
        double OffsetX_value = 0;
        double OffsetY_value = 0;
        private void Move_m11(object sender, PointerRoutedEventArgs e)
        {
            m11_value = m11.Value;
            matrixTrans.Matrix = new Matrix(m11_value, m12_value, m21_value, m22_value, OffsetX_value, OffsetY_value);
        }
        private void Move_m12(object sender, PointerRoutedEventArgs e)
        {
            m12_value = m12.Value;
            matrixTrans.Matrix = new Matrix(m11_value, m12_value, m21_value, m22_value, OffsetX_value, OffsetY_value);
        }
        private void Move_m21(object sender, PointerRoutedEventArgs e)
        {
            m21_value = m21.Value;
            matrixTrans.Matrix = new Matrix(m11_value, m12_value, m21_value, m22_value, OffsetX_value, OffsetY_value);
        }
        private void Move_m22(object sender, PointerRoutedEventArgs e)
        {
            m22_value = m22.Value;
            matrixTrans.Matrix = new Matrix(m11_value, m12_value, m21_value, m22_value, OffsetX_value, OffsetY_value);
        }
        private void Move_OffsetX(object sender, PointerRoutedEventArgs e)
        {
            OffsetX_value = OffsetX.Value;
            matrixTrans.Matrix = new Matrix(m11_value, m12_value, m21_value, m22_value, OffsetX_value, OffsetY_value);
        }
        private void Move_OffsetY(object sender, PointerRoutedEventArgs e)
        {
            OffsetY_value = OffsetY.Value;
            matrixTrans.Matrix = new Matrix(m11_value, m12_value, m21_value, m22_value, OffsetX_value, OffsetY_value);
        }




    }
}
