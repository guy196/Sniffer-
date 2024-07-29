using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Net.Sockets;
using System.Diagnostics;
using SharpPcap.LibPcap;
using System.Net.NetworkInformation;
using SharpPcap;
using System.Net;
using System.Threading;
using System.Windows.Threading;
using PacketDotNet;
using System.Windows.Controls;

namespace WBFWireSharkGUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICaptureDevice? _selectedDevice;
        private bool isCapturing = false;
        private static int packetIndex = 0;
        private static string? packetInfo;
        List<PhysicalAddress> macAdresses = new List<PhysicalAddress>();
        private List<Point> positions = new List<Point>
        {
            new Point(50, 50),
            new Point(150, 100),
            new Point(250, 150)
            // Add more positions as needed
        };
        public MainWindow()
        {
            InitializeComponent();
            LoadDevices();
        }
        private void LoadDevices()
        {
            // Use SharpPcap to get a list of available network devices
            var devices = SharpPcap.CaptureDeviceList.Instance;

            if (devices.Count == 0)
            {
                PacketInfoTextBox.Text += "No capture devices found.\n";
                return;
            }

            foreach (var device in devices)
            {
                macAdresses.Add(device.MacAddress);
                DeviceComboBox.Items.Add(device.Description);
            }

            DeviceComboBox.SelectedIndex = 0;

            for (int i = 0; i < macAdresses.Count; i++)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = macAdresses[i].ToString(),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(positions[i].X, positions[i].Y, 0, 0)
                };

                MainGrid.Children.Add(textBlock);
            }
        }



        private void StartCapture_Click(object sender, RoutedEventArgs e)
        {
            int selectedDeviceIndex = DeviceComboBox.SelectedIndex;
            isCapturing = true;
            var devices = CaptureDeviceList.Instance;
            _selectedDevice = devices[selectedDeviceIndex];
            _selectedDevice.OnPacketArrival += device_OnPacketArrival;
            _selectedDevice.Open(); // Set the mode here
            _selectedDevice.StartCapture();
            if (selectedDeviceIndex >= 0 && selectedDeviceIndex < DeviceComboBox.Items.Count)
            {

            }
            else
            {
                MessageBox.Show("Please select a network device.");
            }
            StartPacketCapture(selectedDeviceIndex);//starts capturing 
        }
        private void StartPacketCapture(int deviceIndex)
        {
            // Start packet capture on the selected device
            var processStartInfo = new ProcessStartInfo()
            {
                FileName = @"C:\Program Files\Wireshark\tshark.exe",
                Arguments = $"-i {deviceIndex} -w captured_packets.pcap",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = new Process { StartInfo = processStartInfo })
            {
                process.Start();//starts the t-Shark process
                PacketInfoTextBox.Text += $"Capturing on device {deviceIndex}. Press Enter to stop...\n";
                Thread newThread = new Thread(PrintToTheScreenPacketsInfo);
                newThread.Start();
                {

                    if (!isCapturing)
                    {
                        // Stop the packet capture process
                        process.CloseMainWindow();
                        process.WaitForExit();

                        PacketInfoTextBox.Text += "Packet capture stopped.\n";
                    }
                };
            }
        }

        void PrintToTheScreenPacketsInfo()
        {
            while (isCapturing)
            {
                // Use Dispatcher to update UI from a separate thread
                Dispatcher.Invoke(() =>
                {
                    PacketInfoTextBox.Text += packetInfo;
                    PacketInfoTextBox.Text += '\n';
                });

                Thread.Sleep(100); // Optional: Add a small delay to avoid excessive CPU usage
            }
        }
        static string GetLocalIpAddress()
        {
            string localIpAddress = "";

            try
            {
                // Get the host name of the local machine
                string hostName = Dns.GetHostName();

                // Get the IP addresses associated with the host
                IPAddress[] localIpAddresses = Dns.GetHostAddresses(hostName);

                // Find the first IPv4 address, from adressed like ipv6 Loopback Address
                foreach (IPAddress ipAddress in localIpAddresses)
                {
                    if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIpAddress = ipAddress.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting local IP address: {ex.Message}");
            }

            return localIpAddress;
        }

        private void StopCapture_Click(object sender, RoutedEventArgs e)
        {
            isCapturing = false;

        }
        private static void device_OnPacketArrival(object sender, PacketCapture e)
        {
            packetIndex++;

            var rawPacket = e.GetPacket();
            var packet = PacketDotNet.Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);

            var ethernetPacket = packet.Extract<EthernetPacket>();
            if (ethernetPacket != null)
            
                 packetInfo = $"Source Adress: {ethernetPacket.SourceHardwareAddress}, Destination MAC: {ethernetPacket.DestinationHardwareAddress}";

            }
        }

    }
