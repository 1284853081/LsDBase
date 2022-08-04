using LsDBase.Core;
using System.Text;

namespace LsDBase.Subsidiary
{
    internal static class Auxiliary
    {
        public static string PsdProcessing(string password)
        {
            int count = Encoding.UTF8.GetByteCount(password);
            if (count < 16)
            {
                for (int i = 0; i < 16 - count; i++)
                    password += "\0";
            }
            else if (count > 16)
            {
                byte[] ps = Encoding.UTF8.GetBytes(password);
                byte[] pb = new byte[16];
                for (int i = 0; i < 16; i++)
                    pb[i] = ps[i];
                password = Encoding.UTF8.GetString(pb);
            }
            return password;
        }
        public static byte[] RemoveVazioChar(byte[] bytes)
        {
            List<byte> list = new();
            for(int i = 0; i < bytes.Length-3; i++)
            {
                if (bytes[i] == 255 && bytes[i + 1] == 254 && bytes[i + 2] == 253 && bytes[i + 3] == 255)
                    break;
                list.Add(bytes[i]);
            }
            return list.ToArray();
        }
        public static byte[] ToBytesWithSign(byte[] plain,int digit)
        {
            if (plain.Length - 4 <= digit)
            {
                byte[] result = new byte[digit];
                for (int i = 0; i < digit; i++)
                {
                    if (i < plain.Length)
                        result[i] = plain[i];
                    else if (i == plain.Length)
                    {
                        result[i] = 255;
                        result[i + 1] = 254;
                        result[i + 2] = 253;
                        result[i + 3] = 255;
                        i += 3;
                    }
                    else
                        result[i] = 0;
                }
                return result;
            }
            else
                throw new LsException("数据过长");
        }
        public static byte[] ToBytes(short num)
        {
            byte[] vs = new byte[2];
            int k = Math.Abs(num);
            for(int i = 0; i < 2; i++)
            {
                vs[1-i] = (byte)(k % 256);
                k >>= 8;
            }
            if (num < 0)
                vs[0] += 128;
            return vs;
        }
        public static byte[] ToBytes(ushort num)
        {
            byte k1 = (byte)(num % 256);
            byte k2 = (byte)(num >> 8);
            return new byte[] { k2, k1 };
        }
        public static byte[] ToBytes(int num)
        {
            byte[] vs = new byte[4];
            int k = Math.Abs(num);
            for(int i = 0; i < 4; i++)
            {
                vs[3-i] = (byte)(k % 256);
                k >>= 8;
            }
            if (num < 0)
                vs[0] += 128;
            return vs;
        }
        public static byte[] ToBytes(uint num)
        {
            byte[] vs = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                vs[3-i] = (byte)(num % 256);
                num >>= 8;
            }
            return vs;
        }
        public static byte[] ToBytes(long num)
        {
            byte[] vs = new byte[8];
            long k = Math.Abs(num);
            for (int i = 0; i < 8; i++)
            {
                vs[7-i] = (byte)(k % 256);
                k >>= 8;
            }
            if (num < 0)
                vs[0] += 128;
            return vs;
        }
        public static byte[] ToBytes(ulong num)
        {
            byte[] vs = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                vs[7-i] = (byte)(num % 256);
                num >>= 8;
            }
            return vs;
        }
        public static short ToShort(byte[] bytes)
        {
            if (bytes.Length != 2)
                throw new ArgumentException();
            if (bytes[0] >= 128)
                return (short)-((bytes[0] - 128) * 256 + bytes[1]);
            else
                return (short)(bytes[0] * 256 + bytes[1]);
        }
        public static ushort ToUShort(byte[] bytes)
        {
            if (bytes.Length != 2)
                throw new ArgumentException("数组大小不正确");
            return (ushort)(bytes[0] * 256 + bytes[1]);
        }
        public static int ToInt(byte[] bytes)
        {
            int rs = 0;
            if (bytes.Length != 4)
                throw new ArgumentException("数组大小不正确");
            bool tof = bytes[0] >= 128;
            if (tof)
                bytes[0] -= 128;
            for (int i = 0; i < 4; i++)
            {
                rs += bytes[i];
                rs *= 256;
            }
            if(tof)
                return -rs;
            else
                return rs;
        }
        public static uint ToUInt(byte[] bytes)
        {
            uint rs = 0;
            if (bytes.Length != 4)
                throw new ArgumentException("数组大小不正确");
            for (int i = 0; i < 4; i++)
            {
                rs += bytes[i];
                rs *= 256;
            }
            return rs;
        }
        public static long ToLong(byte[] bytes)
        {
            long rs = 0;
            if (bytes.Length != 8)
                throw new ArgumentException("数组大小不正确");
            bool tof = bytes[0] >= 128;
            if (tof)
                bytes[0] -= 128;
            for (int i = 0; i < 8; i++)
            {
                rs += bytes[i];
                rs *= 256;
            }
            if (tof)
                return -rs;
            else
                return rs;
        }
        public static ulong ToULong(byte[] bytes)
        {
            ulong rs = 0;
            if (bytes.Length != 8)
                throw new ArgumentException("数组大小不正确");
            for (int i = 0; i < 8; i++)
            {
                rs += bytes[i];
                rs *= 256;
            }
            return rs;
        }
    }
}
