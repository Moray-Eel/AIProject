using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject.Classes.Extensions
{
    public static class Extension
    {
        public static double NextDouble(this Random random, int min, int max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }
}
