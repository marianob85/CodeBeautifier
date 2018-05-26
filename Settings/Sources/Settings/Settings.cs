using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.ComponentModel;

namespace Settings.Sources
{
    public class SettingsContainer
    {
        public event EventHandler onOptionsChanged = delegate { };

        private Options m_options = new Options();
        private RegistryUtils.RegistryMonitor monitor = new RegistryUtils.RegistryMonitor( RegistryHive.CurrentUser, @"Software\Manobit" );

        public SettingsContainer()
        {
            read();

            monitor.RegChanged += new EventHandler( onRegChanged );
            monitor.Start();
        }

        ~SettingsContainer()
        {
            monitor.Stop();
        }

        private void onRegChanged( Object sender, EventArgs e )
        {
            onOptionsChanged( this, e );
        }

        public Options options
        {
            get { return m_options; }
        }

        [XmlIgnore]
        public bool changed
        {
            get
            {
                return !m_options.Equals( Options.read() );
            }
        }

        public bool read()
        {
            try
            {
                m_options = Options.read();
                return true;
            }
            catch( System.ArgumentNullException )
            {
                m_options = new Options();
                return false;
            }
        }

        public bool save()
        {
            m_options.save();
            return true;
        }

        public MemoryStream export()
        {
            return m_options.export();
        }

        public bool import( MemoryStream stream )
        {
            try
            {
                m_options = Options.import( stream );
                return true;
            }
            catch( System.InvalidOperationException )
            {
                return false;
            }
        }
    }
}
