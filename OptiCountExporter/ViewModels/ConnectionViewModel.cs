using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountExporter
{
    public class ConnectionViewModel : BaseViewModel
    {

        #region Private Properties
        /// <summary>
        /// Indicates whether a connection to ArtDatabanken's SOAP service is established
        /// </summary>
        private bool connected = false;

        /// <summary>
        /// Indicates whether a connection to ArtDatabanken's SOAP service is established
        /// </summary>
        private string connectionStatusText = "Not connected to Dyntaxa";

        /// <summary>
        /// Status color indicator
        /// </summary>
        private string connectionStatusColor = "Red";

        public ConnectionViewModel()
        {

        }

        #endregion

        #region Public Properties

        public static ConnectionViewModel CreateNewStatus() {
            return new ConnectionViewModel();
        }

        public bool Connected
        {
            get
            {
                return this.connected;
            }
            set
            {
                if (value != this.connected)
                NotifyPropertyChanged();
            }
        }

        public string ConnectionStatusText
        {
            get
            {
                return this.connectionStatusText;
            }
            set
            {
                if (value != this.connectionStatusText)
                {
                    this.connectionStatusText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ConnectionStatusColor
        {
            get
            {
                return this.connectionStatusColor;
            }
            set
            {
                if (value != this.connectionStatusColor)
                {
                    this.connectionStatusColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

    }
}
