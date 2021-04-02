using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public interface IRewindableAction
{
	void Dispatch();
	void Rewind();
}

public class RewindManager : MonoBehaviour
{
	public Action<bool> OnFreeze = null;
	
	struct ActionLog
	{
		public float time;
		public IRewindableAction action;
	}
	
	public static RewindManager instance = null;
	public bool Freezed { get; private set; } = false;

	private List<ActionLog> performedActions = new List<ActionLog>();

	private float localTime = 0f;
	
	private void Awake()
	{
		instance = this;
	}

	public void Dispatch(IRewindableAction action)
	{
		action.Dispatch();

		performedActions.Add(new ActionLog
		{
			time = localTime,
			action = action
		});
	}

	private void LateUpdate()
	{
		if (Freezed && !Input.GetKey(KeyCode.R))
			return;
		
		localTime = Mathf.Max(0f, localTime + (Input.GetKey(KeyCode.R) ? - Time.deltaTime : Time.deltaTime));

		if (! Input.GetKey(KeyCode.R))
		{
			return;
		}
		
		var reversedActions = Enumerable.Reverse(performedActions).TakeWhile(a => a.time > localTime);

		foreach (ActionLog log in reversedActions)
		{
			log.action.Rewind();
		}

		if (reversedActions.Count() > 0)
		{
			performedActions = performedActions.Except(reversedActions).ToList();
		}
	}

	public void SetFreeze(bool value)
	{
		Freezed = value;
		
		OnFreeze?.Invoke(value);
	}
}
