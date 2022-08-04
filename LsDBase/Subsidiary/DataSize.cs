using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsDBase.Subsidiary
{
    public class DataSize
    {
        internal ushort Size { get; }
        private DataSize(ushort size)
            =>Size = size;
        public static DataSize UShort { get => new(9); }
        public static DataSize Short { get => new(10); }  
        public static DataSize UInt { get => new(15); }
        public static DataSize Int { get => new(15); } 
        public static DataSize ULong { get => new(24); }
        public static DataSize Long { get => new(24); }
        public static DataSize String16 { get => new(20); }
        public static DataSize String32 { get => new(36); }
        public static DataSize String64 { get => new(68); }
        public static DataSize String128 { get => new(132); }
        public static DataSize String256 { get => new(260); }
        public static DataSize String512 { get => new(516); }
        public static DataSize String1024 { get => new(1028); }
        public static DataSize String2048 { get => new(2052); }
        public static DataSize String4096 { get => new(4100); }
        public static DataSize String8192 { get => new(8196); }
    }
}
