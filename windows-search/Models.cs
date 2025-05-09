using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_search
{
    public struct Result
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public List<string> Paths { get; set; }
    }
}
