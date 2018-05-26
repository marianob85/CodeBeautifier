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
    public enum LanguageIdent
    {
        [XmlEnum( Name = "VisualStudio" )]
        VisualStudio = 0,
        [XmlEnum( Name = "Extension" )]
        Extension = 1,
    };

    public enum ApplicationMode
    {
        [XmlEnum( Name = "File" )]
        File,
        [XmlEnum( Name = "StdInput" )]
        StdInput
    };
    

    public class AppOptions
    {
        public delegate void EventChanged( Object sender, bool changed );
        public event EventChanged onChanged = delegate { };

        private String m_name = String.Empty;
        private bool m_enable = false;
        private List<String> m_languageIdentExt = new List<String>();
        private LanguageIdent m_languageIdent = LanguageIdent.Extension;
        private ApplicationMode m_applicationMode = ApplicationMode.File;
        private ApplicationModeFile m_appModeFile = new ApplicationModeFile();
        private ApplicationModeStdInput m_appModeStdInput = new ApplicationModeStdInput();
        private List<String> m_ExcludeFiles = new List<String>();
        private bool m_useCbIgnoreFiles = false;
        private bool m_useFullPathForCbIgnore = false;
        private SettingsOptionsConfigFile m_appConfig = new SettingsOptionsConfigFile();
        
        public AppOptions()
        {
        }

        public AppOptions( String name, AppOptions options )
        {
            m_name = name;
            m_enable = options.m_enable;
            m_languageIdent = options.m_languageIdent;
            m_applicationMode = options.applicationMode;
            m_appModeFile = new ApplicationModeFile( options.appModeFile );
            m_appModeStdInput = new ApplicationModeStdInput( options.m_appModeStdInput );
            options.m_languageIdentExt.ForEach( m_languageIdentExt.Add );
            options.m_ExcludeFiles.ForEach( m_ExcludeFiles.Add );
            m_useCbIgnoreFiles = options.m_useCbIgnoreFiles;
            m_useFullPathForCbIgnore = options.m_useFullPathForCbIgnore;
        }

        public AppOptions( String name )
        {
            m_name = name;
        }

        public SettingsOptionsConfigFile applicationConfig
        {
            get { return m_appConfig; }
            set { m_appConfig = value; }
        }

        public ApplicationModeFile appModeFile
        {
            get { return m_appModeFile; }
            set { m_appModeFile = value; }
        }

        public ApplicationModeStdInput appModeStdInput
        {
            get { return m_appModeStdInput; }
            set { m_appModeStdInput = value; }
        }

        public ApplicationMode applicationMode
        {
            get { return m_applicationMode; }
            set
            {
                m_applicationMode = value;
            }
        }

        public String name
        {
            get { return m_name; }
            set
            {
                m_name = value;
            }
        }

        public bool enable
        {
            get { return m_enable; }
            set
            {
                m_enable = value;
            }
        }

        public List<String> languageIdentExt
        {
            get { return m_languageIdentExt; }
            set { m_languageIdentExt = value; }
        }

        public List<String> excludeFiles
        {
            get { return m_ExcludeFiles; }
            set
            {
                m_ExcludeFiles = value;
            }
        }

        public LanguageIdent languageIdent
        {
            get { return m_languageIdent; }
            set
            {
                m_languageIdent = value;
            }
        }

        public bool useIgnoreFiles
        {
            get { return m_useCbIgnoreFiles; }
            set { m_useCbIgnoreFiles = value; }
        }

        public override int GetHashCode()
        {
            return m_name.GetHashCode()
                    * appModeFile.GetHashCode() ^ 2
                    * m_appModeStdInput.GetHashCode() ^ 3
                    * m_applicationMode.GetHashCode()
                    * m_enable.GetHashCode() ^ 4
                    * m_ExcludeFiles.GetHashCode() ^ 5
                    * m_languageIdentExt.GetHashCode() ^ 6
                    * m_useFullPathForCbIgnore.GetHashCode() ^ 7
                    * m_languageIdent.GetHashCode() ^ 8
                    * m_useCbIgnoreFiles.GetHashCode() ^ 9 
                    * m_appConfig.GetHashCode() ^ 10;
        }

        public override bool Equals( Object other )
        {
            if( ReferenceEquals( null, other ) ) return false;
            if( ReferenceEquals( this, other ) ) return true;
            if( this.GetType() != other.GetType() )
                return false;

            return Equals( (AppOptions)other );
        }

        private bool Equals( AppOptions options )
        {
            return m_name.Equals( options.m_name )
                    && m_appModeFile.Equals( options.m_appModeFile )
                    && m_appModeStdInput.Equals( options.m_appModeStdInput )
                    && m_applicationMode.Equals( options.m_applicationMode )
                    && m_enable.Equals( options.m_enable )
                    && m_ExcludeFiles.TrueForAll( options.m_ExcludeFiles.Contains ) && options.m_ExcludeFiles.TrueForAll( m_ExcludeFiles.Contains )
                    && m_languageIdentExt.TrueForAll( options.m_languageIdentExt.Contains ) && options.m_languageIdentExt.TrueForAll( m_languageIdentExt.Contains )
                    && m_languageIdent.Equals( options.m_languageIdent )
                    && m_useCbIgnoreFiles.Equals( options.m_useCbIgnoreFiles )
                    && m_useFullPathForCbIgnore.Equals( options.m_useFullPathForCbIgnore )
                    && m_appConfig.Equals( options.m_appConfig );
        }

        public override string ToString()
        {
            return m_name;
        }
    }
}