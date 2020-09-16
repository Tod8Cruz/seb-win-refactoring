﻿using Microsoft.Win32;

namespace SafeExamBrowser.CustomProtocol.Registrator
{
	public static class Installer
	{
		public static bool IsProtocolAlreadyRegistered(string protocolName)
		{
			try
			{
				var key = Registry.CurrentUser.OpenSubKey(@"Software\Classes", false);
				var rkRoot = key.OpenSubKey(protocolName, RegistryKeyPermissionCheck.ReadSubTree);
				var rkRootUrlProtocolValue = rkRoot.GetValue("URL Protocol");

				if ((rkRootUrlProtocolValue as string) != "") return false;

				var rkShell = rkRoot.OpenSubKey("shell", RegistryKeyPermissionCheck.ReadSubTree);
				var rkOpen = rkShell.OpenSubKey("open", RegistryKeyPermissionCheck.ReadSubTree);
				var rkCommand = rkOpen.OpenSubKey("command", RegistryKeyPermissionCheck.ReadSubTree);
				if (string.IsNullOrEmpty(rkCommand.GetValue("") as string)) return false;
			}
			catch
			{
				return false;
			}

			return true;
		}

		public static bool RegisterProtocol(string protocolName, string appPath)
		{
			try
			{
				var key = Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);
				var rkRoot = key.CreateSubKey(protocolName, RegistryKeyPermissionCheck.ReadWriteSubTree);

				var protocolDescription = $"URL: {protocolName} Protocol";
				if (protocolName == "seb")
				{
					protocolDescription = "URL: Safe Exam Browser Protocol";
				} else if (protocolName == "sebs")
				{
					protocolDescription = "URL: Safe Exam Browser Secure Protocol";
				}

				rkRoot.SetValue("", protocolDescription);
				rkRoot.SetValue("URL Protocol", "");
				var rkIcon = rkRoot.CreateSubKey("DefaultIcon");
				rkIcon.SetValue("", appPath + ", 1");
				var rkShell = rkRoot.CreateSubKey("shell", RegistryKeyPermissionCheck.ReadWriteSubTree);
				var rkOpen = rkShell.CreateSubKey("open", RegistryKeyPermissionCheck.ReadWriteSubTree);
				var rkCommand = rkOpen.CreateSubKey("command", RegistryKeyPermissionCheck.ReadWriteSubTree);
				rkCommand.SetValue("", $"\"{appPath}\" \"%1\"");
			}
			catch
			{
				return false;
			}

			return true;
		}

		public static bool UnregisterProtocol(string protocolName)
		{
			try
			{
				var key = Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);

				key.DeleteSubKeyTree(protocolName);

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}