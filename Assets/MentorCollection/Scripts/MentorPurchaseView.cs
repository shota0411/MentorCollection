using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentorPurchaseView : MonoBehaviour
{
	[SerializeField] private GameObject mentorPurchaseCellPrefab;
	[SerializeField] private Transform scrollContent;

	public void SetCells()
	{
		var characters = MasterDataManager.instance.CharacterTable;
		characters.ForEach(c => {
			var obj = Instantiate(mentorPurchaseCellPrefab) as GameObject;
			obj.transform.SetParentWithReset(scrollContent);
			var cell = obj.GetComponent<MentorPurchaseCell>();
			cell.SetValue(c);
		});
	}
}
