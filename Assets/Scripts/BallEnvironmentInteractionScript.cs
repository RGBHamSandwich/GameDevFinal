using UnityEngine;

public class BallEnvironmentInteractionScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public float force = 20f;
    public float walkSpeed = 2f;
    public LevelStatManager levelStatManager;
    public Rigidbody2D rb2d;
    public float linearDamping = 0f;

    ///// ANIMATION /////
    public delegate void BallStop();
    public static event BallStop EOnBallStop;
    public delegate void BallInHole();
    public static event BallInHole EOnBallInHole;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
            rb2d.linearDamping = 0f;
        }
    }

    ////// BALL IS NOT REFLECTING
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
        rb2d.linearVelocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
        rb2d.angularDamping = 0f;
        rb2d.rotation = 0f;
        rb2d.linearDamping = 0f;
    }

    public void OnHitWall(ContactPoint2D contact)
    {
        rb2d.linearVelocity = Vector2.zero;

        Vector2 wallAngle = contact.normal.normalized;
        Vector2 ballAngle = rb2d.linearVelocity.normalized * -1f;
        Vector2 reflected = Vector2.Reflect(ballAngle, wallAngle);

        // rb2d.linearVelocity = (reflected * ballAngle.magnitude);
        rb2d.AddForce(reflected * 10f, ForceMode2D.Impulse);
        Debug.Log("Reflected off wall with normal: " + wallAngle);
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
        levelStatManager.level++;
        // cue level complete popup or something, update level in a coroutine???

        // stop hit animation
    }

    // public Coroutine NewLevelCoroutine()
    // {
    // }

    public void OnHitSand()
    {
        rb2d.linearDamping = 5;
        rb2d.angularDamping = 5f;

        // cue sand popup or something

        // stop hit animation
    }

    public void OnHitWater()
    {
        ResetBall();

        // cue water popup or something

        // stop hit animation
    }
}
