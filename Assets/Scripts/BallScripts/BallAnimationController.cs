using UnityEngine;

public class BallAnimationController : MonoBehaviour
{
    ///// PRIVATE VARIABLES /////
    private Animator _ballAnimator;

    ///// GIVEN METHODS /////
    void Start()
    {
        _ballAnimator = GetComponent<Animator>();

        BallController.EOnBallHit += TriggerHit;
        BallController.EOnBallStop += TriggerStop;
        BallEnvironmentInteractionScript.EOnBallStop += TriggerStop;
        BallEnvironmentInteractionScript.EOnBallInHole += TriggerInHole;
    }

    void OnDestroy()
    {
        BallController.EOnBallHit -= TriggerHit;
        BallController.EOnBallStop -= TriggerStop;
        BallEnvironmentInteractionScript.EOnBallStop -= TriggerStop;
        BallEnvironmentInteractionScript.EOnBallInHole -= TriggerInHole;
    }

    ///// ANIMATION TRIGGERS /////
    private void TriggerHit()
    {
        _ballAnimator.SetTrigger("BallHitTrigger");
    }

    private void TriggerStop()
    {
        _ballAnimator.SetTrigger("BallStopTrigger");
    }

    private void TriggerInHole()
    {
        _ballAnimator.SetTrigger("BallInHoleTrigger");
    }
}
