using UnityEngine;

public class EndingSceneManagerScript : MonoBehaviour
{
    ///// PRIVATE VARIABLES /////
    private LevelStatManager _levelStatManager;

    ///// GIVEN METHODS /////
    void Start()
    {
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
    }

    ///// BUTTONS /////
    public void TryAgainButton()
    {
        _levelStatManager.cueTitleScene();
    }
}
