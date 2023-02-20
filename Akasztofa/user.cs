using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Akasztofa
{
    internal class user
    {
        private readonly string fid;
        private readonly string pw;
        private int konnyuossz;
        private int konnyunyert;
        private int kozepossz;
        private int kozepnyert;
        private int nehezossz;
        private int neheznyert;

        public user(string fid, string pw)
        {
            this.fid = fid;
            this.pw = pw;
            konnyuossz = 0;
            konnyunyert = 0;
            kozepossz = 0;
            konnyunyert = 0;
            neheznyert = 0;
            nehezossz = 0;
        }

        public user(int konnyuossz, int konnyunyert, int kozepossz, int kozepnyert, int nehezossz, int neheznyert)
        { 
            this.konnyuossz = konnyuossz;
            this.konnyunyert = konnyunyert;
            this.kozepossz = kozepossz;
            this.kozepnyert = kozepnyert;
            this.nehezossz = nehezossz;
            this.neheznyert = neheznyert;
        }

        public string Fid { get => fid; }
        public string Pw { get => pw; }
    }
}
