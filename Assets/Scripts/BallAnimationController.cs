using UnityEngine;

public class BallAnimationController : MonoBehaviour
{
    ///// PRIVATE VARIABLES /////
    private Animator _ballAnimator;
    private SpriteRenderer _ballSpriteRenderer;

    ///// METHODS /////
    void Start()
    {
        _ballAnimator = GetComponent<Animator>();
        _ballSpriteRenderer = GetComponent<SpriteRenderer>();

        BallController.EOnBallHit += TriggerHit;
        BallController.EOnBallStop += TriggerStop;
        BallEnvironmentInteractionScript.EOnBallStop += TriggerStop;
        BallEnvironmentInteractionScript.EOnBallInHole += TriggerInHole;
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {
        BallController.EOnBallHit -= TriggerHit;
        BallController.EOnBallStop -= TriggerStop;
        BallEnvironmentInteractionScript.EOnBallStop -= TriggerStop;
        BallEnvironmentInteractionScript.EOnBallInHole -= TriggerInHole;
    }

    private void TriggerHit()
    {
        _ballAnimator.SetTrigger("BallHitTrigger");
        Debug.Log("Ball hit triggered!");
    }

    private void TriggerStop()
    {
        _ballAnimator.SetTrigger("BallStopTrigger");
        Debug.Log("Ball stop triggered!");
    }

    private void TriggerInHole()
    {
        _ballAnimator.SetTrigger("BallInHoleTrigger");
        Debug.Log("Ball in hole triggered!");
    }
}
