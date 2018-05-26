using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace System
{
    namespace Diagnostics
    {
        ///  /// Creates a mini dump of the current process /// 
        public static class MiniDump
        {
            public static void CreateDump( string path )
            {
                String fileName = DateTime.Now.ToString( "yyyyMMddHHmmssfff" ) + "_" + Guid.NewGuid().ToString( "N" );
                fileName = System.IO.Path.Combine( path, fileName );
                System.IO.Path.ChangeExtension(fileName, ".dmp");

                CreateDump( fileName, MiniDumpType.Normal | MiniDumpType.WithDataSegs | MiniDumpType.WithFullMemory );
            }

            public static void CreateDump( string path, MiniDumpType miniDumpType )
            {
                var exceptionInfo = new MiniDumpExceptionInfo
                                        {
                                            ThreadId = GetCurrentThreadId(),
                                            ExceptionPointers = Marshal.GetExceptionPointers(),
                                            ClientPointers = false // false because own process
                                        };
                Process process = Process.GetCurrentProcess();

                using( var stream = new FileStream( path, FileMode.Create ) )
                {
                    Debug.Assert( stream.SafeFileHandle != null );

                    // The problem Marshal.GetExceptionPointers can return null on x86 machines due to differences
                    // in low-level exception handling.
                    // Then passing a MiniDumpExceptionInfo structure with a NULL ExceptionPointers members causes an
                    // access violation. So we only pass this structure if we got a valid ExceptionPointers member.
                    // It will probably result that x86 machines will see the instruction pointer to the MiniDumpWriteDump
                    // line and not the exception itself.
                    IntPtr exceptionInfoPtr = Marshal.AllocHGlobal( Marshal.SizeOf( exceptionInfo ) );
                    Marshal.StructureToPtr( exceptionInfo, exceptionInfoPtr, false );

                    try
                    {
                        MiniDumpWriteDump(
                                          process.Handle,
                                          process.Id,
                                          stream.SafeFileHandle.DangerousGetHandle(),
                                          miniDumpType,
                                          exceptionInfo.ExceptionPointers == IntPtr.Zero ? IntPtr.Zero : exceptionInfoPtr,
                                          IntPtr.Zero,
                                          IntPtr.Zero );
                    }
                    catch( Exception /*exception*/)
                    {
                    }

                    Marshal.FreeHGlobal( exceptionInfoPtr );
                }
            }

            [DllImport( "kernel32.dll" )]
            private static extern int GetCurrentThreadId();

            [DllImport( "DbgHelp.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi )]
            private static extern Boolean MiniDumpWriteDump(
                IntPtr hProcess,
                Int32 processId,
                IntPtr fileHandle,
                MiniDumpType dumpType,
                IntPtr excepInfo,
                IntPtr userInfo,
                IntPtr extInfo );

            [StructLayout( LayoutKind.Sequential, Pack = 4 )]
            private struct MiniDumpExceptionInfo
            {
                public Int32 ThreadId;
                public IntPtr ExceptionPointers;

                [MarshalAs( UnmanagedType.Bool )]
                public bool ClientPointers;
            }
        }

        [Flags]
        public enum MiniDumpType
        {
            Normal = 0x00000000,
            WithDataSegs = 0x00000001,
            WithFullMemory = 0x00000002,
            WithHandleData = 0x00000004,
            FilterMemory = 0x00000008,
            ScanMemory = 0x00000010,
            WithUnloadedModules = 0x00000020,
            WithIndirectlyReferencedMemory = 0x00000040,
            FilterModulePaths = 0x00000080,
            WithProcessThreadData = 0x00000100,
            WithPrivateReadWriteMemory = 0x00000200,
            WithoutOptionalData = 0x00000400,
            WithFullMemoryInfo = 0x00000800,
            WithThreadInfo = 0x00001000,
            WithCodeSegs = 0x00002000,
            WithoutAuxiliaryState = 0x00004000,
            WithFullAuxiliaryState = 0x00008000
        }
    }
}