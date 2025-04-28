using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Tooltip("The force applied to the ball when hit.")]
    public float force = 30f;  
    
    ///// PRIVATE VARIABLES /////
    private Rigidbody2D rb2d;
    private LevelStatManager _levelStatManager;
    private PlayerMovementController _playerMovementController;
    private bool _canHitBall = true;
    private float holdDownMouseTime = 0f;

    ///// ANIMATION /////
    public delegate void BallHit();
    public static event BallHit EOnBallHit;
    public delegate void BallStop();
    public static event BallStop EOnBallStop;
    public delegate void PlayerSwing();
    public static event PlayerSwing EOnPlayerSwing;

    ///// GIVEN METHODS /////
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
        _playerMovementController = FindFirstObjectByType<PlayerMovementController>();
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

    ///// COROUTINES /////
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
        _playerMovementController?.MovePlayer();
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
            // Debug.Log("Mouse up");
            holdDownMouseTime = Time.time - holdDownMouseTime;

            Vector2 screenMousePos = Input.mousePosition;
            // Debug.Log("Screen click position: " + screenMousePos);
            if (screenMousePos.x < 0 || screenMousePos.x > Screen.width || screenMousePos.y < 0 || screenMousePos.y > Screen.height)
            {
                // Debug.Log("Mouse position out of bounds);
                yield break;
            }
            else if (screenMousePos.y > 505f)
            {
                // Debug.Log("Mouse on menu bar");
                yield break;
            }

            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Debug.Log("World click position: " + worldMousePos);
            Vector2 direction = (worldMousePos - (Vector2)this.transform.position).normalized;
            _canHitBall = false;
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

    ///// ~ fancy ~  METHODS /////
    public void TrueCanHitBall()
    {
        _canHitBall = true;
    }

    public void FalseCanHitBall()
    {
        _canHitBall = false;
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
        _levelStatManager?.AddStroke();
        AudioManagerScript.instance.PlayHitSound();
    }

}
