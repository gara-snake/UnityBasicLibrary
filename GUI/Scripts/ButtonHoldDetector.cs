using UnityEngine;
using UnityEngine.EventSystems;

namespace Snake.Gara.Unity.Basic.Library.GUI
{

	public class ButtonHoldDetector : MonoBehaviour
	{
		private bool isPointerDown = false;
		private float pointerDownTimer = 0.0f;

		// 長押し状態を取得するプロパティ
		public bool IsHolding { get; private set; } = false;

		/// <summary>
		/// 長押し時間
		/// </summary>
		/// <value></value>
		public float HoldTime
		{
			get
			{
				return pointerDownTimer;
			}
		}

		void Start()
		{
			// EventTriggerコンポーネントをアタッチ
			EventTrigger trigger = gameObject.AddComponent<EventTrigger>();

			// PointerDownイベントの登録
			EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry
			{
				eventID = EventTriggerType.PointerDown
			};
			pointerDownEntry.callback.AddListener((eventData) => { OnPointerDown((PointerEventData)eventData); });
			trigger.triggers.Add(pointerDownEntry);

			// PointerUpイベントの登録
			EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry
			{
				eventID = EventTriggerType.PointerUp
			};
			pointerUpEntry.callback.AddListener((eventData) => { OnPointerUp((PointerEventData)eventData); });
			trigger.triggers.Add(pointerUpEntry);
		}

		void Update()
		{
			if (isPointerDown)
			{
				pointerDownTimer += UnityEngine.Time.deltaTime;
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			isPointerDown = true;
			pointerDownTimer = 0.0f;
			IsHolding = true; // 長押しが始まったことを示す
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			Reset();
		}

		private void Reset()
		{
			isPointerDown = false;
			pointerDownTimer = 0.0f;
			IsHolding = false; // 長押しが終了したことを示す
		}

	}

}