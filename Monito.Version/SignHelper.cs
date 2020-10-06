using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Monito.Version
{
	public static class SignHelper
	{
		public static X509Certificate2 GetCertificate()
		{
			var assembly = Assembly.GetAssembly(typeof(SignHelper));
			var names = assembly.GetManifestResourceNames();

			var certificateResourceName = names.FirstOrDefault(s => s.Contains("monito.pfx"));

			var assembyDataStream = assembly.GetManifestResourceStream(certificateResourceName);

			if (assembyDataStream == null) throw new Exception("Certificate not found in assembly.");

			var bytes = new byte[assembyDataStream.Length];
			assembyDataStream.Read(bytes, 0, bytes.Length);
			return new X509Certificate2(bytes, "123");
		}

	}
}
