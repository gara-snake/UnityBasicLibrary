using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Snake.Gara.Unity.Basic.Library.Files
{
	public static class ImageFiles
	{
		// PNGファイルとして一時保存し、ファイルパスのリストを返す
		public static List<string> SaveTextures(List<Texture2D> textures, string prefix = "tex")
		{
			List<string> tempFilePaths = new List<string>();
			string tempDirectory = Path.GetTempPath(); // 一時ディレクトリを取得

			for (int i = 0; i < textures.Count; i++)
			{
				byte[] bytes = textures[i].EncodeToPNG();
				string filePath = Path.Combine(tempDirectory, $"{prefix}_{i}.png");
				File.WriteAllBytes(filePath, bytes);
				tempFilePaths.Add(filePath);
			}

			return tempFilePaths;
		}
	}
}