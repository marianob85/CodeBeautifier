using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Settings.Sources
{
    public class OptionsGeneral
    {
        [Flags]
        public enum LoggerPriority
        {
            [XmlEnum( Name = "None" )]
            None,
            [XmlEnum( Name = "Low" )]
            Low,
            [XmlEnum( Name = "Medium" )]
            Medium,
            [XmlEnum( Name = "High" )]
            High,
            [XmlEnum(Name = "HighWithDump")]
            HighWithDump,
        };

        [Flags]
        public enum EOLType
        {
            [XmlEnum( Name = "Ignore" )]
            Ignore,
            [XmlEnum( Name = "Windows-CRLF" )]
            Windows,
            [XmlEnum( Name = "Unix-LF" )]
            Unix,
            [XmlEnum( Name = "Mac-CR" )]
            Mac,
        };


        private LoggerPriority m_loggerPriority = LoggerPriority.Low;
        private EOLType m_eolType = EOLType.Ignore;
        private bool m_onDocumentSave = false;
        private bool m_onBuildProject = false;
        private bool m_onRebuildProject = false;
        private bool m_onBuildSolution = false;
        private bool m_onrebuildSolution = false;
        private bool m_handleNoneProjectFile = false;
        private bool m_trackChanges = false;
        private bool m_autoFormatWhenKeyPressed = false;
        private bool m_createDumpFileOnError = false;
        private String m_dumpFilePath = String.Empty;
        private FormatOptionForLanguageC m_autoFormatOptionsForC = new FormatOptionForLanguageC();
        private FormatOptionForLanguageCSharp m_autoFormatOptionsForCSharp = new FormatOptionForLanguageCSharp();
        public OptionsGeneral()
        { }

        public OptionsGeneral( OptionsGeneral general )
        {
            m_loggerPriority = general.m_loggerPriority;
            m_eolType = general.m_eolType;
            m_onDocumentSave = general.m_onDocumentSave;
            m_onBuildProject = general.m_onBuildProject;
            m_onRebuildProject = general.m_onRebuildProject;
            m_onBuildSolution = general.m_onBuildSolution;
            m_onrebuildSolution = general.m_onrebuildSolution;
            m_handleNoneProjectFile = general.m_handleNoneProjectFile;
            m_trackChanges = general.m_trackChanges;
            m_autoFormatWhenKeyPressed = general.m_autoFormatWhenKeyPressed;
            m_autoFormatOptionsForC = general.m_autoFormatOptionsForC;
            m_createDumpFileOnError = general.m_createDumpFileOnError;
            m_dumpFilePath = general.m_dumpFilePath;
        }

        [CategoryAttribute("General"),
        DescriptionAttribute("Format document on save"),
        DisplayName("On document save")]
        public bool onDocumentSave
        {
            get { return m_onDocumentSave; }
            set
            {
                m_onDocumentSave = value;
            }
        }

        [CategoryAttribute( "General" ),
        DescriptionAttribute( "Format document on active project build command" ),
        DisplayName( "On build active project")]
        public bool onBuildActiveProject
        {
            get { return m_onBuildProject; }
            set
            {
                m_onBuildProject = value;
            }
        }

        [CategoryAttribute( "General" ),
        DescriptionAttribute( "Format document on active project rebuild command" ),
        DisplayName( "On rebuild active project")]
        public bool onRebuildActiveProject
        {
            get { return m_onRebuildProject; }
            set
            {
                m_onRebuildProject = value;
            }
        }

        [CategoryAttribute( "General" ),
        DescriptionAttribute( "Format document on solution build command" ),
        DisplayName( "On build solution")]
        public bool onBuildSolution
        {
            get { return m_onBuildSolution; }
            set
            {
                m_onBuildSolution = value;
            }
        }

        [CategoryAttribute( "General" ),
        DescriptionAttribute( "Format document on solution rebuild command" ),
        DisplayName( "On rebuild solution")]
        public bool onRebuildSolution
        {
            get { return m_onrebuildSolution; }
            set
            {
                m_onrebuildSolution = value;
            }
        }

        [CategoryAttribute( "Other" ),
        DescriptionAttribute( "Format open file which is not a part of visual studio project" ),
        DisplayName( "Handle none project files")]
        public bool handleNoneProjectFile
        {
            get { return m_handleNoneProjectFile; }
            set { m_handleNoneProjectFile = value; }
        }

        [CategoryAttribute( "Other" ),
        DescriptionAttribute( "Logging details" ),
        DisplayName( "Logging details")]
        public LoggerPriority logType
        {
            get { return m_loggerPriority; }
            set
            {
                m_loggerPriority = value;
            }
        }

        [CategoryAttribute( "Experimental" ),
        DescriptionAttribute( "When code is formatted, it replace only lines that was actually changed by format tool. This affects to the color of the left margin allows you to keep track of the changes you have made in a file" ),
        DisplayName( "Track changes")]
        public bool trackChanges
        {
            get { return m_trackChanges; }
            set { m_trackChanges = value; }
        }

        [CategoryAttribute( "Experimental" ),
        ReadOnlyAttribute( false ),
        DescriptionAttribute( "Automatically format when key is pressed" ),
        DisplayName( "Auto format when key pressed")]
        public bool autoFormatWhenKeyPressed
        {
            get { return m_autoFormatWhenKeyPressed; }
            set { m_autoFormatWhenKeyPressed = value; }
        }

        [CategoryAttribute( "Experimental" ),
        ReadOnlyAttribute( false ),
        DescriptionAttribute( "Automatically format triggers" ),
        DisplayName( "C/C++" )]
        public FormatOptionForLanguageC formatOptionsForC
        {
            get { return m_autoFormatOptionsForC; }
            set { m_autoFormatOptionsForC = value; }
        }

        [CategoryAttribute( "Experimental" ),
        ReadOnlyAttribute( false ),
        DescriptionAttribute( "Automatically format triggers" ),
        DisplayName( "C#" )]
        public FormatOptionForLanguageCSharp formatOptionsForCSharp
        {
            get { return m_autoFormatOptionsForCSharp; }
            set { m_autoFormatOptionsForCSharp = value; }
        }

        [CategoryAttribute( "Experimental" ),
        ReadOnlyAttribute( false ),
        DescriptionAttribute( "Replace end of line" ),
        DisplayName( "Force EOL" )]
        public EOLType eolType
        {
            get { return m_eolType; }
            set { m_eolType = value; }
        }

        [CategoryAttribute( "Error handler" ),
        DescriptionAttribute( "Create dump file on error" ),
        DisplayName( "Create dump file on error" )]
        public bool createDumpFileOnError
        {
            get { return m_createDumpFileOnError; }
            set { m_createDumpFileOnError = value; }
        }

        [CategoryAttribute( "Error handler" ),
        DescriptionAttribute( "Dump file location" ),
        ReadOnlyAttribute( false ),
        DisplayName( "Dump file location" ),
        EditorAttribute( typeof( System.Windows.Forms.Design.FolderNameEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
        public String dumpFilepath
        {
            get { return m_dumpFilePath; }
            set { m_dumpFilePath = value; }
        }

        public override int GetHashCode()
        {
            return m_loggerPriority.GetHashCode() ^ 3
                    * m_onBuildProject.GetHashCode() ^ 6
                    * m_onRebuildProject.GetHashCode() ^ 7
                    * m_onBuildSolution.GetHashCode() ^ 8
                    * m_onrebuildSolution.GetHashCode() ^ 9
                    * m_autoFormatOptionsForC.GetHashCode() ^ 12
                    * m_dumpFilePath.GetHashCode() ^ 10
                    * m_createDumpFileOnError.GetHashCode() ^ 11
                    * m_autoFormatOptionsForCSharp.GetHashCode() ^ 13
                    * m_onDocumentSave.GetHashCode() ^ 14
                    * m_eolType.GetHashCode() ^ 15;
        }

        public override bool Equals( Object other )
        {
            if( ReferenceEquals( null, other ) ) return false;
            if( ReferenceEquals( this, other ) ) return true;
            if( this.GetType() != other.GetType() )
                return false;

            return Equals( (OptionsGeneral)other );
        }

        private bool Equals( OptionsGeneral options )
        {
            return m_loggerPriority.Equals( options.m_loggerPriority )
                && m_eolType.Equals( options.m_eolType )
                && m_onBuildProject.Equals( options.m_onBuildProject )
                && m_onRebuildProject.Equals( options.m_onRebuildProject )
                && m_onBuildSolution.Equals( options.m_onBuildSolution )
                && m_onrebuildSolution.Equals( options.m_onrebuildSolution )
                && m_autoFormatOptionsForC.Equals( options.m_autoFormatOptionsForC )
                && m_autoFormatOptionsForCSharp.Equals( options.m_autoFormatOptionsForCSharp )
                && m_createDumpFileOnError.Equals( options.m_createDumpFileOnError )
                && m_createDumpFileOnError.Equals( options.m_createDumpFileOnError )
                && m_onDocumentSave.Equals( options.m_onDocumentSave );
        }
    }
}