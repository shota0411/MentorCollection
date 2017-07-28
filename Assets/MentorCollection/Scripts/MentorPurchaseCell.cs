using UnityEngine;
using UnityEngine.UI;
using UniRx;


public class MentorPurchaseCell : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Text
        nameLabel,
        rarityLabel,
        flavorTextLabel,
        productivityLabel,
        costLabel;

    [SerializeField] private Button purchaseButton;
    [SerializeField] private CanvasGroup buttonGroup;

    private bool isSold = false;
    private MstCharacter characterData;

    public void SetValue(MstCharacter data)
    {
        iconImage.sprite = Resources.Load<Sprite>("Face/" + data.ImageId);
        characterData = data;
        nameLabel.text = data.Name;
        rarityLabel.text = "";
        for (int i = 0; i < data.Rarity; i++) { rarityLabel.text += "★"; }
        flavorTextLabel.text = data.FlavorText;
        productivityLabel.text = "生産性(lv.1) : " + data.LowerEnergy;
        costLabel.text = string.Format("¥{0:#,0}", data.InitialCost);

        var user = GameManager.instance.User;
        var ch = user.Characters.Find(c => c.MasterId == data.ID);
        isSold = (ch == null) ? false : true;
        if (isSold) SoldView();
        if (!characterData.PurchaseAvailable(user.Money.Value)) buttonGroup.alpha = 0.5f;
        purchaseButton.onClick.AddListener(() =>
        {
            if (isSold) return;
            if (!characterData.PurchaseAvailable(user.Money.Value)) return;
            isSold = true;
            SoldView();
            var chara = user.NewCharacter(characterData);
            PortrateUIManager.instance.MentorTrainingView.AddCharacter(chara);

            // 追記
            PopupManager.instance.OpenCommon(characterData.Name+"が入社しました！");
        });

        user.Money.Subscribe(value => {
            if (isSold) return;
            if (value < data.InitialCost) buttonGroup.alpha = 0.5f;
            else buttonGroup.alpha = 1.0f;
        });
    }

    private void SoldView()
    {
        buttonGroup.alpha = 0.5f;
        costLabel.text = "sold out";
    }
}