using System;

using UnityEngine;

public class CheatTable
{
	public (string, Action)[] Cheats = null;
	
	// All cheats described here
	public CheatTable()
	{
		Cheats = new (string, Action)[]
		{
			("WINRAR", Win),
			("AWAKEORFEI", Suicide),
			("NOTAROBOT", KillAI),
			("AMOGUS", Restart),
		};
	}

	public void Win()
	{
		Gameplay.instance.Win();
	}

	public void Suicide()
	{
		Gameplay.instance.PlayerGetsKilled(GameObject.FindWithTag("Player"));
	}

	public void KillAI()
	{
		foreach (var enemy in GameObject.FindGameObjectsWithTag("EnemyBall"))
		{
			GameObject.Destroy(enemy);
		}
	}

	public void Restart()
	{
		Gameplay.instance.RestartLevel();
	}
}