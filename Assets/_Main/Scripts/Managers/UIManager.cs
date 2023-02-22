using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UIElements;
using TMPro;
using UnityEngine.Events;
using System;
using System.Collections;
using ElephantSDK;

public class UIManager : Singleton<UIManager>
{
    //Animation States
    const string RIGHTWALK = "walkingChar2Anim";
    const string LEFTWALK = "walkingChar1Anim";
    const string RIGHTRUN = "RunChar2";
    const string LEFTRUN = "RunChar1";
    public enum CpiVideoEnum
    {
        CpiVideo,GamePlay
    }
    [Header("Replaces UI elements")]
    public CpiVideoEnum UI;
    private Button tapToStartBtn;
    private Button tapToRetryBtn;
    private Button tapToContinue;
    private GameObject successPanel;
    private GameObject failPanel;
    private TextMeshProUGUI levelText;
    public UnityAction OnLevelStart;
    
    void Start()
    {
        CPIVideo();
        levelText = transform.Find("LevelBar").GetComponentInChildren<TextMeshProUGUI>();
        tapToStartBtn = transform.Find("FullscreenButton").GetComponent<Button>();
        failPanel = transform.Find("FullscreenFail").gameObject;
        tapToRetryBtn = failPanel.GetComponentInChildren<Button>();
        successPanel = transform.Find("FullscreenSuccess").gameObject;
        tapToContinue = successPanel.GetComponentInChildren<Button>();
        levelText.SetText("LEVEL " + PlayerPrefs.GetInt("Level", 1).ToString());

        tapToContinue.onClick.AddListener(TapToContinue);
        tapToRetryBtn.onClick.AddListener(TapToRetry);
        tapToStartBtn.onClick.AddListener(TapToStart);

        successPanel.SetActive(false);
        failPanel.SetActive(false);
        tapToStartBtn.gameObject.SetActive(true);
    }
    IEnumerator TutorialClose() 
    {
        yield return new WaitForSeconds(2f);
        transform.Find("TutorialUI").gameObject.SetActive(false);
        transform.Find("TutorialUI").Find("TapTapTap").gameObject.SetActive(false);
    }

    private void TapToContinue()
    {
        tapToContinue.onClick.RemoveAllListeners();
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        StartCoroutine(FindObjectOfType<LevelManager>().LoadLevel());
    }

    private void TapToRetry()
    {
        tapToRetryBtn.onClick.RemoveAllListeners();
        StartCoroutine(FindObjectOfType<LevelManager>().LoadLevel());
    }

    private void TapToStart()
    {
        tapToStartBtn.onClick.RemoveAllListeners();
        Elephant.LevelStarted(PlayerPrefs.GetInt("Level"));
        if (FindObjectOfType<CharMove>() != null)
        {
            GameObject.Find("PlayerLeft").GetComponent<CharMove>().enabled = true;
            GameObject.Find("PlayerRight").GetComponent<CharMove>().enabled = true;
            GameObject.Find("CameraFollow").GetComponent<CharMove>().enabled = true;
            FindObjectOfType<AnimationsManager>().PlayerLeftAnimations(LEFTRUN);
            FindObjectOfType<AnimationsManager>().PlayerRightAnimations(RIGHTRUN);
            //FindObjectOfType<CharMove>().enabled = true;
            transform.Find("TutorialUI").gameObject.SetActive(true);
            transform.Find("TutorialUI").Find("TapTapTap").gameObject.SetActive(true);
        }
        tapToStartBtn.gameObject.SetActive(false);
        StartCoroutine(TutorialClose());
    }

    public void SuccesGame()
    {
        if (!successPanel.activeSelf)
            Elephant.LevelCompleted(PlayerPrefs.GetInt("Level"));
        successPanel.SetActive(true);
    }

    public void FailGame()
    {
        if (!failPanel.activeSelf)
            Elephant.LevelFailed(PlayerPrefs.GetInt("Level"));
        failPanel.SetActive(true);
    }

    public void CPIVideo()
    {
        if (UI == CpiVideoEnum.CpiVideo)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
        else if (UI == CpiVideoEnum.GamePlay)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
        
    }
}


