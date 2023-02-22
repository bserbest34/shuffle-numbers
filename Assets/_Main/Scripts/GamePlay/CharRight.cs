using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Dlite.Games.Managers;

public class CharRight : MonoBehaviour
{
    NumberManager manager;
    int numberPositive;
    int numberNegative;
    public bool finish;
    public GameObject prefabMoney;
    void Start()
    {
        finish = false;

        manager = FindObjectOfType<NumberManager>();
        manager.SetNumber(gameObject, "1"); 
        if (PlayerPrefs.GetInt("Ring3") == 1)
        {
            transform.parent.Find("HandRight").Find("Armature").Find("Bone").Find("Bone.017").Find("Bone.018").Find("Ring3").gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Ring5") == 1)
        {
            transform.parent.Find("HandRight").Find("Armature").Find("Bone.001").Find("Bone.005").Find("Bone.006").Find("Ring5").gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Ring4") == 1)
        {
            transform.parent.Find("HandRight").Find("Armature").Find("Bone.003").Find("Bone.011").Find("Bone.012").Find("Ring4").gameObject.SetActive(true);
        }
    }
    void Update()
    {
        if (!finish)
            return;
        foreach (var item in FindObjectOfType<FinishMoneyObjects>().moneyList)
        {
            if(item.transform.position.y + 2f < transform.position.y)
            {
                item.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            other.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.transform.DOMoveY(-14f, 1);
            if (other.transform.gameObject.GetComponent<TransformDoorManager>().type == TransformDoorManager.TransformType.Shuffle)
            {
                HapticManager.Haptic(Dlite.Games.HapticType.Warning);
                manager.ShuffleNumber(gameObject);
                transform.Find("StarVFX").GetComponent<ParticleSystem>().Play();
            }
            if (other.transform.gameObject.GetComponent<TransformDoorManager>().type == TransformDoorManager.TransformType.Positive)
            {
                HapticManager.Haptic(Dlite.Games.HapticType.Success);
                var numberPositive = int.Parse(other.transform.Find("Positive").Find("Positive").GetComponent<TextMeshPro>().text.Remove(0, 1));

                if (FindObjectOfType<CharLeft>().birlesti)
                {
                    manager.AddNumber(gameObject, numberPositive, true);
                }
                else
                {
                    manager.AddNumber(gameObject, numberPositive);
                }
            }
            if (other.transform.gameObject.GetComponent<TransformDoorManager>().type == TransformDoorManager.TransformType.Negative)
            {
                HapticManager.Haptic(Dlite.Games.HapticType.Failure);
                var numberNegative = int.Parse(other.transform.Find("Negative").Find("Negative").GetComponent<TextMeshPro>().text.Remove(0, 1));

                if (FindObjectOfType<CharLeft>().birlesti)
                {
                    manager.SubNumber(gameObject, numberNegative, true);
                }
                else
                {
                    manager.SubNumber(gameObject, numberNegative);
                }
            }
            if (other.transform.gameObject.GetComponent<TransformDoorManager>().type == TransformDoorManager.TransformType.Multiple)
            {
                HapticManager.Haptic(Dlite.Games.HapticType.Success);
                var numberMultiple = int.Parse(other.transform.Find("Multiple").Find("Multiple").GetComponent<TextMeshPro>().text.Remove(0, 1));
                if (FindObjectOfType<CharLeft>().birlesti)
                {
                    manager.MultipleNumber(gameObject, numberMultiple, true);
                }
                else
                {
                    manager.MultipleNumber(gameObject, numberMultiple);
                }
            }
            if (other.transform.gameObject.GetComponent<TransformDoorManager>().type == TransformDoorManager.TransformType.Div)
            {
                HapticManager.Haptic(Dlite.Games.HapticType.Failure);
                var numberDiv = int.Parse(other.transform.Find("Div").Find("Div").GetComponent<TextMeshPro>().text.Remove(0, 1));
                if (FindObjectOfType<CharLeft>().birlesti)
                {
                    manager.DivNumber(gameObject, numberDiv, true);
                }
                else
                {
                    manager.DivNumber(gameObject, numberDiv);
                }
            }
        }
        if (other.CompareTag("Birlesik"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.SoftImpact);
            transform.parent.DOMoveX(10f, 1f);
            if (manager.left.name == "PlayerLeft")
            {
                transform.DOLocalMoveX(manager.GetMergePositionXForRight(), 1);
            }else
            {
                transform.DOLocalMoveX(manager.GetMergePositionXForLeft(), 1);
            }
        }
        if (other.CompareTag("Ring3"))
        {
            PlayerPrefs.SetInt("Ring3", 1);
            if(PlayerPrefs.GetInt("Ring3") == 1)
            {
                transform.parent.Find("HandRight").Find("Armature").Find("Bone").Find("Bone.017").Find("Bone.018").Find("Ring3").gameObject.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Ring5"))
        {
            PlayerPrefs.SetInt("Ring5", 1);
            if(PlayerPrefs.GetInt("Ring5") == 1)
            {
                transform.parent.Find("HandRight").Find("Armature").Find("Bone.001").Find("Bone.005").Find("Bone.006").Find("Ring5").gameObject.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Ring4"))
        {
            PlayerPrefs.SetInt("Ring4", 1);
            if (PlayerPrefs.GetInt("Ring4") == 1)
            {
                transform.parent.Find("HandRight").Find("Armature").Find("Bone.003").Find("Bone.011").Find("Bone.012").Find("Ring4").gameObject.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
        Finish(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Birlesik"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.SoftImpact);
            transform.parent.DOMoveX(15.8f, 1f);
            if (FindObjectOfType<NumberManager>().degisti == true)
            {
                transform.DOLocalMoveX(-0.68f, 1);
            }
            else
            {
                transform.DOLocalMoveX(0, 1);
            }

            manager.SetNumber(gameObject,manager.ReadNumber(gameObject).ToString());
        }
    }
    

    void Finish(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.Success);
            other.GetComponent<BoxCollider>().enabled = false;
            finish = true;
            GameObject.Find("CM vcam1").transform.gameObject.SetActive(false);
            transform.root.GetComponent<CharMove>().speedInZAxis = 0;
            GameObject.Find("PlayerLeft").GetComponent<CharMove>().speedInZAxis = 0;
            GameObject.Find("CameraFollow").GetComponent<CharMove>().speedInZAxis = 0;
            transform.root.GetComponent<CharMove>().speedInYAxis = 0.262f;
            GameObject.Find("PlayerLeft").GetComponent<CharMove>().speedInYAxis = 0.262f;
            GameObject.Find("CameraFollow").GetComponent<CharMove>().speedInYAxis = 0.262f;

        }
        if (other.CompareTag("100"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.HeavyImpact);
            other.GetComponent<BoxCollider>().enabled = false;
            if (manager.ReadAllNumber() > 100)
            {
                FindObjectOfType<MoneyManager>().InstantiateMoney(2);
                manager.SubNumber(gameObject, 100, true);
                other.transform.Find("ConfettiVFX").GetComponent<ParticleSystem>().Play();
            }
            else
            {
                FindObjectOfType<UIManager>().SuccesGame();
                transform.root.GetComponent<CharMove>().enabled = false;
                GameObject.Find("PlayerLeft").GetComponent<CharMove>().enabled = false;
                GameObject.Find("CameraFollow").GetComponent<CharMove>().enabled = false;
            }
        }
        if (other.CompareTag("200"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.HeavyImpact);
            other.GetComponent<BoxCollider>().enabled = false;
            if (manager.ReadAllNumber() > 200)
            {
                FindObjectOfType<MoneyManager>().InstantiateMoney(2);
                manager.SubNumber(gameObject, 200, true);
                other.transform.Find("ConfettiVFX").GetComponent<ParticleSystem>().Play();
            }
            else
            {
                FindObjectOfType<UIManager>().SuccesGame();
                transform.root.GetComponent<CharMove>().enabled = false;
                GameObject.Find("PlayerLeft").GetComponent<CharMove>().enabled = false;
                GameObject.Find("CameraFollow").GetComponent<CharMove>().enabled = false;
            }
        }
        if (other.CompareTag("300"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.HeavyImpact);
            other.GetComponent<BoxCollider>().enabled = false;
            if (manager.ReadAllNumber() > 500)
            {
                FindObjectOfType<MoneyManager>().InstantiateMoney(2);
                manager.SubNumber(gameObject, 500, true);
                other.transform.Find("ConfettiVFX").GetComponent<ParticleSystem>().Play();
            }
            else
            {
                FindObjectOfType<UIManager>().SuccesGame();
                transform.root.GetComponent<CharMove>().enabled = false;
                GameObject.Find("PlayerLeft").GetComponent<CharMove>().enabled = false;
                GameObject.Find("CameraFollow").GetComponent<CharMove>().enabled = false;
            }
        }
        if (other.CompareTag("400"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.HeavyImpact);
            other.GetComponent<BoxCollider>().enabled = false;
            if (manager.ReadAllNumber() > 1000)
            {
                FindObjectOfType<MoneyManager>().InstantiateMoney(2);
                manager.SubNumber(gameObject, 1000, true);
                other.transform.Find("ConfettiVFX").GetComponent<ParticleSystem>().Play();
            }
            else
            {
                FindObjectOfType<UIManager>().SuccesGame();
                transform.root.GetComponent<CharMove>().enabled = false;
                GameObject.Find("PlayerLeft").GetComponent<CharMove>().enabled = false;
                GameObject.Find("CameraFollow").GetComponent<CharMove>().enabled = false;
            }
        }
        if (other.CompareTag("500"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.HeavyImpact);
            other.GetComponent<BoxCollider>().enabled = false;
            if (manager.ReadAllNumber() > 5000)
            {
                FindObjectOfType<MoneyManager>().InstantiateMoney(2);
                manager.SubNumber(gameObject, 5000, true);
                other.transform.Find("ConfettiVFX").GetComponent<ParticleSystem>().Play();
            }
            else
            {
                FindObjectOfType<UIManager>().SuccesGame();
                transform.root.GetComponent<CharMove>().enabled = false;
                GameObject.Find("PlayerLeft").GetComponent<CharMove>().enabled = false;
                GameObject.Find("CameraFollow").GetComponent<CharMove>().enabled = false;
            }
        }
        if (other.CompareTag("600"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.HeavyImpact);
            other.GetComponent<BoxCollider>().enabled = false;
            if (manager.ReadAllNumber() > 10000)
            {
                FindObjectOfType<MoneyManager>().InstantiateMoney(2);
                manager.SubNumber(gameObject, 10000, true);
                other.transform.Find("ConfettiVFX").GetComponent<ParticleSystem>().Play();
            }
            else
            {
                FindObjectOfType<UIManager>().SuccesGame();
                transform.root.GetComponent<CharMove>().enabled = false;
                GameObject.Find("PlayerLeft").GetComponent<CharMove>().enabled = false;
                GameObject.Find("CameraFollow").GetComponent<CharMove>().enabled = false;
            }
        }
        if (other.CompareTag("700"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.HeavyImpact);
            other.GetComponent<BoxCollider>().enabled = false;
            if (manager.ReadAllNumber() > 20000)
            {
                FindObjectOfType<MoneyManager>().InstantiateMoney(2);
                manager.SubNumber(gameObject, 20000, true);
                other.transform.Find("ConfettiVFX").GetComponent<ParticleSystem>().Play();
            }
            else
            {
                FindObjectOfType<UIManager>().SuccesGame();
                transform.root.GetComponent<CharMove>().enabled = false;
                GameObject.Find("PlayerLeft").GetComponent<CharMove>().enabled = false;
                GameObject.Find("CameraFollow").GetComponent<CharMove>().enabled = false;
            }
        }
        if (other.CompareTag("800"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.HeavyImpact);
            other.GetComponent<BoxCollider>().enabled = false;
            if (manager.ReadAllNumber() > 50000)
            {
                FindObjectOfType<MoneyManager>().InstantiateMoney(2);
                manager.SubNumber(gameObject, 50000, true);
                other.transform.Find("ConfettiVFX").GetComponent<ParticleSystem>().Play();
            }
            else
            {
                FindObjectOfType<UIManager>().SuccesGame();
                transform.root.GetComponent<CharMove>().enabled = false;
                GameObject.Find("PlayerLeft").GetComponent<CharMove>().enabled = false;
                GameObject.Find("CameraFollow").GetComponent<CharMove>().enabled = false;
            }
        }
        if (other.CompareTag("900"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.HeavyImpact);
            other.GetComponent<BoxCollider>().enabled = false;
            if (manager.ReadAllNumber() > 75000)
            {
                FindObjectOfType<MoneyManager>().InstantiateMoney(2);
                manager.SubNumber(gameObject, 75000, true);
                other.transform.Find("ConfettiVFX").GetComponent<ParticleSystem>().Play();
            }
            else
            {
                FindObjectOfType<UIManager>().SuccesGame();
                transform.root.GetComponent<CharMove>().enabled = false;
                GameObject.Find("PlayerLeft").GetComponent<CharMove>().enabled = false;
                GameObject.Find("CameraFollow").GetComponent<CharMove>().enabled = false;
            }
        }
        if (other.CompareTag("1000"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.HeavyImpact);
            other.GetComponent<BoxCollider>().enabled = false;
            if (manager.ReadAllNumber() > 100000)
            {
                FindObjectOfType<MoneyManager>().InstantiateMoney(2);
                manager.SubNumber(gameObject, 100000, true);
                other.transform.Find("ConfettiVFX").GetComponent<ParticleSystem>().Play();
            }
            FindObjectOfType<UIManager>().SuccesGame();
            transform.root.GetComponent<CharMove>().enabled = false;
            GameObject.Find("PlayerLeft").GetComponent<CharMove>().enabled = false;
            GameObject.Find("CameraFollow").GetComponent<CharMove>().enabled = false;
        }
    }
}