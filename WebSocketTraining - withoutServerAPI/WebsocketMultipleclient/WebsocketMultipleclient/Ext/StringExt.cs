using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSocketServerProject.Ext
{
    public static class StringExt
    {
        public static void Dump(this string chaine, ConsoleColor color)
        {
            Console.ForegroundColor=color;
            Console.Write(chaine);
        }
    }
}
