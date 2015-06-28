using System;

namespace UsbEventNotifier
{
	internal class UsbSubscription
	{
		public UsbSubscription (int venderId, int productId, Action onConnectedCallback, Action onDisconnectedCallback)
		{
			VendorId = venderId;
			ProductId = productId;
			OnConnected = onConnectedCallback;
			OnDisconnected = onDisconnectedCallback;
		}

		public int VendorId { get; set; }

		public int ProductId { get; set; }

		public Action OnConnected { get; set; }

		public Action OnDisconnected { get; set; }
	}
}

