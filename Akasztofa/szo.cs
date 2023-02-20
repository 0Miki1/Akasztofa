using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akasztofa
{
    internal class szo
    {
        private string word;
        private int nehezseg;
        private static List<string> nehezsegek;

        public szo(string word, int nehezseg)
        {
            this.word = word;
            this.nehezseg = nehezseg;
            nehezsegek = new List<string>() { "Könnyű", "Közepes", "Nehéz" };
        }

        public static List<string> Nehezsegek { get => nehezsegek; }
        public string Word { get => word; }
        public int Nehezseg { get => nehezseg; }
    }
}
