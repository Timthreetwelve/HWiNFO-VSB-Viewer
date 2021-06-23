// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using NLog;
using TKUtils;
#endregion Using directives

namespace HWiNFOVSBViewer
{
    public partial class MainWindow : Window
    {
        #region NLog Instance
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        #endregion NLog Instance

        #region Regex instances
        private static readonly Regex numOnly = new Regex(@"\D");
        private static readonly Regex noNums = new Regex(@"\d");
        #endregion Regex instances

        public MainWindow()
        {
            UserSettings.Init(UserSettings.AppFolder, UserSettings.DefaultFilename, true);

            InitializeComponent();

            ReadSettings();
        }

        #region Settings
        private void ReadSettings()
        {
            // Change the log file filename when debugging
            string env = Debugger.IsAttached ? "debug" : "temp";
            GlobalDiagnosticsContext.Set("TempOrDebug", env);

            // Startup message in the temp file
            log.Info($"{AppInfo.AppName} {AppInfo.TitleVersion} is starting up");

            // Unhandled exception handler
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Settings change event
            UserSettings.Setting.PropertyChanged += UserSettingChanged;

            // Window position
            Top = UserSettings.Setting.WindowTop;
            Left = UserSettings.Setting.WindowLeft;
            Width = UserSettings.Setting.WindowWidth;
            Height = UserSettings.Setting.WindowHeight;

            // Window state
            WindowState = WindowState.Normal;

            // Max screen height
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            // Set Datagrid zoom
            double curZoom = UserSettings.Setting.GridZoom;
            HWGrid.LayoutTransform = new ScaleTransform(curZoom, curZoom);

            // Alternate row shading
            if (UserSettings.Setting.ShadeAltRows)
            {
                AltRowShadingOn();
            }

            // Show grid lines
            if (!UserSettings.Setting.ShowGridLines)
            {
                HWGrid.GridLinesVisibility = DataGridGridLinesVisibility.None;
            }

            Title = AppInfo.AppName + " - " + AppInfo.TitleVersion;
        }
        #endregion Settings

        #region Check to see if HWiNFO is running
        private void IsHWiNFORunning()
        {
            Process[] pname64 = Process.GetProcessesByName("HWiNFO64");
            Process[] pname32 = Process.GetProcessesByName("HWiNFO32");
            if (pname64.Length == 0 && pname32.Length == 0)
            {
                log.Error("HWiNFO is not running");
                _ = MessageBox.Show("HWiNFO is not running",
                                    "ERROR",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
            }
            else
            {
                log.Debug("HWiNFO is running");
                ReadVSB();
                LoadGrid();
            }
        }
        #endregion Check to see if HWiNFO is running

        #region Read registry and add values to a list
        private void ReadVSB()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\HWiNFO64\\VSB"))
            {
                HWiNFO info = new HWiNFO();
                if (key != null)
                {
                    log.Debug($"{key.ValueCount} registry values found");
                    foreach (string valname in key.GetValueNames())
                    {
                        string value = Registry.GetValue(key.Name, valname, "missing").ToString();
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
                }
                else
                {
                    info.Index = 0;
                    info.Sensor = "No registry values found. Is HWiNFO running and configured correctly?";
                    log.Error("No registry values found. Is HWiNFO running and configured correctly?");
                    HWiNFO.HWList.Add(info);
                }
            }
        }
        #endregion Read registry and add values to a list

        #region Load the datagrid
        private void LoadGrid()
        {
            List<HWiNFO> sortedList = HWiNFO.HWList;
            sortedList.Sort();
            HWGrid.ItemsSource = sortedList;
        }
        #endregion Load the datagrid

        #region Reread the registry and refresh the datagrid
        private void RefreshData()
        {
            HWiNFO.HWList.Clear();
            ReadVSB();
            List<HWiNFO> sortedList = HWiNFO.HWList;
            sortedList.Sort();
            HWGrid.ItemsSource = sortedList;
            ResetCols();
            HWGrid.Items.Refresh();
        }
        #endregion Reread the registry and refresh the datagrid

        #region Keyboard events
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                ShowAbout();
            }

            if (e.Key == Key.F5)
            {
                RefreshData();
            }

            if (e.Key == Key.Add && (Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                GridLarger();
            }

            if (e.Key == Key.Subtract && (Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                GridSmaller();
            }
        }
        #endregion Keyboard events

        #region Mouse Events
        private void DataGridTasks_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control)
                return;

            if (e.Delta > 0)
            {
                GridLarger();
            }
            else if (e.Delta < 0)
            {
                GridSmaller();
            }
        }
        #endregion Mouse Events

        #region Menu events
        private void MnuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MnuRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void MnuRemoveSort_Click(object sender, RoutedEventArgs e)
        {
            ResetCols();
        }
        private void MnuCopy_Click(object sender, RoutedEventArgs e)
        {
            Copy2Clipboard();
        }

        private void MnuSaveToCsv_Click(object sender, RoutedEventArgs e)
        {
            SaveToCSV();
        }

        private void MnuSaveToHtml_Click(object sender, RoutedEventArgs e)
        {
            SaveToHtml();
        }

        private void GridSmaller_Click(object sender, RoutedEventArgs e)
        {
            GridSmaller();
        }

        private void GridLarger_Click(object sender, RoutedEventArgs e)
        {
            GridLarger();
        }

        private void MnuAbout_Click(object sender, RoutedEventArgs e)
        {
            ShowAbout();
        }

        private void TbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = tbxSearch.Text;

            // I really don't understand how this works
            ICollectionView cv = CollectionViewSource.GetDefaultView(HWGrid.ItemsSource);
            cv.Filter = filter?.Length == 0
                ? (Predicate<object>)null
                : (o =>
                {
                    HWiNFO hw = o as HWiNFO;
                    return hw.Label.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                           hw.Sensor.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                           hw.Value.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                           hw.Index.ToString().IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;
                });
            btnSearch.IsEnabled = !string.IsNullOrEmpty(tbxSearch.Text);
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            tbxSearch.Clear();
        }
        #endregion Menu events

        #region Window events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsHWiNFORunning();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            log.Info("{0} is shutting down.", AppInfo.AppName);

            // Shut down NLog
            LogManager.Shutdown();

            // save settings
            UserSettings.Setting.WindowLeft = Left;
            UserSettings.Setting.WindowTop = Top;
            UserSettings.Setting.WindowWidth = Width;
            UserSettings.Setting.WindowHeight = Height;
            UserSettings.SaveSettings();
        }
        #endregion Window events

        #region Reset column sort
        private void ResetCols()
        {
            foreach (DataGridColumn column in HWGrid.Columns)
            {
                column.SortDirection = null;
            }
            HWGrid.Items.SortDescriptions.Clear();
        }
        #endregion Reset column sort

        #region Setting change
        private void UserSettingChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyInfo prop = sender.GetType().GetProperty(e.PropertyName);
            var newValue = prop?.GetValue(sender, null);
            switch (e.PropertyName)
            {
                case "ShadeAltRows":
                    if ((bool)newValue)
                    {
                        AltRowShadingOn();
                    }
                    else
                    {
                        AltRowShadingOff();
                    }
                    break;
                case "ShowGridLines":
                    if ((bool)newValue)
                    {
                        HWGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
                    }
                    else
                    {
                        HWGrid.GridLinesVisibility = DataGridGridLinesVisibility.None;
                    }
                    break;
            }
            //log.Debug($"***Setting change: {e.PropertyName} New Value: {newValue}");
        }
        #endregion Setting change

        #region Alternate row shading
        private void AltRowShadingOff()
        {
            HWGrid.AlternationCount = 0;
            HWGrid.RowBackground = new SolidColorBrush(Colors.White);
            HWGrid.AlternatingRowBackground = new SolidColorBrush(Colors.White);
            HWGrid.Items.Refresh();
        }

        private void AltRowShadingOn()
        {
            HWGrid.AlternationCount = 2;
            HWGrid.RowBackground = new SolidColorBrush(Colors.White);
            HWGrid.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            HWGrid.Items.Refresh();
        }
        #endregion Alternate row shading

        #region Grid Size
        private void GridSmaller()
        {
            double curZoom = UserSettings.Setting.GridZoom;
            if (curZoom > 0.5)
            {
                curZoom -= .05;
                UserSettings.Setting.GridZoom = Math.Round(curZoom, 2);
            }
            HWGrid.LayoutTransform = new ScaleTransform(curZoom, curZoom);
        }

        private void GridLarger()
        {
            double curZoom = UserSettings.Setting.GridZoom;
            if (curZoom < 2.0)
            {
                curZoom += .05;
                UserSettings.Setting.GridZoom = Math.Round(curZoom, 2);
            }

            HWGrid.LayoutTransform = new ScaleTransform(curZoom, curZoom);
        }
        #endregion Grid Size

        #region Copy to clipboard
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
        private void SaveToCSV()
        {
            string fname = "HWiNFO_VSB_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".csv";
            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "Save Grid as CSV FIle",
                Filter = "CSV File|*.csv",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                FileName = fname
            };
            var result = dialog.ShowDialog();
            if (result == true)
            {
                Copy2Clipboard();
                string gridData = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                File.WriteAllText(dialog.FileName, gridData, Encoding.UTF8);
            }
        }
        #endregion Save grid to CSV file

        #region Save to HTML
        private void SaveToHtml()
        {
            StringBuilder sb = new StringBuilder();
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
                .Append("<th style='width: 3.5%;'>Index</th>")
                .Append("<th style='width: 25%;'>Sensor</th>")
                .Append("<th style='width: 25%;'>Label</th>")
                .Append("<th style='width: 10%;'>Value</th>")
                .Append("<th style='width: 10%;'>Value Raw</th>")
                .AppendLine("</tr>");
            foreach (HWiNFO row in HWiNFO.HWList)
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
            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "Save Grid as HTML FIle",
                Filter = "HTML File|*.html",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                FileName = fname
            };
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                File.WriteAllText(dialog.FileName, html, Encoding.UTF8);
            }
        }
        #endregion Save to HTML

        #region Unhandled Exceptions
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            log.Error("Unhandled Exception");
            Exception e = (Exception)args.ExceptionObject;
            log.Error(e.Message);
            if (e.InnerException != null)
            {
                log.Error(e.InnerException.ToString());
            }
            log.Error(e.StackTrace);
        }
        #endregion Unhandled Exceptions

        #region Show the About window
        private void ShowAbout()
        {
            About about = new About
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            _ = about.ShowDialog();
        }
        #endregion Show the About window
    }
}
