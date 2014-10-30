using System.Windows;
using System.Windows.Controls;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCMayIn.xaml
    /// </summary>
    public partial class UCMayIn : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.MAYIN mMayIn = null;

        public UCMayIn(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void SetValues()
        {
            if (mMayIn == null)
            {

                txtTieuDeMayIn.Text = "";
                txtSoLanIn.Text = "1";
                ckHopDungTien.IsChecked = false;
                ckChoPhepIn.IsChecked = true;
                if (cbbTenMayIn.Items.Count > 0)
                {
                    cbbTenMayIn.SelectedIndex = 0;
                }
                btnThem.Content = "Thêm máy in";
            }
            else
            {
                txtSoLanIn.Text = mMayIn.SoLanIn.ToString();
                txtTieuDeMayIn.Text = mMayIn.TieuDeIn;
                ckHopDungTien.IsChecked = mMayIn.HopDungTien;
                cbbTenMayIn.SelectedItem = mMayIn.TenMayIn;
                ckChoPhepIn.IsChecked = mMayIn.Visual;
                btnThem.Content = "Cập nhật máy in";
            }
        }

        private void GetValues()
        {
            mMayIn.TieuDeIn = txtTieuDeMayIn.Text;
            mMayIn.SoLanIn = System.Convert.ToInt32(txtSoLanIn.Text);
            mMayIn.HopDungTien = ckHopDungTien.IsChecked;
            mMayIn.TenMayIn = cbbTenMayIn.SelectedItem.ToString();
            mMayIn.Visual = ckChoPhepIn.IsChecked;
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTieuDeMayIn.Text == "")
            {
                lbStatus.Text = "Tiêu đề máy in không được bỏ trống";
                return false;
            }

            if (txtSoLanIn.Text == "")
                txtSoLanIn.Text = "1";
            return true;
        }

        private void lvMayIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mMayIn = (Data.MAYIN)((ListViewItem)lvData.SelectedItems[0]).Tag;
                SetValues();
            }
        }

        private void btnMoi_Click(object sender, RoutedEventArgs e)
        {
            mMayIn = null;
            SetValues();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValues())
            {
                if (mMayIn == null)
                {
                    mMayIn = new Data.MAYIN();
                    GetValues();
                    Data.BOMayIn.Them(mMayIn);
                    lbStatus.Text = "Thêm thành công";
                    LoadDanhSachMayIn();
                    btnMoi_Click(sender, e);
                }
                else
                {
                    GetValues();
                    Data.BOMayIn.Sua(mMayIn);
                    lbStatus.Text = "Cập nhật thành công";
                    LoadDanhSachMayIn();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mMayIn = (Data.MAYIN)((ListViewItem)lvData.SelectedItems[0]).Tag;
                Data.BOMayIn.Xoa(mMayIn.MayInID);
                lbStatus.Text = "Xóa thành công";
            }
        }

        private void LoadDanhSachMayIn()
        {
            System.Collections.Generic.List<Data.MAYIN> lsArray = Data.BOMayIn.GetAll();
            lvData.Items.Clear();
            foreach (Data.MAYIN item in lsArray)
            {
                ListViewItem li = new ListViewItem();
                li.Content = item;
                li.Tag = item;
                lvData.Items.Add(li);
            }
        }

        private void LoadPrinter()
        {
            foreach (string s in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbbTenMayIn.Items.Add(s);
            }
            if (cbbTenMayIn.Items.Count > 0)
            {
                cbbTenMayIn.SelectedIndex = 0;
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mMayIn = null;
            SetValues();
            LoadPrinter();
            LoadDanhSachMayIn();
        }
    }
}