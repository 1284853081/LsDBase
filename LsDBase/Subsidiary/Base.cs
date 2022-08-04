using LsDBase.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsDBase.Subsidiary
{
    internal static class Base
    {
        public static string GetBasePath(string db)
        {
            using BinaryReader reader = new(new FileStream(LSDB.BasePath, FileMode.Open));
            Aes aes = new();
            while(reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (!reader.ReadBoolean())
                {
                    string dbname = aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(32)));
                    if (db == dbname)
                        return aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(132)));
                    else
                        reader.BaseStream.Position += 132;
                }
            }
            return string.Empty;
        }
        public static string Write(string db)
        {
            Aes aes = new();
            long po = GetPosition();
            using BinaryWriter writer = new(new FileStream(LSDB.BasePath, FileMode.Open, FileAccess.Write));
            if (po != -1)
                writer.BaseStream.Position = po;
            else
                writer.Seek(0, SeekOrigin.End);
            writer.Write(false);
            writer.Write(Auxiliary.ToBytesWithSign(aes.EncryptStr(db), 32));
            string path = $"{LSDB.DBaseSavePath}/{Namer.GetRandomName()}";
            writer.Write(Auxiliary.ToBytesWithSign(aes.EncryptStr(path), 132));
            return path;
        }
        public static string Delete(string db)
        {
            string path = GetBasePath(db);
            long position = GetPosition(db);
            using BinaryWriter writer = new(new FileStream(LSDB.BasePath,FileMode.Open, FileAccess.Write));
            writer.BaseStream.Position = position;
            writer.Write(true);
            return path;
        }
        public static long GetPosition()
        {
            using BinaryReader reader = new(new FileStream(LSDB.BasePath, FileMode.Open, FileAccess.Read));
            long po = -1;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (reader.ReadBoolean())
                    return reader.BaseStream.Position - 1;
                else
                    reader.BaseStream.Position += 164;
            }
            return po;
        }
        public static long GetPosition(string db)
        {
            Aes aes = new();
            using BinaryReader reader = new(new FileStream(LSDB.BasePath, FileMode.Open, FileAccess.Read));
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (!reader.ReadBoolean())
                {
                    string dbname = aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(32)));
                    if (db == dbname)
                        return reader.BaseStream.Position - 33;
                    else
                        reader.BaseStream.Position += 132;
                }
                else
                    reader.BaseStream.Position += 164;
            }
            throw new LsException("配置信息错误，无法查询到数据库信息");
        }
        public static List<string> GetAllBase()
        {
            List<string> list = new();
            Aes aes = new();
            using BinaryReader reader = new(new FileStream(LSDB.BasePath, FileMode.Open, FileAccess.Read));
            while(reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (!reader.ReadBoolean())
                {
                    list.Add(aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(32))));
                    reader.BaseStream.Position += 132;
                }
                else
                    reader.BaseStream.Position += 164;
            }
            return list;
        }
        public static bool ContainsDB(string dbname)
            =>GetAllBase().Contains(dbname);
    }
}
