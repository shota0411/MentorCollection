using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class PopupManager
	: SingletonMonoBehaviour<PopupManager> {

	public bool isOpened {
		get {
			return popupList.Count > 0 ? true : false;
		}
	}

	// Prefabを手動で紐付け
	[SerializeField]
	private GameObject 
		commonPopup,
		descriptionPopup; // 追記

	private List<IPopupController> popupList = new List<IPopupController>();

	public void RemoveLastPopup () {
		if (popupList.Count > 0) 
			popupList.RemoveAt(popupList.Count-1);
	}

	public void OpenCommon (string message, UnityAction onCloseFinish = null) {
		var popup = CreatePopup( commonPopup );
		var popupController = popup.GetComponent<CommonPopupController>();
		popupController.SetValue( message, onCloseFinish );
		popupList.Add( popupController );
		popupController.Open(null);
	}

	// 追記
	public void OpenDescription (Character data, UnityAction onCloseFinish = null) {
		var popup = CreatePopup( descriptionPopup );
		var popupController = popup.GetComponent<DescriptionPopupController>();
		popupController.SetValue( data, onCloseFinish );
		popupList.Add( popupController );
		popupController.Open(null);
	}

	public GameObject CreatePopup (GameObject prefab) {
		GameObject popup = Instantiate( prefab );
		popup.transform.SetParentWithReset( this.transform );
		return popup;
	}
}