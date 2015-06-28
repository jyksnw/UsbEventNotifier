using System;

namespace UsbEventNotifier
{
	public class UsbEventNotifierException : Exception
	{
		private const string BASE_MESSAGE = "UsbEventNotifierException: ";

		public UsbEventNotifierException ()
			: base ( BASE_MESSAGE + base.Message)
		{
		}

		public UsbEventNotifierException(string message)
			:base (BASE_MESSAGE + message)
		{
		}

		public UsbEventNotifierException(string message, Exception innerException)
			: base (BASE_MESSAGE + message, innerException)
		{
		}
	}
}

