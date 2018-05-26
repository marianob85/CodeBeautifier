using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Settings.Sources.Validator;

namespace Settings.Forms
{
    public partial class LanguageExt : UserControl, IValidator
    {
        public event EventHandler Action = delegate { };
        private IList<String> m_List;

        public LanguageExt( IList<String> extensions )
        {
            m_List = extensions;
            InitializeComponent();

            this.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                                    | System.Windows.Forms.AnchorStyles.Top
                                                                    | System.Windows.Forms.AnchorStyles.Left
                                                                    | System.Windows.Forms.AnchorStyles.Right );

            this.Name = "PanelCodeBeautifulLanguageSpecified";
            this.tbNew.Clear();
            this.bAdd.Enabled = false;
            this.bRemove.Enabled = false;

            refreshList();
        }

        private void refreshList()
        {
            Action( this, null );
            this.lv_List.Clear();

            if( m_List == null )
            {
                return;
            }

            if( m_List.Count == 0 )
            {
                return;
            }

            foreach( String obj in m_List )
            {
                ListViewItem lwi = new ListViewItem();

                lwi.Name = obj;
                lwi.Text = obj;

                this.lv_List.Items.Add( lwi );
            }
        }


        private void bAdd_Click( System.Object sender, System.EventArgs e )
        {
            String LangExt = FormatValue( this.tbNew.Text );

            if( m_List.Contains ( LangExt ) )
            {
                return;
            }

            m_List.Add( LangExt );

            this.tbNew.Clear();

            refreshList();
        }

        private void bRemove_Click( System.Object sender, System.EventArgs e )
        {
            foreach( ListViewItem item in lv_List.SelectedItems )
            {
                m_List.Remove( item.Name );
            }

            refreshList();
        }

        private String FormatValue( System.String Text )
        {
            Text = Text.Trim();
            Text = Text.TrimStart( new Char[] { '.' } );

            return Text;
        }

        private void tbNew_TextChanged( System.Object sender, System.EventArgs e )
        {
            TextBox tb = sender as TextBox;

            String Lang = FormatValue( tb.Text );

            if( String.IsNullOrEmpty( Lang ) )
            {
                this.bAdd.Enabled = false;
                return;
            }

            foreach( Char ch in System.IO.Path.GetInvalidFileNameChars() )
            {
                if( Lang.Contains( ch.ToString() ) )
                {
                    this.bAdd.Enabled = false;
                    return;
                }
            }

            this.bAdd.Enabled = true;
        }

        private void lv_List_ItemSelectionChanged( System.Object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e )
        {
            ListView lw = sender as ListView;

            this.bRemove.Enabled = lw.SelectedItems.Count != 0;
        }


        private void tbNew_KeyDown( System.Object sender, System.Windows.Forms.KeyEventArgs e )
        {
            if( e.KeyCode == System.Windows.Forms.Keys.Enter )
            {
                e.Handled = true;
                this.bAdd_Click( null, null );
            }
        }

        public IValidatorData validate()
        {
            if( m_List == null || m_List.Count == 0 )
            {
                return new ValidatorData { message = "Files extension list empty", result = ValidatorDataResult.Error };
            }
            return new ValidatorData();
        }
    }
}
