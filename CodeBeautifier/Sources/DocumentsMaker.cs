using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using Settings.Sources;

namespace Manobit.CodeBeautifier.Sources
{
    public class DocumentsMakerSingle : IDocumentsMaker
    {
        protected delegate void EventHandlerProgress( Object sender, EventArgs e, ProjectItem projectItem, UInt32 currentElement, UInt32 maxElements, UInt32 succesCount, UInt32 failedCount );
        protected delegate void EventHandlerProgressA( Object sender, EventArgs e, String filePath, UInt32 currentElement, UInt32 maxElements, UInt32 succesCount, UInt32 failedCount );
        protected delegate void EventHandlerProgressB( Object sender, EventArgs e, Document document, UInt32 currentElement, UInt32 maxElements, UInt32 succesCount, UInt32 failedCount );
        protected delegate void EventHandlerFinish( Object sender, EventArgs e );
        protected delegate void EventHandlerStart( Object sender, EventArgs e, String status );
        protected event EventHandlerProgress Progress = delegate { };
        protected event EventHandlerProgressA ProgressA = delegate { };
        protected event EventHandlerProgressB ProgressB = delegate { };
        protected event EventHandlerFinish Finished = delegate { };
        protected event EventHandlerStart Started = delegate { };

        protected IServiceProvider m_serviceProvider;
        private Logger m_logger;
        protected Options m_settings;
        protected bool m_cancelPending = false;
        public class KeyPressedData
        {
            TextSelection m_textSelection;
            String m_key;

            public KeyPressedData( TextSelection textSelection, String key )
            {
                m_textSelection = textSelection;
                m_key = key;
            }
            public TextSelection textSelection
            {
                get { return m_textSelection; }
            }
            public String key
            {
                get { return m_key; }
            }
        }

        public DocumentsMakerSingle( IServiceProvider serviceProvider, Options settings )
        {
            this.m_logger = new Logger( serviceProvider, settings );
            m_serviceProvider = serviceProvider;
            m_settings = settings;
        }

        public virtual bool make( List<Object> objects )
        {
            UInt32 stMax = (UInt32)( m_settings.AppList.Count * objects.Count );
            UInt32 stCount = 0;
            UInt32 stSuccess = 0;
            UInt32 stFailed = 0;
            m_cancelPending = false;

            try
            {
                Started( this, null, "Files proceed..." );
                foreach( Object obj in objects )
                {
                    foreach( AppOptions param in m_settings.AppList )
                    {
                        if( param.enable == false )
                        {
                            continue;
                        }
                        bool maked = false;
                        Maker maker = Maker.CreateInstance( m_serviceProvider, m_settings, param );
                        if( obj is String )
                        {
                            ProgressA( this, null, obj as String, stCount, stMax, stSuccess, stFailed );
                            maked = maker.make( obj as String );
                            ProgressA( this, null, obj as String, stCount, stMax, stSuccess, stFailed );
                        }
                        else if( obj is ProjectItem )
                        {
                            Progress( this, null, obj as ProjectItem, stCount, stMax, stSuccess, stFailed );
                            maked = maker.make( obj as ProjectItem );
                            Progress( this, null, obj as ProjectItem, stCount, stMax, stSuccess, stFailed );
                        }
                        else if( obj is Document )
                        {
                            ProgressB( this, null, obj as Document, stCount, stMax, stSuccess, stFailed );
                            maked = maker.make( obj as Document );
                            ProgressB( this, null, obj as Document, stCount, stMax, stSuccess, stFailed );
                        }
                        ++stCount;
                        if( maked ) ++stSuccess; else ++stFailed;

                        if (m_cancelPending)
                            return false;
                    }
                }
                Finished( this, null );
            }
            catch( Exception e )
            {
                m_logger.Log( e );
            }
                        
            return true;
        }

        public virtual bool make( KeyPressedData keyPressedData )
        {
            
            try
            {
                foreach( AppOptions param in m_settings.AppList )
                {
                    Maker maker = Maker.CreateInstance( m_serviceProvider, m_settings, param );
                    maker.make( keyPressedData );
                }
            }
            catch( Exception e )
            {
                m_logger.Log( e );
            }
            return true;
        }
    }
}
