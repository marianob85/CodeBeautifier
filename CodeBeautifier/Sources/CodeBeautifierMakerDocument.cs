using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;

namespace Manobit.CodeBeautifier.Sources
{
    public class BreakpointsManager
    {
        public struct BreakpointStorage
        {
            public BreakpointStorage(Breakpoint Bp)
            {
                file = Bp.File;
                line = Bp.FileLine;
                column = Bp.FileColumn;
                condition = Bp.Condition;
                conditionType = Bp.ConditionType;
                language = Bp.Language;
                hitCount = Bp.HitCountTarget;
                hitCountType = Bp.HitCountType;
                enabled = Bp.Enabled;
            }

            // Fields
            public String file;
            public int line;
            public int column;
            public String condition;
            public dbgBreakpointConditionType conditionType;
            public String language;
            public int hitCount;
            public dbgHitCountType hitCountType;
            public bool enabled;
        };

        private List<BreakpointStorage> m_breakpointStorage = new List<BreakpointStorage>();
        private String m_file;
        private EnvDTE80.DTE2 m_dte2 = null;

        public BreakpointsManager(EnvDTE80.DTE2 dte2)
        {
            m_dte2 = dte2;
        }

        public void Save(String file, bool remove)
        {
            m_file = file;
            m_breakpointStorage.Clear();
            foreach (Breakpoint BP in m_dte2.Debugger.Breakpoints)
            {
                if (file.CompareTo(BP.File) == 0)
                {
                    m_breakpointStorage.Add(new BreakpointStorage(BP));

                    if (remove)
                    {
                        BP.Delete();
                    }
                }
            }
        }

        public void Restore()
        {
            // Delete old breakpoints
            foreach (Breakpoint BP in m_dte2.Debugger.Breakpoints)
            {
                if (m_file.CompareTo(BP.File) == 0)
                {
                    BP.Delete();
                }
            }
            foreach (BreakpointStorage BP in m_breakpointStorage)
            {
                try
                {
                    m_dte2.Debugger.Breakpoints.Add("", BP.file, BP.line, BP.column, BP.condition, BP.conditionType, BP.language, "", 0, "", BP.hitCount, BP.hitCountType);

                    foreach (Breakpoint DTEBP in m_dte2.Debugger.Breakpoints)
                    {
                        try
                        {
                            if ((DTEBP.File.CompareTo(BP.file) == 0)
                                && (DTEBP.FileLine == BP.line))
                            {
                                DTEBP.Enabled = BP.enabled;
                            }
                        }
                        catch (Exception) { }
                    }
                }
                catch (Exception) { }
            }
        }

    };

    public class BookmarkManager
    {
        public BookmarkManager() { }

        public void Save(TextDocument textDocument)
        {
            m_textDocument = textDocument;
            m_bookMarkStorage.Clear();

            EditPoint hEP = m_textDocument.StartPoint.CreateEditPoint();

            while (hEP.NextBookmark())
            {
                if (m_bookMarkStorage.Contains(hEP.Line))
                {
                    break;
                }
                m_bookMarkStorage.Add(hEP.Line);
            }
        }

        public void Restore()
        {
            m_textDocument.ClearBookmarks();
            EditPoint hEP = m_textDocument.StartPoint.CreateEditPoint();

            foreach (int iLine in m_bookMarkStorage)
            {
                try
                {
                    hEP.MoveToLineAndOffset(iLine, 1);
                }
                catch (Exception)
                { }

                hEP.SetBookmark();
            }
        }

        private TextDocument m_textDocument;
        private List<int> m_bookMarkStorage = new List<int>(20);
    };


    public interface ICodeBeautifierDocument
    {
        bool make( TextDocument textDocument, String orgText, String newText );
    }

    public class CodeBeautifierDocument : ICodeBeautifierDocument
    {
        protected EnvDTE80.DTE2 m_dte2 = null;

        public CodeBeautifierDocument(EnvDTE80.DTE2 dte2)
        {
            m_dte2 = dte2;
        }

        public bool make(TextDocument textDocument, String orgText, String newText)
        {
            Document hDocument = textDocument.Parent as Document;
            BreakpointsManager hBPMgr = new BreakpointsManager(m_dte2);
            BookmarkManager hBMMgr = new BookmarkManager();

            hBPMgr.Save(hDocument.FullName, false);
            hBMMgr.Save(textDocument);

            int iLine = textDocument.Selection.ActivePoint.Line;
            int iLineCharOffset = textDocument.Selection.ActivePoint.LineCharOffset;

            int findOptions = (int)(vsFindOptions.vsFindOptionsFromStart | vsFindOptions.vsFindOptionsMatchInHiddenText);
            textDocument.ReplaceText(orgText, newText, findOptions);

            // set by EditPoint didn't work correctly :|
            int iMinLine = Math.Min(iLine, textDocument.EndPoint.Line);
            textDocument.Selection.GotoLine(iMinLine, false); // MoveToLineAndOffset( iMinLine, 1, false );

            int iCharOffset = Math.Min(iLineCharOffset, textDocument.Selection.ActivePoint.LineLength);
            textDocument.Selection.MoveToLineAndOffset(iMinLine, iCharOffset == 0 ? 1 : iCharOffset, false);

            hBPMgr.Restore();
            hBMMgr.Restore();

            return true;
        }
    }

    public class CodeBeautifierDocumentWithtrackChanges : ICodeBeautifierDocument
    {
        protected EnvDTE80.DTE2 m_dte2 = null;

        public CodeBeautifierDocumentWithtrackChanges(EnvDTE80.DTE2 dte2)
        {
            m_dte2 = dte2;
        }

        public bool make(TextDocument textDocument, String orgText, String newText)
        {
            BreakpointsManager hBPMgr = new BreakpointsManager(m_dte2);
            BookmarkManager hBMMgr = new BookmarkManager();
            Document hDocument = textDocument.Parent as Document;

            DiffPlex.Differ diff = new DiffPlex.Differ();
            DiffPlex.Model.DiffResult result = diff.CreateLineDiffs ( orgText, newText, false, false );

            hBPMgr.Save(hDocument.FullName, false);
            hBMMgr.Save(textDocument);

            textDocument.DTE.UndoContext.Open( "CodeBeautiful" );
            try
            {
                int lineOffset = 0;
                foreach( var diffBlock in result.DiffBlocks )
                {
                    EnvDTE80.EditPoint2 editpoint = textDocument.CreateEditPoint() as EnvDTE80.EditPoint2;
                    EnvDTE80.EditPoint2 endLine = textDocument.CreateEditPoint() as EnvDTE80.EditPoint2;
                    editpoint.MoveToLineAndOffset( diffBlock.DeleteStartA + lineOffset + 1, 1 );
                    try
                    {
                        endLine.MoveToLineAndOffset(diffBlock.DeleteStartA + lineOffset + diffBlock.DeleteCountA + 1, 1);
                    }
                    catch (System.ArgumentException /*ex*/)
                    {
                        endLine.MoveToLineAndOffset(diffBlock.DeleteStartA + lineOffset + diffBlock.DeleteCountA, 1 );
                        endLine.EndOfLine();
                    }
                    
                    editpoint.Delete( endLine );
                    editpoint.InsertNewLine( diffBlock.InsertCountB );
                    for( int line = 0 ; line < diffBlock.InsertCountB ; ++line )
                    {
                        editpoint.MoveToLineAndOffset( diffBlock.DeleteStartA + lineOffset + line + 1, 1 );
                        editpoint.Insert( result.PiecesNew[ diffBlock.InsertStartB + line ] );
                    }
                    lineOffset += diffBlock.InsertCountB - diffBlock.DeleteCountA;
                }
            }
            catch( Exception e )
            {
                textDocument.DTE.UndoContext.Close();
                throw e;
            }

            textDocument.DTE.UndoContext.Close();
          
            // Restoring break points should take into consideration that some lines was changed or added
            hBPMgr.Restore();
            hBMMgr.Restore();

            return true;
        }
    }
}


        