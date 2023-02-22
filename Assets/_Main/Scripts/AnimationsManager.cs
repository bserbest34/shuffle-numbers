using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    internal void PlayerLeftAnimations(string newState)
    {
        GameObject.Find("PlayerLeft").GetComponent<Animator>().Play(newState);
    }
    internal void PlayerRightAnimations(string newState)
    {
        GameObject.Find("PlayerRight").GetComponent<Animator>().Play(newState);
    }
}
