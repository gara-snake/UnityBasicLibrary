using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Snake.Gara.Unity.Basic.Library.Files
{
	public static class TempFiles
	{
		// 一時ファイルを削除
		public static void DeleteTempFiles(List<string> tempFilePaths)
		{
			foreach (string filePath in tempFilePaths)
			{
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}
			}
		}
	}
}