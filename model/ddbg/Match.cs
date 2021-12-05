using System;
using System.Collections.Generic;


public class Match
{
	public int Winner => _winner;

	// redo with CachedReadOnly
	List<Deck> Kingdom => _kingdom;

	Player _firstPlayer;
	Player _secondPlayer;
	List<Deck> _kingdom;

	int _winner = -1;

	public Match(Agent firstAgent, Agent secondAgent, MatchType matchType, Random random)
	{
		_firstPlayer = new Player(random);
		_secondPlayer = new Player(random);
		_kingdom = new List<Deck>(matchType.Kingdom);

		_firstPlayer.Setup(matchType.StartingDeck);
		_secondPlayer.Setup(matchType.StartingDeck);
	}

	void PlayerTurn(Player player, Agent agent)
	{
		player.StartTurn();

		while(player.Phase != PlayerPhase.OFFTURN)
		{
			agent.MakeMove(this);
		}

		CheckForGameEnd();
	}

	bool CheckForGameEnd()
	{
		int emptyPiles = 0;
		bool provinceFound = false;

		foreach(Deck deck in _kingdom)
		{
			if(deck.Cards.Count == 0)
			{
				emptyPiles++;
			}

			// todo: this is a bit of a hack
			if(deck.Top != null)
			{
				if(deck.Top.Type == Presets.Province)
				{
					provinceFound = true;
				}
			}
		}

		if(!provinceFound || emptyPiles >= 3)
		{
			return true;
		}
		
	return false;
	}



}