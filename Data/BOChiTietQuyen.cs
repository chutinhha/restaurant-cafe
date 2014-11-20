using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChiTietQuyen
    {       
        public CHITIETQUYEN ChiTietQuyen { get; set; }
        public QUYEN Quyen { get; set; }
        public CHUCNANG ChucNang { get; set; }
        FrameworkRepository<CHITIETQUYEN> frmChiTietQuyen = null;
        FrameworkRepository<QUYEN> frmQuyen = null;
        FrameworkRepository<CHUCNANG> frmChucNang = null;
        public BOChiTietQuyen(Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmChiTietQuyen = new FrameworkRepository<CHITIETQUYEN>(transit.KaraokeEntities, transit.KaraokeEntities.CHITIETQUYENs);
            frmQuyen = new FrameworkRepository<QUYEN>(transit.KaraokeEntities, transit.KaraokeEntities.QUYENs);
            frmChucNang = new FrameworkRepository<CHUCNANG>(transit.KaraokeEntities, transit.KaraokeEntities.CHUCNANGs);
        }
        public BOChiTietQuyen()
        {
            ChiTietQuyen = new CHITIETQUYEN();
            Quyen = new QUYEN();
            ChucNang = new CHUCNANG();
        }

        public IQueryable<BOChiTietQuyen> GetAll(int MaQuyen, Transit mTransit)
        {
            var res = (from ctq in frmChiTietQuyen.Query()
                       join q in frmQuyen.Query() on ctq.QuyenID equals q.MaQuyen
                       join cn in frmChucNang.Query() on ctq.ChucNangID equals cn.ChucNangID
                       where ctq.Deleted == false && ctq.Deleted == false && ctq.QuyenID == MaQuyen
                       select new BOChiTietQuyen
                       {
                           ChiTietQuyen = ctq,
                           Quyen = q,
                           ChucNang = cn
                       });
            return res;
        }
        private int Them(BOChiTietQuyen item, Transit mTransit)
        {
            frmChiTietQuyen.AddObject(item.ChiTietQuyen);
            return item.ChiTietQuyen.ChiTietQuyenID;
        }

        private int Xoa(BOChiTietQuyen item, Transit mTransit)
        {
            frmChiTietQuyen.DeleteObject(item.ChiTietQuyen);
            return item.ChiTietQuyen.ChiTietQuyenID;
        }

        private int Sua(BOChiTietQuyen item, Transit mTransit)
        {
            frmChiTietQuyen.Update(item.ChiTietQuyen);
            return item.ChiTietQuyen.ChiTietQuyenID;
        }

        public void Luu(List<BOChiTietQuyen> lsArray, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOChiTietQuyen item in lsArray)
                {
                    if (item.ChiTietQuyen.ChiTietQuyenID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            frmChiTietQuyen.Commit();
        }
    }
}
