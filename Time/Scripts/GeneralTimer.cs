using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Snake.Gara.Unity.Basic.Library.Time
{

	public class GeneralTimer
	{
		private Stopwatch stopwatch;
		private TimeSpan pausedTime;
		private TimeSpan duration;
		private List<double> notificationRemainingTimes;
		private HashSet<double> notifiedTimes;

		public event Action<GeneralTimer, double> OnTimeReached;

		// タイマーが満了したら呼び出すコールバック
		public event Action OnComplete;

		public GeneralTimer()
		{
			stopwatch = new Stopwatch();
			pausedTime = TimeSpan.Zero;
			duration = TimeSpan.Zero;
			notificationRemainingTimes = new List<double>();
			notifiedTimes = new HashSet<double>();
		}

		public double Duration
		{
			get
			{
				return duration.TotalMilliseconds;
			}
		}

		// 制限時間をミリ秒で設定
		public void SetDuration(double milliseconds)
		{
			duration = TimeSpan.FromMilliseconds(milliseconds);
		}

		private bool running = false;
		public bool IsRunning
		{
			get
			{
				return running && 0 < RemainingMilliseconds;
			}
		}

		// 通知する時間を追加
		public void AddNotificationRemainingTime(double milliseconds)
		{
			notificationRemainingTimes.Add(milliseconds);
		}

		// 通知する時間をリセット
		public void ResetNotificationRemainingTimes()
		{
			notificationRemainingTimes.Clear();
			notifiedTimes.Clear();
		}

		// タイマーを開始
		public void Start()
		{
			if (!stopwatch.IsRunning)
			{
				stopwatch.Start();
				running = true;
			}
		}

		// タイマーを一時停止
		public void Pause()
		{
			if (stopwatch.IsRunning)
			{
				stopwatch.Stop();
				pausedTime = stopwatch.Elapsed;
				running = false;
			}
		}

		// タイマーを再開
		public void Resume()
		{
			if (!stopwatch.IsRunning)
			{
				stopwatch.Start();
				running = true;
			}
		}

		// タイマーをリセット
		public void Reset()
		{
			stopwatch.Reset();
			pausedTime = TimeSpan.Zero;
			running = false;
			notifiedTimes.Clear();
		}

		private TimeSpan elapsedOffset = TimeSpan.Zero;

		private TimeSpan GetElapsedTime()
		{
			if (stopwatch.IsRunning)
			{
				return stopwatch.Elapsed;
			}
			else
			{
				return pausedTime;
			}
		}

		// 経過時間をミリ秒で返却
		public double ElapsedMilliseconds
		{
			get
			{
				return GetElapsedTime().TotalMilliseconds;
			}
		}

		// 残り時間をミリ秒で返却
		public double RemainingMilliseconds
		{
			get
			{
				TimeSpan remaining = duration - GetElapsedTime();
				return Math.Max(remaining.TotalMilliseconds, 0);
			}
		}

		// 残り時間を秒で返却
		public double RemainingSeconds
		{
			get
			{
				return RemainingMilliseconds / 1000;
			}
		}

		private bool isChanged = false;

		// 残り時間を設定（ミリ秒）
		public void SetRemainingMilliseconds(double milliseconds)
		{
			isChanged = true;

			// 制限時間を更新して、経過時間をリセット
			SetDuration(milliseconds);

			// milliseconds以下通知はもう一度送るので、notifiedTimesから削除する
			foreach (double time in notificationRemainingTimes)
			{
				if (time <= milliseconds)
				{
					notifiedTimes.Remove(time);
				}
			}

			stopwatch.Reset();
			pausedTime = TimeSpan.Zero;

		}

		// 指定した時間が経過しているか
		public bool HasElapsed()
		{
			return GetElapsedTime() >= duration;
		}

		// タイマーが開始されているか
		public bool HasStarted
		{
			get
			{
				return ElapsedMilliseconds != 0 || isChanged;
			}
		}

		// タイマーの進捗率
		public float Progress
		{
			get
			{
				return (float)(ElapsedMilliseconds / duration.TotalMilliseconds);
			}
		}

		// タイマーの監視
		public void Update()
		{
			foreach (double time in notificationRemainingTimes)
			{
				// 残り時間がtime以下になったタイミングで通知
				if (RemainingMilliseconds <= time && !notifiedTimes.Contains(time))
				{
					OnTimeReached?.Invoke(this, time);
					notifiedTimes.Add(time);
				}
			}

			if (HasElapsed())
			{
				OnComplete?.Invoke();
				running = false;
			}

		}

	}
}