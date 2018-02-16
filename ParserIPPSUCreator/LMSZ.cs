using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserIPPSUCreator
{
    public class LMSZ
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EgissoId { get; set; }

        public LMSZ(int id, string name, string egissoid)
        {
            Id = id;
            Name = name;
            EgissoId = egissoid;
        }
    }
}
