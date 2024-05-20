using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    private Animator animator;
    //[SerializeField] private AudioClip darkSoulsSound;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonExit;
    private GameManager gm;
    public event Action Finish;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        buttonExit.enabled = false;
        buttonRestart.enabled = false;
        gm.HelicopterDeath += LaunchAnimationDeath;
        gm.HelicopterWin += LaunchAnimationWin;
    }

    void LaunchAnimationDeath()
    {
        if(animator.name == "SceneTransitionDeath")
            animator.SetTrigger("IsDead");
    }
    void LaunchAnimationWin()
    {
        if(animator.name == "SceneTransitionWin")
            animator.SetTrigger("Win");
    }
    public void FinishAnimation()
    {
        buttonExit.enabled = true;
        buttonRestart.enabled = true;
    }

    public void DestroyAllObjects() => Finish?.Invoke();

    private void OnDestroy()
    {
        gm.HelicopterDeath -= LaunchAnimationDeath;
        gm.HelicopterWin -= LaunchAnimationWin;
    }
    //public void StartMusic() => AudioManager.Instance.PlayClip(darkSoulsSound);
}

