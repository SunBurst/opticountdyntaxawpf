using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountExporter
{
    class ZooSample : Sample
    {
        private List<ZooPlankton> exportedSamples { get; set; }

        public ZooSample()
        {
            exportedSamples = new List<ZooPlankton>();
        }
    }
}
