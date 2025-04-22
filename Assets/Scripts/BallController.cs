using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public float force = 20f;  
    public float walkSpeed = 2f;  
    // could i use the "find" functionality to locate these in the scene instead of 
    // assigning it in unity??
    public LevelStatManager levelStatManager;
    
    ///// PRIVATE VARIABLES /////
    private Rigidbody2D rb2d;
    private SpriteRenderer _ballSpriteRenderer;
    private bool _canHitBall = true;
    // private BallAnimationController _ballAnimationController;
    private Transform _playerTransform;

    ///// ANIMATION /////
    public delegate void BallHit();
    public static event BallHit EOnBallHit;
    public delegate void BallStop();
    public static event BallStop EOnBallStop;
    public delegate void PlayerSwing();
    public static event PlayerSwing EOnPlayerSwing;
    public delegate void PlayerIdle();
    public static event PlayerIdle EOnPlayerIdle;

    ///// METHODS /////
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _ballSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
    void Update()
    {     
        if(_canHitBall)
        {
            StartCoroutine(HandleHitCoroutine());
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
        EOnPlayerIdle?.Invoke();

        // once the players stops moving, they can hit the ball again
        _canHitBall = true;

    }

    private IEnumerator HandleHitCoroutine()
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

        // on click, get mouse position
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Mouse down");
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Debug.Log("Mouse up");
            _canHitBall = false;
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)this.transform.position).normalized;
            
            if (direction.x < 0)
            {
                _ballSpriteRenderer.flipX = true;
            }
            else if (direction.x > 0)
            {
                _ballSpriteRenderer.flipX = false;
            }

            EOnPlayerSwing?.Invoke();
            yield return new WaitForSeconds(1f);

            HitByClub(direction, force);
            EOnBallHit?.Invoke();
            StartCoroutine(_canHitBallCoroutine()); 
        }

        yield return null;

    }

    public void HitByClub(Vector2 angle, float force)
    {
        rb2d.AddForce(angle * force * 10);   
        levelStatManager?.AddStroke();
    }

}
