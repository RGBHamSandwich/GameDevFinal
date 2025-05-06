using UnityEngine;

public class BallEnvironmentInteractionScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Tooltip("How much sand slows the ball down (used for linear and angular damping).")]
    public float sandDamping = 2f;

    ///// PRIVATE VARIABLES /////
    private Rigidbody2D _ballRigidBody2D;
    private LevelStatManager _levelStatManager;
    private AudioManagerScript _audioManagerScript;

    ///// ANIMATION /////
    public delegate void BallStop();
    public static event BallStop EOnBallStop;
    public delegate void BallInHole();
    public static event BallInHole EOnBallInHole;
    
    ///// GIVEN METHODS /////
    void Start()
    {
        _ballRigidBody2D = GetComponent<Rigidbody2D>();
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hole"))
        {
            OnHitHole();
        }

        if (other.CompareTag("Sand"))
        {
            OnHitSand();
        }

        if (other.CompareTag("Water"))
        {
            OnHitWater();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sand"))
        {
            OnExitSand();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.collider.CompareTag("Wall"))
        {
            OnHitWall();
        }
    }

    ///// ~ fancy ~  METHODS /////
    public void ResetBall()
    {
        EOnBallStop?.Invoke();
        transform.position = Vector3.zero;
        _ballRigidBody2D.linearVelocity = Vector2.zero;
        _ballRigidBody2D.angularVelocity = 0f;
        _ballRigidBody2D.angularDamping = 0f;
        _ballRigidBody2D.rotation = 0f;
        _ballRigidBody2D.linearDamping = 0f;
    }

    public void OnHitWall()
    {
        _audioManagerScript?.PlayBounceSound();
    }

    public void OnHitHole()
    {
        ResetBall();
        EOnBallInHole?.Invoke();
        _levelStatManager?.cueNextLevel();
        _audioManagerScript?.PlayHoleSound();
    }

    public void OnHitSand()
    {
        _ballRigidBody2D.linearDamping = sandDamping;
        _ballRigidBody2D.angularDamping = sandDamping;
        _audioManagerScript?.PlaySandSound();
    }

    public void OnExitSand()
    {
        _ballRigidBody2D.linearDamping = 0f;
        _ballRigidBody2D.angularDamping = 0f;
    }

    public void OnHitWater()
    {
        ResetBall();
        _audioManagerScript?.PlayWaterSound();
    }
}
