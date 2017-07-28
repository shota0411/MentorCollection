using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.SerializableAttribute]public class MasterDataManager 
	: SingletonMonoBehaviour<MasterDataManager>
{
	[SerializeField]
	private List<MstCharacter> characterTable = new List<MstCharacter>();
	public List<MstCharacter> CharacterTable { get { return characterTable; } }
	// キャラクターをIDで引っ張れるようにしておく
	public MstCharacter GetCharacterById(int id)
	{
		return characterTable.Find(c => c.ID == id);
	}

	// 追記
	public int GetConsumptionMoney (Character data)
	{
		return (int)(data.Master.InitialCost * Mathf.Pow(1.1f, data.Level-1));
	}

	// PublishしてゲットしたURL
	const string csvUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vTcquPRaWVFIuaR6bZdnniobzA9SanTP2Ea3DpSWf05tNpNOG2eqvVPMMiVOMoqOndc0x1ZPZJCio1b/pub?gid=0&single=true&output=csv";

	// GameManagerから呼んでもらう
	public void LoadData(UnityAction onFinish)
	{
		ConnectionManager.instance.ConnectionAPI(
			csvUrl,
			(string result) => {
				var csv = CSVReader.SplitCsvGrid(result);
				for (int i=1; i<csv.GetLength(1)-1; i++) 
				{
					var data = new MstCharacter();
					data.SetFromCSV( GetRaw(csv, i) );
					characterTable.Add(data);
				}
				onFinish();
			}
		);
	}

	private string[] GetRaw (string[,] csv, int row) {
		string[] data = new string[ csv.GetLength(0) ];
		for (int i=0; i<csv.GetLength(0); i++) {
			data[i] = csv[i, row];
		}
		return data;
	}
}