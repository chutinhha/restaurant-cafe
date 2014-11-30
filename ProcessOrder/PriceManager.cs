using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessOrder
{
    public class PriceManager
    {
        private IQueryable<Data.MENULOAIGIA> mQueryMenuLoaiGia;
        private Data.Transit mTransit;
        private List<Data.BOMenuGia> mListMenuGia;
        public List<Data.BOMenuGia> _ListMenuGia 
        {
            get { return mListMenuGia; }
        }
        public PriceManager(Data.Transit transit)
        {
            mTransit = transit;
            mQueryMenuLoaiGia = Data.BOMenuLoaiGia.GetAllLoaiGiaRun(mTransit);            
            mListMenuGia = new List<Data.BOMenuGia>();
        }
        public bool CheckMutiablePrice(Data.BOChiTietBanHang chitiet)
        {
            var list = Data.BOMenuGia.GetAllByKichThuocMonVaLoaiGia(mTransit, chitiet.MENUKICHTHUOCMON, mQueryMenuLoaiGia);
            mListMenuGia.Clear();
            foreach (var item in list)
            {
                if (item.Gia>0)
                {
                    mListMenuGia.Add(item);
                }
            }
            if (mListMenuGia.Count==1)
            {
                Data.BOMenuGia gia=mListMenuGia[0];
                chitiet.ChangePriceChiTietBanHang(gia.MenuGia.Gia);             
            }
            else if(mListMenuGia.Count>1)
            {
                return true;
            }
            return false;
        }
    }
}
