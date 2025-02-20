
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebTexture
{
	public static IEnumerator DownloadImage(string url, RawImage rawImage)
	{
		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
		{
			yield return uwr.SendWebRequest();

			if (uwr.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Failed to download image: " + uwr.error);
			}
			else
			{
				Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
				rawImage.texture = texture;
			}
		}
	}


}