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
	public Deck Play => _play;

	public int Coins => _coins;
	public int Actions => _actions;
	public int Buys => _buys;

	public PlayerPhase Phase => _phase;

	Deck _hand;
	Deck _draw;
	Deck _discard;
	Deck _play;

	int _coins;
	int _actions;
	int _buys;

	PlayerPhase _phase;

	public Player(Random random)
	{
		_hand = new Deck(random);
		_draw = new Deck(random);
		_discard = new Deck(random);
		_play = new Deck(random);

		_phase = PlayerPhase.OFFTURN;
	}

	// ================================================================
	// round-based effects
	// ================================================================
	public void Setup(IEnumerable<CardType> startingDeck)
	{
		_phase = PlayerPhase.OFFTURN;

		foreach(CardType type in startingDeck)
		{
			_draw.Add(new Card(type));
		}

		_draw.Shuffle();
		DrawCards(5);
	}

	public void StartTurn()
	{
		_phase = PlayerPhase.ACTION;

		_actions = 0;
		_coins = 0;
		_buys = 0;
	}

	public void PlayAction(Card card)
	{
		if(!(_phase == PlayerPhase.ACTION))
		{
			throw new Exception("attempted PlayAction not during action phase");
		}

		if(!card.Type.IsAction)
		{
			throw new Exception("attempted PlayAction on card that is not an action");
		}

		if(_actions < 1)
		{
			throw new Exception("attempted PlayAction when no actions are available");
		}

		_actions--;

		PlayCard(card);
	}

	public void PlayTreasure(Card card)
	{
		if(!(_phase == PlayerPhase.TREASURE))
		{
			throw new Exception("attempted PlayTreasure not during treasure phase");
		}

		if(!card.Type.IsTreasure)
		{
			throw new Exception("attempted PlayTreasure on card that is not a treasure");
		}

		PlayCard(card);
	}

	public void BuyCard(Deck deck)
	{
		if(deck.Top == null)
		{
			throw new Exception("attempted to buy card from empty deck");
		}

		if(_buys < 1)
		{
			throw new Exception("attempted to buy card, but no buys available");
		}

		if(deck.Top.Type.Cost > _coins)
		{
			throw new Exception("attempted to buy card " + deck.Top.Type.Name + ", which costs " +
				deck.Top.Type.Cost.ToString() + ", but only " + _coins + " + coins available");
		}

		_buys--;
		_coins -= deck.Top.Type.Cost;
		GainCard(deck.Draw());
	}

	public void EndActions()
	{
		if(!(_phase == PlayerPhase.ACTION))
		{
			throw new Exception("attempted to end action phase when not in action phase");
		}

		_phase = PlayerPhase.TREASURE;
	}

	public void EndTreasures()
	{
		if(!(_phase == PlayerPhase.TREASURE))
		{
			throw new Exception("attempted to end treasure phase when not in treasure phase");
		}

		_phase = PlayerPhase.BUY;
	}

	public void EndTurn()
	{
		if(_phase != PlayerPhase.BUY)
		{
			throw new Exception("attempted to end turn when not in buy phase");
		}

		_hand.SendAllTo(_discard);
		_play.SendAllTo(_discard);

		DrawCards(5);

		_phase = PlayerPhase.OFFTURN;
	}


	// ================================================================
	// 
	// ================================================================
	public void GainCard(Card card)
	{
		_discard.Add(card);
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
			_hand.Add(_draw.Draw());
			return true;
		}
	}

	void ShuffleDiscard()
	{
		_discard.SendAllTo(_draw);
		_draw.Shuffle();
	}

	void PlayCard(Card card)
	{
		if(!_hand.Contains(card))
		{
			throw new Exception("attempted to play card " + card.Type.Name + " that is not in hand");
		}

		card.Type.Play(this);
		_hand.Remove(card);
		_play.Add(card);

	}
}