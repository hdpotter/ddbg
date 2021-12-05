using System;
using System.Collections.Generic;


public class Deck
{
	// todo: remake this with CachedReadOnly
	public List<Card> Cards => _cards;
	public Card Top => _cards.Count > 0 ? _cards[_cards.Count - 1] : null;

	List<Card> _cards;
	Random _random;

	public Deck(Random random)
	{
		_cards = new List<Card>();
		_random = random;
	}

	public void Shuffle()
	{
		// knuth shuffle
		for(int i = 0; i < _cards.Count; i++)
		{
			int indexToSwap = _random.Next(_cards.Count);

			Card swap = _cards[indexToSwap];
			_cards[indexToSwap] = _cards[i];
			_cards[i] = swap;
		}
	}

	// todo: consider having deck extend List<Card>
    public void Add(Card card)
	{
		_cards.Add(card);
	}

	public void Remove(Card card)
	{
		_cards.Remove(card);
	}

	public bool Contains(Card card)
    {
		return _cards.Contains(card);
    }

	public void SendAllTo(Deck target)
	{
		target._cards.AddRange(_cards);
		_cards.Clear();
	}

	public Card Draw()
	{
		Card card = _cards[Cards.Count - 1];
		_cards.RemoveAt(Cards.Count - 1);
		return card;
	}
}