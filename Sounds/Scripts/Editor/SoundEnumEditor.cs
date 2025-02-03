#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using Snake.Gara.Unity.Basic.Library.Util;

namespace Snake.Gara.Unity.Basic.Library.Sound
{

	public class SoundEnumEditor : EditorWindow
	{
		private SoundData soundData;
		private string enumName = string.Empty;
		private string outputPath = "Assets/Project/Scripts/SoundID.cs";

		[MenuItem("UBL Tools/Sound Enum Generator")]
		public static void ShowWindow()
		{
			GetWindow<SoundEnumEditor>("Sound Enum Editor");
		}

		private void OnGUI()
		{
			GUILayout.Label("Sound Enum Generator", EditorStyles.boldLabel);

			soundData = (SoundData)EditorGUILayout.ObjectField("Sound Data", soundData, typeof(SoundData), false);

			if (soundData == null)
				return;

			if (string.IsNullOrEmpty(enumName))
			{
				enumName = soundData.name + "ID";
			}
			enumName = EditorGUILayout.TextField("Enum Name", enumName);
			outputPath = EditorGUILayout.TextField("Output Path", outputPath);

			if (GUILayout.Button("Generate Enum"))
			{
				GenerateEnum();
			}
		}

		private void GenerateEnum()
		{
			if (soundData == null)
			{
				Debug.LogError("Sound Data is not set!");
				return;
			}

			StringBuilder enumContent = new StringBuilder();
			enumContent.AppendLine("public enum " + enumName);
			enumContent.AppendLine("{");

			foreach (var clip in soundData.SeClips)
			{
				if (clip != null)
				{
					enumContent.AppendLine("\t" + StringConverter.PascalToSnake(clip.Name).ToUpper() + ",");
				}
			}

			enumContent.AppendLine("}");

			File.WriteAllText(outputPath, enumContent.ToString());
			AssetDatabase.Refresh();

			Debug.Log(enumName + " generated successfully at " + outputPath);
		}
	}

}

#endif