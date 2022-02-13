// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer;

public class UserSettings : SettingsManager<UserSettings>, INotifyPropertyChanged
{
    #region Properties
    public int DarkMode
    {
        get => darkmode;
        set
        {
            darkmode = value;
            OnPropertyChanged();
        }
    }

    public int GridFontWeight
    {
        get => gridFontWeight;
        set
        {
            gridFontWeight = value;
            OnPropertyChanged();
        }
    }

    public bool IncludeDebug
    {
        get => includeDebug;
        set
        {
            includeDebug = value;
            OnPropertyChanged();
        }
    }
    public bool KeepOnTop
    {
        get => keepOnTop;
        set
        {
            keepOnTop = value;
            OnPropertyChanged();
        }
    }
    public int PrimaryColor
    {
        get => primaryColor;
        set
        {
            primaryColor = value;
            OnPropertyChanged();
        }
    }

    public int RowSpacing
    {
        get => rowSpacing;
        set
        {
            rowSpacing = value;
            OnPropertyChanged();
        }
    }

    public int UISize
    {
        get => uiSize;
        set
        {
            uiSize = value;
            OnPropertyChanged();
        }
    }
    public double WindowHeight
    {
        get
        {
            if (windowHeight < 100)
            {
                windowHeight = 100;
            }
            return windowHeight;
        }
        set => windowHeight = value;
    }

    public double WindowLeft
    {
        get
        {
            if (windowLeft < 0 || windowLeft >= SystemParameters.VirtualScreenWidth)
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
            if (windowTop < 0 || windowTop >= SystemParameters.VirtualScreenHeight)
            {
                windowTop = 0;
            }
            return windowTop;
        }
        set => windowTop = value;
    }

    public double WindowWidth
    {
        get
        {
            if (windowWidth < 100)
            {
                windowWidth = 100;
            }
            return windowWidth;
        }
        set => windowWidth = value;
    }
    #endregion Properties

    #region Private backing fields
    private int darkmode = (int)ThemeType.Light;
    private int gridFontWeight = (int)Weight.Regular;
    private bool includeDebug = false;
    private bool keepOnTop = false;
    private int primaryColor = (int)AccentColor.Blue;
    private int rowSpacing = (int)Spacing.Comfortable;
    private int uiSize = (int)MySize.Default;
    private double windowHeight = 500;
    private double windowLeft = 100;
    private double windowTop = 100;
    private double windowWidth = 300;
    #endregion Private backing fields

    #region Handle property change event
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion Handle property change event
}
