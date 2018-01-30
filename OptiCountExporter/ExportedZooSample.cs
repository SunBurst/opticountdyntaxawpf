using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountExporter
{
    class ExportedZooSample : ExportedSample
    {
        protected List<ZooPlankton> exportedSamples { get; set; }

        public ExportedZooSample()
        {
            exportedSamples = new List<ZooPlankton>();
        }
    }
}
