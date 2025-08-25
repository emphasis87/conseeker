# ConSeeker

An application for searching and managing conventions.

# Developer Notes

## Project structure
    
- ConSeeker

    - The main MAUI project
    - Supports Android, Windows, IOS or MacCatylyst

- ConSeeker.Shared

    - Shared components and services

- ConSeeker.Web

    - Web client

- ConSeeker.Web.Client

    - WebAssembly client

### Wireless debugging

**Windows + Android:**

- Visual Studio 2022
- Android Studio
- configure device in *Developer options*
- enable wireless debugging
- add to path %APPDATALOCA%\Android\android-sdk\platform-tools\
```bash
adb <device-ip>:<pairing-port1> # Wireless debugging → Pair device with pairing code
adb connect <device-ip>:<pairing-port2> # Wireless debugging (IP address and port)
adb devices # Check connected
```

### Debugging

- check ConSeeker.Api/Properties/launchSettings.json and configure listening port
- start the **ConSeeker.Api** project (note the IP address in the log output)
- change ConSeeker "ApiClient" http client base address to point to the api
- start the **ConSeeker** project