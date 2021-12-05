using System;
using System.Collections.Generic;


public abstract class CardType
{
	public string Name => _name;
	public bool IsAction => _isAction;
	public bool IsTreasure => _isTreasure;
	public int VictoryPoints => _victoryPoints;
	public int Cost => _cost;

	string _name;
	bool _isAction;
	bool _isTreasure;
	int _cost;
	int _victoryPoints;

	public abstract void Play(Player player);

	public CardType(string name, bool isAction, bool isTreasure, int cost, int victoryPoints)
	{
		_isAction = isAction;
		_isTreasure = isTreasure;
		_name = name;
		_cost = cost;
		_victoryPoints = victoryPoints;
	}
}

public class VanillaCardType : CardType
{
	public int Cards => _cards;
	public int Actions => _actions;
	public int Coins => _coins;
	public int Buys => _buys;

	int _cards;
	int _actions;
	int _coins;
	int _buys;

	public VanillaCardType(
		string name,
		bool isAction,
		bool isTreasure,
		int cost,
		int victoryPoints,
		int cards,
		int actions,
		int coins,
		int buys
	) : base(name, isAction, isTreasure, cost, victoryPoints)
	{
		_cards = cards;
		_actions = actions;
		_coins = coins;
		_buys = buys;
	}

	public override void Play(Player player)
	{
		player.DrawCards(_cards);
		player.AddActions(_actions);
		player.AddCoins(_coins);
		player.AddBuys(_buys);
	}
}