using ArtDatabanken.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountExporter
{
    public class DyntaxaMatchingDialogViewModel : DialogViewModel
    {

        public DyntaxaMatchingDialogViewModel(string message, string search, TaxonNameList result) : base(message)
        {
            Search = search;
            Result = result;
        }

        public string Search { get; set; }
        public TaxonNameList Result { get; }
    }
}
