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
		private List<double> notificationTimes;
		private HashSet<double> notifiedTimes;

		public event Action<double> OnTimeReached;

		public GeneralTimer()
		{
			stopwatch = new Stopwatch();
			pausedTime = TimeSpan.Zero;
			duration = TimeSpan.Zero;
			notificationTimes = new List<double>();
			notifiedTimes = new HashSet<double>();
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
		public void AddNotificationTime(double milliseconds)
		{
			notificationTimes.Add(milliseconds);
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
				return ElapsedMilliseconds != 0;
			}
		}

		// タイマーの監視
		public void Update()
		{
			double elapsedMilliseconds = ElapsedMilliseconds;

			foreach (double time in notificationTimes)
			{
				if (elapsedMilliseconds >= time && !notifiedTimes.Contains(time))
				{
					OnTimeReached?.Invoke(time);
					notifiedTimes.Add(time);
				}
			}
		}

	}

}