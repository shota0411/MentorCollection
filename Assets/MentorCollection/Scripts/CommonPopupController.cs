using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CommonPopupController : BasePopupController
{

	[SerializeField] private Text contentLabel;

	[SerializeField] private Button closeButton;

	private UnityAction onCloseFinish;

	private void Start () {
		closeButton.onClick.AddListener(() => {
			if (onCloseFinish != null) Close( onCloseFinish );
			else Close( null );
		});
	}

	public void SetValue (string text, UnityAction OnCloseFinish = null) {
		contentLabel.text = text;
		if (OnCloseFinish != null) onCloseFinish = OnCloseFinish;
	}
}
