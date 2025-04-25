using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public float force = 30f;  
    public LevelStatManager levelStatManager;
    public PlayerMovementController playerMovementController;
    
    ///// PRIVATE VARIABLES /////
    private Rigidbody2D rb2d;
    private SpriteRenderer _ballSpriteRenderer;
    private bool _canHitBall = true;
    private float holdDownMouseTime = 0f;

    ///// ANIMATION /////
    public delegate void BallHit();
    public static event BallHit EOnBallHit;
    public delegate void BallStop();
    public static event BallStop EOnBallStop;
    public delegate void PlayerSwing();
    public static event PlayerSwing EOnPlayerSwing;

    ///// METHODS /////
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _ballSpriteRenderer = GetComponent<SpriteRenderer>();        
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

    public void ChangeCanHitBall()
    {
        _canHitBall = true;
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
        playerMovementController.MovePlayer();
    }

    private IEnumerator HandleHitCoroutine()
    {
        yield return null;

        if(Input.GetMouseButtonDown(0))            // true on click down
        {
            // Debug.Log("Mouse down");
            holdDownMouseTime = Time.time;
        }

        if(Input.GetMouseButtonUp(0))              // true on click up
        {
            _canHitBall = false;

            // Debug.Log("Mouse up");
            holdDownMouseTime = Time.time - holdDownMouseTime;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)this.transform.position).normalized;

            EOnPlayerSwing?.Invoke();
            yield return new WaitForSeconds(1f);

            HitByClub(direction, CalculateForce(holdDownMouseTime));
            EOnBallHit?.Invoke();
            StartCoroutine(_canHitBallCoroutine()); 
        }

        if(Input.GetMouseButton(0))                 // true while holding down
        {
            // Debug.Log("Mouse held down");
            holdDownMouseTime = Time.time - holdDownMouseTime;
            ShowForce(CalculateForce(holdDownMouseTime));
        }

    }

    public float CalculateForce(float time)
    {
        float result = (Mathf.Sin(time) + 1) * force;
        return result;
    }

    public void ShowForce(float time)
    {
        // halp
    }

    public void HitByClub(Vector2 angle, float thisForce)
    {
        rb2d.AddForce(angle * thisForce * 10f);   
        levelStatManager?.AddStroke();
        AudioManagerScript.instance.PlayHitSound();
    }

}
