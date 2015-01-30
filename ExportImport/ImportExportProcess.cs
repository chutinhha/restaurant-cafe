using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExportImport
{
    public class ImportExportProcess
    {
        public delegate void EventImportExport(string log,bool isError);
        public event EventImportExport _OnImportExport;
        Data.KaraokeEntities mKaraokeEntities;

        private static string LOAINHOM = "Loại Nhóm";
        private static string MAYIN = "Máy In";
        private static string LOAIGIA = "Loại Giá";
        private static string DONVI = "Đơn Vị";
        private static string LOAIDONVIBAN = "Loại Đơn Vị Bán";
        private static string NHOM = "Nhóm";
        private static string MON = "Món";
        private static string CHITIETMON = "Chi Tiết Món";
        private static string CHITIETGIA = "Chi Tiết Giá";
        private static string MONMAYIN = "Món Máy In";
        public ImportExportProcess()
        {
            mKaraokeEntities = new Data.KaraokeEntities();
        }
        public void ExportItem(string url)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            _OnImportExport(String.Format("Đọc dữ liệu..."), false);
            var listItem = (from a in mKaraokeEntities.MENUNHOMs
                           join b in mKaraokeEntities.MENUMONs on a.NhomID equals b.NhomID
                           join c in mKaraokeEntities.MENUKICHTHUOCMONs on b.MonID equals c.MonID
                           where a.Deleted == false && b.Deleted == false && c.Deleted == false
                           select new ImportExportItem
                           {
                               TenNhom=a.TenNgan,
                               TenMon=b.TenNgan,
                               DonGia=c.GiaBanMacDinh,
                               ChiTietMon=c.TenLoaiBan,
                               SoLuongBanMacDinh=c.SoLuongBanBan
                           }).ToList();
            _OnImportExport(String.Format("Khởi tạo dữ liệu..."), false);
            System.Data.DataTable tbl=CreateExcelFile.ListToDataTable(listItem);
            tbl.TableName=MON;
            ds.Tables.Add(tbl);
            _OnImportExport("Lưu tập tin..." + url, false);
            CreateExcelFile.CreateExcelDocument(ds, url);
            _OnImportExport("Lưu tập tin thành công..." + url, false);
        }
        public void Export(string url)
        {
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                _OnImportExport(String.Format("Đọc {0}...",LOAINHOM), false);
                ds.Tables.Add(ObjectConvert<Data.MENULOAINHOM>.GetTableData(mKaraokeEntities.MENULOAINHOMs.ToList(), LOAINHOM));

                _OnImportExport(String.Format("Đọc {0}...", MAYIN), false);
                ds.Tables.Add(ObjectConvert<Data.MAYIN>.GetTableData(mKaraokeEntities.MAYINs.Where(o => o.Deleted == false).ToList(), MAYIN));

                _OnImportExport(String.Format("Đọc {0}...", LOAIGIA), false);
                ds.Tables.Add(ObjectConvert<Data.MENULOAIGIA>.GetTableData(mKaraokeEntities.MENULOAIGIAs.Where(o => o.Deleted == false).ToList(), LOAIGIA));

                _OnImportExport(String.Format("Đọc {0}...", DONVI), false);
                ds.Tables.Add(ObjectConvert<Data.DONVI>.GetTableData(mKaraokeEntities.DONVIs.Where(o => o.Deleted == false).ToList(), DONVI));

                _OnImportExport(String.Format("Đọc {0}...", LOAIDONVIBAN), false);
                ds.Tables.Add(ObjectConvert<Data.LOAIBAN>.GetTableData(mKaraokeEntities.LOAIBANs.Where(o => o.Deleted == false).ToList(), LOAIDONVIBAN));

                _OnImportExport(String.Format("Đọc {0}...", NHOM), false);                
                ds.Tables.Add(ObjectConvert<Data.MENUNHOM>.GetTableData(mKaraokeEntities.MENUNHOMs.Where(o=>o.Deleted==false).ToList(), NHOM));

                _OnImportExport(String.Format("Đọc {0}...", MON), false);                
                ds.Tables.Add(ObjectConvert<Data.MENUMON>.GetTableData(mKaraokeEntities.MENUMONs.Where(o=>o.Deleted==false).ToList(), MON));

                _OnImportExport(String.Format("Đọc {0}...", CHITIETMON), false);
                ds.Tables.Add(ObjectConvert<Data.MENUKICHTHUOCMON>.GetTableData(mKaraokeEntities.MENUKICHTHUOCMONs.Where(o => o.Deleted == false).ToList(), CHITIETMON));

                _OnImportExport(String.Format("Đọc {0}...", CHITIETGIA), false);
                ds.Tables.Add(ObjectConvert<Data.MENUGIA>.GetTableData(mKaraokeEntities.MENUGIAs.ToList(), CHITIETGIA));

                _OnImportExport(String.Format("Đọc {0}...", MONMAYIN), false);
                ds.Tables.Add(ObjectConvert<Data.MENUITEMMAYIN>.GetTableData(mKaraokeEntities.MENUITEMMAYINs.Where(o => o.Deleted == false).ToList(), MONMAYIN));

                _OnImportExport("Lưu tập tin..." + url, false);
                CreateExcelFile.CreateExcelDocument(ds, url);

                _OnImportExport("Xuất dữ liệu thành công...", false);
            }
            catch (Exception ex)
            {
                _OnImportExport("Lỗi..." + ex.Message, true);
            }
        }
        public void ImportItem(string url)
        {
            _OnImportExport("----------------------------", false);
            _OnImportExport(String.Format("Đọc tập tin excel..."), false);
            var listItem = ExcelReader.GetDataToList(url, MON, ImportExportItem.GetProductData);
            List<Data.MENUNHOM> listNhom = new List<Data.MENUNHOM>();
            //List<Data.MENUMON> listMon = new List<Data.MENUMON>();

            _OnImportExport(String.Format("Xóa dữ liệu cũ..."), false);
            mKaraokeEntities.ExecuteStoreCommand("SP_DELETE_ALL_MENU");

            _OnImportExport(String.Format("Tạo {0}...", MAYIN), false);
            Data.MAYIN mayin = new Data.MAYIN();
            mayin.TenMayIn = "Test";
            mayin.TieuDeIn = "BẾP";
            mayin.SoLanIn = 1;
            mayin.Visual = true;
            mKaraokeEntities.MAYINs.AddObject(mayin);

            _OnImportExport(String.Format("Tạo {0}...", MAYIN), false);
            Data.MAYIN mayinBill = new Data.MAYIN();
            mayinBill.TenMayIn = "Test";
            mayinBill.TieuDeIn = "HÓA ĐƠN";
            mayinBill.SoLanIn = 1;
            mayinBill.MayInHoaDon = true;
            mayinBill.Visual = true;
            mKaraokeEntities.MAYINs.AddObject(mayinBill);

            _OnImportExport(String.Format("Tạo {0}...", DONVI), false);
            Data.DONVI donvi = mKaraokeEntities.DONVIs.Where(o => o.DonViID == 1).FirstOrDefault();
            if (donvi==null)
            {
                donvi = new Data.DONVI();
                donvi.TenDonVi = "Số lượng";
                donvi.Visual = true;
                mKaraokeEntities.DONVIs.AddObject(donvi);
            }            

            _OnImportExport(String.Format("Tạo {0}...", LOAIDONVIBAN), false);
            Data.LOAIBAN loaiban = mKaraokeEntities.LOAIBANs.Where(o => o.LoaiBanID == 1).FirstOrDefault();
            if (loaiban==null)
            {
                loaiban.TenLoaiBan = "Cái";
                loaiban.KichThuocBan = 1;
                loaiban.Visual = true;
                mKaraokeEntities.LOAIBANs.AddObject(loaiban);
                donvi.LOAIBANs.Add(loaiban);
            }
            
            foreach (var item in listItem)
            {                
                var nhom = (from a in listNhom
                           where a.TenNgan == item.TenNhom
                           select a).FirstOrDefault();
                if (nhom==null)
                {
                    _OnImportExport(String.Format("Tạo {0}---{1}", NHOM, item.TenNhom), false);
                    nhom = new Data.MENUNHOM();
                    nhom.TenDai = nhom.TenNgan = item.TenNhom;
                    nhom.Visual = true;
                    mKaraokeEntities.MENUNHOMs.AddObject(nhom);
                    listNhom.Add(nhom);
                }
                var mon = (from a in nhom.MENUMONs
                           where a.TenNgan == item.TenMon
                           select a).FirstOrDefault();
                if (mon==null)
                {
                    _OnImportExport(String.Format("Tạo {0}---{1}", MON, item.TenMon), false);
                    mon = new Data.MENUMON();
                    mon.TenNgan = mon.TenDai = item.TenMon;
                    mon.Gia = item.DonGia;
                    mon.Visual = true;                    
                    nhom.MENUMONs.Add(mon);
                    donvi.MENUMONs.Add(mon);
                    nhom.SLMonChoPhepTonKho++;
                    Data.MENUITEMMAYIN monmayin = new Data.MENUITEMMAYIN();
                    mon.MENUITEMMAYINs.Add(monmayin);
                    mayin.MENUITEMMAYINs.Add(monmayin);
                    mKaraokeEntities.MENUMONs.AddObject(mon);
                    //listMon.Add(mon);
                }
                _OnImportExport(String.Format("Tạo {0}---{1}", CHITIETMON, item.ChiTietMon), false);
                Data.MENUKICHTHUOCMON ktm = new Data.MENUKICHTHUOCMON();
                ktm.TenLoaiBan = item.ChiTietMon;
                ktm.GiaBanMacDinh = item.DonGia;
                ktm.SoLuongBanBan = item.SoLuongBanMacDinh;
                ktm.KichThuocLoaiBan = 1;
                ktm.Visual = true;
                ktm.ChoPhepTonKho = true;
                mon.SLMonChoPhepTonKho++;
                mon.MENUKICHTHUOCMONs.Add(ktm);                
                loaiban.MENUKICHTHUOCMONs.Add(ktm);
                donvi.MENUKICHTHUOCMONs.Add(ktm);
                mKaraokeEntities.MENUKICHTHUOCMONs.AddObject(ktm);
            }
            _OnImportExport(String.Format("Lưu dữ liệu..."), false);
            mKaraokeEntities.SaveChanges();
            _OnImportExport(String.Format("Lưu dữ liệu thành công ...{0}",url), false);
        }
        public void Import(string url)
        {
            try
            {
                _OnImportExport("----------------------------", false);
                _OnImportExport(String.Format("Đọc {0}...", LOAINHOM), false);
                var listLoaiNhom = ExcelReader.GetDataToList(url,LOAINHOM, ObjectConvert<Data.MENULOAINHOM>.GetProductData);
                foreach (var item in listLoaiNhom)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.LoaiNhomID, item.TenLoaiNhom), false);
                    var query=from x in mKaraokeEntities.MENULOAINHOMs
                                                  where x.LoaiNhomID == item.LoaiNhomID
                                                  select x;
                    string resuilt = ObjectConvert<Data.MENULOAINHOM>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENULOAINHOMs, item);
                    if (resuilt!="")
                    {
                        _OnImportExport(resuilt, true);        
                    }
                }
                _OnImportExport("----------------------------", false);
                _OnImportExport(String.Format("Đọc {0}...", MAYIN), false);
                var listMayIn = ExcelReader.GetDataToList(url, MAYIN, ObjectConvert<Data.MAYIN>.GetProductData);
                foreach (var item in listMayIn)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.MayInID, item.TenMayIn), false);
                    var query = from x in mKaraokeEntities.MAYINs
                                where x.MayInID == item.MayInID
                                select x;
                    string resuilt = ObjectConvert<Data.MAYIN>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MAYINs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }


                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc loại giá...", false);
                var listLoaiGia = ExcelReader.GetDataToList(url, LOAIGIA, ObjectConvert<Data.MENULOAIGIA>.GetProductData);
                foreach (var item in listLoaiGia)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.LoaiGiaID, item.Ten), false);
                    var query = from x in mKaraokeEntities.MENULOAIGIAs
                                where x.LoaiGiaID == item.LoaiGiaID
                                select x;
                    string resuilt = ObjectConvert<Data.MENULOAIGIA>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENULOAIGIAs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc đơn vị...", false);
                var listDonVi = ExcelReader.GetDataToList(url, DONVI, ObjectConvert<Data.DONVI>.GetProductData);
                foreach (var item in listDonVi)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.DonViID, item.TenDonVi), false);
                    var query = from x in mKaraokeEntities.DONVIs
                                where x.DonViID == item.DonViID
                                select x;
                    string resuilt = ObjectConvert<Data.DONVI>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.DONVIs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc loại đơn vị...", false);                
                var listLoaiDonVi = ExcelReader.GetDataToList(url, LOAIDONVIBAN, ObjectConvert<Data.LOAIBAN>.GetProductData);
                foreach (var item in listLoaiDonVi)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.LoaiBanID, item.TenLoaiBan), false);
                    var query = from x in mKaraokeEntities.LOAIBANs
                                where x.LoaiBanID == item.LoaiBanID
                                select x;
                    string resuilt = ObjectConvert<Data.LOAIBAN>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.LOAIBANs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc nhóm...", false);
                var listNhom = ExcelReader.GetDataToList(url, NHOM, ObjectConvert<Data.MENUNHOM>.GetProductData);
                foreach (var item in listNhom)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.NhomID, item.TenNgan), false);
                    var query = from x in mKaraokeEntities.MENUNHOMs
                                where x.NhomID == item.NhomID
                                select x;
                    string resuilt = ObjectConvert<Data.MENUNHOM>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENUNHOMs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc món...", false);
                var listMon = ExcelReader.GetDataToList(url, MON, ObjectConvert<Data.MENUMON>.GetProductData);
                foreach (var item in listMon)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.MonID, item.TenNgan), false);
                    var query = from x in mKaraokeEntities.MENUMONs
                                where x.MonID == item.MonID
                                select x;
                    string resuilt = ObjectConvert<Data.MENUMON>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENUMONs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc chi tiết món...", false);
                var listChiTietMon = ExcelReader.GetDataToList(url, CHITIETMON, ObjectConvert<Data.MENUKICHTHUOCMON>.GetProductData);
                foreach (var item in listChiTietMon)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.KichThuocMonID, item.TenLoaiBan), false);
                    var query = from x in mKaraokeEntities.MENUKICHTHUOCMONs
                                where x.KichThuocMonID == item.KichThuocMonID
                                select x;
                    string resuilt = ObjectConvert<Data.MENUKICHTHUOCMON>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENUKICHTHUOCMONs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }


                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc chi tiết giá...", false);
                var listChiTietGia = ExcelReader.GetDataToList(url, CHITIETGIA, ObjectConvert<Data.MENUGIA>.GetProductData);
                foreach (var item in listChiTietGia)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.GiaID, item.Gia), false);
                    var query = from x in mKaraokeEntities.MENUGIAs
                                where x.GiaID == item.GiaID
                                select x;
                    string resuilt = ObjectConvert<Data.MENUGIA>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENUGIAs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc món máy in...", false);
                var listMonMayIn = ExcelReader.GetDataToList(url, MONMAYIN, ObjectConvert<Data.MENUITEMMAYIN>.GetProductData);
                foreach (var item in listMonMayIn)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.MayInID, item.MonID), false);
                    var query = from x in mKaraokeEntities.MENUITEMMAYINs
                                where x.MayInID == item.MayInID && x.MonID==item.MonID
                                select x;
                    string resuilt = ObjectConvert<Data.MENUITEMMAYIN>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENUITEMMAYINs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }
                mKaraokeEntities.ExecuteStoreCommand("SP_CREATEDEFAULT_KICHTHUOCMON");
                _OnImportExport("Nhập dữ liệu thành công...", false);
                //============================
                
            }
            catch (Exception ex)
            {
                _OnImportExport("Lỗi..."+ex.Message, true);
            }            

        }

        private void OnImport(string log,bool isError)
        {
            if (_OnImportExport!=null)
	        {
	            _OnImportExport(log,isError);
	        }
        }

        public static Data.MENULOAINHOM GetMenuLoaiNhom(IList<string> rowData, IList<string> columnNames)
        {
            var data = new Data.MENULOAINHOM();
            ObjectConvert<Data.MENULOAINHOM>.GetValue(data, rowData, columnNames);
            return data;
        }
        
    }
}
