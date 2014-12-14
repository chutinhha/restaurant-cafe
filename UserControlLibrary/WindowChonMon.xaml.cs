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
    /// Interaction logic for WindowChonMon.xaml
    /// </summary>
    public partial class WindowChonMon : Window
    {
        public Data.BOMenuKichThuocMon _ItemKichThuocMon = null;
        public Data.BOMenuMon _ItemMon = null;
        private Data.Transit mTransit = null;

        bool IsMon = true;
        public WindowChonMon(Data.Transit transit, bool isMon)
        {
            InitializeComponent();
            mTransit = transit;
            IsMon = isMon;
            uCMenu._IsBanHang = !IsMon;
            uCMenu._OnEventMenuMon += new UCMenu.EventMenuMon(uCMenu__OnEventMenuMon);
            uCMenu._OnEventMenuKichThuocMon += new UCMenu.EventMenuKichThuocMon(uCMenu__OnEventMenuKichThuocMon);
            uCMenu.SetTransit(mTransit);
            uCMenu._IsDanhSachKhuyenMai = false;

        }
        public WindowChonMon(Data.Transit transit, bool isMon, bool IsSoLuongChoPhepTonKho, bool IsSoLuongKhongChoPhepTonKho, bool IsTonKho)
        {
            InitializeComponent();
            mTransit = transit;
            IsMon = isMon;
            uCMenu._IsBanHang = !IsMon;
            uCMenu._OnEventMenuMon += new UCMenu.EventMenuMon(uCMenu__OnEventMenuMon);
            uCMenu._OnEventMenuKichThuocMon += new UCMenu.EventMenuKichThuocMon(uCMenu__OnEventMenuKichThuocMon);
            uCMenu._IsSoLuongChoPhepTonKho = IsSoLuongChoPhepTonKho;
            uCMenu._IsSoLuongKhongChoPhepTonKho = IsSoLuongKhongChoPhepTonKho;
            uCMenu._IsTonKho = IsTonKho;
            uCMenu.SetTransit(mTransit);
            uCMenu._IsDanhSachKhuyenMai = false;

        }

        void uCMenu__OnEventMenuKichThuocMon(Data.BOMenuKichThuocMon ob)
        {
            if (!IsMon)
            {
                _ItemKichThuocMon = ob;
                DialogResult = true;
            }
        }

        void uCMenu__OnEventMenuMon(Data.BOMenuMon ob)
        {
            if (IsMon)
            {
                _ItemMon = ob;
                DialogResult = true;
            }
        }

        private void btnChonMon_Click(object sender, RoutedEventArgs e)
        {
            if (_ItemMon != null)
            {
                DialogResult = true;
            }
            else if (_ItemKichThuocMon != null)
            {
                DialogResult = true;
            }
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
