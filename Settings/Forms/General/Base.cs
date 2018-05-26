using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Settings.Sources;
using Settings.Sources.Validator;


namespace Settings.Forms
{
    public partial class Base : UserControl
    {
        private SettingsContainer m_options = new SettingsContainer();
        private Control m_infoPanel = null;
        public delegate void EventAction( Object sender, Options options );

        public event EventAction Cancel = delegate { };
        public event EventAction Save = delegate { };
        public event EventAction Close = delegate { };

        public Base( Control infoControl = null )
        {
            m_options.read();
            m_infoPanel = infoControl;
            InitializeComponent();

            ImageList myImageList = new ImageList();
            myImageList.Images.Add( "ExclamationMark", Icons.Exclamation_16x );
            myImageList.Images.Add( "Default", Icons.ApplicationServiceOK_16x );
            myImageList.Images.Add( "Error", Icons.ApplicationRoleError_16x );
            myImageList.Images.Add( "Help", Icons.HelpApplication_16x );
            myImageList.Images.Add( "Info", Icons.StatusInformation_16x );
            myImageList.Images.Add( "Warning", Icons.ApplicationRoleWarning_16x );
            m_hMainTreeView.ImageList = myImageList;
            m_hMainTreeView.ImageKey = "Default";
            m_hMainTreeView.SelectedImageKey = "Default";
            m_hMainTreeView.AfterSelect += new TreeViewEventHandler( EventNodeAfterSelect );

            CreateListView();
        }

        public void refresh()
        {
            CreateListView();
        }

        public SettingsContainer settings
        {
            get { return m_options; }
        }

        public bool disableButtons
        {
            get { return this.btnCancel.Visible; }
            set
            {
                this.btnCancel.Visible = !value;
                this.btnSave.Visible = !value;
                this.btnClose.Visible = !value;
            }
        }

        private void CreateListView()
        {
            m_hMainTreeView.Nodes.Clear();

            // ---------------------- start: General node ----------------------
            General cPanleGeneral = new General( m_options.options );

            System.Windows.Forms.TreeNode hNodeLanguage = new System.Windows.Forms.TreeNode();
            hNodeLanguage.Name = "General";
            hNodeLanguage.Text = "General";
            hNodeLanguage.Tag = cPanleGeneral;

            cPanleGeneral.Name = hNodeLanguage.Name;
            cPanleGeneral.Tag = hNodeLanguage;
            cPanleGeneral.Action += new EventHandler( IconState );
            IconState( cPanleGeneral, null );

            // ------------------------ end: General node ----------------------

            // ---------------------- start: Code beautiful parameter node ----------------------
            Applications hPanelCodeBeautiful = new Applications( m_options.options );
            if( m_infoPanel != null )
            {
                ( hPanelCodeBeautiful.Controls[ "panelInfo" ] as Panel ).Controls.Add( m_infoPanel );
            }   
            System.Windows.Forms.TreeNode hCodeBeatifulNode = new System.Windows.Forms.TreeNode();
            hCodeBeatifulNode.Name = "CodeBeatiful";
            hCodeBeatifulNode.Text = "CodeBeatiful";
            hCodeBeatifulNode.Checked = true;
            hCodeBeatifulNode.Tag = hPanelCodeBeautiful;

            hPanelCodeBeautiful.Remove += new EventHandler( OnNodeRemove );
            hPanelCodeBeautiful.Add += new EventHandler( OnNodeAdd );
            hPanelCodeBeautiful.Action += new EventHandler( IconState );
            hPanelCodeBeautiful.Tag = hCodeBeatifulNode;
            IconState( hPanelCodeBeautiful, null );

            m_hMainTreeView.Nodes.Add( hNodeLanguage );
            m_hMainTreeView.Nodes.Add( hCodeBeatifulNode );

            foreach( AppOptions app in m_options.options.AppList )
            {
                OnNodeAdd( app, null );
            }

            // ------------------------ end: Code beautiful parameter node ----------------------
            hCodeBeatifulNode.Expand();
            m_hMainTreeView.Sort();
            m_hMainTreeView.ShowNodeToolTips = true;
            m_hMainTreeView.SelectedNode = m_hMainTreeView.Nodes[ 0 ];
        }


        private void EventNodeAfterSelect( System.Object sender, System.Windows.Forms.TreeViewEventArgs e )
        {
            TreeView hTreeView = sender as TreeView;
            TreeNode hCurrentNode = e.Node;

            try
            {
                m_hMainPanel.Controls.Clear();
                UserControl hUserControl = hCurrentNode.Tag as UserControl;
                hUserControl.Size = m_hMainPanel.Size;
                hUserControl.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                | System.Windows.Forms.AnchorStyles.Top
                                                | System.Windows.Forms.AnchorStyles.Left
                                                | System.Windows.Forms.AnchorStyles.Right );
                m_hMainPanel.Controls.Add( hUserControl );
            }
            catch( System.Exception )
            { }
        }

        private void btnSave_Click( System.Object sender, System.EventArgs e )
        {
            m_options.save();
            Save( this, m_options.options );
        }

        private void btnCancel_Click( System.Object sender, System.EventArgs e )
        {
            m_options.read();
            Cancel( this, m_options.options );
        }

        private void btnClose_Click( System.Object sender, System.EventArgs e )
        {
            if( m_options.changed )
            {
                DialogResult dl = MessageBox.Show( "Save changes ?", "File modified", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question );
                switch( dl )
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        {
                            m_options.save();
                            Save( this, m_options.options );
                        }
                        break;
                    case DialogResult.No:
                        break;
                    default:
                        return;
                }
            }

            Close( this, m_options.options );
        }

        private void OnNodeRemove( Object obj, EventArgs arg )
        {
            String NodeName = ( obj as AppOptions ).name;

            m_hMainTreeView.Nodes[ "CodeBeatiful" ].Nodes.RemoveByKey( NodeName );
        }

        private void OnNodeAdd( Object obj, EventArgs arg )
        {
            AppOptions app = obj as AppOptions;
            System.Windows.Forms.TreeNode hAppParamNode = new System.Windows.Forms.TreeNode();
            hAppParamNode.Name = app.name;
            hAppParamNode.Text = app.name;

            Main cPanelCodeBeautifulMain = new Main( app, hAppParamNode );
            Application cPanelCodeBeautifulApplication = new Application( app );
            Language cPanelCodeBeautifulLanguage = new Language( app );
            ExcludeFiles cPanelCodeBeautifulExcludeFiles = new ExcludeFiles( app );
            Configuration configuration = new Configuration( app );

            hAppParamNode.Tag = cPanelCodeBeautifulMain;
            cPanelCodeBeautifulMain.Name = hAppParamNode.Name;
            cPanelCodeBeautifulMain.Tag = hAppParamNode;
            cPanelCodeBeautifulMain.Action += new EventHandler( IconState );
            IconState( cPanelCodeBeautifulMain, null );

            System.Windows.Forms.TreeNode hExecutableNode = new System.Windows.Forms.TreeNode();
            hExecutableNode.Name = "Application";
            hExecutableNode.Text = "Application";
            hExecutableNode.Tag = cPanelCodeBeautifulApplication;
            cPanelCodeBeautifulApplication.Name = hExecutableNode.Name;
            cPanelCodeBeautifulApplication.Tag = hExecutableNode;
            cPanelCodeBeautifulApplication.Action += new EventHandler( IconState );
            IconState( cPanelCodeBeautifulApplication, null );

            System.Windows.Forms.TreeNode hLanguageNode = new System.Windows.Forms.TreeNode();
            hLanguageNode.Name = "Language";
            hLanguageNode.Text = "Language";
            hLanguageNode.Tag = cPanelCodeBeautifulLanguage;
            cPanelCodeBeautifulLanguage.Name = hLanguageNode.Name;
            cPanelCodeBeautifulLanguage.Tag = hLanguageNode;
            cPanelCodeBeautifulLanguage.Action += new EventHandler( IconState );
            IconState( cPanelCodeBeautifulLanguage, null );

            System.Windows.Forms.TreeNode hExcludeFilesNode = new System.Windows.Forms.TreeNode();
            hExcludeFilesNode.Name = "ExcludeFiles";
            hExcludeFilesNode.Text = "ExcludeFiles";
            hExcludeFilesNode.Tag = cPanelCodeBeautifulExcludeFiles;
            cPanelCodeBeautifulExcludeFiles.Name = hExcludeFilesNode.Name;
            cPanelCodeBeautifulExcludeFiles.Tag = hExcludeFilesNode;
            cPanelCodeBeautifulExcludeFiles.Action += new EventHandler( IconState );
            IconState( cPanelCodeBeautifulExcludeFiles, null );

            System.Windows.Forms.TreeNode configurationNode = new System.Windows.Forms.TreeNode();
            configurationNode.Name = "Configuraton";
            configurationNode.Text = "Configuraton";
            configurationNode.Tag = configuration;
            configuration.Name = configurationNode.Name;
            configuration.Tag = configurationNode;
            configuration.Action += new EventHandler( IconState );
            IconState( configuration, null );

            hAppParamNode.Nodes.Add( hExecutableNode );
            hAppParamNode.Nodes.Add( hLanguageNode );
            hAppParamNode.Nodes.Add( hExcludeFilesNode );
            hAppParamNode.Nodes.Add( configurationNode );

            m_hMainTreeView.Nodes[ "CodeBeatiful" ].Nodes.Add( hAppParamNode );
            m_hMainTreeView.Nodes[ "CodeBeatiful" ].Expand();
            m_hMainTreeView.Sort();
        }

        private void IconState( Object sender, EventArgs arg )
        {
            IValidatorData validator = (sender as IValidator).validate();
            TreeNode node = ( sender as UserControl ).Tag as TreeNode;

            Dictionary<ValidatorDataResult, String> NameMap = new Dictionary<ValidatorDataResult, String>();

            NameMap.Add( ValidatorDataResult.Ok, "Default" );
            NameMap.Add( ValidatorDataResult.Error, "ExclamationMark" );
            NameMap.Add( ValidatorDataResult.Disable, "Error" );
            NameMap.Add( ValidatorDataResult.Warning, "Warning" );

            try
            {
                node.ImageKey = NameMap[validator.result];
                node.SelectedImageKey = NameMap[validator.result ];
                node.ToolTipText = validator.message;
            }
            catch( Exception )
            {
                node.ImageKey = "Help";
                node.SelectedImageKey = "Help";
            }
        }

        private void btnExport_Click( object sender, EventArgs e )
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.AddExtension = true;
            saveFile.DefaultExt = "xml";
            saveFile.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            DialogResult result = saveFile.ShowDialog();

            if( result == DialogResult.OK )
            {
                FileStream file = new FileStream( saveFile.FileName, FileMode.Create );
                MemoryStream test = m_options.export();
                test.WriteTo( file );
                file.Close();
            }
        }

        private void btnImport_Click( object sender, EventArgs e )
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.AddExtension = true;
            openFile.DefaultExt = "xml";
            openFile.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            DialogResult result = openFile.ShowDialog();

            if( result == DialogResult.OK )
            {
                MemoryStream stream = new MemoryStream( System.IO.File.ReadAllBytes( openFile.FileName ));
                stream.Seek( 0, SeekOrigin.Begin );
                m_options.import( stream );
                CreateListView();
            }
        }
    }
}
