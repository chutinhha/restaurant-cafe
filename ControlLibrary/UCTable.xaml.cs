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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for UCTable.xaml
    /// </summary>
    public partial class UCTable : UserControl
    {
        public Data.BAN _Ban { get; set; }
        public UCTable()
        {
            InitializeComponent();
        }
        public void setText(string text)
        {
            lblText.Content = text;
        }
    }
}
