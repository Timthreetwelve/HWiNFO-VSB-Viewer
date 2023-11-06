// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

/// <summary>
/// <para>
/// This inspired by the of answers from this question
/// https://stackoverflow.com/questions/19147/what-is-the-correct-way-to-create-a-single-instance-wpf-application
/// </para>
/// <para>
/// And this blog post
/// https://weblog.west-wind.com/posts/2016/May/13/Creating-Single-Instance-WPF-Applications-that-open-multiple-Files
/// </para>
/// </summary>
namespace HWiNFOVSBViewer
{
    public partial class App : Application
    {
        #region Properties
        /// <summary>
        /// Number of language strings in a resource dictionary
        /// </summary>
        public static int LanguageStrings { get; set; }

        /// <summary>
        /// Uri of the resource dictionary
        /// </summary>
        /// Number of language strings in the test resource dictionary
        /// </summary>
        public static int TestLanguageStrings { get; set; }

        /// <summary>
        /// Uri of the resource dictionary
        /// </summary>
        public static string LanguageFile { get; set; }

        /// <summary>
        /// Uri of the test resource dictionary
        /// </summary>
        public static string TestLanguageFile { get; set; }

        /// <summary>
        /// Culture at startup
        /// </summary>
        public static CultureInfo StartupCulture { get; set; }

        /// <summary>
        /// UI Culture at startup
        /// </summary>
        public static CultureInfo StartupUICulture { get; set; }

        /// <summary>
        /// Number of language strings in the default resource dictionary
        /// </summary>
        public static int DefaultLanguageStrings { get; set; }
        #endregion Properties

        public Mutex Mutex;

        public App()
        {
            SingleInstanceCheck();
        }

        /// <summary>
        /// Override the Startup Event.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            // Initialize settings here so that saved language can be accessed below.
            ConfigHelpers.InitializeSettings();

            // Resource dictionary for language
            ResourceDictionary resDict = new();

            // Get culture info at startup
            StartupCulture = CultureInfo.CurrentCulture;
            StartupUICulture = CultureInfo.CurrentUICulture;

            try
            {
                DefaultLanguageStrings = ResourceHelpers.GetTotalDefaultLanguageCount();
            }
            // If the above fails, set culture and language to en-US.
            catch (Exception)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                resDict.Source = new Uri("Languages/Strings.en-US.xaml", UriKind.RelativeOrAbsolute);
            }
        }

        public void SingleInstanceCheck()
        {
            Mutex = new Mutex(true, "HWiNFOVSBViewer", out bool isOnlyInstance);
            if (!isOnlyInstance)
            {
                // get our process info and then loop the other processes
                Process curProcess = Process.GetCurrentProcess();
                foreach (Process process in Process.GetProcessesByName(curProcess.ProcessName))
                {
                    // if the process id is not the same and the executable has the same path
                    if (!process.Id.Equals(curProcess.Id)
                        && process.MainModule.FileName.Equals(curProcess.MainModule.FileName))
                    {
                        // get the handle of the other process
                        IntPtr windowHandle = process.MainWindowHandle;

                        // if the app is minimized restore it
                        if (NativeMethods.IsIconic(windowHandle))
                        {
                            _ = NativeMethods.ShowWindow(windowHandle,
                                NativeMethods.ShowWindowCommand.Restore);
                        }

                        // move the app to the foreground
                        _ = NativeMethods.SetForegroundWindow(windowHandle);
                    }
                }
                Environment.Exit(0);
            }
        }
    }
}
