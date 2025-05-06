using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CustomCameraController : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public CinemachineCamera BallCamera;
    public CinemachineCamera LevelCamera;

    ///// GIVEN METHODS /////
    void Start()
    {
        StartCoroutine(LevelToBallCameraCoroutine());    
    }

    ///// COROUTINES /////
    private IEnumerator LevelToBallCameraCoroutine()
    {
        yield return new WaitForSeconds(3f);
        BallCamera.Priority = 1;
        LevelCamera.Priority = 0;
    }
}
