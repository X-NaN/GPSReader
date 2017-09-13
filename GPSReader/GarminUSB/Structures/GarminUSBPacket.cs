using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

/**
 * 2017.7.20 xnn  GarminUSB中Packet包数据结构定义
 * 
 * 与GarminUSB.dll中的数据结构对应
 * 
 * */
namespace GPSReader.GarminUSB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GarminUSBPacket
    {
        public System.Byte mPacketType;//数据包类型
        public System.Byte  mReserved1;
        public System.UInt16 mReserved2;
        public System.UInt16 mPacketId;//数据包ID
	    public System.UInt16 mReserved3;
        public System.UInt32 mDataSize;
        public System.String mData;    //Packet中存储的GPS数据


    }
}
