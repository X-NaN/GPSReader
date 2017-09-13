using System;
using System.Runtime.InteropServices;

namespace GPSReader.USB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public class DevBroadcastHdr
    {
        internal Int32 dbch_size;
        internal Int32 dbch_devicetype;
        internal Int32 dbch_reserved;
    }
}