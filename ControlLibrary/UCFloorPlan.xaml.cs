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
    /// Interaction logic for UCFloorPlan.xaml
    /// </summary>
    public partial class UCFloorPlan : UserControl
    {
        public delegate void EventFloorPlan(POSButtonTable tbl);
        public event EventFloorPlan _OnEventFloorPlan;
        public bool _IsEdit { get; set; }
        private Data.Transit mTransit;        
        public Data.KHU _Khu { get; set; }
        private Data.BOBan mBOBan;
        private Data.BOKhu mBOKhu;
        public UCFloorPlan()
        {
            InitializeComponent();
        }

        public void Init(Data.Transit tran)
        {            
            mTransit = tran;
            mBOBan = new Data.BOBan(mTransit);
            mBOKhu = new Data.BOKhu(mTransit);
        }       
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void LoadAlllStatus()
        {
            var list = Data.BOTableStatus.GetAll(mTransit).ToList();
            foreach (POSButtonTable item in gridFloorPlan.Children)
            {
                if (list.Count==0)
                {
                    break;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (item._Ban.BanID==list[i].TableID)
                    {
                        item._ButtonTableStatusColor = (POSButtonTable.POSButtonTableStatusColor)list[i].Status;
                        i--;
                        break;
                    }
                }
            }
        }
        public void SaveChange()
        {
            for (int i = 0; i < this.gridFloorPlan.Children.Count; i++)                        
            {
                POSButtonTable tbl = (POSButtonTable)this.gridFloorPlan.Children[i];
                if (tbl._Ban.BanID == 0)
                {                    
                    mBOBan.Them(tbl._Ban);
                    tbl._ButtonTableStatus = POSButtonTable.POSButtonTableStatus.None;
                }
                else
                {
                    switch (tbl._ButtonTableStatus)
                    {
                        case POSButtonTable.POSButtonTableStatus.Edit:                            
                            mBOBan.Sua(tbl._Ban);
                            tbl._ButtonTableStatus = POSButtonTable.POSButtonTableStatus.None;
                            break;
                        case POSButtonTable.POSButtonTableStatus.Delete:                            
                            mBOBan.Xoa(tbl._Ban);
                            gridFloorPlan.Children.RemoveAt(i);
                            i--;
                            break;
                        default:
                            break;
                    }
                }
            }
            mBOBan.Commit();
            mBOKhu.Sua(_Khu);
            mBOKhu.Commit();
        }
        public void LoadTable()
        {
            if (_Khu != null)
            {                                
                gridFloorPlan.Children.Clear();
                IQueryable<Data.BAN> list;
                if (_IsEdit)
                {
                    list = mBOBan.GetAllTablePerArea(_Khu);
                }
                else
                {
                    list = mBOBan.GetVisualTablePerArea(_Khu);
                }
                foreach (var ban in list)
                {
                    addTable(ban);
                }
                if (!_IsEdit)
                {
                    LoadAlllStatus();
                }
                imgBackground.Source = Utilities.ImageHandler.BitmapImageFromByteArray(_Khu.Hinh);
            }
        }
        
        public void LoadBackgroundImage(BitmapImage img)
        {
            imgBackground.Source = img;
        }
        public void LoadTable(Data.KHU khu)
        {
            _Khu = khu;
            LoadTable();
        }
        public void addTable(Data.BAN ban)
        {
            POSButtonTable tbl = new POSButtonTable();
            tbl.IsEnabled = true;
            tbl._UserControlParent = this;
            tbl._Ban = ban;
            tbl._IsEdit = this._IsEdit;
            tbl.TableDraw();
            tbl._ButtonTableStatus = POSButtonTable.POSButtonTableStatus.None;
            tbl.Click += new RoutedEventHandler(tbl_Click);
            gridFloorPlan.Children.Add(tbl);
        }
        public void removeTable(POSButtonTable ban)
        {
            if (ban._Ban.BanID == 0)
            {
                gridFloorPlan.Children.Remove(ban);
            }
            else
            {
                ban._ButtonTableStatus = ControlLibrary.POSButtonTable.POSButtonTableStatus.Delete;
                ban.Visibility = System.Windows.Visibility.Hidden;
            }
        }        
        void tbl_Click(object sender, RoutedEventArgs e)
        {
            POSButtonTable tbl = (POSButtonTable)sender;
            if (_OnEventFloorPlan != null)
            {
                _OnEventFloorPlan(tbl);
            }
        }
    }
}
