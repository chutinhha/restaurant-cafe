﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCLichBieuKhongDinhKy.xaml
    /// </summary>
    public partial class UCLichBieuKhongDinhKy : UserControl
    {
        private Data.Transit mTransit = null;
        public UCLichBieuKhongDinhKy(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnMoi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
