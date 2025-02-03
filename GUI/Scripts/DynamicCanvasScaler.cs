using UnityEngine;
using UnityEngine.UI;

public class DynamicCanvasScaler : MonoBehaviour
{
	public CanvasScaler canvasScaler;

	void Start()
	{
		if (canvasScaler == null)
		{
			canvasScaler = GetComponent<CanvasScaler>();
		}

		if (canvasScaler == null) return;

		UpdateCanvasScaler();
	}

	void UpdateCanvasScaler()
	{
		float screenAspect = (float)Screen.width / Screen.height;

		// 16:9のアスペクト比を基準に動的にmatchWidthOrHeightを変更
		if (screenAspect > 1.0f) // 横長
		{
			canvasScaler.matchWidthOrHeight = 1.0f; // 高さに合わせる
		}
		else // 縦長
		{
			canvasScaler.matchWidthOrHeight = 0.0f; // 幅に合わせる
		}
	}
}
