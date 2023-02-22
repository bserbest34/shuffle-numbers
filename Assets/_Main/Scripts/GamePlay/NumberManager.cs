using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dlite.Games.Managers;
public class NumberManager : MonoBehaviour
{
    private int currentNumber;
    public bool degisti;
    //Animations States
    const string THROWRIGHT = "HandThrowRight";
    const string THROWLEFT = "HandThrowRight 1";
    const string RIGHTIDLE = "HandIdle";
    const string LEFTIDLE = "HandIdle 1";

    internal GameObject left;
    internal GameObject right;

    public Sequence mySequence;

    void Start()
    {
        mySequence = DOTween.Sequence();
        left = GameObject.Find("PlayerLeft");
        right = GameObject.Find("PlayerRight");
        degisti = false;
    }
    void Update()
    {
        ChangeNumberPos();
    }
    public void CloseAllNumbers(GameObject character)
    {
        //character.transform.Find("Numbers1").transform.DOLocalMoveX(-4f, 1);
        //character.transform.Find("Numbers2").transform.DOLocalMoveX(0, 1);
        //character.transform.Find("Numbers3").transform.DOLocalMoveX(4f, 1);
        for (int i = 0; i < character.transform.Find("Numbers1").childCount; i++)
        {
            if (character.transform.Find("Numbers1").GetChild(i).name == "IceFloorTrail")
                continue;
            character.transform.Find("Numbers1").GetChild(i).gameObject.SetActive(false);
            if (character.transform.Find("Numbers2").GetChild(i).name == "IceFloorTrail")
                continue;
            character.transform.Find("Numbers2").GetChild(i).gameObject.SetActive(false);
            if (character.transform.Find("Numbers3").GetChild(i).name == "IceFloorTrail")
                continue;
            character.transform.Find("Numbers3").GetChild(i).gameObject.SetActive(false);
        }
    }
    public float GetMergePositionXForLeft()
    {
        int rightDigitsCount = ReadNumber(right.transform.Find("Numbers").gameObject).ToString().Length;
        int leftDigitsCount = ReadNumber(left.transform.Find("Numbers").gameObject).ToString().Length;
        if (left.name == "PlayerLeft")
        {
            if (leftDigitsCount == 3 && rightDigitsCount == 3) { return -0.1f; }
            else if (leftDigitsCount == 3 && rightDigitsCount == 2) { return -0.05f; }
            else if (leftDigitsCount == 3 && rightDigitsCount == 1) { return 0.007f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 3) { return -0.083f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 2) { return -0.005f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 1) { return 0.027f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 3) { return -0.078f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 2) { return 0.012f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 1) { return 0.06f; }
        }else
        {
            if (leftDigitsCount == 3 && rightDigitsCount == 3) { return -0.36f; }
            else if (leftDigitsCount == 3 && rightDigitsCount == 2) { return -0.307f; }
            else if (leftDigitsCount == 3 && rightDigitsCount == 1) { return -0.228f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 3) { return -0.3f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 2) { return -0.28f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 1) { return -0.21f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 3) { return -0.36f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 2) { return -0.269f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 1) { return -0.222f; }
        }

        return 0;
    }
    public float GetMergePositionXForRight()
    {
        int rightDigitsCount = ReadNumber(right.transform.Find("Numbers").gameObject).ToString().Length;
        int leftDigitsCount = ReadNumber(left.transform.Find("Numbers").gameObject).ToString().Length;
        if (left.name == "PlayerLeft")
        {
            if (leftDigitsCount == 3 && rightDigitsCount == 3) { return 0.1f; }
            else if (leftDigitsCount == 3 && rightDigitsCount == 2) { return 0.05f; }
            else if (leftDigitsCount == 3 && rightDigitsCount == 1) { return 0.047f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 3) { return 0f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 2) { return 0.005f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 1) { return -0.01f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 3) { return -0.07f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 2) { return -0.056f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 1) { return -0.06f; }
        }else
        {
            if (leftDigitsCount == 3 && rightDigitsCount == 3) { return 0.36f; }
            else if (leftDigitsCount == 3 && rightDigitsCount == 2) { return 0.341f; }
            else if (leftDigitsCount == 3 && rightDigitsCount == 1) { return 0.344f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 3) { return 0.32f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 2) { return 0.28f; }
            else if (leftDigitsCount == 2 && rightDigitsCount == 1) { return 0.25f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 3) { return 0.24f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 2) { return 0.238f; }
            else if (leftDigitsCount == 1 && rightDigitsCount == 1) { return 0.222f; }
        }

        return 0;
    }

    public void SetAllNumber(string number)
    {
        CloseAllNumbers(GameObject.Find("PlayerLeft").transform.Find("Numbers").transform.gameObject);
        CloseAllNumbers(GameObject.Find("PlayerRight").transform.Find("Numbers").transform.gameObject);
        int numberOfDigits = number.ToString().Length;
        if (int.Parse(number) <= 0)
        {
            number = "0";
        }
        switch (numberOfDigits)
        {
            case 1:
                //right.transform.Find("Numbers").transform.Find("Numbers3").Find(number.ToString()).gameObject.SetActive(true);
                SetNumber(right.transform.Find("Numbers").gameObject, number);
                break;
            case 2:
                //right.transform.Find("Numbers").transform.Find("Numbers3").Find(number.ToString()[1].ToString()).gameObject.SetActive(true);
                SetNumber(right.transform.Find("Numbers").gameObject, (number[1].ToString()));
                //left.transform.Find("Numbers").transform.Find("Numbers3").Find(number.ToString()[0].ToString()).gameObject.SetActive(true);
                SetNumber(left.transform.Find("Numbers").gameObject, (number[0].ToString()));
                break;
            case 3:
                //right.transform.Find("Numbers").transform.Find("Numbers3").Find(number.ToString()[2].ToString()).gameObject.SetActive(true);
                //right.transform.Find("Numbers").transform.Find("Numbers2").Find(number.ToString()[1].ToString()).gameObject.SetActive(true);

                SetNumber(right.transform.Find("Numbers").gameObject, (number[1].ToString() + number[2].ToString()));

                //left.transform.Find("Numbers").transform.Find("Numbers3").Find(number.ToString()[0].ToString()).gameObject.SetActive(true);

                SetNumber(left.transform.Find("Numbers").gameObject, number[0].ToString());
                break;
            case 4:
                //left.transform.Find("Numbers").transform.Find("Numbers2").Find(number.ToString()[0].ToString()).gameObject.SetActive(true);
                //left.transform.Find("Numbers").transform.Find("Numbers3").Find(number.ToString()[1].ToString()).gameObject.SetActive(true);

                SetNumber(left.transform.Find("Numbers").gameObject, (number[0].ToString() + number.ToString()[1].ToString()));


                //right.transform.Find("Numbers").transform.Find("Numbers3").Find(number.ToString()[3].ToString()).gameObject.SetActive(true);
                //right.transform.Find("Numbers").transform.Find("Numbers2").Find(number.ToString()[2].ToString()).gameObject.SetActive(true);

                SetNumber(right.transform.Find("Numbers").gameObject, (number[2].ToString() + number.ToString()[3].ToString()));
                break;
            case 5:
                //left.transform.Find("Numbers").transform.Find("Numbers3").Find(number.ToString()[1].ToString()).gameObject.SetActive(true);
                //left.transform.Find("Numbers").transform.Find("Numbers2").Find(number.ToString()[0].ToString()).gameObject.SetActive(true);
                SetNumber(left.transform.Find("Numbers").gameObject,(number[0].ToString() + number[1].ToString()));


                //right.transform.Find("Numbers").transform.Find("Numbers3").Find(number.ToString()[4].ToString()).gameObject.SetActive(true);
                //right.transform.Find("Numbers").transform.Find("Numbers2").Find(number.ToString()[3].ToString()).gameObject.SetActive(true);
                //right.transform.Find("Numbers").transform.Find("Numbers1").Find(number.ToString()[2].ToString()).gameObject.SetActive(true);

                SetNumber(right.transform.Find("Numbers").gameObject, (number[2].ToString() + number[3].ToString() + number[4].ToString()));
                break;
            case 6:
                //left.transform.Find("Numbers").transform.Find("Numbers3").Find(number.ToString()[2].ToString()).gameObject.SetActive(true);
                //left.transform.Find("Numbers").transform.Find("Numbers2").Find(number.ToString()[1].ToString()).gameObject.SetActive(true);
                //left.transform.Find("Numbers").transform.Find("Numbers1").Find(number.ToString()[0].ToString()).gameObject.SetActive(true);

                SetNumber(left.transform.Find("Numbers").gameObject, (number[0].ToString() + number[1].ToString() + number[2].ToString()));

                //right.transform.Find("Numbers").transform.Find("Numbers3").Find(number.ToString()[5].ToString()).gameObject.SetActive(true);
                //right.transform.Find("Numbers").transform.Find("Numbers2").Find(number.ToString()[4].ToString()).gameObject.SetActive(true);
                //right.transform.Find("Numbers").transform.Find("Numbers1").Find(number.ToString()[3].ToString()).gameObject.SetActive(true);


                SetNumber(right.transform.Find("Numbers").gameObject, (number[3].ToString() + number[4].ToString() + number[5].ToString()));
                break;
        }
        if (left.name == "PlayerLeft")
        {
            left.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForLeft(), 0.253f, 0), 0f, 1, 1);
            right.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForRight(), 0.253f, 0), 0f, 1, 1);
        }
        else
        {
            left.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForLeft(), 0.253f, 0), 0f, 1, 1);
            right.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForRight(), 0.253f, 0), 0f, 1, 1);
        }
    }
    public void SetNumber(GameObject character,string number)
    {
        if (int.Parse(number) < 0)
        {
            number = "0";
        }
        if(int.Parse(number) >= 999)
        {
            number = "999";
        }
        CloseAllNumbers(character);
        int numberOfDigits = number.ToString().Length;
        //Debug.Log("digits" + numberOfDigits);
        //Debug.Log("number " + number);
        switch (numberOfDigits)
        {
            case 1:
                character.transform.Find("Numbers1").Find(number.ToString()).gameObject.SetActive(true);
                character.transform.Find("Numbers1").transform.DOLocalMoveX(0f, 1);
                character.transform.Find("Numbers2").transform.DOLocalMoveX(0f, 1);
                character.transform.Find("Numbers3").transform.DOLocalMoveX(0f, 1);
                break;
            case 2:
                character.transform.Find("Numbers1").Find(number.ToString()[0].ToString()).gameObject.SetActive(true);
                character.transform.Find("Numbers2").Find(number.ToString()[1].ToString()).gameObject.SetActive(true);
                character.transform.Find("Numbers1").transform.DOLocalMoveX(-2f, 1);
                character.transform.Find("Numbers2").transform.DOLocalMoveX(2f, 1);
                break;
            case 3:
                character.transform.Find("Numbers1").Find(number.ToString()[0].ToString()).gameObject.SetActive(true);
                character.transform.Find("Numbers2").Find(number.ToString()[1].ToString()).gameObject.SetActive(true);
                character.transform.Find("Numbers3").Find(number.ToString()[2].ToString()).gameObject.SetActive(true);
                character.transform.Find("Numbers1").transform.DOLocalMoveX(-4f, 1);
                character.transform.Find("Numbers2").transform.DOLocalMoveX(0f, 1);
                character.transform.Find("Numbers3").transform.DOLocalMoveX(4f, 1);
                break;
        }
    }
    public void SetSinglePos(GameObject character, string number)
    {
        int numberOfDigits = number.ToString().Length;

        switch (numberOfDigits)
        {
            case 1:
                character.transform.Find("Numbers1").transform.DOLocalMoveX(0f, 1);
                character.transform.Find("Numbers2").transform.DOLocalMoveX(0f, 1);
                character.transform.Find("Numbers3").transform.DOLocalMoveX(0f, 1);
                break;
            case 2:
                character.transform.Find("Numbers1").transform.DOLocalMoveX(-2f, 1);
                character.transform.Find("Numbers2").transform.DOLocalMoveX(2f, 1);
                break;
            case 3:
                character.transform.Find("Numbers1").transform.DOLocalMoveX(-4f, 1);
                character.transform.Find("Numbers2").transform.DOLocalMoveX(0f, 1);
                character.transform.Find("Numbers3").transform.DOLocalMoveX(4f, 1);
                break;
        }
    }
    public string ReadNumber(GameObject character)
    {
        string number = "";
        for (int i = 0; i < character.transform.Find("Numbers1").childCount; i++)
        {
            if (character.transform.Find("Numbers1").GetChild(i).gameObject.activeInHierarchy)
            {
                if (character.transform.Find("Numbers1").GetChild(i).name == "IceFloorTrail")
                    continue;
                number += character.transform.Find("Numbers1").GetChild(i).gameObject.name;
            }
        }
        for (int i = 0; i < character.transform.Find("Numbers2").childCount; i++)
        {
            if (character.transform.Find("Numbers2").GetChild(i).name == "IceFloorTrail")
                continue;
            if (character.transform.Find("Numbers2").GetChild(i).gameObject.activeInHierarchy)
            {
                number += character.transform.Find("Numbers2").GetChild(i).gameObject.name;
            }
        }
        for (int i = 0; i < character.transform.Find("Numbers3").childCount; i++)
        {
            if (character.transform.Find("Numbers2").GetChild(i).name == "IceFloorTrail")
                continue;
            if (character.transform.Find("Numbers3").GetChild(i).gameObject.activeInHierarchy)
            {
                number += character.transform.Find("Numbers3").GetChild(i).gameObject.name;
            }
        }
        return number;
    }
    public int ReadAllNumber()
    {
        string number = "";
        for (int i = 0; i < left.transform.Find("Numbers").transform.Find("Numbers1").childCount; i++)
        {
            if (left.transform.Find("Numbers").transform.Find("Numbers1").GetChild(i).gameObject.activeInHierarchy)
            {
                if(left.transform.Find("Numbers").transform.Find("Numbers1").GetChild(i).name == "IceFloorTrail")
                    continue;
                number += left.transform.Find("Numbers").transform.Find("Numbers1").GetChild(i).gameObject.name;
            }
        }
        for (int i = 0; i < left.transform.Find("Numbers").transform.Find("Numbers2").childCount; i++)
        {
            if (left.transform.Find("Numbers").transform.Find("Numbers2").GetChild(i).gameObject.activeInHierarchy)
            {
                if (left.transform.Find("Numbers").transform.Find("Numbers2").GetChild(i).name == "IceFloorTrail")
                    continue;
                number += left.transform.Find("Numbers").transform.Find("Numbers2").GetChild(i).gameObject.name;
            }
        }
        for (int i = 0; i < left.transform.Find("Numbers").transform.Find("Numbers3").childCount; i++)
        {
            if (left.transform.Find("Numbers").transform.Find("Numbers3").GetChild(i).gameObject.activeInHierarchy)
            {
                if (left.transform.Find("Numbers").transform.Find("Numbers3").GetChild(i).name == "IceFloorTrail")
                    continue;
                number += left.transform.Find("Numbers").transform.Find("Numbers3").GetChild(i).gameObject.name;
            }
        }
        for (int i = 0; i < right.transform.Find("Numbers").transform.Find("Numbers1").childCount; i++)
        {
            if (right.transform.Find("Numbers").transform.Find("Numbers1").GetChild(i).gameObject.activeInHierarchy)
            {
                if (right.transform.Find("Numbers").transform.Find("Numbers1").GetChild(i).name == "IceFloorTrail")
                    continue;
                number += right.transform.Find("Numbers").transform.Find("Numbers1").GetChild(i).gameObject.name;
            }
        }
        for (int i = 0; i < right.transform.Find("Numbers").transform.Find("Numbers2").childCount; i++)
        {
            if (right.transform.Find("Numbers").transform.Find("Numbers2").GetChild(i).gameObject.activeInHierarchy)
            {
                if (right.transform.Find("Numbers").transform.Find("Numbers2").GetChild(i).name == "IceFloorTrail")
                    continue;
                number += right.transform.Find("Numbers").transform.Find("Numbers2").GetChild(i).gameObject.name;
            }
        }
        for (int i = 0; i < right.transform.Find("Numbers").transform.Find("Numbers3").childCount; i++)
        {
            if (right.transform.Find("Numbers").transform.Find("Numbers3").GetChild(i).gameObject.activeInHierarchy)
            {
                if (right.transform.Find("Numbers").transform.Find("Numbers3").GetChild(i).name == "IceFloorTrail")
                    continue;
                number += right.transform.Find("Numbers").transform.Find("Numbers3").GetChild(i).gameObject.name;
            }
        }
        return Int32.Parse(number);


    }
    public void ShuffleNumber(GameObject character)
    {
        int numberOfDigits = ReadNumber(character).ToString().Length;
        switch (numberOfDigits)
        {
            case 1:
                return;
            case 2:
                Vector3 numbers1Position = character.transform.Find("Numbers1").transform.localPosition;
                Vector3 numbers2Position = character.transform.Find("Numbers2").transform.localPosition;
                character.transform.Find("Numbers1").transform.DOLocalMoveX(numbers2Position.x, 1f);
                character.transform.Find("Numbers2").transform.DOLocalMoveX(numbers1Position.x, 1f);
                GameObject numbers1 = character.transform.Find("Numbers1").gameObject;
                GameObject numbers2 = character.transform.Find("Numbers2").gameObject;
                numbers1.name = "Numbers2";
                numbers2.name = "Numbers1";
                break;
            case 3:
                numbers1Position = character.transform.Find("Numbers1").transform.localPosition;
                numbers2Position = character.transform.Find("Numbers2").transform.localPosition;
                character.transform.Find("Numbers1").transform.DOLocalMoveX(numbers2Position.x, 1f);
                character.transform.Find("Numbers2").transform.DOLocalMoveX(numbers1Position.x, 1f);
                numbers1 = character.transform.Find("Numbers1").gameObject;
                numbers2 = character.transform.Find("Numbers2").gameObject;
                numbers1.name = "Numbers2";
                numbers2.name = "Numbers1";
                break;
            default:
                break;
        }
    }
    public void FallFirstNumber(GameObject character)
    {
        Vector3 number3Position = character.transform.Find("Numbers3").transform.localPosition;
        Vector3 number2Position = character.transform.Find("Numbers2").transform.localPosition;
        Vector2 number1Position = character.transform.Find("Numbers1").transform.localPosition;
        character.transform.Find("Numbers3").transform.DOLocalMoveX(number2Position.x, 1f);
        character.transform.Find("Numbers2").transform.DOLocalMoveX(number1Position.x, 1f);
        character.transform.Find("Numbers1").transform.DOLocalMoveX(number3Position.x, 1f);
        GameObject numbers3 = character.transform.Find("Numbers3").gameObject;
        GameObject numbers2 = character.transform.Find("Numbers2").gameObject;
        GameObject numbers1 = character.transform.Find("Numbers1").gameObject;
        numbers2.name = "Numbers1";
        numbers1.name = "Numbers3";
        numbers3.name = "Numbers2";
    }
    public void FallMiddleNumber(GameObject character)
    {
        Vector3 numbers3Position = character.transform.Find("Numbers3").transform.localPosition;
        Vector3 numbers2Position = character.transform.Find("Numbers2").transform.localPosition;       
        character.transform.Find("Numbers3").transform.DOLocalMoveX(numbers2Position.x, 1f);
        //character.transform.Find("Numbers2").transform.DOLocalMoveX(numbers3Position.x, 1f);
        GameObject numbers3 = character.transform.Find("Numbers3").gameObject;
        GameObject numbers2 = character.transform.Find("Numbers2").gameObject;
        numbers3.name = "Numbers2";
        numbers2.name = "Numbers3";
    }
    //public void FallThirthNumber(GameObject character)
    //{
    //    Vector3 number3Position = character.transform.Find("Numbers3").transform.localPosition;
    //    Vector3 number2Position = character.transform.Find("Numbers2").transform.localPosition;
    //    Vector2 number1Position = character.transform.Find("Numbers1").transform.localPosition;
    //    character.transform.Find("Numbers3").transform.DOLocalMoveX(number2Position.x, 1f);
    //    character.transform.Find("Numbers2").transform.DOLocalMoveX(number1Position.x, 1f);
    //    character.transform.Find("Numbers1").transform.DOLocalMoveX(number3Position.x, 1f);
    //    GameObject numbers3 = character.transform.Find("Numbers3").gameObject;
    //    GameObject numbers2 = character.transform.Find("Numbers2").gameObject;
    //    GameObject numbers1 = character.transform.Find("Numbers1").gameObject;
    //    numbers2.name = "Numbers1";
    //    numbers1.name = "Numbers3";
    //    numbers3.name = "Numbers2";
    //}
    public void SetNumbers()
    {
        GameObject temp;
        temp = left;
        left = right;
        right = temp;
    }
    IEnumerator DegistiFalse()
    {
        yield return new WaitForSeconds(0f);
        degisti = false;
    }
    IEnumerator DegistiTrue()
    {
        yield return new WaitForSeconds(0f);
        degisti = true;
    }
    void ChangeNumberPos()
    {
        int rightDigitsCount = ReadNumber(right.transform.Find("Numbers").gameObject).ToString().Length;
        int leftDigitsCount = ReadNumber(left.transform.Find("Numbers").gameObject).ToString().Length;
        if (FindObjectOfType<CharRight>().finish == true)
            return;
        if (GameObject.Find("PlayerRight").GetComponent<CharMove>().enabled == false)
            return;
        else if (Input.GetMouseButtonDown(0))
        {
            HapticManager.Haptic(Dlite.Games.HapticType.MediumImpact);
            SetNumbers();
            if (FindObjectOfType<CharLeft>().birlesti == true && !degisti)
            {
                if (left.name == "PlayerLeft")
                {
                    left.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForLeft(), 0.253f, 0), 0.1f, 1, 1);
                    right.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForRight(), 0.253f, 0), 0.1f, 1, 1);
                }
                else
                {
                    left.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForLeft() , 0.253f, 0), 0.1f, 1, 1);
                    right.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForRight() , 0.253f, 0), 0.1f, 1, 1);
                }
                FindObjectOfType<AnimationsManager>().PlayerRightAnimations(THROWRIGHT);
                FindObjectOfType<AnimationsManager>().PlayerLeftAnimations(THROWLEFT);
                Invoke("WalkAnimations", 1.05f);
                StartCoroutine(DegistiTrue());
            }
            else if (FindObjectOfType<CharLeft>().birlesti == true && degisti)
            {
                if (left.name == "PlayerLeft")
                {
                    left.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForLeft(), 0.253f, 0), 0.1f, 1, 1);
                    right.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForRight(), 0.253f, 0), 0.1f, 1, 1);
                }
                else
                {
                    left.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForLeft(), 0.253f, 0), 0.1f, 1, 1);
                    right.transform.Find("Numbers").transform.DOLocalJump(new Vector3(GetMergePositionXForRight(), 0.253f, 0), 0.1f, 1, 1);
                }
                FindObjectOfType<AnimationsManager>().PlayerRightAnimations(THROWRIGHT);
                FindObjectOfType<AnimationsManager>().PlayerLeftAnimations(THROWLEFT);
                Invoke("WalkAnimations", 1.05f);
                StartCoroutine(DegistiFalse());
            }
            else if (!degisti)
            {
                FindObjectOfType<AnimationsManager>().PlayerRightAnimations(THROWRIGHT);
                FindObjectOfType<AnimationsManager>().PlayerLeftAnimations(THROWLEFT);
                mySequence.Append(GameObject.Find("PlayerRight").transform.Find("Numbers").transform.DOLocalJump(new Vector3(-0.68f, 0.253f, 0), 0.1f, 1, 1));
                mySequence.Append(GameObject.Find("PlayerLeft").transform.Find("Numbers").transform.DOLocalJump(new Vector3(0.68f, 0.253f, 0), 0.1f, 1, 1));
                Invoke("WalkAnimations", 1.05f);
                StartCoroutine(DegistiTrue());
            }
            else if (degisti)
            {
                FindObjectOfType<AnimationsManager>().PlayerRightAnimations(THROWRIGHT);
                FindObjectOfType<AnimationsManager>().PlayerLeftAnimations(THROWLEFT);
                mySequence.Append(GameObject.Find("PlayerLeft").transform.Find("Numbers").transform.DOLocalJump(new Vector3(-0.014f, 0.253f, 0), 0.1f, 1, 1));
                mySequence.Append(GameObject.Find("PlayerRight").transform.Find("Numbers").transform.DOLocalJump(new Vector3(-0.023f, 0.253f, 0), 0.1f, 1, 1));
                Invoke("WalkAnimations", 1.05f); 
                StartCoroutine(DegistiFalse());

            }
            GameObject.Find("PlayerRight").transform.Find("Numbers").Find("Numbers1").Find("IceFloorTrail").GetComponent<ParticleSystem>().Play();
            GameObject.Find("PlayerRight").transform.Find("Numbers").Find("Numbers2").Find("IceFloorTrail").GetComponent<ParticleSystem>().Play();
            GameObject.Find("PlayerRight").transform.Find("Numbers").Find("Numbers3").Find("IceFloorTrail").GetComponent<ParticleSystem>().Play();
            GameObject.Find("PlayerLeft").transform.Find("Numbers").Find("Numbers1").Find("IceFloorTrail").GetComponent<ParticleSystem>().Play();
            GameObject.Find("PlayerLeft").transform.Find("Numbers").Find("Numbers2").Find("IceFloorTrail").GetComponent<ParticleSystem>().Play();
            GameObject.Find("PlayerLeft").transform.Find("Numbers").Find("Numbers3").Find("IceFloorTrail").GetComponent<ParticleSystem>().Play();
        }
    }
    void WalkAnimations()
    {
        {
            FindObjectOfType<AnimationsManager>().PlayerLeftAnimations(LEFTIDLE);
            FindObjectOfType<AnimationsManager>().PlayerRightAnimations(RIGHTIDLE);
        }
    }
    public void AddNumber(GameObject character,int number, bool birlestiMi = false)
    {
        if (birlestiMi)
        {
            SetAllNumber((ReadAllNumber() + number).ToString());
        }
        else
        {
            SetNumber(character, (Int32.Parse(ReadNumber(character)) + number).ToString());
        }
    }
    public void SubNumber(GameObject character,int number, bool birlestiMi = false)
    {
        if (birlestiMi)
        {
            SetAllNumber((ReadAllNumber() - number).ToString());
        }
        else
        {
            SetNumber(character, (Int32.Parse(ReadNumber(character)) - number).ToString());
        }
    }
    public void MultipleNumber(GameObject character,int number, bool birlestiMi = false)
    {
        if (birlestiMi)
        {
            SetAllNumber((ReadAllNumber() * number).ToString());
        }
        else
        {

            SetNumber(character, (Int32.Parse(ReadNumber(character)) * number).ToString());
        }
    }
    public void DivNumber(GameObject character,int number, bool birlestiMi = false)
    {
        if (birlestiMi)
        {
            SetAllNumber((ReadAllNumber() / number).ToString());
        }
        else
        {

            SetNumber(character, (Int32.Parse(ReadNumber(character)) / number).ToString());
        }
    }
}
