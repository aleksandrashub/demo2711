using demo21111.Context;
using demo21111.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo21111
{
    public static class Helper
    {
        public static readonly User11129Context user11129 = new User11129Context();
        public static Client curruser = new Client();
        public static int role;
        public static Zakaz zakaz = null;

    }
}
