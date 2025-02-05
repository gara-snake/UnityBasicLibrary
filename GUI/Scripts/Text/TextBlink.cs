using UnityEngine;
using UnityEngine.UI;

namespace Snake.Gara.Unity.Basic.Library.GUI
{

	public class TextBlink : MonoBehaviour
	{
		// 点滅周期
		public float BlinkInterval = 0.5f;
		public Color BlinkColor = Color.red;

		private float time = 0;

		private bool isBlink = false;

		private TextMesh textMesh;
		private Text text;

		private Color originalColor;

		void Start()
		{
			textMesh = GetComponent<TextMesh>();
			text = GetComponent<Text>();

			if (textMesh != null)
			{
				originalColor = textMesh.color;
			}
			else if (text != null)
			{
				originalColor = text.color;
			}
		}

		void Update()
		{
			time += UnityEngine.Time.deltaTime;

			if (time > BlinkInterval)
			{
				isBlink = !isBlink;
				time = 0;
			}

			if (textMesh != null)
			{
				textMesh.color = isBlink ? BlinkColor : originalColor;
			}
			else if (text != null)
			{
				text.color = isBlink ? BlinkColor : originalColor;
			}

		}
	}

}