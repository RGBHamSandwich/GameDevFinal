using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Tooltip("How fast the player moves towards the ball.")]
    public float walkSpeed = 2f;  

    ///// PRIVATE VARIABLES /////
    private BallController _ballController;
    private Transform _playerTransform;
    private Transform _ballTransform;
    private SpriteRenderer _playerSpriteRenderer;

    ///// ANIMATION /////
    public delegate void PlayerIdle();
    public static event PlayerIdle EOnPlayerIdle;

    ///// GIVEN METHODS /////
    void Start()
    {
        _ballController = FindFirstObjectByType<BallController>();
        _playerTransform = GetComponent<Transform>();
        _ballTransform = GameObject.FindGameObjectWithTag("Ball").GetComponent<Transform>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
    }

    ///// COROUTINES /////
    private IEnumerator _movePlayerCoroutine()
        {
            Vector2 target = _ballTransform.position;
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
            _ballController?.TrueCanHitBall();
        }

    ///// ~ fancy ~  METHODS /////
    public void MovePlayer()
    {
        // get direction from player to ball
        Vector2 direction = _ballTransform.position - _playerTransform.position;
        direction.Normalize();

        if(direction.x < 0)
            {
                // Debug.Log("Mouse left; x = " + direction.x);
                _playerSpriteRenderer.flipX = true;
            }
            else if(direction.x > 0)
            {
                // Debug.Log("Mouse right; x = " + direction.x);
                _playerSpriteRenderer.flipX = false;
            }

        // start the coroutine to move the player
        StartCoroutine(_movePlayerCoroutine());
    }
}
