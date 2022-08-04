using LsDBase.Core;

namespace LsDBase.Subsidiary
{
    internal static class Columns
    {
        public static void Write(string db,string table,string field,ushort size,ushort origin)
        {
            Aes aes = new();
            long po = GetPosition();
            using BinaryWriter writer = new(new FileStream(LSDB.ColumnPath, FileMode.Open, FileAccess.Write));
            if(po != -1)
                writer.BaseStream.Position = po;
            else
                writer.Seek(0, SeekOrigin.End);
            writer.Write(false);
            writer.Write(Auxiliary.ToBytesWithSign(aes.EncryptStr(db), 32));
            writer.Write(Auxiliary.ToBytesWithSign(aes.EncryptStr(table), 32));
            writer.Write(Auxiliary.ToBytesWithSign(aes.EncryptStr(field), 32));
            writer.Write(aes.Encrypt(Auxiliary.ToBytes(size)));
            writer.Write(aes.Encrypt(Auxiliary.ToBytes(origin)));
        }
        public static ushort ReadFieldSize(string db,string table,string field,out ushort origin)
        {
            long po = GetPosition(db, table, field);
            Aes aes = new();
            using BinaryReader reader = new(new FileStream(LSDB.ColumnPath, FileMode.Open, FileAccess.Read));
            reader.BaseStream.Position = po;
            ushort size = Auxiliary.ToUShort(aes.Decrypt(reader.ReadBytes(2)));
            origin = Auxiliary.ToUShort(aes.Decrypt(reader.ReadBytes(2)));
            return size;
        }
        public static List<ushort> ReadAllFieldSize(string db,string table)
        {
            List<string> vs = GetAllFields(db, table);
            List<ushort> fields = new();
            foreach (string v in vs)
            {
                ushort a = ReadFieldSize(db, table, v, out ushort b);
                fields.Add(a);
            }
            return fields;
        }
        public static void DeleteField(string db,string table,string field)
        {
            long po = GetPosition(db,table,field);
            using BinaryWriter writer = new(new FileStream(LSDB.ColumnPath, FileMode.Open, FileAccess.Write));
            writer.BaseStream.Position = po - 97;
            writer.Write(true);
        }
        public static long GetPosition()
        {
            using BinaryReader reader = new(new FileStream(LSDB.ColumnPath, FileMode.Open, FileAccess.Read));
            long po = -1;
            while(reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (reader.ReadBoolean())
                    return reader.BaseStream.Position - 1;
                else
                    reader.BaseStream.Position += 100;
            }
            return po;
        }
        public static List<long> GetPosition(string db)
        {
            Aes aes = new();
            List<long> list = new();
            using BinaryReader reader = new(new FileStream(LSDB.ColumnPath, FileMode.Open, FileAccess.Read));
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (!reader.ReadBoolean())
                {
                    string dbname = aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(32)));
                    if (db == dbname)
                        list.Add(reader.BaseStream.Position);
                    reader.BaseStream.Position += 68;
                }
                else
                    reader.BaseStream.Position += 100;
            }
            return list;
        }
        public static List<long> GetPosition(string db,string table)
        {
            Aes aes = new();
            List<long> list = GetPosition(db);
            List<long> list2 = new();
            using BinaryReader reader = new(new FileStream(LSDB.ColumnPath, FileMode.Open, FileAccess.Read));
            for (int i = 0; i < list.Count; i++)
            {
                reader.BaseStream.Position = list[i];
                string tablename = aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(32)));
                if (table == tablename)
                    list2.Add(reader.BaseStream.Position);
            }
            return list2;
        }
        public static long GetPosition(string db, string table,string field)
        {
            Aes aes = new();
            List<long> list = GetPosition(db,table);
            using BinaryReader reader = new(new FileStream(LSDB.ColumnPath, FileMode.Open, FileAccess.Read));
            for (int i = 0; i < list.Count; i++)
            {
                reader.BaseStream.Position = list[i];
                string fieldname = aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(32)));
                if (field == fieldname)
                    return reader.BaseStream.Position;
            }
            throw new LsException("配置信息错误，无法查询到字段信息");
        }
        public static List<string> GetAllFields(string db,string table)
        {
            Aes aes = new();
            List<long> index = GetPosition(db, table);
            List<string> list = new();
            using BinaryReader reader = new(new FileStream(LSDB.ColumnPath, FileMode.Open, FileAccess.Read));
            for (int i = 0; i < index.Count; i++)
            {
                reader.BaseStream.Position = index[i];
                list.Add(aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(32))));
            }
            return list;
        }
        public static Dictionary<string,ushort[]> GetAllFieldsSize(string db,string table)
        {
            List<string> vs = GetAllFields(db,table);
            Dictionary<string,ushort[]> list = new();
            foreach (string v in vs)
            {
                ushort[] rs = new ushort[2];
                ushort a = ReadFieldSize(db, table, v, out ushort b);
                rs[0] = a;
                rs[1] = b;
                list.Add(v, rs);
            }
            return list;
        }
        public static bool ContainsField(string db,string table,string field)
            =>GetAllFields(db,table).Contains(field);
    }
}
