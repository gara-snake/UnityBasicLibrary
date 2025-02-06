using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.CameraUtil
{

	[RequireComponent(typeof(UnityEngine.Camera))]
	public class FixedAspectRatio : MonoBehaviour
	{

		public new UnityEngine.Camera camera;

		public float targetAspect = 9.0f / 16.0f;

		void Start()
		{
			if (camera == null)
			{
				camera = GetComponent<UnityEngine.Camera>();
			}

			if (camera == null) return;

			float windowAspect = (float)Screen.width / (float)Screen.height;
			float scaleHeight = windowAspect / targetAspect;

			if (scaleHeight < 1.0f) // 画面の高さが不足
			{
				Rect rect = camera.rect;

				rect.width = 1.0f;
				rect.height = scaleHeight;
				rect.x = 0;
				rect.y = (1.0f - scaleHeight) / 2.0f;

				camera.rect = rect;
			}
			else // 画面の幅が不足
			{
				float scaleWidth = 1.0f / scaleHeight;

				Rect rect = camera.rect;

				rect.width = scaleWidth;
				rect.height = 1.0f;
				rect.x = (1.0f - scaleWidth) / 2.0f;
				rect.y = 0;

				camera.rect = rect;
			}
		}
	}

}