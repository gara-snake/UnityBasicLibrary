using System;
using System.Text.RegularExpressions;

namespace Snake.Gara.Unity.Basic.Library.Util
{
	public class StringConverter
	{
		public static string PascalToSnake(string input)
		{
			// 正規表現を使ってパスカルケースをスネークケースに変換
			var snakeCase = Regex.Replace(input, "([a-z])([A-Z])", "$1_$2").ToLower();
			return snakeCase;
		}
	}
}