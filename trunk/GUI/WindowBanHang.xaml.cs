using System.Windows;
using System.Windows.Controls;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowBanHang.xaml
    /// </summary>
    public partial class WindowBanHang : Window
    {
        private Data.Transit mTransit = null;

        public WindowBanHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uCMenuBanHang.OnEventMenu += new ControlLibrary.UCMenu.EventMenu(uCMenuBanHang_OnEventMenu);
            uCMenuBanHang.Init(mTransit);
        }

        private void uCMenuBanHang_OnEventMenu(object ob)
        {
            if (ob is Data.MENUKICHTHUOCMON)
            {
                Data.POSChiTietBanHang item = new Data.POSChiTietBanHang();
                Data.MENUKICHTHUOCMON ktm = (Data.MENUKICHTHUOCMON)ob;
                item.MENUKICHTHUOCMON = new Data.MENUKICHTHUOCMON();
                item.MENUKICHTHUOCMON.GiaBanMacDinh = ktm.GiaBanMacDinh;
                item.MENUKICHTHUOCMON.SoLuongBanBan = ktm.SoLuongBanBan;
                item.SoLuongBan = ktm.SoLuongBanBan;
                item.GiaBan = ktm.GiaBanMacDinh;
                item.MENUKICHTHUOCMON.TenLoaiBan = ktm.TenLoaiBan;
                item.MENUKICHTHUOCMON.MENUMON = new Data.MENUMON();
                item.MENUKICHTHUOCMON.MENUMON.TenDai = ktm.MENUMON.TenDai;
                AddChiTietBanHang(item);
            }
        }

        private Data.CHITIETBANHANG mChiTietBanHang = null;

        private void btnChucNang_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddChiTietBanHang(Data.POSChiTietBanHang item)
        {
            ListViewItem li = new ListViewItem();
            li.Content = item;
            li.Tag = item;
            lvData.Items.Add(item);
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}