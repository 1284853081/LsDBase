using LsDBase.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsDBase.Subsidiary
{
    internal static class Table
    {
        public static string GetTablePath(string db,string table)
        {
            Aes aes = new();
            using BinaryReader reader = new(new FileStream(LSDB.TablePath, FileMode.Open, FileAccess.Read));
            reader.BaseStream.Position = GetPosition(db, table);
            return aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(132)));
        }
        public static string Write(string db,string table)
        {
            Aes aes = new();
            long po = GetPosition();
            using BinaryWriter writer = new(new FileStream(LSDB.TablePath, FileMode.Open, FileAccess.Write));
            if (po != -1)
                writer.BaseStream.Position = po;
            else
                writer.Seek(0, SeekOrigin.End);
            writer.Write(false);
            writer.Write(Auxiliary.ToBytesWithSign(aes.EncryptStr(db), 32));
            writer.Write(Auxiliary.ToBytesWithSign(aes.EncryptStr(table), 32));
            string path = $"{Base.GetBasePath(db)}/{Namer.GetRandomName(false)}";
            writer.Write(Auxiliary.ToBytesWithSign(aes.EncryptStr(path), 132));
            return path;
        }
        public static string Delete(string db,string table)
        {
            string path = GetTablePath(db, table);
            long position = GetPosition(db, table) - 65;
            using BinaryWriter writer = new(new FileStream(LSDB.TablePath, FileMode.Open, FileAccess.Write));
            writer.BaseStream.Position = position;
            writer.Write(true);
            return path;
        }
        public static long GetPosition()
        {
            using BinaryReader reader = new(new FileStream(LSDB.TablePath, FileMode.Open, FileAccess.Read));
            long po = -1;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (reader.ReadBoolean())
                    return reader.BaseStream.Position - 1;
                else
                    reader.BaseStream.Position += 196;
            }
            return po;
        }
        public static List<long> GetPosition(string db)
        {
            Aes aes = new();
            List<long> list = new();
            using BinaryReader reader = new(new FileStream(LSDB.TablePath, FileMode.Open, FileAccess.Read));
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (!reader.ReadBoolean())
                {
                    string dbname = aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(32)));
                    if (db == dbname)
                        list.Add(reader.BaseStream.Position);
                    reader.BaseStream.Position += 164;
                }
                else
                    reader.BaseStream.Position += 196;
            }
            return list;
        }
        public static long GetPosition(string db,string table)
        {
            Aes aes = new();
            List<long> list = GetPosition(db);
            using BinaryReader reader = new(new FileStream(LSDB.TablePath, FileMode.Open, FileAccess.Read));
            for (int i = 0; i < list.Count; i++)
            {
                reader.BaseStream.Position = list[i];
                string tablename = aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(32)));
                if (table == tablename)
                    return reader.BaseStream.Position;
            }
            throw new LsException("配置信息错误，无法查询到表信息");
        }
        public static List<string> GetAllTables(string db)
        {
            Aes aes = new();
            List<long> index = GetPosition(db);
            List<string> list = new();
            using BinaryReader reader = new(new FileStream(LSDB.TablePath, FileMode.Open, FileAccess.Read));
            for (int i = 0; i < index.Count; i++)
            {
                reader.BaseStream.Position = index[i];
                list.Add(aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(32))));
            }
            return list;
        }
        public static Dictionary<string,string> GetAllTablesPath(string db)
        {
            List<string> tables = GetAllTables(db);
            Aes aes = new();
            Dictionary<string,string> dict = new();
            foreach (string table in tables)
                dict.Add(table, GetTablePath(db, table));
            return dict;
        }
        public static bool ContainsTable(string db, string table)
            => GetAllTables(db).Contains(table);
    }
}