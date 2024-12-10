using UnityEngine;
using UnityEngine.UI;


namespace Snake.Gara.Unity.Basic.Library.GUI
{
	public class ProgressBar : MonoBehaviour
	{
		[Range(0, 1)]
		public float fillAmount = 1.0f; // 0%～100%のゲージの進行度（0.0f～1.0f）

		private RawImage rawImage;

		private float imageW;
		private float imageH;

		void Start()
		{
			// 同じゲームオブジェクトにアタッチされているRawImageコンポーネントを取得
			rawImage = GetComponent<RawImage>();
			if (rawImage == null)
			{
				Debug.LogError("RawImage component not found!");
			}
			else
			{
				imageW = rawImage.rectTransform.sizeDelta.x;
				imageH = rawImage.rectTransform.sizeDelta.y;
			}
		}

		void Update()
		{
			if (rawImage != null)
			{
				SetFillAmount(fillAmount);
			}
		}

		public void SetFillAmount(float amount)
		{
			// FillAmountを0～1の範囲に制限
			amount = Mathf.Clamp01(amount);

			// RectTransformを調整して表示領域を制御
			rawImage.rectTransform.sizeDelta = new Vector2(imageW * amount, imageH);
		}
	}
}