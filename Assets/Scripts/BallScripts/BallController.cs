using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Tooltip("The force applied to the ball when hit.")]
    public float force = 30f;  
    
    ///// PRIVATE VARIABLES /////
    private Rigidbody2D rb2d;
    private LevelStatManager _levelStatManager;
    private PlayerMovementController _playerMovementController;
    private AudioManagerScript _audioManagerScript;
    private PowerBarControllerScript _powerBarControllerScript;
    private bool _canHitBall = true;
    private float startHoldMouseTime = 0f;
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
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
        _powerBarControllerScript = FindFirstObjectByType<PowerBarControllerScript>();
    }

    void Update()
    {     
        if(_canHitBall)
        {
            HandleHit();
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
        while (rb2d.linearVelocity.magnitude > 0.2f)
        {
            // wait until the ball stops to reset _canHitBall
            yield return null;
        }

        rb2d.linearVelocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
        
        EOnBallStop?.Invoke();
        _playerMovementController?.MovePlayer();
    }

    private IEnumerator DelayHitCoroutine(Vector2 direction, float holdDownMouseTime)
    {
        yield return new WaitForSeconds(1f);
        HitByClub(direction, CalculateForce(holdDownMouseTime));
    }

    ///// ~ fancy ~  METHODS /////
    public void HandleHit()
    {
        if(Input.GetMouseButtonDown(0))            // true on click down
        {
            startHoldMouseTime = Time.time;
        }

        if(Input.GetMouseButtonUp(0))              // true on click up
        {
            Vector2 screenMousePos = Input.mousePosition;

            if(!IsMouseInGame(screenMousePos))
            {
                return;
            }

            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (worldMousePos - (Vector2)this.transform.position).normalized;
            
            _canHitBall = false;
            EOnPlayerSwing?.Invoke();
            StartCoroutine(DelayHitCoroutine(direction, holdDownMouseTime));
        }

        if(Input.GetMouseButton(0))                 // true while holding down
        {
            holdDownMouseTime = Time.time - startHoldMouseTime;
            _powerBarControllerScript?.ShowForce(CalculateForce(holdDownMouseTime));
        }

    }

    public bool IsMouseInGame(Vector2 screenMousePos)
    {
        float x = screenMousePos.x;
        float y = screenMousePos.y;
        float screenHeight = Screen.height;

        if (x < 0 || x > Screen.width || y < 0 || y > screenHeight)
        {
            return false;
        }

        // Since (0,0) is bottom-left, and menu bar is at top:
        float maxInGameHeight = screenHeight - _levelStatManager._levelStatUIHeight;
        return y <= maxInGameHeight;
    }

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
        float theta = (time - 1.6f);                        // theta shifted horizontally
        float thisForce = (Mathf.Sin(theta) + 1f) / 2f;      // sin equation on [0, 1]
        return thisForce;
    }

    public void HitByClub(Vector2 angle, float thisForce)
    {
        EOnBallHit?.Invoke();
        StartCoroutine(_canHitBallCoroutine()); 

        rb2d.AddForce(angle * thisForce * force * force);   
        _levelStatManager?.AddStroke();
        _audioManagerScript.PlayHitSound();
    }
}
