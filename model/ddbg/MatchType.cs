using System;
using System.Collections.Generic;

public class MatchType
{
	public IEnumerable<Deck> Kingdom => _kingdom;
	public IEnumerable<CardType> StartingDeck => _startingDeck;

	List<Deck> _kingdom;
	List<CardType> _startingDeck;

	public MatchType(IEnumerable<Deck> kingdom, IEnumerable<CardType> startingDeck)
	{
		_kingdom = new List<Deck>(kingdom);
		_startingDeck = new List<CardType>(startingDeck);
	}

}
