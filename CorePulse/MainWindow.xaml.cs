using System.Text;
using HandyControl.Controls;
using SystemWindow = System.Windows.Window;
using HandyControl.Data;
using LibreHardwareMonitor.Hardware;
using System.Management;
using System.Net.Sockets;
using System.Net;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows;

namespace CorePulse
{
    public partial class MainWindow : SystemWindow
    {
        private readonly Computer _computer;
        private readonly DispatcherTimer _timer;
        
        
        public MainWindow()
        {
            InitializeComponent();
            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMotherboardEnabled = true,
                IsMemoryEnabled = true
            };
            gpuName_txt.Content = GetGpuName(_computer);
            _computer.Open();
            
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2) 
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Call the method to get hardware component info
           
            //_ = GetAllComponents(); // Run GetAllComponents asynchronously
            Task.Run(() => GetAllComponents());
        }
        private static string GetGpuName(Computer computer)
        {
            computer.Open();

            var gpuName = computer.Hardware
                .Where(hardware => hardware.HardwareType == HardwareType.GpuAmd || hardware.HardwareType == HardwareType.GpuNvidia)
                .Select(hardware => 
                {
                    hardware.Update();
                    return hardware.Name;
                })
                .FirstOrDefault(); // Get the first GPU name if available

            computer.Close();

            return gpuName;
        }

        private async void InitializeServer()
        {
            Task.Run(async () =>
            {
                  const int PORT = 50654;
                  TcpListener listener = new TcpListener(IPAddress.Any, PORT);
                  listener.Start();
                  Console.WriteLine($"Server started on port {PORT}");

                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    _ = Task.Run(() => HandleClient(client));
                }
            });
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            InitializeServer();
            Growl.InfoGlobal(new GrowlInfo
            {
                Message = "This is an informational message!",
                WaitTime = 3,    // Close after 3 seconds
                StaysOpen = false,  // Does not stay open indefinitely
                                    // Growl = Growl // The GrowlPanel defined in the XAML
            });

        }

       
        public async Task GetAllComponents()
        {
            try
            {
                UpdateHardware();
                var (cpuUsage, cpuTemperature) = GetCpuInfo();
                var (gpuUsage, gpuTemperature, vramUsage, powerConsumption, fanSpeed) = GetGpuInfo();
                var (ramUsage, totalRam) = GetRamInfo();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    UpdateUI(cpuUsage, cpuTemperature, gpuUsage, gpuTemperature, vramUsage, powerConsumption, fanSpeed, ramUsage);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllComponents: {ex.Message}");
            }
        }
        
        private void UpdateUI(double? cpuUsage, double? cpuTemperature, double? gpuUsage, double? gpuTemperature,
            double? vramUsage, double? powerConsumption, double? fanSpeed, double? ramUsage)
        {
            cpuUsage_bar.Value = cpuUsage.HasValue ? Convert.ToUInt32(cpuUsage.Value) : 0;
            cpuTemperature_Circle.Value = cpuTemperature.HasValue ? Convert.ToUInt32(cpuTemperature.Value) : 0;
            gpuUsage_bar.Value = gpuUsage.HasValue ? Convert.ToUInt32(gpuUsage.Value) : 0;
            gpuTemperature_Circle.Value = gpuTemperature.HasValue ? Convert.ToUInt32(gpuTemperature.Value) : 0;
            gpuPowerConsumption.Value = powerConsumption.HasValue ? Convert.ToUInt32(powerConsumption.Value) : 0;
            gpuFanSpeed.Value = fanSpeed.HasValue ? Convert.ToUInt32(fanSpeed.Value) : 0;
            vRAM.Value = vramUsage.HasValue ? Convert.ToUInt32(vramUsage.Value) : 0;
            ramUsage_bar.Value = ramUsage.HasValue ? Convert.ToUInt32(ramUsage.Value) : 0;

            globalUsage_bar.Value = (int)((gpuUsage_bar.Value + cpuUsage_bar.Value + ramUsage_bar.Value) / 3);
            gpu_usage_txt.Content = gpuUsage_bar.Value + "%";
            global_usage_txt.Content = globalUsage_bar.Value + "%";
            ram_usage_txt.Content = ramUsage_bar.Value + "%";
            cpuUsage_txt.Content = cpuUsage_bar.Value + "%";
            gpuTemperature_txt.Content = gpuTemperature_Circle.Value.ToString() + "ºC";
            cpuTemperature_txt.Content = cpuTemperature_Circle.Value.ToString() + "ºC";
        }

        async Task WaitSecondsAsync(int seconds)
        {
            // Delay for the specified number of seconds
            await Task.Delay(seconds * 1000);
        }
        
        private async Task HandleClient(TcpClient client)
        {
            var remoteEndPoint = client.Client?.RemoteEndPoint?.ToString() ?? "Unknown";
            Console.WriteLine($"{remoteEndPoint} connected");

            using (NetworkStream stream = client.GetStream())
            {
                try
                {
                    //await SendData(stream, "Welcome to the server!");
                    await SendData(stream, GetHardwareInfo());
                    await WaitSecondsAsync(4);
                    await SendData(stream, GetHardwareInfo());
                    
                    
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"{remoteEndPoint}: {receivedData}");
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }

            Console.WriteLine($"{remoteEndPoint} disconnected");
        }

        private async Task SendData(NetworkStream stream, string data)
        {
            byte[] responseData = Encoding.UTF8.GetBytes(data);
            byte[] lengthBytes = BitConverter.GetBytes(responseData.Length);

            if (!BitConverter.IsLittleEndian) Array.Reverse(lengthBytes);

            await stream.WriteAsync(lengthBytes, 0, lengthBytes.Length);
            await stream.WriteAsync(responseData, 0, responseData.Length);
            Console.WriteLine($"Sent: {data}");
        }

        public string GetHardwareInfo()
        {
            UpdateHardware();
            var cpuInfo = GetCpuInfo();
            var gpuInfo = GetGpuInfo();
            var ramInfo = GetRamInfo();

            //return $"CPU: {cpuInfo}, GPU: {gpuInfo}, RAM: {ramInfo}";
            return $"{cpuInfo};{gpuInfo};{ramInfo}";
        }

        private void UpdateHardware()
        {
            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update();
            }
        }

        public (double? Usage, double? Temperature) GetCpuInfo()
        {
            
            var cpuUsage = (int?)_computer.Hardware
                .Where(h => h.HardwareType == HardwareType.Cpu)
                .SelectMany(h => h.Sensors)
                .Where(s => s.SensorType == SensorType.Load && s.Name.Contains("Total"))
                .Select(s => s.Value)
                .FirstOrDefault() ?? 0;

            var cpuTemperature =(int?) _computer.Hardware
                .Where(h => h.HardwareType == HardwareType.Cpu)
                .SelectMany(h => h.Sensors)
                .Where(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Package"))
                .Select(s => s.Value)
                .FirstOrDefault() ?? 0;
            Debug.WriteLine(cpuUsage);

            return (cpuUsage,cpuTemperature);
        }

        public (double? Usage, double? Temperature, double? VramUsage, double? PowerConsumption, double? FanSpeed) GetGpuInfo()
        {
            
            var gpuUsage = (int?)_computer.Hardware
                .Where(h => h.HardwareType == HardwareType.GpuNvidia || h.HardwareType == HardwareType.GpuAmd)
                .SelectMany(h => h.Sensors)
                .Where(s => s.SensorType == SensorType.Load)
                .Select(s => s.Value)
                .FirstOrDefault();

            var gpuTemperature = (int?)_computer.Hardware
                .Where(h => h.HardwareType == HardwareType.GpuNvidia || h.HardwareType == HardwareType.GpuAmd)
                .SelectMany(h => h.Sensors)
                .Where(s => s.SensorType == SensorType.Temperature)
                .Select(s => s.Value)
                .FirstOrDefault();

          
            var vramUsage = (int?)_computer.Hardware
                .Where(h => h.HardwareType == HardwareType.GpuNvidia || h.HardwareType == HardwareType.GpuAmd)
                .SelectMany(h => h.Sensors)
                .Where(s => s.SensorType == SensorType.Clock && s.Name.Contains("Used"))
                .Select(s => s.Value)
                .FirstOrDefault() ?? 0;

           
            var powerConsumption = (int?)_computer.Hardware
                .Where(h => h.HardwareType == HardwareType.GpuNvidia || h.HardwareType == HardwareType.GpuAmd)
                .SelectMany(h => h.Sensors)
                .Where(s => s.SensorType == SensorType.Power)
                .Select(s => s.Value)
                .FirstOrDefault();

          
            var fanSpeed = _computer.Hardware
                .Where(h => h.HardwareType == HardwareType.GpuNvidia || h.HardwareType == HardwareType.GpuAmd)
                .SelectMany(h => h.Sensors)
                .Where(s => s.SensorType == SensorType.Fan)
                .Select(s => s.Value)
                .FirstOrDefault();
            

            Debug.WriteLine($"GPU Usage: {gpuUsage}, GPU Temperature: {gpuTemperature}, VRAM Usage: {vramUsage}, Power Consumption: {powerConsumption}, Fan Speed: {fanSpeed}");

            return (gpuUsage,gpuTemperature,vramUsage,powerConsumption,fanSpeed);
        }


        public (double? Usage, double? Total) GetRamInfo()
        {
            // Get RAM usage percentage
            var ramUsage = (int?)_computer.Hardware
                .Where(h => h.HardwareType == HardwareType.Memory)
                .SelectMany(h => h.Sensors)
                .Where(s => s.SensorType == SensorType.Load)
                .Select(s => s.Value)
                .FirstOrDefault();

            // Optionally, get total RAM (if needed)
            // This is just a placeholder; you can replace it with actual total RAM if desired.
            var totalRam = GetTotalRamAmount(); // Method to get total RAM

            Debug.WriteLine($"RAM Usage: {ramUsage}, Total RAM: {totalRam}");

            return (ramUsage,totalRam);
        }
        private double? GetTotalRamAmount()
        {
            try
            {
                ulong totalCapacity = 0;
                var searcher = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory");
                foreach (ManagementObject obj in searcher.Get())
                {
                    totalCapacity += (ulong)obj["Capacity"];
                }

                // Convert to gigabytes and return as double
                return totalCapacity / (1024.0 * 1024 * 1024);
            }
            catch (Exception)
            {
                // Return 0 in case of an error
                return 0;
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //_cancellationTokenSource.Cancel(); // Signal cancellation to the server
            _computer.Close(); // Ensure the computer is closed when the window is closed
            _timer.Stop(); // Stop the timer when closing the window
        }
    }
}
