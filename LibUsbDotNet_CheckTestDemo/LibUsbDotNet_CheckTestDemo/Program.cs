using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibUsbDotNet;
using LibUsbDotNet.Main;
using LibUsbDotNet.Info;
using System.Collections.ObjectModel;

namespace LibUsbDotNet_CheckTestDemo
{
    class Program
    {
        public static void PrintUsbInfo()
        {
            UsbDevice usbDevice = null;
            UsbRegDeviceList allDevices = UsbDevice.AllDevices;

            Console.WriteLine("Found {0} devices", allDevices.Count);

            foreach (UsbRegistry usbRegistry in allDevices)
            {
                Console.WriteLine("Got device: {0}\r\n", usbRegistry.FullName);

                if (usbRegistry.Open(out usbDevice))
                {
                    Console.WriteLine("Device Information\r\n------------------");

                    Console.WriteLine("{0}", usbDevice.Info.ToString());

                    Console.WriteLine("VID & PID: {0} {1}", usbDevice.Info.Descriptor.VendorID, usbDevice.Info.Descriptor.ProductID);

                    Console.WriteLine("\r\nDevice configuration\r\n--------------------");
                    foreach (UsbConfigInfo usbConfigInfo in usbDevice.Configs)
                    {
                        Console.WriteLine("{0}", usbConfigInfo.ToString());

                        Console.WriteLine("\r\nDevice interface list\r\n---------------------");
                        ReadOnlyCollection<UsbInterfaceInfo> interfaceList = usbConfigInfo.InterfaceInfoList;
                        foreach (UsbInterfaceInfo usbInterfaceInfo in interfaceList)
                        {
                            Console.WriteLine("{0}", usbInterfaceInfo.ToString());

                            Console.WriteLine("\r\nDevice endpoint list\r\n--------------------");
                            ReadOnlyCollection<UsbEndpointInfo> endpointList = usbInterfaceInfo.EndpointInfoList;
                            foreach (UsbEndpointInfo usbEndpointInfo in endpointList)
                            {
                                Console.WriteLine("{0}", usbEndpointInfo.ToString());
                            }
                        }
                    }
                    usbDevice.Close();
                }
                Console.WriteLine("\r\n----- Device information finished -----\r\n");
            }
        }
        static void Main(string[] args)
        {
            PrintUsbInfo();
            Console.ReadLine();
        }
    }
}
