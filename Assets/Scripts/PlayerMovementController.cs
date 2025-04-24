using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public float walkSpeed = 2f;  
    public BallController ballController;

    ///// PRIVATE VARIABLES /////
    private Transform _playerTransform;
    private Transform _ballTransform;
    private SpriteRenderer _playerSpriteRenderer;

    ///// ANIMATION /////
    public delegate void PlayerIdle();
    public static event PlayerIdle EOnPlayerIdle;

    void Start()
    {
        _playerTransform = GetComponent<Transform>();
        _ballTransform = GameObject.FindGameObjectWithTag("Ball").GetComponent<Transform>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayer()
    {
        // get direction from player to ball
        Vector2 direction = _ballTransform.position - _playerTransform.position;
        direction.Normalize();

        if(direction.x < 0)
            {
                Debug.Log("Mouse left; x = " + direction.x);
                _playerSpriteRenderer.flipX = true;
            }
            else if(direction.x > 0)
            {
                Debug.Log("Mouse right; x = " + direction.x);
                _playerSpriteRenderer.flipX = false;
            }

        // start the coroutine to move the player
        StartCoroutine(_movePlayerCoroutine());
    }

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
        ballController.ChangeCanHitBall();
    }
}
