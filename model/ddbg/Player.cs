using System;
using System.Collections.Generic;

public enum PlayerPhase
{
	ACTION,
	TREASURE,
	BUY,
	OFFTURN
}


public class Player
{
	// todo: redo with view classes
	public Deck Hand => _hand;
	public Deck Draw => _draw;
	public Deck Discard => _discard;

	public int Coins => _coins;
	public int Actions => _actions;
	public int Buys => _buys;

	public PlayerPhase Phase => _phase;

	Deck _hand;
	Deck _draw;
	Deck _discard;

	int _coins;
	int _actions;
	int _buys;

	PlayerPhase _phase;

	public Player(Random random)
	{
		_hand = new Deck(random);
		_draw = new Deck(random);
		_discard = new Deck(random);

		_phase = PlayerPhase.OFFTURN;
	}

	public void GainCard(Card card)
	{
		_discard.AddCard(card);
	}

	public void DrawCards(int cards = 1)
	{
		for(int i = 0; i < cards; i++)
		{
			if(!DrawCard())
			{
				break;
			}
		}
	}

	public void AddActions(int actions)
	{
		_actions += actions;
	}

	public void AddCoins(int coins)
	{
		_coins += coins;
	}

	public void AddBuys(int buys)
	{
		_buys += buys;
	}

	bool DrawCard()
	{
		if(_draw.Cards.Count == 0)
		{
			ShuffleDiscard();
		}

		if(_draw.Cards.Count == 0)
		{
			return false;
		}
		else
		{
			_hand.AddCard(_draw.Draw());
			return true;
		}
	}

	void ShuffleDiscard()
	{
		_discard.SendAllTo(_draw);
		_draw.Shuffle();
	}

}