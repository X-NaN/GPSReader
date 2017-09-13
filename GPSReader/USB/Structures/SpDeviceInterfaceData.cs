using System;
using System.Runtime.InteropServices;

namespace GPSReader.USB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SpDeviceInterfaceData
    {
        public int cbSize;
        public Guid InterfaceClassGuid;
        public int Flags;
        public int Reserved;
    }
}