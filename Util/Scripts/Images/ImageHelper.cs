using System;
using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.Util
{

	public class ImageHelper
	{

		public static Texture2D FlipTexture(Texture2D original)
		{
			Texture2D flipped = new Texture2D(original.width, original.height);

			// 左右のみ反転させる
			for (int i = 0; i < original.width; i++)
			{
				for (int j = 0; j < original.height; j++)
				{
					flipped.SetPixel(original.width - i - 1, j, original.GetPixel(i, j));
				}
			}

			flipped.Apply();

			return flipped;
		}

		public static Texture2D ResizeTexture(Texture2D original, float scale)
		{
			int width = (int)(original.width * scale);
			int height = (int)(original.height * scale);

			Texture2D resized = new Texture2D(width, height);

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					resized.SetPixel(i, j, original.GetPixelBilinear((float)i / width, (float)j / height));
				}
			}

			resized.Apply();

			return resized;
		}

	}



}
