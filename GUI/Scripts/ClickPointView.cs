using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.GUI
{

	public class ClickPointView : MonoBehaviour
	{
		public RectTransform Contents;

		// Contentsの表示非表示を指定する
		public void SetActive(bool active)
		{
			Contents.gameObject.SetActive(active);
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector2 mousePosition = Input.mousePosition;
				Vector2 anchoredPosition;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(
					Contents.parent as RectTransform,
					mousePosition,
					null,
					out anchoredPosition
				);
				Contents.anchoredPosition = anchoredPosition;
			}
		}
	}

}