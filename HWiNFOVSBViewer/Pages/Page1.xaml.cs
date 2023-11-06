// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Pages;

/// <summary>
/// Displays the primary page
/// </summary>
public partial class Page1 : UserControl
{
    #region NLog Instance
    private static readonly Logger log = LogManager.GetCurrentClassLogger();
    #endregion NLog Instance

    #region Regex instances
    private static readonly Regex numOnly = new(@"\D");
    private static readonly Regex noNums = new(@"\d");
    #endregion Regex instances

    #region Static property for Page1
    internal static Page1 P1 { get; set; }
    #endregion Static property for Page1

    public Page1()
    {
        InitializeComponent();
    }

    #region Page Loaded event
    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        P1 = this;
        HWiNFO.HWList.Clear();
        CheckRegistry();
        SetFontWeight((Weight)UserSettings.Setting.GridFontWeight);
        SetRowSpacing((Spacing)UserSettings.Setting.RowSpacing);
    }
    #endregion Page Loaded event

    #region Set the row spacing
    /// <summary>
    /// Sets the padding around the rows in the datagrid
    /// </summary>
    /// <param name="spacing"></param>
    public void SetRowSpacing(Spacing spacing)
    {
        switch (spacing)
        {
            case Spacing.Compact:
                DataGridAssist.SetCellPadding(HWGrid, new Thickness(15, 2, 15, 2));
                break;
            case Spacing.Comfortable:
                DataGridAssist.SetCellPadding(HWGrid, new Thickness(15, 5, 15, 5));
                break;
            case Spacing.Wide:
                DataGridAssist.SetCellPadding(HWGrid, new Thickness(15, 8, 15, 8));
                break;
        }
    }
    #endregion Set the row spacing

    #region Set the font weight
    /// <summary>
    /// Sets the weight of the font in the datagrid
    /// </summary>
    /// <param name="weight"></param>
    public void SetFontWeight(Weight weight)
    {
        switch (weight)
        {
            case Weight.Thin:
                HWGrid.FontWeight = FontWeights.Thin;
                break;
            case Weight.Regular:
                HWGrid.FontWeight = FontWeights.Regular;
                break;
            case Weight.SemiBold:
                HWGrid.FontWeight = FontWeights.SemiBold;
                break;
            case Weight.Bold:
                HWGrid.FontWeight = FontWeights.Bold;
                break;
        }
    }
    #endregion Set the font weight

    #region Check registry for HWiNFO64 and HWiNFO32
    /// <summary>
    /// Determines if registry key for HWiNFO64 or HWiNFO32 is present
    /// and sets the HWiNFO.RegistryKey property accordingly.
    /// Then the methods to read the registry and load the datagrid are called.
    /// </summary>
    private void CheckRegistry()
    {
        RegistryKey key64 = Registry.CurrentUser.OpenSubKey("Software\\HWiNFO64\\VSB");
        RegistryKey key32 = Registry.CurrentUser.OpenSubKey("Software\\HWiNFO32\\VSB");

        if (key64 != null)
        {
            HWiNFO.RegistryKey = "Software\\HWiNFO64\\VSB";
            key64.Close();
            key64.Dispose();
            log.Debug("HKCU\\Software\\HWiNFO64\\VSB was found.");
            ReadVSB();
            LoadGrid();
        }
        else if (key32 != null)
        {
            HWiNFO.RegistryKey = "Software\\HWiNFO32\\VSB";
            key32.Close();
            key32.Dispose();
            log.Debug("HKCU\\Software\\HWiNFO32\\VSB was found.");
            ReadVSB();
            LoadGrid();
        }
        else
        {
            HWiNFO info = new()
            {
                Index = 0,
                Sensor = GetStringResource("MsgText_NoRegistryValues")
            };
            log.Error("No registry values found.");
            HWiNFO.HWList.Add(info);
            LoadGrid();
            _ = IsHWiNFORunningAsync();
        }
    }
    #endregion Check registry for HWiNFO64 and HWiNFO32

    #region Check to see if HWiNFO is running
    /// <summary>
    /// Determines if HWiNFO is running. Called only if registry keys were not found.
    /// Displays an error dialog.
    /// </summary>
    /// <returns></returns>
    private static async Task IsHWiNFORunningAsync()
    {
        Process[] pname64 = Process.GetProcessesByName("HWiNFO64");
        Process[] pname32 = Process.GetProcessesByName("HWiNFO32");
        if (pname64.Length == 0 && pname32.Length == 0)
        {
            log.Error("HWiNFO is not running");
            ErrorDialog error = new()
            {
                Message = $"{GetStringResource("MsgText_HWiNFONotRunning")}\n\n{GetStringResource("MsgText_HWiNFONotFound")}"
            };
            _ = await DialogHost.Show(error, "MainDialogHost");
        }
    }
    #endregion Check to see if HWiNFO is running

    #region Read registry and add values to a list
    /// <summary>
    /// Reads the appropriate registry key to build the list that populates the datagrid
    /// </summary>
    private static void ReadVSB()
    {
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(HWiNFO.RegistryKey);
        HWiNFO info = new();
        if (key != null)
        {
            foreach (string valname in key.GetValueNames())
            {
                string value = Registry.GetValue(key.Name, valname, "missing").ToString();
                // The registry values are in the form of
                //    Color#
                //    Label#
                //    Sensor#
                //    Value#
                //    ValueRaw#
                // The following regex & switch will separate the numeric part (#) from the registry value
                // assign it to the "Index" and the other part to the corresponding value in the HWiNFO class.
                // When a match is made with "valueraw" all of the values are added to HWList and the process
                // repeats until all of the registry values have been read.
                string index = numOnly.Replace(valname, "");
                string regText = noNums.Replace(valname, "");
                info.Index = int.Parse(index);
                switch (regText.ToLower())
                {
                    case "color":
                        // Don't need the color value
                        break;
                    case "sensor":
                        info.Sensor = value;
                        break;
                    case "label":
                        info.Label = value;
                        break;
                    case "value":
                        info.Value = value;
                        break;
                    case "valueraw":
                        info.ValueRaw = value;
                        HWiNFO.HWList.Add(info);
                        info = new HWiNFO();
                        break;
                }
            }
            log.Debug($"{HWiNFO.HWList.Count} records parsed from {key.ValueCount} registry values");
            SnackbarMsg.QueueMessage($"{HWiNFO.HWList.Count} {GetStringResource("MsgText_RecordsParsed1")}" +
                                            $" {key.ValueCount} {GetStringResource("MsgText_RecordsParsed2")}", 2000);
        }
    }
    #endregion Read registry and add values to a list

    #region Load the datagrid
    /// <summary>
    /// Sorts the list by Index and assigns the list as the items source for the datagrid
    /// </summary>
    private void LoadGrid()
    {
        HWGrid.ItemsSource = HWiNFO.HWList.OrderBy(x => x.Index).ToList();
    }
    #endregion Load the datagrid

    #region Reread the registry and refresh the datagrid
    /// <summary>
    /// Refreshes the datagrid
    /// </summary>
    public void RefreshData()
    {
        HWiNFO.HWList.Clear();
        CheckRegistry();
        ResetCols();
        HWGrid.Items.Refresh();
        if (tbxSearch.Text.Length > 0)
        {
            FilterTheGrid();
        }
        if (HWGrid.Items.Count == 1)
        {
            SnackbarMsg.ClearAndQueueMessage(GetStringResource("MsgText_RefreshedItems1"), 1000);
        }
        else
        {
            SnackbarMsg.ClearAndQueueMessage($"{HWGrid.Items.Count} {GetStringResource("MsgText_RefreshedItemsMany")}", 1000);
        }
    }
    #endregion Reread the registry and refresh the datagrid

    #region Reset column sort
    /// <summary>
    /// Removes any sorts from the datagrid columns
    /// </summary>
    public void ResetCols()
    {
        foreach (DataGridColumn column in HWGrid.Columns)
        {
            column.SortDirection = null;
        }
        HWGrid.Items.SortDescriptions.Clear();

        SnackbarMsg.ClearAndQueueMessage(GetStringResource("MsgText_ColumnSortCleared"), 1000);
    }
    #endregion Reset column sort

    #region Copy to clipboard
    /// <summary>
    /// Copies the datagrid to the Windows clipboard.
    /// This method is also used by the save CSV file method.
    /// </summary>
    private void Copy2Clipboard()
    {
        // Clear the clipboard
        Clipboard.Clear();

        // Include the header row
        HWGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;

        // Temporarily set selection mode to all rows
        HWGrid.SelectionMode = DataGridSelectionMode.Extended;

        // Select all the cells
        HWGrid.SelectAllCells();

        // Execute the copy
        ApplicationCommands.Copy.Execute(null, HWGrid);

        // Unselect the cells
        HWGrid.UnselectAllCells();

        // Set selection mode back to one row
        HWGrid.SelectionMode = DataGridSelectionMode.Single;
    }
    #endregion Copy to clipboard

    #region Save grid to CSV file
    /// <summary>
    /// Presents a file save dialog and then builds a CSV file by using the
    ///  built-in Clipboard DataFormats.CommaSeparatedValue method.
    /// </summary>
    private async Task SaveToCSVAsync()
    {
        string fname = "HWiNFO_VSB_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".csv";
        SaveFileDialog dialog = new()
        {
            Title = GetStringResource("MenuItem_SaveCSV"),
            Filter = "CSV File|*.csv",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            FileName = fname
        };
        var result = dialog.ShowDialog();
        if (result == true)
        {
            try
            {
                Copy2Clipboard();
                string gridData = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                File.WriteAllText(dialog.FileName, gridData, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error saving file.");
                ErrorDialog error = new()
                {
                    Message = $"{GetStringResource("MsgText_ErrorSavingFile")}\n\n{ex.Message}"
                };
                _ = await DialogHost.Show(error, "MainDialogHost");
            }
        }
    }
    #endregion Save grid to CSV file

    #region Save to HTML file
    /// <summary>
    /// Builds an HTML file then presents a file save dialog
    /// </summary>
    private async Task SaveToHtmlAsync()
    {
        StringBuilder sb = new();
        sb.AppendLine("<!DOCTYPE HTML>")
            .AppendLine("<html>")
            .AppendLine("<head>")
            .AppendLine("<title> HWiNFO Registry Info </title>")
            .AppendLine("<meta http-equiv='content-type' content='text/html;charset = utf-8' />")
            .AppendLine("</head>")
            .AppendLine("<style>")
            .AppendLine("body {font-family: Sans-serif; font-size: 90%; background-color: #E1E3E6;}")
            .AppendLine("td { padding: 5px;}")
            .AppendLine("th { background-color: #AFEEEE; color: #2F2F2F; padding: 7px 0px 7px 0px;}")
            .Append("table {table-layout: fixed; width: 100%; background-color: #FAFAFA; ")
            .AppendLine("box-shadow: 0 0 10px 10px #9E9E9E;}")
            .Append("table, th, td { border-style: solid; border-width: 1px; border-color: #2F2F2F; ")
            .AppendLine("border-collapse: collapse; word-wrap: break-word;}")
            .AppendLine("div {width: 95%; position: absolute; top: 0; bottom: 0; left: 0; right: 0; margin: auto;} ")
            .AppendLine("</style>")
            .AppendLine("<body><div><br>")
            .AppendLine("<table>")
            .AppendLine("<tr>")
            .Append("<th style='width: 3.5%;'>").Append(GetStringResource("GridColumn_Index")).Append("</th>")
            .Append("<th style='width: 25%;'>").Append(GetStringResource("GridColumn_Sensor")).Append("</th>")
            .Append("<th style='width: 25%;'>").Append(GetStringResource("GridColumn_Label")).Append("</th>")
            .Append("<th style='width: 10%;'>").Append(GetStringResource("GridColumn_Value")).Append("</th>")
            .Append("<th style='width: 10%;'>").Append(GetStringResource("GridColumn_ValueRaw")).Append("</th>")
            .AppendLine("</tr>");
        foreach (HWiNFO row in HWGrid.Items)
        {
            sb.Append("<tr>")
                .Append("<td style='text-align: center'>").Append(row.Index).Append("</td>")
                .Append("<td >").Append(row.Sensor).Append("</td>")
                .Append("<td >").Append(row.Label).Append("</td>")
                .Append("<td >").Append(row.Value).Append("</td>")
                .Append("<td >").Append(row.ValueRaw).Append("</td>")
                .Append("</tr>");
        }
        sb.AppendLine("</table>")
            .AppendLine("</br></div></body>")
            .AppendLine("</html>");
        string html = sb.ToString();

        string fname = "HWiNFO_VSB_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".html";
        SaveFileDialog dialog = new()
        {
            Title = GetStringResource("MenuItem_SaveHTML"),
            Filter = "HTML File|*.html",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            FileName = fname
        };
        bool? result = dialog.ShowDialog();
        if (result == true)
        {
            try
            {
                File.WriteAllText(dialog.FileName, html, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error saving file.");
                ErrorDialog error = new()
                {
                    Message = $"{GetStringResource("MsgText_ErrorSavingFile")}\n\n{ex.Message}"
                };
                _ = await DialogHost.Show(error, "MainDialogHost");
            }
        }
    }
    #endregion Save to HTML

    #region Mouse enter/leave shadow effect
    /// <summary>
    /// Subtle change in shadow effect when mouse is over cards
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Card_MouseEnter(object sender, MouseEventArgs e)
    {
        Card card = sender as Card;
        ElevationAssist.SetElevation(card, Elevation.Dp4);
    }

    private void Card_MouseLeave(object sender, MouseEventArgs e)
    {
        Card card = sender as Card;
        ElevationAssist.SetElevation(card, Elevation.Dp2);
    }
    #endregion Mouse enter/leave shadow effect

    #region Menu click events
    private void MnuCopy_Click(object sender, RoutedEventArgs e)
    {
        Copy2Clipboard();
        SnackbarMsg.ClearAndQueueMessage($"{HWGrid.Items.Count} rows copied to the clipboard");
    }

    private async void MnuSaveToCsv_Click(object sender, RoutedEventArgs e)
    {
        await SaveToCSVAsync();
    }

    private async void MnuSaveToHtml_Click(object sender, RoutedEventArgs e)
    {
        await SaveToHtmlAsync();
    }

    private void MnuRefresh_Click(object sender, RoutedEventArgs e)
    {
        RefreshData();
    }

    private void MnuRemoveSort_Click(object sender, RoutedEventArgs e)
    {
        ResetCols();
    }

    private void GridSmaller_Click(object sender, RoutedEventArgs e)
    {
        MainWindowUIHelpers.EverythingSmaller();
    }

    private void GridLarger_Click(object sender, RoutedEventArgs e)
    {
        MainWindowUIHelpers.EverythingLarger();
    }
    #endregion Menu click events

    #region Filter textbox changed event
    /// <summary>
    /// Used by the "filter" textbox at the top
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TbxSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        FilterTheGrid();
    }

    private void FilterTheGrid()
    {
        string filter = tbxSearch.Text;

        if (string.Equals(filter, "~"))
        {
            filter = "\u00b0";
        }

        ICollectionView cv = CollectionViewSource.GetDefaultView(HWGrid.ItemsSource);
        if (filter?.Length == 0)
        {
            cv.Filter = null;
            SnackbarMsg.ClearAndQueueMessage(GetStringResource("MsgText_RowsShownAll"), 2000);
        }
        else
        {
            cv.Filter = o =>
            {
                HWiNFO hw = o as HWiNFO;
                return hw.Label.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                       hw.Sensor.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                       hw.Value.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                       hw.Index.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase);
            };
            if (HWGrid.Items.Count == 1)
            {
                SnackbarMsg.ClearAndQueueMessage(GetStringResource("MsgText_RowsShown1"), 1000);
            }
            else
            {
                SnackbarMsg.ClearAndQueueMessage($"{HWGrid.Items.Count} {GetStringResource("MsgText_RowsShownMany")}", 1000);
            }
        }
    }
    #endregion Filter textbox changed event
}
