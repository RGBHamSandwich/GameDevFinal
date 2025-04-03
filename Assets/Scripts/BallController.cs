using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public float force = 20f;  
    public float walkSpeed = 2f;  
    // could i use the "find" functionality to locate these in the scene instead of 
    // assigning it in unity??
    public ScoreCounter _scoreCounter;
    
    ///// PRIVATE VARIABLES /////
    private Rigidbody2D rb2d;
    private bool _canHitBall = true;
    // private BallAnimationController _ballAnimationController;
    private Transform _playerTransform;

    ///// ANIMATION /////
    public delegate void BallHit();
    public static event BallHit EOnBallHit;
    public delegate void BallStop();
    public static event BallStop EOnBallStop;

    ///// METHODS /////
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
    void Update()
    {     
        if(_canHitBall)
        {
            HandleHit();
            // MORE MAGNITUDE = LESS SPEED
            // _ballAnimationController.BallSpeed = 1 - rb2d.linearVelocity.magnitude;
        }

        if(rb2d.linearVelocity.magnitude >= 0.1f)
        {
            rb2d.linearVelocity *= 0.99f;
        }
    }

    private IEnumerator _canHitBallCoroutine()
    {
        yield return new WaitForSeconds(1f);
        while (rb2d.linearVelocity.magnitude > 0.1f)
        {
            // wait until the ball stops to reset _canHitBall
            yield return null;
        }

        rb2d.linearVelocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
        
        EOnBallStop?.Invoke();
        _canHitBall = true;

        StartCoroutine(_movePlayerCoroutine());
    }

    private IEnumerator _movePlayerCoroutine()
    {
        Vector2 target = this.transform.position;
        Vector2 current = _playerTransform.position;

        while (Vector2.Distance(current, target) > 0.5f)
        {
            current = _playerTransform.position;
            _playerTransform.position = Vector2.MoveTowards(current, target, walkSpeed * Time.deltaTime);
            yield return null;
        }
        _playerTransform.position = Vector2.MoveTowards(current, target, walkSpeed * Time.deltaTime);
    }

    public void HandleHit()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Debug.Log("W pressed");
            HitByClub(new Vector2(0, 1), force);
            EOnBallHit?.Invoke();

            _canHitBall = false;
            StartCoroutine(_canHitBallCoroutine());   
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Debug.Log("A pressed");
            HitByClub(new Vector2(-1, 0), force);
            EOnBallHit?.Invoke();

            _canHitBall = false;
            StartCoroutine(_canHitBallCoroutine()); 
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Debug.Log("S pressed");
            HitByClub(new Vector2(0, -1), force);
            EOnBallHit?.Invoke();

            _canHitBall = false;
            StartCoroutine(_canHitBallCoroutine()); 
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Debug.Log("D pressed");
            HitByClub(new Vector2(1, 0), force);
            EOnBallHit?.Invoke();

            _canHitBall = false;
            StartCoroutine(_canHitBallCoroutine()); 
        }
    }

    public void HitByClub(Vector2 angle, float force)
    {
        rb2d.AddForce(angle * force * 10);   
        _scoreCounter?.AddStroke();
    }

}
