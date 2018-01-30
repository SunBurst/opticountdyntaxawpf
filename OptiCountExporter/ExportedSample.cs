using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountExporter
{
    public class ExportedSample
    {
        protected string Origin { get; set; }
        protected DateTime Date { get; set; }
        protected int SampleNumber { get; set; }
        protected string Method { get; set; }
        protected string Lab { get; set; }
    }
}
