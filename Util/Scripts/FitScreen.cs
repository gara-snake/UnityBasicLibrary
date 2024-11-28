using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.Util
{
	/// <summary>
	/// ゲーム中のカメラのアス比を固定します
	/// </summary>
	public class FitScreen : MonoBehaviour
	{

		Camera _camera;

		[SerializeField] Vector2 targetResolution;

		private void Awake()
		{
			_camera = GetComponent<Camera>();
			SetScreen();
		}

		void SetScreen()
		{
			var scrnAspect = (float)Screen.width / (float)Screen.height;
			var targAspect = targetResolution.x / targetResolution.y;

			var rate = targAspect / scrnAspect;
			var rect = new Rect(0, 0, 1, 1);

			if (rate < 1)
			{
				rect.width = rate;
				rect.x = 0.5f - rect.width * 0.5f;
			}
			else
			{
				rect.height = 1 / rate;
				rect.y = 0.5f - rect.height * 0.5f;
			}

			_camera.rect = rect;
		}
	}
}
