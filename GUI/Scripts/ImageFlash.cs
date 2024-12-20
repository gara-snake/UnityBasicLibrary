using UnityEngine;
using UnityEngine.UI;

namespace Snake.Gara.Unity.Basic.Library.GUI
{
	/// <summary>
	/// Imageを点滅させるクラス
	/// </summary>
	[RequireComponent(typeof(MaskableGraphic))]
	public class ImageFlash : MonoBehaviour
	{
		public MaskableGraphic targetImage;
		public float flashDuration = 1.0f;
		public Color flashColor = Color.white;

		private Color originalColor;
		private float timer;
		private bool isFlashing;

		void Start()
		{
			if (targetImage == null)
			{
				Debug.LogError("Target Image is not assigned.");
				return;
			}

			originalColor = targetImage.color;
		}

		void Update()
		{
			if (isFlashing)
			{
				timer += UnityEngine.Time.deltaTime;
				float lerp = Mathf.PingPong(timer, flashDuration) / flashDuration;
				targetImage.color = Color.Lerp(originalColor, flashColor, lerp);

				if (timer >= flashDuration)
				{
					isFlashing = false;
					targetImage.color = originalColor;
				}
			}
		}

		public void StartFlashing()
		{
			if (targetImage == null)
			{
				Debug.LogError("Target Image is not assigned.");
				return;
			}

			isFlashing = true;
			timer = 0f;
		}
	}

}