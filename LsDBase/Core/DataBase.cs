using LsDBase.Subsidiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsDBase.Core
{
    public class DataBase
    {
        private readonly string _name;
        private readonly Aes _aes;
        private readonly string _path;
        internal DataBase(string dbname,string path,string password)
        {
            _name = dbname;
            _path = path;
            _aes = new Aes(password);
        }
        public List<string> GetTables()
            => Table.GetAllTables(_name);
        public List<string> GetFields(string table)
            => Columns.GetAllFields(_name, table);
        public void CreateTable(string table,string[] fields,params DataSize[] sizes)
        {
            if (GetTables().Contains(table))
                throw new LsException($"数据表{table}已经存在");
            if (fields.Length != sizes.Length)
                throw new LsException("字段与字段大小数目不匹配");
            ushort k = 1;
            for(int i = 0; i < fields.Length; i++)
            {
                Columns.Write(_name, table, fields[i], sizes[i].Size, k);
                k += sizes[i].Size;
            }
            string path = Table.Write(_name, table);
            File.Create(path).Close();
        }
        public void DeleteTable(string table)
        {
            if (!GetTables().Contains(table))
                throw new LsException($"数据表{table}不存在");
            string path = Table.Delete(_name, table);
            File.Delete(path);
        }
        public LsReader Select(string table,Fields fields,KeyValues condition)
        {
            if (!GetTables().Contains(table))
                throw new LsException($"数据表{table}不存在");
            List<string> vs = GetValidData(table,out List<string> keys);
            if (fields.IsAll && condition.IsNull)
            {
                while(vs.Contains("False"))
                    vs.Remove("False");
                return new LsReader(vs, keys.Count);
            }
            else if (fields.IsAll && !condition.IsNull)
            {
                List<string> vs2 = new();
                for (int i = 0; i < condition.Count; i++)
                {
                    if(vs.Contains(condition[i].Value))
                    {
                        int t = vs.IndexOf(condition[i].Value);
                        int k = t % (keys.Count+1);
                        for (int j = 0; j < keys.Count; j++)
                            vs2.Add(vs[t - k + 1 + j]);
                    }
                }
                return new LsReader(vs2, keys.Count);
            }
            else if (!fields.IsAll && condition.IsNull)
            {
                List<string> vs1 = new();
                for (int i = 0; i < vs.Count; i += keys.Count + 1)
                    for (int j = 0; j < fields.Count; j++)
                        vs1.Add(vs[i + 1 + keys.IndexOf(fields[j])]);
                return new LsReader(vs1, keys.Count);
            }
            else
            {
                List<string> vs2 = new();
                for (int i = 0; i < condition.Count; i++)
                {
                    if (vs.Contains(condition[i].Value))
                    {
                        int t = vs.IndexOf(condition[i].Value);
                        int k = t % (keys.Count + 1);
                        for (int j = 0; j < fields.Count; j++)
                            vs2.Add(vs[t - k + 1 + keys.IndexOf(fields[j])]);
                    }
                }
                return new LsReader(vs2, keys.Count);
            }
        }
        public void Update(string table, KeyValues kvps,KeyValues condition)
        {
            long len = GetTableLineBytes(table);
            if(condition.IsNull)
            {
                using BinaryWriter writer = new(new FileStream(Table.GetTablePath(_name,table), FileMode.Open, FileAccess.Write));
                for(int i = 0; i < kvps.Count; i++)
                {
                    int j = 0;
                    ushort size = Columns.ReadFieldSize(_name, table, kvps[i].Key, out ushort po); 
                    while (writer.BaseStream.Position<writer.BaseStream.Length)
                    {
                        writer.BaseStream.Position = j++ * len + po;
                        writer.Write(Auxiliary.ToBytesWithSign(_aes.EncryptStr(kvps[i].Value),size));
                    }
                }
            }
            else
            {
                List<string> vs = new();
                for (int i = 0; i < condition.Count; i++)
                    vs.Add(condition[i].Value);
                long pos = GetDataLine(table,vs);
                if (pos == -1)
                    throw new LsException("满足条件的数据不存在");
                using BinaryWriter writer = new(new FileStream(Table.GetTablePath(_name, table), FileMode.Open, FileAccess.Write));
                for (int i = 0; i < kvps.Count; i++)
                {
                    ushort size = Columns.ReadFieldSize(_name, table, kvps[i].Key, out ushort po);
                    writer.BaseStream.Position = pos * len + po;
                    writer.Write(Auxiliary.ToBytesWithSign(_aes.EncryptStr(kvps[i].Value), size));
                }
            }
        }
        public void Insert(string table,KeyValues kvps)
        {
            long m = GetPosition(table);
            List<ushort> keys = Columns.ReadAllFieldSize(_name, table);
            if (kvps.Count != keys.Count)
                throw new LsException("数据不足");
            using BinaryWriter writer = new(new FileStream(Table.GetTablePath(_name,table), FileMode.Open, FileAccess.Write));
            if (m != -1)
                writer.BaseStream.Position = m;
            else
                writer.Seek(0, SeekOrigin.End);
            writer.Write(false);
            for (int i = 0; i < keys.Count; i++)
                writer.Write(Auxiliary.ToBytesWithSign(_aes.EncryptStr(kvps[i].Value), keys[i]));
        }
        public void Delete(string table,KeyValues condition)
        {
            long len = GetTableLineBytes(table);
            List<string> vs = new();
            for (int i = 0; i < condition.Count; i++)
                vs.Add(condition[i].Value);
            long pos = GetDataLine(table, vs);
            using BinaryWriter writer = new(new FileStream(Table.GetTablePath(_name, table), FileMode.Open, FileAccess.Write));
            for (int i = 0; i < condition.Count; i++)
            {
                writer.BaseStream.Position = pos * len;
                writer.Write(true);
            }
        }
        private long GetTableLineBytes(string table)
        {
            if (!GetTables().Contains(table))
                throw new LsException($"数据表{table}不存在");
            Dictionary<string, ushort[]> data = Columns.GetAllFieldsSize(_name, table);
            long position = 1;
            foreach(var field in data)
                position += field.Value[0];
            return position;
        }
        private List<string> GetAllData(string table,out List<string> keys)
        {
            List<string> data = new();
            keys = new();
            if (!GetTables().Contains(table))
                throw new LsException($"数据表{table}不存在");
            using BinaryReader reader = new(new FileStream
                (Table.GetTablePath(_name, table), FileMode.Open, FileAccess.Read));
            Dictionary<string, ushort[]> kvps = Columns.GetAllFieldsSize(_name, table);
            while(reader.BaseStream.Position<reader.BaseStream.Length)
            {
                data.Add(reader.ReadBoolean().ToString());
                foreach (var kvp in kvps)
                    data.Add(_aes.DecryptStr(Auxiliary.RemoveVazioChar(reader.ReadBytes(kvp.Value[0]))));
            }
            foreach (var kvp in kvps)
                keys.Add(kvp.Key);
            return data;
        }
        private List<string> GetValidData(string table,out List<string> keys)
        {
            List<string> list = GetAllData(table,out keys);
            Dictionary<string, ushort[]> kvps = Columns.GetAllFieldsSize(_name, table);
            while (list.Contains("True"))
                list.RemoveRange(list.IndexOf("True"), kvps.Count + 1);
            return list;
        }
        private int GetDataLine(string table, List<string> data)
        {
            List<string> list = GetValidData(table,out _);
            int count = Columns.GetAllFieldsSize(_name, table).Count + 1;
            for(int line = 0; line < list.Count;)
            {
                List<string> vs = new();
                for (int i = 0; i < count; i++)
                    vs.Add(list[line++]);
                bool tof = true;
                for(int i = 0; i < data.Count; i++)
                    tof = tof && vs.Contains(data[i]);
                if (tof)
                    return (line - 1) / count;
            }
            return -1;
        }
        private long GetPosition(string table)
        {
            long len = GetTableLineBytes(table);
            using BinaryReader reader = new(new FileStream(Table.GetTablePath(_name,table), FileMode.Open, FileAccess.Read));
            long po = -1;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (reader.ReadBoolean())
                    return reader.BaseStream.Position - 1;
                else
                    reader.BaseStream.Position += len - 1;
            }
            return po;
        }
    }
}
