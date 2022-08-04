using LsDBase.Subsidiary;
using System.Security.Cryptography;
using System.Text;

namespace LsDBase.Core
{
    internal class Aes
    {
        private readonly AesCng builder;
        private readonly byte[] vector = new byte[16] { 23, 31, 41, 47, 59, 67, 73, 83, 97, 103, 109, 127, 137, 149, 157, 167 };
        internal Aes(string key)
        {
            key = Auxiliary.PsdProcessing(key);
            builder = new();
            builder.Key = Encoding.Default.GetBytes(key);
        }
        internal Aes()
        {
            builder = new();
            builder.Key = new byte[16] { 2, 7, 17, 23, 37, 47, 59, 71, 83, 97, 107, 127, 137, 151, 167, 179 };
        }
        public byte[] EncryptStr(string s)
        {
            List<int> index = new();
            char[] chars = s.ToCharArray();
            List<byte> bytes = new();
            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 0 && chars[i] > 255)
                    index.Add(0);
                else if (i > 0 && chars[i - 1] <= 255 && chars[i] > 255)
                    index.Add(bytes.Count + 3 * index.Count);
                if (chars[i] > 255)
                {
                    int m = chars[i];
                    int b = m % 251;
                    int c = b % 13;
                    int k1 = (m - b) / 251;
                    int k2 = (b - c) / 13;
                    bytes.Add((byte)k1);
                    bytes.Add((byte)k2);
                    bytes.Add((byte)c);
                }
                else
                    bytes.Add((byte)chars[i]);
                if (i < chars.Length - 1 && chars[i] > 255 && chars[i + 1] <= 255)
                    index.Add(bytes.Count + 3 * index.Count);
                else if (i == chars.Length - 1 && chars[i] > 255)
                    index.Add(bytes.Count + 3 * index.Count);
            }
            byte[] cipherBytes = builder.EncryptCfb(bytes.ToArray(), vector);
            List<byte> cipher = cipherBytes.ToList();
            for (int i = 0; i < index.Count; i += 2)
            {
                cipher.Insert(index[i], 2);
                cipher.Insert(index[i], 1);
                cipher.Insert(index[i], 0);
                cipher.Insert(index[i + 1], 2);
                cipher.Insert(index[i + 1], 1);
                cipher.Insert(index[i + 1], 0);
            }
            return cipher.ToArray();
        }
        public byte[] Encrypt(byte[] s)
            => builder.EncryptCfb(s, vector);
        public string DecryptStr(byte[] cipherBytes)
        {
            List<int> index = new();
            List<byte> bytes = new();
            for (int i = 0; i < cipherBytes.Length; i++)
            {
                if(i+2<cipherBytes.Length)
                {
                    if (cipherBytes[i] == 0 && cipherBytes[i + 1] == 1 && cipherBytes[i + 2] == 2)
                    {
                        i += 2;
                        index.Add(bytes.Count);
                        continue;
                    }
                }
                bytes.Add(cipherBytes[i]);
            }
            byte[] plainBytes = builder.DecryptCfb(bytes.ToArray(), vector);
            StringBuilder sb = new();
            bool ischar = false;
            for (int i = 0; i < plainBytes.Length; i++)
            {
                if (index.Contains(i))
                    ischar = !ischar;
                if (ischar)
                {
                    for (int j = i; j < index[index.IndexOf(i) + 1]; j += 3)
                    {
                        byte k1 = plainBytes[j];
                        byte k2 = plainBytes[j + 1];
                        byte k3 = plainBytes[j + 2];
                        sb.Append((char)(k1 * 251 + k2 * 13 + k3));
                    }
                    i = index[index.IndexOf(i) + 1] - 1;
                }
                else
                    sb.Append((char)plainBytes[i]);
            }
            return sb.ToString();
        }
        public byte[] Decrypt(byte[] cipher)
            => builder.DecryptCfb(cipher, vector);
    }
}
