using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class DescriptionPopupController : BasePopupController
{

	[SerializeField] private Text
		nameLabel,
		flavorTextLabel,
		rarityLabel,
		levelLabel,
		productivityLabel;

	[SerializeField] 
	private Button closeButton;

	private UnityAction onCloseFinish;

	private void Start () 
	{
		closeButton.onClick.AddListener(() => {
			if (onCloseFinish != null) Close( onCloseFinish );
			else Close( null );
		});
	}

	public void SetValue (Character data, UnityAction OnCloseFinish = null) 
	{
		var master = data.Master;
		nameLabel.text = master.Name;
		flavorTextLabel.text = master.FlavorText;
		productivityLabel.text = string.Format("生産性 : ¥ {0:#,0} /tap", data.Power);
		levelLabel.text = "Lv." + data.Level;
		rarityLabel.text = "";
		for (var i = 0; i < master.Rarity; i++) { rarityLabel.text += "★"; }
		if (OnCloseFinish != null) onCloseFinish = OnCloseFinish;
	}
}