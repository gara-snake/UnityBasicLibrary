using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SettingsWrapper
{
	public List<Setting> settings;

	[Serializable]
	public class Setting
	{
		public string key;
		public string value;
	}

	public Dictionary<string, string> ToDictionary()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (var setting in settings)
		{
			dictionary[setting.key] = setting.value;
		}
		return dictionary;
	}
}

public class SettingsLoader
{

	private static SettingsLoader instance;

	public Dictionary<string, string> Settings;

	public static void Initialize(string fileName = "setting.json")
	{
		if (instance == null)
		{
			instance = new SettingsLoader();
			instance.LoadSettings(fileName);
		}
	}

	private void LoadSettings(string fileName)
	{
		string path = Path.Combine(Application.streamingAssetsPath, fileName);

		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SettingsWrapper wrapper = JsonUtility.FromJson<SettingsWrapper>(json);
			Settings = wrapper.ToDictionary();
		}
		else
		{
			Debug.LogError("Settings file not found: " + path);
		}
	}

	public static string Get(string key)
	{
		if (instance == null)
		{
			Debug.LogError("SettingsLoader is not initialized.");
			return null;
		}

		if (instance.Settings.ContainsKey(key))
		{
			return instance.Settings[key];
		}
		else
		{
			Debug.LogError("Key not found: " + key);
			return null;
		}
	}


}