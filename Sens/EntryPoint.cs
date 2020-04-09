using System;
using Sens;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using log4net;
using System.Reflection;
using Microsoft.Win32;
using System.Diagnostics;
using Squirrel;
using System.Windows;

namespace Sens {

    /// <summary>
    /// The entry point of this program. Set to take precedence over 'auto-generated' Main method in App.xaml\App.xaml.cs\App\Main() (App.g.i.cs)
    /// Artificial entry point to handle exceptions: The entry point is wrapped in a try catch block, catching any and all unhandled exceptions
    /// produced by this program. The specific exception is caught and its stack trace is recorded into a textfile. A generic exception is then 
    /// thrown to crash the program as the original exception is not realistically handled (only recorded).
    /// </summary>
    public class EntryPoint {


        //some predefined rules/models to the compiler (would be auto-generated)
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]

        public static void Main(string[] args) {

            if (args.Length != 0) {
                AppSettings.Default.LaunchMinimized = true;
            }

            //auto-updating the app from github repo (Squirrel), async = runs in the background
            if (AppSettings.Default.AutoUpdate) {
                Task.Run(async () => {

                    var mgr = UpdateManager.GitHubUpdateManager("https://github.com/MatejRefka/Sens");

                    using (var result = await mgr) {

                        await result.UpdateApp();
                    }
                });
            }

            try {
                //default actions of the program
                Sens.App app = new Sens.App();
                app.InitializeComponent();
                app.Run();

            }
            catch (Exception e) {
                //output file path
                string path = System.AppDomain.CurrentDomain.BaseDirectory + @"DebugFile.txt";
                //append the debug file with a new specific exception alongside its details
                using (StreamWriter writer = new StreamWriter(path, append: true)) {
                    writer.WriteLine(DateTime.UtcNow + ", UTC");
                    writer.WriteLine(e.Message);
                    writer.WriteLine(e.StackTrace + "\n");
                }

                //throw a general exception to crash the program (avoiding the propagation of errors)
                throw new Exception("Unhandled exception, Stack Trace @Debug/DebugFile.txt");
            }
        }
    }
}
