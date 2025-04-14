using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CustomCameraController : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public CinemachineCamera BallCamera;
    public CinemachineCamera LevelCamera;

    ///// METHODS /////
    void Start()
    {
        StartCoroutine(LevelToBallCameraCoroutine());    
    }

    void Update()
    {
        
    }

    private IEnumerator LevelToBallCameraCoroutine()
    {
        yield return new WaitForSeconds(3f);

        if (BallCamera == null || LevelCamera == null)
        {
            Debug.LogError("BallCamera or LevelCamera is not assigned in the inspector.");
            yield break;
        }
        BallCamera.Priority = 1;
        LevelCamera.Priority = 0;
    }
    private IEnumerator BallToLevelCameraCoroutine()
    {
        if (BallCamera == null || LevelCamera == null)
        {
            Debug.LogError("BallCamera or LevelCamera is not assigned in the inspector.");
            yield break;
        }

        yield return new WaitForSeconds(3f);
        BallCamera.Priority = 0;
        LevelCamera.Priority = 1;
    }
}
