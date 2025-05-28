using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class SoundFiles
{
	public static async Task<AudioClip> LoadAudioClip(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			Debug.LogError("Path is empty.");
			return null;
		}

		// UnityWebRequestを作成
		using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG))
		{

			// リクエストの送信（非同期処理）
			var operation = request.SendWebRequest();
			while (!operation.isDone)
			{
				await Task.Yield();
			}

			// エラーチェック
			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError($"Failed to load audio clip: {request.error}" + " path: " + path);
				return null;
			}

			// AudioClipの取得
			return DownloadHandlerAudioClip.GetContent(request);
		}
	}

}