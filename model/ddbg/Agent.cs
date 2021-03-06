using System;
using System.Collections.Generic;


public abstract class Agent
{
	public Player Player => _player;

	protected Match _match;
	protected Player _player;

	public void Setup(Match match, Player player)
	{
		_match = match;
		_player = player;
	}


	public abstract void TakeTurn();
}

public class BigMoneyAgent : Agent
{
	List<Deck> _decks;

	public BigMoneyAgent() : base()
	{
	}

	public void SetupDecks()
	{
		Deck copper = null;
		Deck silver = null;
		Deck gold = null;
		Deck province = null;

		foreach(Deck deck in _match.Kingdom)
		{
			if(deck.Top.Type == Presets.Copper)
			{
				copper = deck;
			}
			else if(deck.Top.Type == Presets.Silver)
			{
				silver = deck;
			}
			else if(deck.Top.Type == Presets.Gold)
			{
				gold = deck;
			}
			else if(deck.Top.Type == Presets.Province)
			{
				province = deck;
			}
		}

		_decks = new List<Deck>();
		_decks.Add(province);
		_decks.Add(gold);
		_decks.Add(silver);
		_decks.Add(copper);
	}

	public override void TakeTurn()
	{
		if(_decks == null)
		{
			SetupDecks();
		}

		// end action phase
		_player.EndActions();

		// play treasures
		bool treasureFound = true;
		while(treasureFound)
		{
			treasureFound = false;
			Card treasure = null;
			foreach (Card card in _player.Hand.Cards)
			{
				if (card.Type.IsTreasure)
				{
					treasureFound = true;
					treasure = card;
					break;
				}
			}
			if(treasureFound)
            {
				_player.PlayTreasure(treasure);
            }
		}

		_player.EndTreasures();

		// buy cards
		foreach (Deck deck in _decks)
		{
			if (deck.Cards.Count > 0)
			{
				if (deck.Top.Type.Cost <= _player.Coins)
				{
					_player.BuyCard(deck);
					break;
				}
			}
		}

		_player.EndTurn();
	}

}

