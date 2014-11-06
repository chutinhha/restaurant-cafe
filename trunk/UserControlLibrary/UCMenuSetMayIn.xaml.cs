using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCMenuSetMayIn.xaml
    /// </summary>
    public partial class UCMenuSetMayIn : UserControl
    {
        public Data.MENUMON _Mon { get; set; }

        public UCMenuSetMayIn()
        {
            InitializeComponent();
        }

        private Data.Transit mTransit = null;

        public void Init(Data.Transit transit)
        {
            mTransit = transit;
            if (OnEventExit == null)
                btnHuy.Visibility = System.Windows.Visibility.Hidden;
        }

        private void LoadDanhSach()
        {
            List<Data.MAYIN> lsMayIn = Data.BOMayIn.GetAll(mTransit);
            List<Data.MENUITEMMAYIN> lsMonMayIn = Data.BOMenuItemMayIn.GetAll(_Mon.MonID, mTransit);
            List<ShowData> lsShowData = new List<ShowData>();
            foreach (Data.MAYIN mi in lsMayIn)
            {
                ShowData item = new ShowData();
                item.TenMayIn = mi.TieuDeIn;
                item.MonID = _Mon.MonID;
                item.MayInID = mi.MayInID;
                if (lsMonMayIn.Exists(s => s.MayInID == mi.MayInID))
                {
                    item.Values = true;
                }
                lsShowData.Add(item);
            }
            lvData.ItemsSource = lsShowData;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            List<Data.MENUITEMMAYIN> lsMonMayIn = new List<Data.MENUITEMMAYIN>();
            foreach (ShowData item in lvData.Items)
            {
                lsMonMayIn.Add(new Data.MENUITEMMAYIN() { MonID = item.MonID, MayInID = item.MayInID, Deleted = !item.Values });
            }
            Data.BOMenuItemMayIn.Luu(lsMonMayIn, mTransit);
            LoadDanhSach();
            MessageBox.Show("Lưu thành công");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetValues(_Mon);
        }

        public void SetValues(Data.MENUMON mon)
        {
            _Mon = mon;
            if (_Mon != null)
            {
                txtTenMon.Text = _Mon.TenDai;
                LoadDanhSach();
            }
        }

        private class ShowData
        {
            public string TenMayIn { get; set; }

            public int MayInID { get; set; }

            public bool Values { get; set; }

            public int MonID { get; set; }

            public ShowData()
            {
                Values = false;
                MayInID = 0;
                MonID = 0;
                TenMayIn = "";
            }
        }

        public delegate void OnExit();

        public delegate void OnMinimized();

        public event OnExit OnEventExit;

        public event OnMinimized OnEventMinimized;

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            if (OnEventExit != null)
            {
                OnEventExit();
            }
        }
    }
}