using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Settings.Sources;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Reflection;
using System.Diagnostics;
using Manobit.CodeBeautifier;
using Manobit.CodeBeautifier.Sources;

namespace SettingsTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new Form1() );
           // Application.Run( new About( AppDomain.CurrentDomain.GetAssemblies() ) );
        }
    }
}
