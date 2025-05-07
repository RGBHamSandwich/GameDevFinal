using UnityEngine;

public class BallAnimationController : MonoBehaviour
{
    ///// PRIVATE VARIABLES /////
    private Animator _ballAnimator;

    ///// GIVEN METHODS /////
    void Start()
    {
        _ballAnimator = GetComponent<Animator>();

        BallController.EOnBallHit += BallInMotionTrue;
        BallController.EOnBallStop += BallInMotionFalse;
        BallEnvironmentInteractionScript.EOnBallStop += BallInMotionFalse;
        BallEnvironmentInteractionScript.EOnBallInHole += TriggerInHole;
    }

    void OnDestroy()
    {
        BallController.EOnBallHit -= BallInMotionTrue;
        BallController.EOnBallStop -= BallInMotionFalse;
        BallEnvironmentInteractionScript.EOnBallStop -= BallInMotionFalse;
        BallEnvironmentInteractionScript.EOnBallInHole -= TriggerInHole;
    }

    ///// ANIMATION TRIGGERS /////
    private void TriggerHit()
    {
        _ballAnimator.SetTrigger("BallHitTrigger");
    }

    private void TriggerInHole()
    {
        _ballAnimator.SetTrigger("BallInHoleTrigger");
    }

    private void BallInMotionTrue()
    {
        _ballAnimator.SetBool("BallInMotion", true);
    }
    private void BallInMotionFalse()
    {
        _ballAnimator.SetBool("BallInMotion", false);
    }
}
