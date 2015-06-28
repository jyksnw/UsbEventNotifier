# UsbEventNotifier

A simple C# library developed for use on Linux systems running the Mono-Runtime. Allows an application to subscribe to a USB's vendor Id (vid) and Product Id (pid) triggering an assigned action when the USB device is either connected or disconnected.

Example: Load/Unload userspace C# level driver for the given USB device on connection/disconnection
