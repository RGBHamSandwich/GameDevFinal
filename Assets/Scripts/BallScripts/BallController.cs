using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Tooltip("The force applied to the ball when hit.")]
    public float force = 30f;  
    [Tooltip("The height of the menu bar in pixels.")]
    
    ///// PRIVATE VARIABLES /////
    private Rigidbody2D rb2d;
    private LevelStatManager _levelStatManager;
    private PlayerMovementController _playerMovementController;
    private AudioManagerScript _audioManagerScript;
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
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
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

            if(!IsMouseInGame(screenMousePos))
            {
                // Debug.Log("Mouse position out of bounds; not hitting ball");
                yield break;
            }

            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Debug.Log("World click position: " + worldMousePos);
            Vector2 direction = (worldMousePos - (Vector2)this.transform.position).normalized;
            _canHitBall = false;
            EOnPlayerSwing?.Invoke();
            yield return new WaitForSeconds(1f);    // delay for swing animation

            HitByClub(direction, CalculateForce(holdDownMouseTime));
        }

        if(Input.GetMouseButton(0))                 // true while holding down
        {
            // Debug.Log("Mouse held down");
            holdDownMouseTime = Time.time - holdDownMouseTime;
            ShowForce(CalculateForce(holdDownMouseTime));   // not yet implemented; remove if not needed
        }

    }

    ///// ~ fancy ~  METHODS /////
    public bool IsMouseInGame(Vector2 screenMousePos)
    {
        float x = screenMousePos.x;
        float y = screenMousePos.y;
        float screenHeight = Screen.height;
        // Debug.Log("Screen height: " + screenHeight);

        if (x < 0 || x > Screen.width || y < 0 || y > screenHeight)
        {
            // Debug.Log("Mouse position out of bounds");
            return false;
        }

        // Since (0,0) is bottom-left, and menu bar is at top:
        float maxInGameHeight = screenHeight - _levelStatManager._levelStatUIHeight;
        // Debug.Log("Max click height: " + maxInGameHeight);
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
        float result = (Mathf.Sin(time) + 1) * force;
        return result;
    }

    public void ShowForce(float time)   // not yet implemented
    {
    }

    public void HitByClub(Vector2 angle, float thisForce)
    {
        EOnBallHit?.Invoke();
        StartCoroutine(_canHitBallCoroutine()); 

        rb2d.AddForce(angle * thisForce * 10f);   
        _levelStatManager?.AddStroke();
        _audioManagerScript.PlayHitSound();
    }
}
