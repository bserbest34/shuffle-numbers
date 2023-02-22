using UnityEngine;
using DG.Tweening;
using TMPro;

public class MoneyFollowUI : MonoBehaviour
{
    Transform target;
    internal int minMoney;
    internal int maxMoney;
    TextMeshProUGUI moneyText;

    private void Start()
    {
        moneyText = GameObject.Find("Canvas").transform.Find("MoneyUI").transform.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        minMoney = FindObjectOfType<MoneyManager>().minMoney;
        maxMoney = FindObjectOfType<MoneyManager>().maxMoney;
        target = GameObject.FindGameObjectWithTag("MoneyImage").transform;

        transform.DOMove(target.position, 1f).OnComplete(() =>
        {
            int textInt = int.Parse(moneyText.text);
            moneyText.text = (textInt + Random.Range(minMoney, maxMoney)).ToString();
            PlayerPrefs.SetInt("Money", int.Parse(moneyText.text));
            Destroy(gameObject);
        });
    }
}