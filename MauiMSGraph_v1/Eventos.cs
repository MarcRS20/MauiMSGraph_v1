using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiMSGraph_v1
{
    public class Eventos
    {
        private static int _count = 1;

        public int IDNumber { get; set; }
        public string Name { get; set; }
        public string Hour { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }

        public Eventos()
        {
            IDNumber = ++_count;
        }
    }
}
