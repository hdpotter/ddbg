using System;
using System.Collections.Generic;


public class Match
{
	public int Winner => _winner;

	// redo with CachedReadOnly
	public List<Deck> Kingdom => _kingdom;

	Player _firstPlayer;
	Player _secondPlayer;
	Agent _firstAgent;
	Agent _secondAgent;

	List<Deck> _kingdom;

	int _winner = -1;
	Player _lastPlayer;

	public Match(Agent firstAgent, Agent secondAgent, MatchType matchType, Random random)
	{
		_firstPlayer = new Player(random);
		_secondPlayer = new Player(random);
		_kingdom = new List<Deck>(matchType.Kingdom);

		_firstPlayer.Setup(matchType.StartingDeck);
		_secondPlayer.Setup(matchType.StartingDeck);
		_lastPlayer = _secondPlayer;

		_firstAgent = new BigMoneyAgent(this, _firstPlayer);
		_secondAgent = new BigMoneyAgent(this, _secondPlayer);
	}

	// returns true if the match is over
	public bool NextTurn()
	{
		if(_winner < 0)
		{
			throw new Exception("attempted to take turn, but game is over");
		}

		Agent agent = _lastPlayer == _secondPlayer ? _firstAgent : _secondAgent;

		agent.Player.StartTurn();
		agent.TakeTurn(); //todo: split this into separate actions taken via a limited interface

		if(agent.Player.Phase != PlayerPhase.OFFTURN)
		{
			throw new Exception("agent did not end turn");
		}

		if(CheckForGameEnd())
		{
			CalculateWinner();
			return true;
		}
		else
		{
			return false;
		}
	}

	void CalculateWinner()
	{
		if(VictoryPointsInDeck(_firstPlayer) > VictoryPointsInDeck(_secondPlayer))
		{
			_winner = 1;
		}
		else
		{
			_winner = 2;
		}
	}

	int VictoryPointsInDeck(Player player)
	{
		int victoryPoints = 0;
		foreach(Card card in player.Draw.Cards)
		{
			victoryPoints += card.Type.VictoryPoints;
		}

		foreach(Card card in player.Discard.Cards)
		{
			victoryPoints += card.Type.VictoryPoints;
		}

		return victoryPoints;
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