using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.ProcessOrder
{
    public class ChiTietBanHang:CHITIETBANHANG
    {
        public bool IsDeleted { get; set; }
        private int mSoLuongBanTam;
        
        public ChiTietBanHang()
        {             
        }
        public ChiTietBanHang(MENUKICHTHUOCMON ktm)
        {
            this.MENUKICHTHUOCMON = new Data.MENUKICHTHUOCMON();
            this.MENUKICHTHUOCMON.GiaBanMacDinh = ktm.GiaBanMacDinh;
            this.MENUKICHTHUOCMON.SoLuongBanBan = ktm.SoLuongBanBan;
            this.SoLuongBan = ktm.SoLuongBanBan;
            this.GiaBan = ktm.GiaBanMacDinh;
            this.MENUKICHTHUOCMON.TenLoaiBan = ktm.TenLoaiBan;
            this.MENUKICHTHUOCMON.MENUMON = new Data.MENUMON();
            this.MENUKICHTHUOCMON.MENUMON.TenDai = ktm.MENUMON.TenDai;

            mSoLuongBanTam = (int)this.SoLuongBan;
        }
        public string TenMon
        {
            get
            {                                                               
                return MENUKICHTHUOCMON.MENUMON.TenDai + " (" + MENUKICHTHUOCMON.TenLoaiBan + ")";
            }
        }

        public string TongTien
        {

            get
            {
                return (GiaBan * SoLuongBan).ToString();
            }
        }
    }
}
