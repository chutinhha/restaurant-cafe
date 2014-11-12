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
        private Data.Transit mTransit = null;

        public UCMenuSetMayIn()
        {
            InitializeComponent();
        }

        public delegate void OnExit();

        public event OnExit OnEventExit;

        public Data.MENUMON _Mon { get; set; }

        public void Init(Data.Transit transit)
        {
            mTransit = transit;
            mTransit.KaraokeEntities = new Data.KaraokeEntities();
            mTransit.KaraokeEntities.MENUITEMMAYINs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            if (OnEventExit == null)
                btnHuy.Visibility = System.Windows.Visibility.Hidden;
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

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            if (OnEventExit != null)
            {
                OnEventExit();
            }
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetValues(_Mon);
        }

        private class ShowData
        {
            public ShowData()
            {
                Values = false;
                MayInID = 0;
                MonID = 0;
                TenMayIn = "";
            }

            public int MayInID { get; set; }

            public int MonID { get; set; }

            public string TenMayIn { get; set; }

            public bool Values { get; set; }
        }
    }
}