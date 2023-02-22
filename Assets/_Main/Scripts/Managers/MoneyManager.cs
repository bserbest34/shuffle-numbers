using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public GameObject instantiatedMoney;
    public int minMoney;
    public int maxMoney;
    Vector3 beginMoneyPoint;

    private void Start()
    {
        GameObject.Find("Canvas").transform.Find("MoneyUI").transform.Find("MoneyText").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Money").ToString();
        beginMoneyPoint = transform.Find("MoneyUI").transform.Find("MoneyBeginPoint").transform.position;
    }

    public void InstantiateMoney(int count)
    {
        StartCoroutine(SetMoneyToUI(count, minMoney, maxMoney));
    }

    private IEnumerator SetMoneyToUI(int count, int minMoney, int maxMoney)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(instantiatedMoney, beginMoneyPoint, Quaternion.identity, GameObject.Find("Canvas").transform);
            instantiatedMoney.GetComponent<MoneyFollowUI>().minMoney = minMoney;
            instantiatedMoney.GetComponent<MoneyFollowUI>().maxMoney = maxMoney;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
