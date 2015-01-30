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
    /// Interaction logic for UCGiamGiaNhom.xaml
    /// </summary>
    public partial class UCGiamGiaNhom : UserControl
    {
        private Data.KaraokeEntities mKaraokeEntities;
        private IQueryable<Data.MENUNHOM> mQueryMenuNhom;
        public UCGiamGiaNhom()
        {
            InitializeComponent();
            mKaraokeEntities = new Data.KaraokeEntities();
        }        

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoaiNhom();
        }

        private void LoadLoaiNhom()
        {
            cboLoaiNhom.ItemsSource = Data.BOMenuLoaiNhom.GetAll(mKaraokeEntities);
            cboLoaiNhom.DisplayMemberPath = "TenLoaiNhom";
            cboLoaiNhom.SelectedValuePath = "LoaiNhomID";
        }

        private void cboLoaiNhom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboLoaiNhom.SelectedItem!=null)
            {
                var nhom = cboLoaiNhom.SelectedItem as Data.MENULOAINHOM;
                lvData.ItemsSource=mQueryMenuNhom = Data.BOMenuNhom.GetAll(mKaraokeEntities,nhom);
            }
        }        
        private void btnHuyThayDoi_Click(object sender, RoutedEventArgs e)
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, lvData.ItemsSource);
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            foreach (var nhom in mQueryMenuNhom)
            {
                if (nhom.EntityState==System.Data.EntityState.Modified)
                {
                    var queryMon = Data.BOMenuMon.GetAll(mKaraokeEntities, nhom);
                    foreach (var item in queryMon)
                    {
                        item.GiamGia = nhom.GiamGia;
                    }
                }
            }
            mKaraokeEntities.SaveChanges();
        }
    }
}
