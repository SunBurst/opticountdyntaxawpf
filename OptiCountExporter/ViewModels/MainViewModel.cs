using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OptiCountExporter
{
    public class MainViewModel
    {
        #region Public Properties

        public ConnectionViewModel connectionViewModel { get; set; }
        public SampleViewModel sampleViewModel { get; set; }

        #endregion

        public MainViewModel(IDialogService dialogService)
        {
            this.connectionViewModel = new ConnectionViewModel();
            this.sampleViewModel = new SampleViewModel(dialogService);
        }

    }
}
