using System;
using System.Collections.Generic;


public class Card
{
	public CardType Type => _type;

	CardType _type;

	public Card(CardType type)
	{
		_type = type;
	}

	public override string ToString()
	{
		return _type.Name;
	}
}