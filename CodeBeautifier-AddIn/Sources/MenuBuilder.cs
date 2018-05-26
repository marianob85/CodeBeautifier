using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;

namespace CodeBeautifier.Sources
{
    class MenuBuilder
    {
        private DTE2 m_applicationObject = null;
        private AddIn m_addInInstance = null;

        public MenuBuilder( DTE2 _applicationObject, AddIn _addInInstance )
        {
            m_applicationObject = _applicationObject;
            m_addInInstance = _addInInstance;
        }

        public CommandBar CreateMainMenu( CommandBar Parent, String Name, String Caption, String ToolTip, bool Group, int Position )
        {
            CommandBar menu;

            // If this menu exist, return handler
            foreach( CommandBarControl control in Parent.Controls )
            {
                if( control.Caption.CompareTo( Caption ) == 0 )
                {
                    return ( (CommandBarPopup)control ).CommandBar;
                }
            }

            Commands2 hCommands = m_applicationObject.Commands as Commands2;

            menu = (CommandBar)hCommands.AddCommandBar( (String)Name, vsCommandBarType.vsCommandBarTypeMenu, Parent, Position );

            CommandBarPopup Popup = (CommandBarPopup)menu.Parent;

            Popup.Caption = (String)Caption;
            Popup.TooltipText = (String)ToolTip;

            return menu;
        }

        public void BuildCommand( CommandBar CurrentCmdBar, Command hCommand, bool Group )
        {
            int iPos = ( CurrentCmdBar.Parent as CommandBarPopup ).Controls.Count + 1;

            try
            {
                CommandBarControl hButton = hCommand.AddControl( CurrentCmdBar, iPos ) as CommandBarControl;
                hButton.BeginGroup = Group;
            }
            catch( Exception )
            {
            }

        }

        public CommandBar BuildUI( CommandBar Parent, String CommandPath, Command hCommand, bool Group, int Pos )
        {
            String[] hstrPath = CommandPath.Split( new String[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries );

            CommandBar CurrentCmdBar = Parent;
            int iPos = Pos;

            // Create menu
            foreach( String MenuName in hstrPath )
            {
                CurrentCmdBar = CreateMainMenu( CurrentCmdBar, CaptionToName( MenuName ), MenuName, "", false, iPos );

                iPos = ( (CommandBarPopup)CurrentCmdBar.Parent ).Controls.Count + 1;
            }

            return CurrentCmdBar;
        }

        private String CaptionToName( String Caption )
        {
            String Name = Caption;
            Name = Name.Replace( " ", "" );
            Name = Name.Replace( "&", "" );

            return Name;
        }

    }
}
