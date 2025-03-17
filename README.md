# CorePulse - System Monitoring App

CorePulse is a system monitoring application designed to track and display hardware performance metrics in real-time. This project is a work in progress and is intended to work in conjunction with an Android application that visualizes the collected hardware data remotely.

## Features

- **Real-time Monitoring:**
  - CPU Usage & Temperature
  - GPU Usage, Temperature, VRAM Usage, Power Consumption & Fan Speed
  - RAM Usage & Total RAM
- **Server Communication:**
  - Runs a TCP server to send system hardware data to a connected Android application.
- **UI Display:**
  - Uses `HandyControl` for a visually appealing UI.
  - Displays system metrics using progress bars and textual updates.

## Dependencies

This application is built with the following libraries:

- [HandyControl](https://github.com/HandyOrg/HandyControl) - For enhanced WPF UI elements.
- [LibreHardwareMonitor](https://github.com/LibreHardwareMonitor/LibreHardwareMonitor) - For retrieving hardware information.

## How It Works

1. **Hardware Monitoring:**
   - Uses `LibreHardwareMonitor` to gather system metrics.
   - Data is refreshed every 2 seconds using a `DispatcherTimer`.

2. **Networking:**
   - A TCP server listens for connections on port `50654`.
   - Upon connection, it sends system metrics to the Android app.
   - Data is sent in a structured format separated by `;` for easy parsing.

3. **UI Updates:**
   - Hardware metrics are displayed in a user-friendly way using `HandyControl` components.
   - Uses `Dispatcher.Invoke` to update the UI from background threads.

## Prerequisites
- Windows OS (since it relies on `Win32_PhysicalMemory` for RAM data)
- .NET (minimum required version depends on dependencies)

## Future Plans
 - **Improve Data Parsing:** Enhance the structure of transmitted data for better compatibility with the Android app.
 - **Add More Sensors:** Include additional system metrics like disk usage, network activity, and power draw.
 - **Enhance UI:** Improve visualization with graphs and animations.
 - **Secure Networking:** Implement encryption and better authentication for data transmission.

## Images
![CorePulse Screenshot](https://media.discordapp.net/attachments/590299384525226036/1351217049678119034/image.png?ex=67d99271&is=67d840f1&hm=06556e7f7469a69679279cfb0074696994c33b89fdc430357c4cc2b47d84c784&=&quality=lossless)