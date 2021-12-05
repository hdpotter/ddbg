using System;
using System.Collections.Generic;


public class Match
{
	// todo: redo with CachedReadOnly
    public List<int> Winners => _winners;
	public int CurrentRound => _currentRound;

	// todo: redo with CachedReadOnly
	public List<Deck> Kingdom => _kingdom;

	List<Agent> _agents;
	List<Deck> _kingdom;

	List<int> _winners;
	int _currentRound = 0;

	public Match(Agent firstAgent, Agent secondAgent, MatchType matchType, Random random)
	{
		_agents = new List<Agent>();
		_agents.Add(firstAgent);
		_agents.Add(secondAgent);

		foreach(Agent agent in _agents)
		{
			agent.Setup(this, new Player(random));
			agent.Player.Setup(matchType.StartingDeck);
		}

		_kingdom = new List<Deck>(matchType.Kingdom);
	}

	// returns true if the match is over
	public bool NextRound()
	{
		if(_winners != null)
		{
			throw new Exception("attempted to take turn, but game is over");
		}

		for(int i = 0; i < _agents.Count; i++)
		{
			_agents[i].Player.StartTurn();
			_agents[i].TakeTurn();

			if(_agents[i].Player.Phase != PlayerPhase.OFFTURN)
			{
				throw new Exception("agent did not end turn");
			}
		
			if(CheckForGameEnd())
			{
				CalculateWinner(i);
				return true;
			}
		}

		_currentRound++;
		return false;
	}

	// lastToPlay is the index of the player after whose turn the game ended
    void CalculateWinner(int lastToPlay)
	{
		// find the list of players tied for most points
		int mostPoints = int.MinValue;
		List<int> contenders = new List<int>();

		for(int i = 0; i < _agents.Count; i++)
		{
			int points = VictoryPointsInDeck(_agents[i].Player);
			if(points == mostPoints)
			{
				contenders.Add(i);
			}
			else if(points > mostPoints)
			{
				mostPoints = points;
				contenders.Clear();
				contenders.Add(i);
			}
		}

		// find the list of players tied for fewest turns
		int minTurns = int.MaxValue;
		_winners = new List<int>();

		foreach(int contender in contenders)
		{
			int turns = _currentRound + (lastToPlay >= contender ? 1 : 0);

			if(turns == minTurns)
			{
				_winners.Add(contender);
			}
			else if(turns < minTurns)
			{
				_winners.Clear();
				_winners.Add(contender);
			}
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