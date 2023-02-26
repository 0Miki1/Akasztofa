using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akasztofa
{
    class szo
    {
        private string word;
        private int nehezseg;
        private static List<string> nehezsegek = new List<string> { "Könnyű", "Közepes", "Nehéz" };

        public szo(string word, int nehezseg)
        {
            this.word = word;
            this.nehezseg = nehezseg;
        }

        public szo(string word)
        {
            this.word = word;
            nehezseg = -1;
        }

        public static List<string> Nehezsegek { get => nehezsegek; }
        public string Word { get => word; }
        public int Nehezseg { get => nehezseg; }

        public override string ToString()
        {
            return $"{word} | [{nehezsegek[nehezseg + 1]}]";
        }
    }
}
