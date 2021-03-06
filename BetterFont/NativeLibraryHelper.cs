﻿using System;
using System.IO;
using System.Runtime.InteropServices;

namespace BetterFont
{
	internal static class NativeLibraryHelper
	{
		[DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
		private static extern IntPtr NativeLoadLibrary(string path);

		public static void LoadLibrary(string libName)
		{
			IntPtr lib = NativeLoadLibrary(libName);
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

			string[] names = typeof(NativeLibraryHelper).Assembly.GetManifestResourceNames();

			using (Stream stream = typeof(NativeLibraryHelper).Assembly.GetManifestResourceStream(resourceName))
			{
				byte[] data = new BinaryReader(stream).ReadBytes((int)stream.Length);
				File.WriteAllBytes(dllPath, data);
				LoadLibrary(dllPath);
			}
		}
	}
}
