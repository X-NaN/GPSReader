using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using GPSReader.GarminUSB.Structures;

/**
 * 2017.7.20 xnn GarminUSB接口函数
 * 
 * 对应GarminUSB.dll中的函数
 * */
namespace GPSReader.GarminUSB.Classes.DLL
{
    public static class GarminUSB
    {
        //发送数据包
        [DllImport("GarminUSB.dll", SetLastError = true)]
        public static extern void SendPacket(GarminUSBPacket aPacket);

        //接收数据包
        [DllImport("GarminUSB.dll", SetLastError = true)]
        public static extern GarminUSBPacket GetPacket();

        //初始化
        [DllImport("GarminUSB.dll", SetLastError = true)]
        public static extern void Initialize();

        //判断是否检测到GarminUSB设备
        [DllImport("GarminUSB.dll", SetLastError = true)]
        public static extern int deectedGarminUSB();

        //获得GPS数据  IntPtr为GPS数据所在位置
        [DllImport("GarminUSB.dll", SetLastError = true)]
        public static extern IntPtr getGPSData();

        //通知GarminUSB接收机设备，向主机开始发送GPS数据包
        [DllImport("GarminUSB.dll", SetLastError = true, EntryPoint = "setGarminUSBReady")]
        public static extern int setGarminUSBReady();



    }
}
