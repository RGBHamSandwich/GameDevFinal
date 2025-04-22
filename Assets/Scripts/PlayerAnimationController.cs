using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _playerAnimator;

    void Start()
    {
        _playerAnimator = GetComponent<Animator>();

        BallController.EOnPlayerSwing += TriggerSwing;
        BallController.EOnBallStop += TriggerWalk;
        BallController.EOnPlayerIdle += TriggerIdle;
    }

    void OnDestroy()
    {
        BallController.EOnPlayerSwing += TriggerSwing;
        BallController.EOnBallStop -= TriggerWalk;
        BallController.EOnPlayerIdle -= TriggerIdle;
    }

    void Update()
    {
        
    }

    private void TriggerSwing()
    {
        _playerAnimator.SetTrigger("PlayerSwingTrigger");
        Debug.Log("Player swing triggered!");
    }

    private void TriggerCry()
    {
        _playerAnimator.SetTrigger("PlayerCryTrigger");
        Debug.Log("Player cry triggered!");
    }

    private void TriggerWalk()
    {
        _playerAnimator.SetTrigger("PlayerWalkTrigger");
        Debug.Log("Player walk triggered!");
    }

    private void TriggerIdle()
    {
        _playerAnimator.SetTrigger("PlayerIdleTrigger");
        Debug.Log("Player idle triggered!");
    }

}
