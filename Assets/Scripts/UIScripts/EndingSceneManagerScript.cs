using UnityEngine;

public class EndingSceneManagerScript : MonoBehaviour
{
    private LevelStatManager _levelStatManager;
    void Start()
    {
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
    }

    void Update()
    {
        
    }

    public void TryAgainButton()
    {
        _levelStatManager.cueTitleScene();
    }
}
