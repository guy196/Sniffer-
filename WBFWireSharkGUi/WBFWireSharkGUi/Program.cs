//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.Net.Sockets;
//using System.Diagnostics;
//using SharpPcap.LibPcap;

//namespace WBFWireSharkGUi
//{
//    /// <summary>
//    /// Interaction logic for MainWindow.xaml
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        private LibPcapLiveDevice _selectedDevice;

//        public MainWindow()
//        {
//            InitializeComponent();

//        }
//        private void StartPacketCapture_Click(object sender, RoutedEventArgs e)
//        {
//            PacketInfoTextBox.Text += "Starting network capture...\n";

//            // List available network devices
//            ListNetworkDevices();

//            Console.Write("Enter the number of the device to capture from: ");
//            if (int.TryParse(Console.ReadLine(), out int deviceIndex))
//            {
//                // Start packet capture on the selected device
//                StartPacketCapture(deviceIndex);
//            }
//            else
//            {
//                PacketInfoTextBox.Text += "Invalid input. Exiting...\n";
//            }
//        }
//        private void ListNetworkDevices()
//        {
//            // Use the "tshark" command to list available network devices
//            var processStartInfo = new ProcessStartInfo
//            {
//                FileName = @"C:\Program Files\Wireshark\tshark.exe",
//                Arguments = "-D",
//                RedirectStandardOutput = true,
//                UseShellExecute = false,
//                CreateNoWindow = true
//            };

//            using (var process = new Process { StartInfo = processStartInfo })
//            {
//                process.Start();

//                // Read and display the output
//                while (!process.StandardOutput.EndOfStream)
//                {
//                    string line = process.StandardOutput.ReadLine();
//                    PacketInfoTextBox.Text += line + "\n";
//                }
//            }
//        }
//        private void StartPacketCapture(int deviceIndex)
//        {
//            // Start packet capture on the selected device
//            var processStartInfo = new ProcessStartInfo
//            {
//                FileName = @"C:\Program Files\Wireshark\tshark.exe",
//                Arguments = $"-i {deviceIndex} -w captured_packets.pcap",
//                UseShellExecute = false,
//                CreateNoWindow = true
//            };

//            using (var process = new Process { StartInfo = processStartInfo })
//            {
//                process.Start();
//                PacketInfoTextBox.Text += $"Capturing on device {deviceIndex}. Press Enter to stop...\n";

//                // Wait for user input (press Enter) to stop the capture
//                Console.ReadLine();

//                // Stop the packet capture process
//                process.CloseMainWindow();
//                process.WaitForExit();

//                PacketInfoTextBox.Text += "Packet capture stopped.\n";
//            }
//        }
//    }
//}
