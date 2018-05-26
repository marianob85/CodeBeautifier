// Guids.cs
// MUST match guids.h
using System;

namespace Manobit.CodeBeautifier
{
    static class GuidList
    {
        public const string guidCodeBeautifierPkgString = "c2024206-0b8d-4e03-9ad0-4cc3bfe60dea";
        public const string guidCodeBeautifierCmdSetString = "d8b8bfb5-df0b-41c9-9217-ce2a27e62c23";

        public static readonly Guid guidCodeBeautifierCmdSet = new Guid(guidCodeBeautifierCmdSetString);
    };
}