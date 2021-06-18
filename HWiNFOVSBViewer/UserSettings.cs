// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

#region Using directives
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TKUtils;
#endregion Using directives

namespace HWiNFOVSBViewer
{
    public class UserSettings : SettingsManager<UserSettings>, INotifyPropertyChanged
    {
        #region Constructor
        public UserSettings()
        {
            // Set defaults
            GridZoom = 1;
            WindowLeft = 100;
            WindowTop = 100;
        }
        #endregion Constructor

        #region Properties
        public double GridZoom
        {
            get
            {
                if (gridZoom <= 0)
                {
                    gridZoom = 1;
                }
                return gridZoom;
            }
            set
            {
                gridZoom = value;
                OnPropertyChanged();
            }
        }

        public bool ShadeAltRows
        {
            get => shadeAltRows;
            set
            {
                shadeAltRows = value;
                OnPropertyChanged();
            }
        }

        public bool ShowGridLines
        {
            get => showGridLines;
            set
            {
                showGridLines = value;
                OnPropertyChanged();
            }
        }

        public double WindowLeft
        {
            get
            {
                if (windowLeft < 0)
                {
                    windowLeft = 0;
                }
                return windowLeft;
            }
            set => windowLeft = value;
        }

        public double WindowTop
        {
            get
            {
                if (windowTop < 0)
                {
                    windowTop = 0;
                }
                return windowTop;
            }
            set => windowTop = value;
        }
        #endregion Properties

        #region Private backing fields
        private double gridZoom;
        private bool shadeAltRows;
        private bool showGridLines;
        private double windowLeft;
        private double windowTop;
        #endregion Private backing fields

        #region Handle property change event
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion Handle property change event
    }
}
