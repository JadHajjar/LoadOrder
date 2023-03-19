using System;
using System.Collections.Generic;
using System.Threading;

namespace LoadOrderToolTwo.Domain.Utilities;

public class DelayedAction<TKey>
{
	private readonly Dictionary<TKey, Timer> _timers;
	private readonly int _delayMilliseconds;

	public DelayedAction(int delayMilliseconds)
	{
		_timers = new Dictionary<TKey, Timer>();
		_delayMilliseconds = delayMilliseconds;
	}

	public void Run(TKey key, Action<TKey> action)
	{
		lock (_timers)
		{
			if (_timers.TryGetValue(key, out var timer))
			{
				timer.Change(_delayMilliseconds, Timeout.Infinite);
			}
			else
			{
				timer = new Timer(_ =>
				{
					lock (_timers)
					{
						action(key);
						_timers.Remove(key);
					}
				}, null, _delayMilliseconds, Timeout.Infinite);

				_timers.Add(key, timer);
			}
		}
	}
}

public class DelayedAction
{
	private Timer? _timer;
	private readonly int _delayMilliseconds;

	public DelayedAction(int delayMilliseconds)
	{
		_delayMilliseconds = delayMilliseconds;
	}

	public void Run(Action action)
	{
		if (_timer != null)
		{
			_timer.Change(_delayMilliseconds, Timeout.Infinite);
		}
		else
		{
			_timer = new Timer(_ =>
			{
					action();
					_timer = null;
			}, null, _delayMilliseconds, Timeout.Infinite);
		}
	}
}
