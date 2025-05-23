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
    private LevelStatManager _levelStatManager;

    ///// ANIMATION /////
    public delegate void PlayerIdle();
    public static event PlayerIdle EOnPlayerIdle;
    public delegate void PlayerWalk();
    public static event PlayerWalk EOnPlayerWalk;

    ///// GIVEN METHODS /////
    void Start()
    {
        _ballController = FindFirstObjectByType<BallController>();
        _playerTransform = GetComponent<Transform>();
        _ballTransform = GameObject.FindGameObjectWithTag("Ball").GetComponent<Transform>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
    }

    ///// COROUTINES /////
    private IEnumerator _movePlayerCoroutine(Vector2 current, Vector2 target)
        {
            EOnPlayerWalk?.Invoke();

            while (Vector2.Distance(current, target) > 0.5f)
            {
                current = _playerTransform.position;
                _playerTransform.position = Vector2.MoveTowards(current, target, walkSpeed * Time.deltaTime);
                yield return null;
            }

            _playerTransform.position = Vector2.MoveTowards(current, target, walkSpeed * Time.deltaTime);
            EOnPlayerIdle?.Invoke();

            if(_levelStatManager.strokes >= _levelStatManager.strokesToBeat && _levelStatManager.level != 0)
            {
                yield return new WaitForSeconds(3f);
                _levelStatManager.TooManyStrokes();
            }

            // once the players stops moving, they can hit the ball again
            _ballController?.TrueCanHitBall();
        }

    ///// ~ fancy ~  METHODS /////
    public void MovePlayer()
    {
        Vector2 direction = _ballTransform.position - _playerTransform.position;
        direction.Normalize();

        if(direction.x < 0)
            {
                _playerSpriteRenderer.flipX = true;
            }
            else if(direction.x > 0)
            {
                _playerSpriteRenderer.flipX = false;
            }

        Vector2 target = _ballTransform.position;
        Vector2 current = _playerTransform.position;
        
        if(Vector2.Distance(current, target) < 1f)
        {
            _ballController?.TrueCanHitBall();
            return;
        }

        StartCoroutine(_movePlayerCoroutine(current, target));
    }
}
