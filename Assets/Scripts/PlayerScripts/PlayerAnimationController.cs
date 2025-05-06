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
        PlayerMovementController.EOnPlayerWalk += TriggerWalk;
    }

    void OnDestroy()
    {
        BallController.EOnPlayerSwing -= TriggerSwing;
        PlayerMovementController.EOnPlayerIdle -= TriggerIdle;
        PlayerMovementController.EOnPlayerWalk -= TriggerWalk;
    }

    ///// TRIGGERS /////
    private void TriggerSwing()
    {
        _playerAnimator.SetTrigger("PlayerSwingTrigger");
    }

    private void TriggerWalk()
    {
        _playerAnimator.SetTrigger("PlayerWalkTrigger");
    }

    private void TriggerIdle()
    {
        _playerAnimator.SetTrigger("PlayerIdleTrigger");
    }

}
