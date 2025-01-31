using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Snake.Gara.Unity.Basic.Library.Files
{
	public class ImageFiles : System.IDisposable
	{
		private List<string> tempFilePaths = new List<string>();

		// テクスチャをPNGファイルとして一時保存し、ファイルパスを返す
		public string SaveTexture(Texture2D texture, string name = "tex")
		{
			byte[] bytes = texture.EncodeToPNG();
			string tempFilePath = Path.Combine(Path.GetTempPath(), $"{name}.png");
			File.WriteAllBytes(tempFilePath, bytes);
			tempFilePaths.Add(tempFilePath);
			return tempFilePath;
		}

		// PNGファイルとして一時保存し、ファイルパスのリストを返す
		public List<string> SaveTextures(List<Texture2D> textures, string prefix = "tex_")
		{
			List<string> paths = new List<string>();
			var i = 0;

			foreach (Texture2D texture in textures)
			{
				paths.Add(SaveTexture(texture, prefix + i.ToString()));
				i++;
			}

			return paths;
		}

		// Pngファイルを読み込んでテクスチャを返す
		public static Texture2D LoadTexture(string filePath)
		{
			if (File.Exists(filePath))
			{
				byte[] bytes = File.ReadAllBytes(filePath);
				Texture2D texture = new Texture2D(2, 2);
				texture.LoadImage(bytes);
				return texture;
			}
			else
			{
				throw new FileNotFoundException($"The file {filePath} was not found.");
			}
		}

		// Pngファイルを読み込んでテクスチャのリストを返す
		public List<Texture2D> LoadTextures(List<string> filePaths)
		{
			List<Texture2D> textures = new List<Texture2D>();

			foreach (string path in filePaths)
			{
				textures.Add(LoadTexture(path));
			}

			return textures;
		}

		// 一時ファイルを削除する
		public void Dispose()
		{
			foreach (string path in tempFilePaths)
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			tempFilePaths.Clear();
		}
	}
}