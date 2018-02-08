using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountExporter
{
    class PhytoSample : Sample
    {
        /// <summary>
        /// List of phytoplankton samples
        /// </summary>
        private List<PhytoPlankton> exportedSamples { get; set; }

        public PhytoSample()
        {
            exportedSamples = new List<PhytoPlankton>();
        }

        public void AddPhyto(PhytoPlankton phyto)
        {
            this.exportedSamples.Add(phyto);
        }

        public void PrintSamples()
        {
            foreach (var sample in this.exportedSamples)
            {
                Console.WriteLine(sample);
            }
        }

    }
}
