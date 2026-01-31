using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.ConsoleApp
{
    public static class Global
    {
        private static string _Menu1 { get; set; }
        private static string _Menu2 { get; set; }
        private static string _Menu3 { get; set; }

        public static string Menu1{
            get => _Menu1;
            set => _Menu1 = value;
        }

        public static string Menu2{
            get => _Menu2;
            set => _Menu2 = value;
        }

        public static string Menu3{
            get => _Menu3;
            set => _Menu3 = value;
        }
    }
}