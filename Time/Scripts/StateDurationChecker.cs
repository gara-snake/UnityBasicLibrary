using System;
using System.Diagnostics;

namespace Snake.Gara.Unity.Basic.Library.Time
{

	public class StateDurationChecker
	{
		private float requiredDuration; // 状態が持続する必要がある時間（秒）
		private bool isStateActive;
		private float stateElapsedTime; // 状態がアクティブになってからの経過時間

		// 状態持続完了時に実行されるコールバックデリゲート
		private Action onStateDurationMetCallback;

		public StateDurationChecker(float duration, Action callback)
		{
			requiredDuration = duration;
			isStateActive = false;
			stateElapsedTime = 0.0f;
			onStateDurationMetCallback = callback;
		}

		// 更新メソッド
		public void Update(float deltaTime, Func<bool> checkState)
		{
			if (checkState())
			{
				if (!isStateActive)
				{
					// 状態がアクティブになった場合、リセット
					isStateActive = true;
					stateElapsedTime = 0.0f;
				}
				else
				{
					// 状態がアクティブな場合、持続時間を計測
					stateElapsedTime += deltaTime;
					if (stateElapsedTime >= requiredDuration)
					{
						OnStateDurationMet();
						isStateActive = false; // 状態が持続したことを検知した後はリセット
					}
				}
			}
			else
			{
				// 状態がアクティブでない場合、リセット
				isStateActive = false;
				stateElapsedTime = 0.0f;
			}
		}

		// 状態が指定された時間持続した場合に呼ばれるメソッド
		private void OnStateDurationMet()
		{
			onStateDurationMetCallback?.Invoke();
		}

		// 進捗を返すプロパティ
		public float Progress
		{
			get
			{
				if (requiredDuration == 0) return 0;
				return Math.Min(stateElapsedTime / requiredDuration, 1.0f);
			}
		}
	}

}