using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// 追記
using UniRx;

public class PortrateUIManager
: SingletonMonoBehaviour<PortrateUIManager>
{
    [SerializeField] private MentorPurchaseView mentorPurchaseView;
    [SerializeField] private MentorTrainingView mentorTrainingView;
    public MentorPurchaseView MentorPurchaseView { get { return mentorPurchaseView; } }
    public MentorTrainingView MentorTrainingView { get { return mentorTrainingView; } }

    [SerializeField] private Button workButton, dataClearButton;
    [SerializeField] private Text 
        moneyLabel,
        autoWorkLabel,
        employeesCountLabel,
        productivityLabel;

    [SerializeField] private Const.View 
        currentView = Const.View.Close, 
        lastView = Const.View.Purchase;


    private static User User { get { return GameManager.instance.User; } }

    public void Setup ()
    {
        mentorPurchaseView.SetCells();
        mentorTrainingView.SetCells();

        UpdateView();

        workButton.onClick.AddListener(() =>
        {
            var power = User.Characters.Sum(c => c.Power);
            if (power == 0) power = 1;
            User.AddMoney(power);
            UpdateView();
        });

        dataClearButton.onClick.AddListener(() => 
        {
            PlayerPrefs.DeleteAll();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        });

        // 追記
        User.Money.Subscribe(_ => { UpdateView(); });
    }

    public void UpdateView()
    {
        moneyLabel.text = string.Format("¥{0:#,0}", User.Money.Value);
        employeesCountLabel.text = string.Format("{0:#,0}人", User.Characters.Count);
        productivityLabel.text = string.Format("¥{0:#,0}", User.ProductivityPerTap);
    }

    public void ChangeView (Const.View nextView) 
    {
        if (currentView == nextView) return;
        lastView = currentView;
        currentView = nextView;
        isMoving = true;
        switch (nextView)
        {
            case Const.View.Purchase:
                mentorPurchaseView.gameObject.SetActive(true);
                mentorTrainingView.gameObject.SetActive(false);
                break;
            case Const.View.Training:
                mentorPurchaseView.gameObject.SetActive(false);
                mentorTrainingView.gameObject.SetActive(true);
                break;
            case Const.View.Close:
                break;
        }
    }

    [SerializeField] private Button openButton;
    [SerializeField] private Transform 
        mainPanel, 
        openPoint, 
        closePoint;
    private bool isMoving = false;
    private float 
        easing = 8f,
        maxSpeed = 3f,
        stopDistance = 0.001f;

    private void Update () {
        if (!isMoving) return;
        var target = (currentView == Const.View.Close) ? closePoint : openPoint;

        // position
        Vector3 v = Vector3.Lerp(
            mainPanel.position, 
            target.position, 
            Time.deltaTime * easing) - mainPanel.position;
        if (v.magnitude > maxSpeed) v = v.normalized * maxSpeed;
        mainPanel.position += v;

        // rotation
        mainPanel.rotation = Quaternion.Lerp(
            transform.rotation,
            target.rotation,
            Time.deltaTime * easing);

        if (isMoving) {
            float distance = Vector3.Distance(mainPanel.position, target.position);
            if (stopDistance > distance) isMoving = false;
        }
    }
}