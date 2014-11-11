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

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCDinhLuong.xaml
    /// </summary>
    public partial class UCDinhLuong : UserControl
    {
        private Data.Transit mTransit = null;
        public UCDinhLuong()
        {
            InitializeComponent();            
        }

        public void Init(Data.Transit transit)
        {
            mTransit = transit;
            uCMenu.OnEventMenu += new UCMenu.EventMenu(uCMenu_OnEventMenu);
            uCMenu.Init(mTransit);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        void uCMenu_OnEventMenu(object ob)
        {
            if (ob is Data.MENUKICHTHUOCMON)
            {
                uCDanhSachDinhLuong.Init((Data.MENUKICHTHUOCMON)ob, mTransit);
                uCDanhSachDinhLuong.LoadDanhSach();
            }
        }
    }
}
