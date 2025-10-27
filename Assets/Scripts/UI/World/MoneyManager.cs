using UnityEngine;

using TMPro;
using Framework;


public class MoneyManager : MonoBehaviour
{
    [SerializeField] private float timeLeft = 300f;

    private TextMeshProUGUI _text;
    private int _moneyPerSecond = 15;

    public int moneyAmount;
    public int timerExtra;
    public int deliveredPluchies;
    public int totalPluchieAmount = 2;

    public static MoneyManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        _text = scoreObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        timerExtra = Mathf.RoundToInt(timeLeft) * _moneyPerSecond;

        if (deliveredPluchies == totalPluchieAmount || timeLeft <= 0)
        {
            //SceneSwitcher.Instance.SetAndLoadScene("FinalScoreScene");
           _text.text = $"${moneyAmount+timerExtra}";
        }
        else
            _text.text = $"${moneyAmount}";
    }
}
