using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Dlite.Games.Managers;

public class CharLeft : MonoBehaviour
{
    NumberManager manager;
    public bool birlesti;
    public bool duvar;

    void Start()
    {
        duvar = false;
        birlesti = false;
        manager = FindObjectOfType<NumberManager>();
        manager.SetNumber(gameObject, "1");
        if (PlayerPrefs.GetInt("Ring") == 1)
        {
            transform.parent.Find("HandRight").transform.Find("Armature").Find("Bone.002").Find("Bone.008").Find("Bone.009").Find("Ring").gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Ring2") == 1)
        {
            transform.parent.Find("HandRight").transform.Find("Armature").Find("Bone.004").Find("Bone.014").Find("Bone.015").Find("Ring2").gameObject.SetActive(true);
        }
    }
    IEnumerator DegistiFalse()
    {
        yield return new WaitForSeconds(0f);
        manager.degisti = false;
    }
    IEnumerator DegistiTrue()
    {
        yield return new WaitForSeconds(0f);
        manager.degisti = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Door"))
        {
            if (!birlesti)
            {
                other.GetComponent<BoxCollider>().enabled = false;
            }
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

                if (birlesti)
                {
                    //manager.AddNumber(gameObject, numberPositive, true);
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

                if (birlesti)
                {
                    //manager.SubNumber(gameObject, numberPositive, true);
                }
                else
                {
                    manager.SubNumber(gameObject, numberNegative);
                    //if (manager.ReadNumber(gameObject) < 0)
                    //{
                    //    manager.SetNumber(gameObject, "0");
                    //}
                }
            }
            if (other.transform.gameObject.GetComponent<TransformDoorManager>().type == TransformDoorManager.TransformType.Multiple)
            {
                HapticManager.Haptic(Dlite.Games.HapticType.Success);
                var numberMultiple = int.Parse(other.transform.Find("Multiple").Find("Multiple").GetComponent<TextMeshPro>().text.Remove(0, 1));
                if (!birlesti)
                {
                    manager.MultipleNumber(gameObject, numberMultiple);
                }
            }
            if (other.transform.gameObject.GetComponent<TransformDoorManager>().type == TransformDoorManager.TransformType.Div)
            {

                HapticManager.Haptic(Dlite.Games.HapticType.Failure);
                var numberDiv = int.Parse(other.transform.Find("Div").Find("Div").GetComponent<TextMeshPro>().text.Remove(0, 1));
                if (!birlesti)
                {
                    manager.DivNumber(gameObject, numberDiv);
                }
            }
        }
        if (other.CompareTag("Birlesik"))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.SoftImpact);
            transform.parent.DOMoveX(3.7f, 1f);
            birlesti = true;
            if(manager.left.name == "PlayerLeft")
            {
                transform.DOLocalMoveX(manager.GetMergePositionXForLeft(), 1);
            }else
            {
                transform.DOLocalMoveX(manager.GetMergePositionXForRight(), 1);
            }
        }
        if (other.CompareTag("Wall"))
        {
            manager.SetNumbers();
            other.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(SetWallColliderTrue(other));
            transform.DOKill();
            GameObject.Find("PlayerRight").transform.Find("Numbers").transform.DOKill();
            HapticManager.Haptic(Dlite.Games.HapticType.Failure);
            if (FindObjectOfType<NumberManager>().left.name == "PlayerLeft")
            {
                transform.DOLocalJump(new Vector3(0, 0.253f, 0), 0.1f, 1, 1); 
                GameObject.Find("PlayerRight").transform.Find("Numbers").transform.DOLocalJump(new Vector3(0, 0.253f, 0), 0.1f, 1, 1);
                manager.degisti = false;
            }
            else
            {
                transform.DOLocalJump(new Vector3(0.68f, 0.253f, 0), 0.1f, 1, 1);

                GameObject.Find("PlayerRight").transform.Find("Numbers").transform.DOLocalJump(new Vector3(-0.68f, 0.253f, 0), 0.1f, 1, 1);
                manager.degisti = true;
            }
        }
        if (other.CompareTag("Ring"))
        {
            PlayerPrefs.SetInt("Ring", 1);
            if(PlayerPrefs.GetInt("Ring") == 1)
            {
                transform.parent.Find("HandRight").transform.Find("Armature").Find("Bone.002").Find("Bone.008").Find("Bone.009").Find("Ring").gameObject.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Ring2"))
        {
            PlayerPrefs.SetInt("Ring2", 1);
            if(PlayerPrefs.GetInt("Ring2") == 1)
            {
                transform.parent.Find("HandRight").transform.Find("Armature").Find("Bone.004").Find("Bone.014").Find("Bone.015").Find("Ring2").gameObject.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
    }
    IEnumerator SetWallColliderTrue(Collider other)
    {
        yield return new WaitForSeconds(0.2f);
        other.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Birlesik"))
        {

            HapticManager.Haptic(Dlite.Games.HapticType.SoftImpact);
            transform.parent.DOLocalMoveX(0, 1);
            birlesti = false;
            if (FindObjectOfType<NumberManager>().degisti == true)
            {
                transform.DOLocalMoveX(0.68f, 1);
            }
            else
            {
                transform.DOLocalMoveX(0, 1);
            }

            manager.SetNumber(gameObject, manager.ReadNumber(gameObject).ToString());
        }
    }
}
