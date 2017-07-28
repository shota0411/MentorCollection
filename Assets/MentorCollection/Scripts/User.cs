using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

[Serializable]
public class User
{
	[SerializeField] private IntReactiveProperty money;
	[SerializeField] private List<Character> characters;

	public ReadOnlyReactiveProperty<int> Money
	{
		get { return money.ToReadOnlyReactiveProperty(); }
	}

	public List<Character> Characters
	{
		get { return characters ?? (characters = new List<Character>()); }
	}

	public int ProductivityPerTap
	{
		get 
		{ 
			int sum = characters.Sum(c => c.Power);
			return (sum == 0) ? 1 : sum; 
		}
	}

	// moneyに売り上げを加算
	public void AddMoney(int cost)
	{
		money.Value += cost;
	}

	// 追記
	// LevelUp時にコストを消費するところ
	public void ConsumptionLevelUpCost(int cost)
	{
		money.Value -= cost;
	}

	public Character NewCharacter(MstCharacter data)
	{
		var uniqueId = (Characters.Count == 0) ? 1 : characters[characters.Count - 1].UniqueID + 1;
		var chara = new Character(uniqueId, data);
		characters.Add(chara);
		money.Value -= data.InitialCost;
		return chara;
	}
}