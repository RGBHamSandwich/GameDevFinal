using UnityEngine;

public class BallEnvironmentInteractionScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public float force = 20f;
    public float walkSpeed = 2f;
    public float linearDamping = 0f;

    ///// PRIVATE VARIABLES /////
    private Rigidbody2D _ballRigidBody2D;
    private LevelStatManager _levelStatManager;
    private AudioManagerScript _audioManagerScript;

    ///// ANIMATION /////
    public delegate void BallStop();
    public static event BallStop EOnBallStop;
    public delegate void BallInHole();
    public static event BallInHole EOnBallInHole;
    
    void Start()
    {
        _ballRigidBody2D = GetComponent<Rigidbody2D>();
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hole"))
        {
            Debug.Log("Ball in hole!");
            OnHitHole();
        }
        if (other.CompareTag("Sand"))
        {
            Debug.Log("Ball in sand!");
            OnHitSand();
        }
        if (other.CompareTag("Water"))
        {
            Debug.Log("Ball in water!");
            OnHitWater();
        }
    }

    ////// SAND IS NOT UN-SANDING
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sand"))
        {
            Debug.Log("Ball out of sand!");
            _ballRigidBody2D.linearDamping = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.collider.CompareTag("Wall"))
        {
            ContactPoint2D contact = collision.contacts[0];
            Debug.Log("Hit wall at: " + contact.point);
            OnHitWall(contact);
        }
    }

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

    public void OnHitWall(ContactPoint2D contact)                           ///// if i don't come back to this, remove "contact"
    {
        _audioManagerScript?.PlayBounceSound();

        // replacing wall logic with Unity's physicsmaterial
        // _ballRigidBody2D.linearVelocity = Vector2.zero;

        // Vector2 wallAngle = contact.normal.normalized;
        // Vector2 ballAngle = _ballRigidBody2D.linearVelocity.normalized * -1f;
        // Vector2 reflected = Vector2.Reflect(ballAngle, wallAngle);

        // // rb2d.linearVelocity = (reflected * ballAngle.magnitude);
        // _ballRigidBody2D.AddForce(reflected * 10f, ForceMode2D.Impulse);
        // Debug.Log("Reflected off wall with normal: " + wallAngle);
    }

    public void OnOutOfBounds()
    {
        // reset ball to last position
    
    }

    ///// put the hole interactions in their own script !!!!!!!
    public void OnHitHole()
    {
        ResetBall();
        EOnBallInHole?.Invoke();
        _levelStatManager?.AddLevel();
        _audioManagerScript?.PlayHoleSound();

        // cue level complete popup or something, update level in a coroutine???

    }

    // public Coroutine NewLevelCoroutine()
    // {
    // }

    public void OnHitSand()
    {
        _ballRigidBody2D.linearDamping = 5;
        _ballRigidBody2D.angularDamping = 5f;

        _audioManagerScript?.PlaySandSound();

        // cue sand popup or something

        // stop hit animation
    }

    public void OnHitWater()
    {
        ResetBall();
        _audioManagerScript?.PlayWaterSound();

        // cue water popup or something

        // stop hit animation
    }
}
