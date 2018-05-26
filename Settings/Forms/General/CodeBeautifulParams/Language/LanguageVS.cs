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
    public partial class LanguageVS : UserControl, IValidator
    {
        public event EventHandler Action = delegate { };

        public LanguageVS()
        {
            InitializeComponent();

            this.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                            | System.Windows.Forms.AnchorStyles.Top
                            | System.Windows.Forms.AnchorStyles.Left
                            | System.Windows.Forms.AnchorStyles.Right );

            this.Name = "PanelCodeBeautifulLanguageSpecified";
        }

        public IValidatorData validate()
        {
            return new ValidatorData();
        }
    }
}
