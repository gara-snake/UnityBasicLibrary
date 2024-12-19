using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.Files
{
	public static class ZipUtility
	{
		// ファイルパスのリストを受け取り、ZIPファイルとして圧縮し、ZIPファイルのパスを返す
		public static string CreateZipFromFiles(List<string> filePaths, string zipFilePath)
		{
			if (filePaths.Count > 0)
			{
				if (File.Exists(zipFilePath))
				{
					File.Delete(zipFilePath);
				}

				using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
				{
					foreach (string filePath in filePaths)
					{
						string entryName = Path.GetFileName(filePath);
						zip.CreateEntryFromFile(filePath, entryName);
					}
				}

				Debug.Log("ZIP file created successfully: " + zipFilePath);
				return zipFilePath;
			}
			else
			{
				Debug.LogError("No files found to zip.");
				return null;
			}
		}
	}
}