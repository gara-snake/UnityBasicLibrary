using System;
using System.IO;
using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.Files
{
    public static class JsonFileUtility
    {
        // オブジェクトをJSON形式でシリアライズし、一時ファイルに保存してファイルパスを返す
        public static string SaveObjectToTempFile<T>(T obj)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
            string jsonString = JsonUtility.ToJson(obj);

            File.WriteAllText(tempFilePath, jsonString);
            return tempFilePath;
        }

        // 一時ファイルからオブジェクトを読み込み、デシリアライズして返す
        public static T LoadObjectFromTempFile<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonUtility.FromJson<T>(jsonString);
            }
            else
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }
        }
    }
}