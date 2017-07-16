using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentorTrainingView : MonoBehaviour
{
	[SerializeField] private GameObject mentorTrainingCellPrefab;
	[SerializeField] private Transform scrollContent;
	private List<MentorTrainingCell> cells = new List<MentorTrainingCell>();

	private void Start ()
	{
		gameObject.SetActive(false);
	}

	// ゲーム開始時に保存してるデータが持ってるCharacterのCellを全部生成する用
	public void SetCells()
	{
		var characters = GameManager.instance.User.Characters;
		characters.ForEach(c => { CreateCell(c); });
	}

	// ゲーム中にメンターの購入がなされたときに一つCellを追加する用
	public void AddCharacter(Character data)
	{
		CreateCell(data);
	}

	private void CreateCell(Character data)
	{
		var obj = Instantiate(mentorTrainingCellPrefab) as GameObject;
		obj.transform.SetParentWithReset(scrollContent);
		var cell = obj.GetComponent<MentorTrainingCell>();
		cell.SetValue(data);
		cells.Add(cell);
	}
}