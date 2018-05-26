// PkgCmdID.cs
// MUST match PkgCmdID.h
using System;

namespace Manobit.CodeBeautifier
{
    static class PkgCmdIDList
    {
        public const uint cmdCurrentDocument = 0x0100;
        public const uint cmdAllOpenDocuments = 0x0101;
        public const uint cmdSelectedProject = 0x0102;
        public const uint cmdSelectedSolution = 0x0103;
        public const uint cmdOptions = 0x0200;
        public const uint cmdAbout = 0x0201;
    };
}