using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowThongTinCongTy.xaml
    /// </summary>
    public partial class WindowThongTinCongTy : Window
    {
        private Data.Transit mTransit = null;
        public WindowThongTinCongTy(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }
    }
}
