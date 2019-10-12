using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace LibUsbDotNet_ShowTestDemo
{
    class Program
    {
        public static UsbDevice MyUsbDevice;

        public static void Main(string[] args)
        {
            // Dump all devices and descriptor information to console output.
            UsbRegDeviceList allDevices = UsbDevice.AllDevices;
            Debug.WriteLine(allDevices.Count);
            foreach (UsbRegistry usbRegistry in allDevices)
            {
                if (usbRegistry.Open(out MyUsbDevice))
                {
                    Console.WriteLine(MyUsbDevice.Info.ToString()+"--------1-----------");
                    for (int iConfig = 0; iConfig < MyUsbDevice.Configs.Count; iConfig++)
                    {
                        UsbConfigInfo configInfo = MyUsbDevice.Configs[iConfig];
                        Console.WriteLine(configInfo.ToString()+"---------2-----------");

                        ReadOnlyCollection<UsbInterfaceInfo> interfaceList = configInfo.InterfaceInfoList;
                        for (int iInterface = 0; iInterface < interfaceList.Count; iInterface++)
                        {
                            UsbInterfaceInfo interfaceInfo = interfaceList[iInterface];
                            Console.WriteLine(interfaceInfo.ToString()+"--------3-------");

                            ReadOnlyCollection<UsbEndpointInfo> endpointList = interfaceInfo.EndpointInfoList;
                            for (int iEndpoint = 0; iEndpoint < endpointList.Count; iEndpoint++)
                            {
                                Console.WriteLine(endpointList[iEndpoint].ToString()+"-------4--------");
                            }
                        }
                    }
                }
            }


            // Free usb resources.
            // This is necessary for libusb-1.0 and Linux compatibility.
            UsbDevice.Exit();

            // Wait for user input..
            Console.ReadKey();
        }
    }
}
