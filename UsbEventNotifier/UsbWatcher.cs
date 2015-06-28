using System;
using System.Linq;
using LibUsbDotNet.DeviceNotify;
using LibUsbDotNet.DeviceNotify.Linux;
using System.Collections.Generic;
using System.Dynamic;

namespace UsbEventNotifier
{
	public sealed class UsbWatcher
	{
		private static readonly Lazy<UsbWatcher> m_Instance = new Lazy<UsbWatcher> (() => new UsbWatcher ());

		public static UsbWatcher Instance {
			get {
				return m_Instance.Value;
			}
		}

		List<UsbSubscription> m_Devices;
		IDeviceNotifier m_Watcher;

		private UsbWatcher ()
		{
			m_Devices = new List<UsbSubscription> ();

			// Set-up the USB watcher
			m_Watcher = new LinuxDeviceNotifier ();
			m_Watcher.OnDeviceNotify += new EventHandler<DeviceNotifyEventArgs> (UsbDeviceNotifier_OnDeviceNotify);
			m_Watcher.Enabled = true;
		}

		/// <summary>
		/// Subscribe to listen for the specified vid and pid.
		/// </summary>
		/// <param name="vid">USB Vedor Id</param>
		/// <param name="pid">USB Product Id</param>
		/// <param name = "onConnectedCallback">Callback function to call when device is connected</param>
		/// <param name = "onDisconnectedCallback">Callback function to call when device is disconnected</param>
		public void Subscribe (int vid, int pid, Action onConnectedCallback, Action onDisconnectedCallback)
		{
			m_Devices.Add (new UsbSubscription (vid, pid, onConnectedCallback, onDisconnectedCallback));
		}

		/// <summary>
		/// Unsubscribe from listening for the specified vid and pid.
		/// </summary>
		/// <param name="vid">USB Vendor Id</param>
		/// <param name="pid">USB Product Id</param>
		public void Unsubscribe (int vid, int pid)
		{
			var subscription = m_Devices.FirstOrDefault (u => (u.VendorId == vid) && (u.ProductId == pid));

			m_Devices.Remove (subscription);
		}

		private void UsbDeviceNotifier_OnDeviceNotify (object sender, DeviceNotifyEventArgs e)
		{
			try {

				UsbSubscription subscription = m_Devices.FirstOrDefault (u => ((u.VendorId == e.Device.IdVendor) && (u.ProductId == e.Device.IdProduct)));

				if (subscription != default(UsbSubscription)) {
					if (e.EventType == EventType.DeviceArrival) {
						subscription.OnConnected ();
					} else if (e.EventType == EventType.DeviceRemoveComplete) {
						subscription.OnDisconnected ();
					}
				}

			} catch (Exception ex) {
				throw new UsbEventNotifierException (ex.Message, ex.InnerException);
			}
		}
	}
}

