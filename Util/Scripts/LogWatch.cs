using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Snake.Gara.Unity.Basic.Library.Util
{

	public class LogWatch : MonoBehaviour
	{
		public Text logText;

		private int maxLogMessages = 20;

		private List<string> logMessages = new List<string>();

		void Awake()
		{
			Application.logMessageReceived += HandleLog;
		}

		void OnDestroy()
		{
			Application.logMessageReceived -= HandleLog;
		}

		private void HandleLog(string logString, string stackTrace, LogType type)
		{
			logMessages.Add(logString);

			// 最大ログメッセージ数を超えた場合、最初のメッセージを削除
			if (logMessages.Count > maxLogMessages)
			{
				logMessages.RemoveAt(0);
			}

			// 表示するログメッセージを更新
			logText.text = string.Join("\n", logMessages.ToArray());
		}

		public void AddLog(string message)
		{
			HandleLog(message, "", LogType.Log);
		}
	}

}