using System;
using System.Collections.Generic;


public class Presets
{
	public static CardType Copper => _copper;
	public static CardType Silver => _silver;
	public static CardType Gold => _gold;
	public static CardType Platinum => _platinum;

	public static CardType Estate => _estate;
	public static CardType Duchy => _duchy;
	public static CardType Province => _province;
	public static CardType Colony => _colony;

	public static CardType Laboratory => _laboratory;
	public static CardType Smithy => _smithy;
	public static CardType Village => _village;
	public static CardType Market => _market;


	static CardType _copper = MakeTreasureType("copper", 0, 1);
	static CardType _silver = MakeTreasureType("silver", 3, 2);
	static CardType _gold = MakeTreasureType("gold", 6, 3);
	static CardType _platinum = MakeTreasureType("platinum", 9, 5);

	static CardType _estate = MakeVictoryType("estate", 2, 1);
	static CardType _duchy = MakeVictoryType("duchy", 5, 3);
	static CardType _province = MakeVictoryType("province", 8, 6);
	static CardType _colony = MakeVictoryType("colony", 11, 10);

	static CardType _laboratory = new VanillaCardType("laboratory", true, false, 5, 0, 2, 1, 0, 0);
	static CardType _smithy = new VanillaCardType("smithy", true, false, 4, 0, 3, 0, 0, 0);
	static CardType _village = new VanillaCardType("village", true, false, 3, 0, 1, 2, 0, 0);
	static CardType _market = new VanillaCardType("market", true, false, 5, 0, 1, 1, 1, 1);


	public static MatchType MakeStandardMatchType()
	{
		List<Deck> kingdom = new List<Deck>();
		kingdom.Add(MakeKingdomDeck(Copper, 46));
		kingdom.Add(MakeKingdomDeck(Silver, 30));
		kingdom.Add(MakeKingdomDeck(Gold, 30));
		kingdom.Add(MakeKingdomDeck(Estate, 8));
		kingdom.Add(MakeKingdomDeck(Duchy, 8));
		kingdom.Add(MakeKingdomDeck(Province, 8));

		List<CardType> startingHand = new List<CardType>();
		startingHand.Add(Estate);
		startingHand.Add(Estate);
		startingHand.Add(Estate);
		startingHand.Add(Copper);
		startingHand.Add(Copper);
		startingHand.Add(Copper);
		startingHand.Add(Copper);
		startingHand.Add(Copper);
		startingHand.Add(Copper);
		startingHand.Add(Copper);

		return new MatchType(kingdom, startingHand);
	}



	// string name,
	// bool isAction,
	// bool isTreasure,
	// int cost,
	// int victoryPoints,
	// int cards,
	// int actions,
	// int coins,
	// int buys

	static CardType MakeTreasureType(string name, int cost, int coins)
	{
		return new VanillaCardType(name, false, true, cost, 0, 0, 0, coins, 0);
	}

	static CardType MakeVictoryType(string name, int cost, int victoryPoints)
	{
		return new VanillaCardType(name, false, false, cost, victoryPoints, 0, 0, 0, 0);
	}

	static Deck MakeKingdomDeck(CardType type, int numCards = 10)
	{
		// todo: think about how to propagate random here
		Deck deck = new Deck(null);

		for(int i = 0; i < numCards; i++)
		{
			deck.Add(new Card(type));
		}

		return deck;
	}
}