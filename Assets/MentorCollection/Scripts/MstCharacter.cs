using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

[System.SerializableAttribute]
public class MstCharacter
{
	[SerializeField]
	private int
		id,
		rarity,
		maxLevel,
		growthType,
		lowerEnergy,
		upperEnergy,
		initialCost;

	[SerializeField]
	private string
		name,
		imageId,
		flavorText;

	public void SetFromCSV(string[] data)
	{
		id          = int.Parse( data[0] );
		name        = data[1];
		imageId     = data[2];
		flavorText  = data[3];
		rarity      = int.Parse( data[4] );
		maxLevel    = int.Parse( data[5] );
		growthType  = int.Parse( data[6] );
		lowerEnergy = int.Parse( data[7] );
		upperEnergy = int.Parse( data[8] );
		initialCost = int.Parse( data[9] );
	}

	public int ID
	{
		get { return id; }
	}

	public int Rarity
	{
		get { return rarity; }
	}

	public int MaxLevel
	{
		get { return maxLevel; }
	}

	public int GrowthType
	{
		get { return growthType; }
	}

	public int LowerEnergy
	{
		get { return lowerEnergy; }
	}

	public int UpperEnergy
	{
		get { return upperEnergy; }
	}

	public int InitialCost
	{
		get { return initialCost; }
	}

	public string Name
	{
		get { return name; }
	}

	public string ImageId
	{
		get { return imageId; }
	}

	public string FlavorText
	{
		get { return flavorText; }
	}

//	public bool PurchaseAvailable(int currentMoney)
//	{
//		return (currentMoney < InitialCost) ? false : true;
//	}
}
