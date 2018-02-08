using System;
using System.Collections.Generic;

namespace OptiCountExporter
{
    /// <summary>
    /// Information about a sample
    /// </summary>
    public class Sample
    {
        /// <summary>
        /// List of samples
        /// </summary>
        protected List<Plankton> exportedSamples { get; set; }

        /// <summary>
        /// Where the sample was taken
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// When the sample was taken
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Sample number
        /// </summary>
        public int SampleNumber { get; set; }

        /// <summary>
        /// Name of input file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The input file's absolute path
        /// </summary>
        public string FilePath { get; set; }
    }
}
