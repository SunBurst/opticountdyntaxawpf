using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace OptiCountExporter
{
    /// <summary>
    /// A base view model that fires Property Changed events as needed
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Private Properties

        /// <summary>
        /// Session object for querying ArtDatabanken's SOAP service
        /// </summary>
        private static DyntaxaService _dyntaxaSession;

        #endregion

        protected DyntaxaService DyntaxaSession
        { get { return _dyntaxaSession;  } }

        #region Constructor
        
        /// <summary>
        /// Initializes the ArtDatabanken SOAP session object
        /// </summary>
        public BaseViewModel() {
            _dyntaxaSession = GlobalVariables.SetupDyntaxaService();
        }

        #endregion

        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
