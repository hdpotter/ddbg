using System;
using System.Collections.Generic;


public abstract class Agent
{
	Player _player;

	public Agent(Player player)
	{
		_player = player;
	}

	public abstract void MakeMove(Match match);
}

