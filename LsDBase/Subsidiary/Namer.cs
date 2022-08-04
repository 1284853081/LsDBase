using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsDBase.Subsidiary
{
    internal static class Namer
    {
        public static string GetRandomName(bool tof = true)
        {
            if(tof)
                return Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            else
                return Path.ChangeExtension(Path.GetFileName(Path.GetRandomFileName()),"lsf");
        }
    }
}
