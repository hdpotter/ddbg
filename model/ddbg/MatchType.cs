using System;
using System.Collections.Generic;

public class MatchType
{
	public IEnumerable<Deck> Kingdom => _kingdom;
	public IEnumerable<CardType> StartingHand => _startingHand;

	List<Deck> _kingdom;
	List<CardType> _startingHand;

	public MatchType(IEnumerable<Deck> kingdom, IEnumerable<CardType> startingHand)
	{
		_kingdom = new List<Deck>(kingdom);
		_startingHand = new List<CardType>(startingHand);
	}

}
