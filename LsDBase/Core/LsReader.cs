using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsDBase.Core
{
    public class LsReader
    {
        private readonly List<string> _data = new();
        private readonly int count = 0;
        private int index = -1;
        internal LsReader(List<string> data,int count)
        {
            _data = data;
            this.count = count;
        }
        public bool Read()
        {
            if (++index * count < _data.Count)
                return true;
            else
            {
                index = -1;
                return false;
            }
        }
        public string GetString(int ind)
           => _data[index * count + ind];
        public long GetInt64(int ind)
        {
            if (long.TryParse(_data[index * count + ind], out long val))
                return val;
            else
                return default;
        }
        public int GetInt32(int ind)
        {
            if (int.TryParse(_data[index * count + ind], out int val))
                return val;
            else
                return default;
        }
        public DateTime GetDateTime(int ind)
        {
            if (DateTime.TryParse(_data[index * count + ind], out DateTime val))
                return val;
            else
                return default;
        }
        public bool GetBoolean(int ind)
        {
            if (bool.TryParse(_data[index * count + ind], out bool val))
                return val;
            else
                return default;
        }
        public byte GetByte(int ind)
        {
            if (byte.TryParse(_data[index * count + ind], out byte val))
                return val;
            else
                return default;
        }
        public sbyte GetSByte(int ind)
        {
            if (sbyte.TryParse(_data[index * count + ind], out sbyte val))
                return val;
            else
                return default;
        }
        public char GetChar(int ind)
        {
            if (char.TryParse(_data[index * count + ind], out char val))
                return val;
            else
                return default;
        }
        public decimal GetDecimal(int ind)
        {
            if (decimal.TryParse(_data[index * count + ind], out decimal val))
                return val;
            else
                return default;
        }
        public double GetDouble(int ind)
        {
            if (double.TryParse(_data[index * count + ind], out double val))
                return val;
            else
                return default;
        }
        public float GetFloat(int ind)
        {
            if (float.TryParse(_data[index * count + ind], out float val))
                return val;
            else
                return default;
        }
        public short GetInt16(int ind)
        {
            if (short.TryParse(_data[index * count + ind], out short val))
                return val;
            else
                return default;
        }
        public ushort GetUInt16(int ind)
        {
            if (ushort.TryParse(_data[index * count + ind], out ushort val))
                return val;
            else
                return default;
        }
        public uint GetUInt32(int ind)
        {
            if (uint.TryParse(_data[index * count + ind], out uint val))
                return val;
            else
                return default;
        }
        public ulong GetUInt64(int ind)
        {
            if (ulong.TryParse(_data[index * count + ind], out ulong val))
                return val;
            else
                return default;
        }
    }
}
