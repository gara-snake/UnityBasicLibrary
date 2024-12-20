using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.Files
{
	public class ZipUtility : System.IDisposable
	{
		private List<string> tempFiles = new List<string>();

		// ファイルパスのリストを受け取り、ZIPファイルとして圧縮し、ZIPファイルのパスを返す
		public string CreateZipFromFiles(List<string> filePaths, string zipFilePath)
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

		// ZIPファイルから指定されたファイル名のファイルを取り出し、一時ファイルとして保存してファイルパスを返す
		public string ExtractFileFromZip(string zipFilePath, string fileName)
		{
			if (File.Exists(zipFilePath))
			{
				using (ZipArchive zip = ZipFile.OpenRead(zipFilePath))
				{
					foreach (ZipArchiveEntry entry in zip.Entries)
					{
						if (entry.FullName == fileName)
						{
							string extractedFilePath = Path.Combine(Path.GetTempPath(), entry.FullName);
							entry.ExtractToFile(extractedFilePath, true);
							tempFiles.Add(extractedFilePath);
							return extractedFilePath;
						}
					}
				}

				Debug.LogError("The file was not found in the ZIP file.");
				return null;
			}
			else
			{
				Debug.LogError("The ZIP file was not found.");
				return null;
			}
		}


		// ZIPファイルを解凍し、ファイルパスのリストを返す
		public List<string> ExtractZipToFiles(string zipFilePath)
		{
			List<string> extractedFilePaths = new List<string>();

			if (File.Exists(zipFilePath))
			{
				using (ZipArchive zip = ZipFile.OpenRead(zipFilePath))
				{
					foreach (ZipArchiveEntry entry in zip.Entries)
					{
						string extractedFilePath = Path.Combine(Path.GetTempPath(), entry.FullName);
						entry.ExtractToFile(extractedFilePath, true);
						extractedFilePaths.Add(extractedFilePath);
						tempFiles.Add(extractedFilePath);
					}
				}

				Debug.Log("ZIP file extracted successfully: " + zipFilePath);
				return extractedFilePaths;
			}
			else
			{
				Debug.LogError("The ZIP file was not found.");
				return null;
			}
		}

		public void Dispose()
		{
			foreach (var tempFile in tempFiles)
			{
				if (File.Exists(tempFile))
				{
					File.Delete(tempFile);
				}
			}
			tempFiles.Clear();
		}
	}
}