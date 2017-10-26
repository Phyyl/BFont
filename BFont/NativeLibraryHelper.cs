using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace BFontCore
{
	internal static class NativeLibraryHelper
	{
		[DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
		private static extern IntPtr NativeLoadLibrary(string path);

		public static void LoadLibrary(string libName)
		{
			NativeLoadLibrary(libName);
		}

		public static void LoadEmbeddedLibrary(string @namespace, string libName)
		{
			string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
			string dllPath = Path.Combine(tempDir, libName);

			if (!Directory.Exists(tempDir))
			{
				Directory.CreateDirectory(tempDir);
			}

			string resourceName = $"{@namespace}.{libName}";

			using (Stream stream = typeof(NativeLibraryHelper).Assembly.GetManifestResourceStream(resourceName))
			{
				byte[] data = new BinaryReader(stream).ReadBytes((int)stream.Length);
				File.WriteAllBytes(dllPath, data);
				LoadLibrary(dllPath);
			}
		}
	}
}
