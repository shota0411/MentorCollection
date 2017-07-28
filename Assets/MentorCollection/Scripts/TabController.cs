using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour {

	[SerializeField]
	private Button recluitButton, trainingButton, closeButton;

	private void Start ()
	{
		recluitButton.onClick.AddListener(() => {
			PortrateUIManager.instance.ChangeView(Const.View.Purchase);
		});
		trainingButton.onClick.AddListener(() => {
			PortrateUIManager.instance.ChangeView(Const.View.Training);
		});
		closeButton.onClick.AddListener(() => {
			PortrateUIManager.instance.ChangeView(Const.View.Close);
		});
	}

}