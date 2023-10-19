using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ScoreHandler ScoreHandler;

    void StartGame()
    {
        animator.enabled = false;
        ScoreHandler.CanEarnScore = true;
    }
}
