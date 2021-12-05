using System;
using System.Collections.Generic;


public class Match
{
	// redo with CachedReadOnly
	List<Deck> Kingdom => _kingdom;

	Player _firstPlayer;
	Player _secondPlayer;
	List<Deck> _kingdom;

	public Match(Agent firstAgent, Agent secondAgent, List<Deck> kingdom, IEnumerable<CardType> startingDeck, Random random)
	{
		_firstPlayer = new Player(random);
		_secondPlayer = new Player(random);
		_kingdom = kingdom;

		_firstPlayer.Setup(startingDeck);
		_secondPlayer.Setup(startingDeck);
	}

	void PlayerTurn(Player player, Agent agent)
	{
		player.StartTurn();

		while(player.Phase != PlayerPhase.OFFTURN)
		{
			agent.MakeMove(this);
		}
	}
}