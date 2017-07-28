using UnityEngine;

[System.SerializableAttribute]
public class Character 
{
	[SerializeField] private int uid, masterId, level;

	public int UniqueID { get { return uid; } }
	public int MasterId { get { return masterId; } }
	public int Level { get { return level; } }

	public MstCharacter Master
	{
		get
		{
			return MasterDataManager.instance.GetCharacterById(masterId);
		}
	}

	public int Power 
	{
		get 
		{
			int power = 
				Master.LowerEnergy 
				+ ( 
					(level - 1) 
					* (Master.UpperEnergy - Master.LowerEnergy) 
					/ (Master.MaxLevel - 1) 
				);
			return power;
		}
	}

	public Character (int uniqueId, MstCharacter data)
	{
		uid = uniqueId;
		level = 1; 
		masterId = data.ID;
	}


	// 追記部分
	public void LevelUp ()
	{
		level += 1;
	}
	
	public bool IsLevelMax 
	{ 
		get
		{  
			return (level >= Master.MaxLevel) ? true : false;
		} 
	}
}