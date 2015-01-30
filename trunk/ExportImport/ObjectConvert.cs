using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExportImport
{
    public class ObjectConvert<TEntity> where TEntity:System.Data.Objects.DataClasses.EntityObject
    //public class ObjectConvert<TEntity> where TEntity : new()
    {
        public static System.Data.DataTable GetTableData(List<TEntity> list, string tableName)
        {
            var tbl = CreateExcelFile.ListToDataTable(list);
            tbl.TableName = tableName;
            return tbl;
        }
        public static void Coppy(TEntity source, TEntity desc)
        {
            foreach (var item in typeof(TEntity).GetProperties())
            {
                if (CreateExcelFile.CheckType(item.PropertyType) && item.CanWrite && item.CanRead)
                {
                    item.SetValue(desc, item.GetValue(source, null), null);
                }
            }  
        }        
        public static void GetValue(TEntity data,IList<string> rowData, IList<string> columnNames)
        {
            foreach (var item in typeof(TEntity).GetProperties())
            {
                if (CreateExcelFile.CheckType(item.PropertyType))
                {
                    item.SetValue(data, ConvertType(item.PropertyType, rowData[columnNames.IndexOf(item.Name.ToLower())]), null);
                }
                //MaMayIn = Convert.ToInt32(),
            }
        }
        private static object ConvertType(Type type, string value)
        {
            if (type==typeof(int))
            {
                return ExcelReader.ToInt32(value);
            }
            if (type == typeof(int?))
            {
                return ExcelReader.ToInt32Nullable(value);
            }
            if (type == typeof(decimal))
            {
                return ExcelReader.ToDecimal(value);
            }
            if (type == typeof(decimal?))
            {
                return ExcelReader.ToDecimalNullable(value);
            }
            if (type == typeof(bool))
            {
                return ExcelReader.ToBoolean(value);
            }
            if (type == typeof(bool?))
            {
                return ExcelReader.ToBooleanNullable(value);
            }
            if (type == typeof(DateTime))
            {
                return ExcelReader.ToDateTime(value);
            }
            if (type == typeof(DateTime?))
            {
                return ExcelReader.ToDateTimeNullable(value);
            }            
            return value;
        }        
        public static TEntity GetProductData(IList<string> rowData, IList<string> columnNames)
        {
            TEntity entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
            ObjectConvert<TEntity>.GetValue(entity, rowData, columnNames);
            return entity;            
        }
        public static string UpdateData(Data.KaraokeEntities kara,IQueryable<TEntity> query, System.Data.Objects.ObjectSet<TEntity> obj, TEntity data)
        {
            string resuilt = "";
            try
            {
                TEntity entity = query.FirstOrDefault();
                if (entity == null)
                {
                    entity = data;
                    obj.AddObject(entity);
                }
                else
                {                    
                    ObjectConvert<TEntity>.Coppy(data, entity);
                    //kara.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                }
                kara.SaveChanges();        
            }
            catch (Exception ex)
            {
                resuilt = "Lỗi..."+ex.Message;
            }
            return resuilt;
        }
    }
}
