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

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowSoDoBanThemSoNhieu.xaml
    /// </summary>
    public partial class WindowSoDoBanThemSoNhieu : Window
    {
        private ControlLibrary.UCFloorPlan mUCFloorPlan;
        public WindowSoDoBanThemSoNhieu(ControlLibrary.UCFloorPlan uc)
        {
            mUCFloorPlan = uc;
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if (txtSoBan.Text!="")
            {
                int soban = Utilities.MoneyFormat.ConvertToInt(txtSoBan.Text);
                if (chkXoaBanCu.IsChecked==true)
                {
                    mUCFloorPlan.RemoveAllTable();
                }
                mUCFloorPlan.AddTable(soban);
                this.DialogResult = true;
            }
            else
            {
                WindowMessageBox.ShowDialog("Nhập số bàn cần thêm");
            }
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
