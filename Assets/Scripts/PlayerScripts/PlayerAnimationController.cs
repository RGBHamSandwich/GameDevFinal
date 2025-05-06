using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    ///// PRIVATE VARIABLES /////
    private Animator _playerAnimator;

    ///// GIVEN METHODS /////
    void Start()
    {
        _playerAnimator = GetComponent<Animator>();

        BallController.EOnPlayerSwing += TriggerSwing;
        PlayerMovementController.EOnPlayerIdle += TriggerIdle;
        LevelStatManager.EOnPlayerCry += TriggerCry;
        PlayerMovementController.EOnPlayerCry += TriggerCry;
        PlayerMovementController.EOnPlayerWalk += TriggerWalk;
    }

    void OnDestroy()
    {
        BallController.EOnPlayerSwing -= TriggerSwing;
        PlayerMovementController.EOnPlayerIdle -= TriggerIdle;
        LevelStatManager.EOnPlayerCry -= TriggerCry;
        PlayerMovementController.EOnPlayerCry -= TriggerCry;
        PlayerMovementController.EOnPlayerWalk -= TriggerWalk;
    }

    void Update()
    {    
    }

    ///// TRIGGERS /////
    private void TriggerSwing()
    {
        _playerAnimator.SetTrigger("PlayerSwingTrigger");
        // Debug.Log("Player swing triggered!");
    }

    private void TriggerCry()
    {
        _playerAnimator.SetTrigger("PlayerCryTrigger");
        // Debug.Log("Player cry triggered!");
    }

    private void TriggerWalk()
    {
        _playerAnimator.SetTrigger("PlayerWalkTrigger");
        // Debug.Log("Player walk triggered!");
    }

    private void TriggerIdle()
    {
        _playerAnimator.SetTrigger("PlayerIdleTrigger");
        // Debug.Log("Player idle triggered!");
    }

}
