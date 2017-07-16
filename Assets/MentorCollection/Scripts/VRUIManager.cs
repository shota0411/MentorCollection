using UnityEngine;
using UnityEngine.UI;

public class VRUIManager : MonoBehaviour {

	[SerializeField]
	private Button toPortrateButton;

	void Start () {
		toPortrateButton.onClick.AddListener(() => {
			DeviceManager.instance.ToPortrate();
		});
	}

}