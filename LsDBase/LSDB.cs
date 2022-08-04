using LsDBase.Core;
using LsDBase.Subsidiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsDBase
{
    public static class LSDB
    {
        internal readonly static string AppPath = $"{Environment.CurrentDirectory}/LSDB";
        internal readonly static string DBaseSavePath = $"{Environment.CurrentDirectory}/LSDB/data";
        internal readonly static string BasePath = $"{Environment.CurrentDirectory}/LSDB/ls_base.lsdt";
        internal readonly static string TablePath = $"{Environment.CurrentDirectory}/LSDB/ls_table.lsdt";
        internal readonly static string ColumnPath = $"{Environment.CurrentDirectory}/LSDB/ls_column.lsdt";
        internal static void QueryPath()
        {
            if (!Directory.Exists(AppPath))
                Directory.CreateDirectory(AppPath);
            if (!Directory.Exists(DBaseSavePath))
                Directory.CreateDirectory(DBaseSavePath);
            if (!File.Exists(BasePath))
                File.Create(BasePath).Close();
            if (!File.Exists(TablePath))
                File.Create(TablePath).Close();
            if (!File.Exists(ColumnPath))
                File.Create(ColumnPath).Close();
        }
        public static DataBase CreateDB(string dbname,string password)
        {
            QueryPath();
            if (Base.ContainsDB(dbname))
                throw new LsException($"数据库{dbname}已经存在");
            string path = Base.Write(dbname);
            Directory.CreateDirectory(path);
            return new DataBase(dbname, path, password);
        }
        public static DataBase Login(string dbname,string password)
        {
            QueryPath();
            if (!Base.ContainsDB(dbname))
                throw new LsException($"数据库{dbname}不存在,请先创建数据库再登陆");
            string path = Base.GetBasePath(dbname);
            return new DataBase(dbname, path, password);
        }
        public static void DeleteDB(string dbname)
        {
            if (!Base.ContainsDB(dbname))
                throw new LsException($"数据库{dbname}不存在,删除失败");
            string path = Base.Delete(dbname);
            Directory.Delete(path);
        }
        public static List<string> GetAllBase()
            => Base.GetAllBase();
    }
}
